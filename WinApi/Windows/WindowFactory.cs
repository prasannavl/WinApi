using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using WinApi.Kernel32;
using WinApi.User32;

namespace WinApi.Windows
{
    public class WindowFactory
    {
        public static WindowProc DefWindowProc = User32Methods.DefWindowProc;
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly WindowProc m_classInitializerProcRef;
        private readonly WindowProc m_windowProc;

        public delegate void ClassInfoMutator(ref WindowClassExBlittable classInfo);

        public WindowFactory(string name, WindowClassStyles styles, IntPtr hInstance, IntPtr hIcon, IntPtr hCursor,
            IntPtr hBgBrush, WindowProc wndProc)
        {
            var cache = Cache.Instance;
            var className = name ?? Guid.NewGuid().ToString();

            this.ClassName = className;
            this.InstanceHandle = hInstance;
            this.m_windowProc = wndProc ?? DefWindowProc;

            this.m_classInitializerProcRef = this.ClassInitializerProc;

            var classInfo = new WindowClassEx
            {
                Size = cache.WindowClassExSize,
                ClassName = className,
                CursorHandle = hCursor,
                IconHandle = hIcon,
                Styles = styles,
                BackgroundBrushHandle = hBgBrush,
                WindowProc = this.m_classInitializerProcRef,
                InstanceHandle = hInstance
            };
            this.RegisterClass(ref classInfo);
        }

        public WindowFactory(ref WindowClassEx classEx)
        {
            this.ClassName = classEx.ClassName;
            this.InstanceHandle = classEx.InstanceHandle;
            this.m_windowProc = classEx.WindowProc ?? DefWindowProc;

            this.m_classInitializerProcRef = this.ClassInitializerProc;
            // Leave the reference untouched. So, use a copy for the modified registration.
            var classExClone = classEx;
            classExClone.WindowProc = this.m_classInitializerProcRef;

            this.RegisterClass(ref classExClone);
        }

        public WindowFactory(string existingClassName, string targetClassName,
            IntPtr srcInstanceHandle, IntPtr targetInstanceHandle,
            ClassInfoMutator mutator)
        {
            WindowClassExBlittable classInfo;
            if (!User32Methods.GetClassInfoEx(srcInstanceHandle, existingClassName, out classInfo)) throw new Exception("Class is not registered - " + existingClassName);

            var className = targetClassName ?? Guid.NewGuid().ToString();
            var ciClassName = Marshal.SystemDefaultCharSize == 1
                ? Marshal.StringToHGlobalAnsi(className)
                : Marshal.StringToHGlobalUni(className);

            classInfo.Size = (uint) Marshal.SizeOf<WindowClassExBlittable>();
            classInfo.ClassName = ciClassName;
            classInfo.InstanceHandle = targetInstanceHandle;

            try
            {
                mutator?.Invoke(ref classInfo);

                this.ClassName = className;
                this.InstanceHandle = classInfo.InstanceHandle;
                this.m_windowProc = Marshal.GetDelegateForFunctionPointer<WindowProc>(classInfo.WindowProc);
                classInfo.WindowProc = Marshal.GetFunctionPointerForDelegate<WindowProc>(this.ClassInitializerProc);
                this.RegisterClass(ref classInfo);
            }
            finally {
                Marshal.FreeHGlobal(ciClassName);
            }
        }

        public string ClassName { get; }
        public IntPtr InstanceHandle { get; }

        private void RegisterClass(ref WindowClassEx classEx)
        {
            if (User32Methods.RegisterClassEx(ref classEx) == 0)
            {
                var errString = string.Empty;
                var err = Kernel32Methods.GetLastError();
                // It may not always be the correct code. Since we aren't using
                // Marshal's last error. If the CLR runtime calls into the  
                // system meanwhile that may be returned instead, at times.
                if (err != 0) errString = "Possible error code: " + err;
                throw new Exception("Failed to register class. " + errString);
            }
        }

        private void RegisterClass(ref WindowClassExBlittable classEx)
        {
            if (User32Methods.RegisterClassEx(ref classEx) == 0)
            {
                var errString = string.Empty;
                var err = Kernel32Methods.GetLastError();
                // It may not always be the correct code. Since we aren't using
                // Marshal's last error. If the CLR runtime calls into the  
                // system meanwhile that may be returned instead, at times.
                if (err != 0) errString = "Possible error code: " + err;
                throw new Exception("Failed to register class. " + errString);
            }
        }

        // This is the initializer WindowProc for the class. It is only called until the NCCREATE message
        // is arrived, at which point the WindowInstanceInitializerProc is called, which again is only
        // executed once. It performs nifty trick to replace the WindowProc of the instance, by chaining
        // WndProc and swapping out base classes to be able to do it without any extra overhead of the 
        // traditional Win32 WndProc methods.
        private unsafe IntPtr ClassInitializerProc(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            try
            {
                Debug.WriteLine("[ClassInitializerProc]: " + hwnd);
                WindowProc winInstanceInitializerProc = null;
                if (msg == (int) WM.NCCREATE)
                {
                    var wParamForNcCreate = Marshal.GetFunctionPointerForDelegate(this.m_windowProc);
                    var createStruct = *(CreateStruct*) lParam.ToPointer();
                    var instancePtr = createStruct.CreateParams;
                    if (instancePtr != IntPtr.Zero)
                    {
                        var winInstance = (WindowCore) GCHandle.FromIntPtr(instancePtr).Target;
                        if (winInstance != null)
                        {
                            winInstanceInitializerProc = winInstance.WindowInstanceInitializerProc;
                            wParam = wParamForNcCreate;
                        }
                    }
                }
                return winInstanceInitializerProc?.Invoke(hwnd, msg, wParam, lParam) ??
                       this.m_windowProc(hwnd, msg, wParam, lParam);
            }
            catch (Exception ex)
            {
                if (!WindowCore.HandleException(ex, null)) throw;
                return IntPtr.Zero;
            }
        }

        private static void ThrowWindowCreationFailed()
        {
            throw new Exception("Failed to create window");
        }

        private static void VerifyWindowInstanceNotInitialized<TWindow>(TWindow windowInstance)
            where TWindow : WindowCore
        {
            if (windowInstance.Factory != null) throw new Exception("Window instance is already initialized");
        }

        public static void DestroyAllWindows(WindowFactory factory)
        {
            var className = factory.ClassName;
            DestroyAllWindows(className);
        }

        public static void DestroyAllWindows(string className)
        {
            IntPtr windowPtr;
            do
            {
                windowPtr = User32Methods.FindWindow(className, null);
                User32Methods.DestroyWindow(windowPtr);
            } while (windowPtr != IntPtr.Zero);
        }

        public static void UnregisterClass(WindowFactory factory)
        {
            User32Methods.UnregisterClass(factory.ClassName, factory.InstanceHandle);
        }

        public static void DestroyAndUnregister(WindowFactory factory)
        {
            DestroyAllWindows(factory);
            UnregisterClass(factory);
        }

        public static WindowFactory Create(string className = null,
            WindowClassStyles styles = WindowClassStyles.CS_HREDRAW | WindowClassStyles.CS_VREDRAW,
            IntPtr? hInstance = null, IntPtr? hIcon = null,
            IntPtr? hCursor = null, IntPtr? hBgBrush = null, WindowProc wndProc = null)
        {
            var cache = Cache.Instance;
            return new WindowFactory(className, styles,
                hInstance ?? cache.ProcessHandle,
                hIcon ?? cache.AppIconHandle,
                hCursor ?? cache.ArrowCursorHandle,
                hBgBrush ?? (IntPtr) SystemColor.COLOR_WINDOW, wndProc);
        }


        public static WindowFactory CreateForExistingClass(string existingClassName, string targetClassName = null,
            IntPtr srcInstance = default(IntPtr), IntPtr? targetInstance = null,
            ClassInfoMutator mutator = null)
        {
            return new WindowFactory(existingClassName, targetClassName, srcInstance,
                targetInstance ?? Cache.Instance.ProcessHandle, mutator);
        }


        public static NativeWindow CreateWindowFromHandle(IntPtr hwnd)
        {
            var win = new NativeWindow();
            var connectableWindow = (INativeAttachable) win;
            connectableWindow.Attach(hwnd);
            return win;
        }

        public static TWindow CreateWindowFromHandle<TWindow>(Func<TWindow> instanceCreator, IntPtr hwnd,
            bool takeOwnership = false)
            where TWindow : WindowCore
        {
            var win = instanceCreator();
            VerifyWindowInstanceNotInitialized(win);
            var attachableWindow = (INativeConnectable) win;
            attachableWindow.Attach(hwnd, takeOwnership);
            attachableWindow.ConnectWindowProc(IntPtr.Zero);
            return win;
        }

        public static NativeWindow CreateExternalWindowCore(string className, string text,
            WindowStyles styles, WindowExStyles exStyles, int x, int y, int width, int height, IntPtr hParent,
            IntPtr hMenu, IntPtr hInstance,
            IntPtr extraParams, uint controlStyles)
        {
            var hwnd = User32Methods.CreateWindowEx(exStyles, className, text,
                styles | (WindowStyles) controlStyles, x, y, width, height, hParent, hMenu, hInstance, extraParams);
            if (hwnd == IntPtr.Zero) ThrowWindowCreationFailed();
            return CreateWindowFromHandle(hwnd);
        }

        public static NativeWindow CreateExternalWindow(string className,
            string text = null, WindowStyles? styles = null,
            WindowExStyles? exStyles = null, int? x = null, int? y = null,
            int? width = null, int? height = null, IntPtr? hParent = null, IntPtr? hMenu = null,
            IntPtr? hInstance = null, IntPtr? extraParams = null, uint? controlStyles = null,
            IConstructionParams constructionParams = null)
        {
            if (string.IsNullOrWhiteSpace(className)) throw new ArgumentException("className is not valid");
            if (constructionParams == null) constructionParams = new ConstructionParams();
            return CreateExternalWindowCore(
                className,
                text,
                styles ?? constructionParams.Styles,
                exStyles ?? constructionParams.ExStyles,
                x ?? constructionParams.X,
                y ?? constructionParams.Y,
                width ?? constructionParams.Width,
                height ?? constructionParams.Height,
                hParent ?? constructionParams.ParentHandle,
                hMenu ?? constructionParams.MenuHandle,
                hInstance ?? Cache.Instance.ProcessHandle,
                extraParams ?? IntPtr.Zero,
                controlStyles ?? constructionParams.ControlStyles);
        }

        public static TWindow CreateExternalWindowCore<TWindow>(Func<TWindow> instanceCreator, bool takeOwnership,
            string className, string text,
            WindowStyles styles,
            WindowExStyles exStyles,
            int x, int y, int width, int height, IntPtr hParent, IntPtr hMenu, IntPtr hInstance,
            IntPtr extraParams, uint controlStyles)
            where TWindow : WindowCore
        {
            var hwnd = User32Methods.CreateWindowEx(exStyles, className, text,
                styles | (WindowStyles) controlStyles, x, y, width, height, hParent, hMenu, hInstance, extraParams);
            if (hwnd == IntPtr.Zero) ThrowWindowCreationFailed();
            return CreateWindowFromHandle(instanceCreator, hwnd, takeOwnership);
        }

        public static TWindow CreateExternalWindow<TWindow>(Func<TWindow> instanceCreator, string className,
            bool takeOwnership = true,
            string text = null, WindowStyles? styles = null,
            WindowExStyles? exStyles = null, int? x = null, int? y = null,
            int? width = null, int? height = null, IntPtr? hParent = null, IntPtr? hMenu = null,
            IntPtr? hInstance = null, IntPtr? extraParams = null, uint? controlStyles = null,
            IConstructionParams constructionParams = null)
            where TWindow : WindowCore
        {
            if (string.IsNullOrWhiteSpace(className)) throw new ArgumentException("className is not valid");
            if (constructionParams == null) constructionParams = new ConstructionParams();
            return CreateExternalWindowCore(
                instanceCreator,
                takeOwnership,
                className,
                text,
                styles ?? constructionParams.Styles,
                exStyles ?? constructionParams.ExStyles,
                x ?? constructionParams.X,
                y ?? constructionParams.Y,
                width ?? constructionParams.Width,
                height ?? constructionParams.Height,
                hParent ?? constructionParams.ParentHandle,
                hMenu ?? constructionParams.MenuHandle,
                hInstance ?? Cache.Instance.ProcessHandle,
                extraParams ?? IntPtr.Zero,
                controlStyles ?? constructionParams.ControlStyles);
        }

        private TWindow CreateWindowCoreFromInstance<TWindow>(TWindow win, string text, WindowStyles styles,
            WindowExStyles exStyles, int x, int y,
            int width, int height, IntPtr hParent, IntPtr hMenu, uint controlStyles)
            where TWindow : WindowCore
        {
            VerifyWindowInstanceNotInitialized(win);
            ((INativeConnectable) win).SetFactory(this);

            var winGcHandle = GCHandle.Alloc(win);
            var extraParam = GCHandle.ToIntPtr(winGcHandle);

            var hwnd = IntPtr.Zero;
            try
            {
                hwnd = User32Methods.CreateWindowEx(exStyles, this.ClassName, text,
                    styles | (WindowStyles) controlStyles, x, y, width, height, hParent, hMenu, this.InstanceHandle,
                    extraParam);
            }
            finally
            {
                winGcHandle.Free();
                if (hwnd == IntPtr.Zero)
                {
                    win.Dispose();
                    ThrowWindowCreationFailed();
                }
            }
            return win;
        }

        public TWindow CreateWindowCore<TWindow>(Func<TWindow> instanceCreator, string text, WindowStyles styles,
            WindowExStyles exStyles, int x, int y,
            int width, int height, IntPtr hParent, IntPtr hMenu, uint controlStyles)
            where TWindow : WindowCore
        {
            return this.CreateWindowCoreFromInstance(instanceCreator(), text, styles, exStyles, x, y, width, height,
                hParent,
                hMenu, controlStyles);
        }

        public TWindow CreateWindow<TWindow>(Func<TWindow> instanceCreator,
            string text = null, WindowStyles? styles = null,
            WindowExStyles? exStyles = null, int? x = null, int? y = null,
            int? width = null, int? height = null, IntPtr? hParent = null, IntPtr? hMenu = null,
            uint? controlStyles = null, IConstructionParams constructionParams = null)
            where TWindow : WindowCore
        {
            var win = instanceCreator();
            return this.CreateWindowInnerCore(win, text, styles, exStyles, x, y, width, height, hParent, hMenu,
                controlStyles, constructionParams);
        }

        public TWindow CreateWindowEx<TWindow>(Func<TWindow> instanceCreator, string text = null,
            WindowStyles? styles = null,
            WindowExStyles? exStyles = null, int? x = null, int? y = null,
            int? width = null, int? height = null, IntPtr? hParent = null, IntPtr? hMenu = null,
            uint? controlStyles = null)
            where TWindow : WindowCore, IConstructionParamsProvider
        {
            var win = instanceCreator();
            var constructionParams = win.GetConstructionParams();
            return this.CreateWindowInnerCore(win, text, styles, exStyles, x, y, width, height, hParent, hMenu,
                controlStyles, constructionParams);
        }

        private TWindow CreateWindowInnerCore<TWindow>(TWindow instance,
            string text, WindowStyles? styles,
            WindowExStyles? exStyles, int? x, int? y,
            int? width, int? height, IntPtr? hParent, IntPtr? hMenu, uint? controlStyles,
            IConstructionParams constructionParams)
            where TWindow : WindowCore
        {
            if (constructionParams == null) constructionParams = new ConstructionParams();
            return this.CreateWindowCoreFromInstance(instance,
                text,
                styles ?? constructionParams.Styles,
                exStyles ?? constructionParams.ExStyles,
                x ?? constructionParams.X,
                y ?? constructionParams.Y,
                width ?? constructionParams.Width,
                height ?? constructionParams.Height,
                hParent ?? constructionParams.ParentHandle,
                hMenu ?? constructionParams.MenuHandle,
                controlStyles ?? constructionParams.ControlStyles);
        }

        public class Cache
        {
            [ThreadStatic] private static Cache t_instance;

            [ThreadStatic] private static WeakReference<Cache> t_weakRefInstance;

            private Cache()
            {
                this.ProcessHandle = Kernel32Methods.GetCurrentProcess();
                this.AppIconHandle = User32Helpers.LoadIcon(IntPtr.Zero, SystemIcon.IDI_APPLICATION);
                this.ArrowCursorHandle = User32Helpers.LoadCursor(IntPtr.Zero, SystemCursor.IDC_ARROW);
                this.WindowClassExSize = (uint) Marshal.SizeOf<WindowClassEx>();
            }

            public IntPtr ArrowCursorHandle { get; set; }
            public IntPtr AppIconHandle { get; set; }
            public uint WindowClassExSize { get; set; }
            public IntPtr ProcessHandle { get; set; }

            public static Cache Instance
            {
                get
                {
                    if (t_instance != null) return t_instance;
                    if ((t_weakRefInstance == null) || !t_weakRefInstance.TryGetTarget(out t_instance)) {
                        t_instance = new Cache();
                    }
                    return t_instance;
                }
            }

            public static void TransitionCacheToWeakReference()
            {
                if (t_instance == null) return;
                if (t_weakRefInstance == null) t_weakRefInstance = new WeakReference<Cache>(t_instance);
                else t_weakRefInstance.SetTarget(t_instance);
                t_instance = null;
            }
        }
    }
}