using System;
using System.Reflection;
using System.Runtime.InteropServices;
using WinApi.Gdi32;
using WinApi.Kernel32;
using WinApi.User32;

namespace WinApi.XWin
{
    public class WindowFactory
    {
        public static WindowProc DefWindowProc = User32Methods.DefWindowProc;
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly WindowProc m_classInitializerProcRef;
        private readonly WindowProc m_windowProc;

        public WindowFactory(string name, WindowClassStyles styles, IntPtr hInstance, IntPtr hIcon, IntPtr hCursor,
            IntPtr hBgBrush, WindowProc wndProc)
        {
            var cache = FactoryCache.Instance;
            var className = name ?? Guid.NewGuid().ToString();

            ClassName = className;
            ProcessHandle = hInstance;
            m_windowProc = wndProc ?? DefWindowProc;

            m_classInitializerProcRef = ClassInitializerProc;

            var classInfo = new WindowClassEx
            {
                Size = cache.WindowClassExSize,
                ClassName = className,
                CursorHandle = hCursor,
                IconHandle = hIcon,
                Styles = styles,
                BackgroundBrushHandle = hBgBrush,
                WindowProc = m_classInitializerProcRef,
                InstanceHandle = hInstance
            };

            if (User32Methods.RegisterClassEx(ref classInfo) == 0)
                throw new Exception("Failed to register window");
        }

        public WindowFactory(ref WindowClassEx classEx)
        {
            ClassName = classEx.ClassName;
            ProcessHandle = classEx.InstanceHandle;
            m_windowProc = classEx.WindowProc ?? DefWindowProc;

            m_classInitializerProcRef = ClassInitializerProc;
            // Leave the reference untouched. So, use a copy for the modified registration.
            var classExClone = classEx;
            classExClone.WindowProc = m_classInitializerProcRef;

            if (User32Methods.RegisterClassEx(ref classExClone) == 0)
                throw new Exception("Failed to register window");
        }

        public string ClassName { get; }

        public IntPtr ProcessHandle { get; }

        private unsafe IntPtr ClassInitializerProc(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            WindowProc winInstanceInitializerProc = null;
            if (msg == (int) WM.NCCREATE)
            {
                wParam = Marshal.GetFunctionPointerForDelegate(m_windowProc);
                var createStruct = *(CreateStruct*) lParam.ToPointer();
                var instancePtr = createStruct.lpCreateParams;
                if (instancePtr != IntPtr.Zero)
                {
                    var winInstance = (WindowBase) GCHandle.FromIntPtr(instancePtr).Target;
                    if (winInstance != null)
                        winInstanceInitializerProc = winInstance.WindowInstanceInitializerProc;
                }
            }
            return winInstanceInitializerProc?.Invoke(hwnd, msg, wParam, lParam) ??
                   User32Methods.DefWindowProc(hwnd, msg, wParam, lParam);
        }

        public static WindowFactory Create(string className, WindowProc wndProc)
        {
            var cache = FactoryCache.Instance;
            return new WindowFactory(className, WindowClassStyles.CS_HREDRAW | WindowClassStyles.CS_VREDRAW,
                cache.ProcessHandle, cache.AppIconHandle, cache.ArrowCursorHandle,
                IntPtr.Zero,
                wndProc);
        }

        public static WindowFactory Create(string className)
        {
            return Create(className, null);
        }

        public static WindowFactory Create(
            WindowClassStyles styles = WindowClassStyles.CS_HREDRAW | WindowClassStyles.CS_VREDRAW,
            string className = null, IntPtr hInstance = default(IntPtr), IntPtr hIcon = default(IntPtr),
            IntPtr hCursor = default(IntPtr), IntPtr hBgBrush = default(IntPtr), WindowProc wndProc = null)
        {
            var cache = FactoryCache.Instance;
            return new WindowFactory(className, styles,
                hInstance == default(IntPtr) ? cache.ProcessHandle : hInstance,
                hIcon == default(IntPtr) ? cache.AppIconHandle : hIcon,
                hCursor == default(IntPtr) ? cache.ArrowCursorHandle : hCursor,
                hBgBrush, wndProc);
        }

        public TWindow CreateWindow<TWindow>(string text, WindowStyles styles, WindowExStyles exStyles, int x, int y,
            int width, int height, IntPtr hParent, IntPtr hMenu)
            where TWindow : WindowCoreBase, new()
        {
            var win = new TWindow();
            ((IWindowCoreConnector) win).SetFactory(this);

            var winGcHandle = GCHandle.Alloc(win);
            var extraParam = GCHandle.ToIntPtr(winGcHandle);

            var hwnd = IntPtr.Zero;
            try
            {
                hwnd = User32Methods.CreateWindowEx(exStyles, ClassName, text,
                    styles, x, y, width, height, hParent, hMenu, ProcessHandle, extraParam);
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

        public TWindow CreateWindow<TWindow>(WindowStyles styles = 0,
            WindowExStyles exStyles = 0, string text = "Window",
            int x = (int) CreateWindowFlags.CW_USEDEFAULT, int y = (int) CreateWindowFlags.CW_USEDEFAULT,
            int width = (int) CreateWindowFlags.CW_USEDEFAULT, int height = (int) CreateWindowFlags.CW_USEDEFAULT,
            IntPtr hParent = default(IntPtr), IntPtr hMenu = default(IntPtr))
            where TWindow : WindowCoreBase, new()
        {
            return CreateWindow<TWindow>(text, styles, exStyles, x, y, width, height, hParent,
                hMenu);
        }

        public TWindow CreateFrameWindow<TWindow>(
            WindowStyles styles =
                WindowStyles.WS_OVERLAPPEDWINDOW | WindowStyles.WS_CLIPCHILDREN | WindowStyles.WS_CLIPSIBLINGS,
            WindowExStyles exStyles = WindowExStyles.WS_EX_APPWINDOW | WindowExStyles.WS_EX_WINDOWEDGE,
            string text = "Window",
            int x = (int) CreateWindowFlags.CW_USEDEFAULT, int y = (int) CreateWindowFlags.CW_USEDEFAULT,
            int width = (int) CreateWindowFlags.CW_USEDEFAULT, int height = (int) CreateWindowFlags.CW_USEDEFAULT,
            IntPtr hParent = default(IntPtr), IntPtr hMenu = default(IntPtr))
            where TWindow : WindowCoreBase, new()
        {
            return CreateWindow<TWindow>(text, styles, exStyles, x, y, width, height, hParent,
                hMenu);
        }

        public static TWindow CreateWindow<TWindow>(string className, string text, WindowStyles styles,
            WindowExStyles exStyles, int x, int y,
            int width, int height, IntPtr hParent, IntPtr hMenu, IntPtr hInstance, IntPtr lpParam)
            where TWindow : WindowCoreBase, new()
        {
            var hwnd = VerifyWindowCreation(User32Methods.CreateWindowEx(exStyles, className, text,
                styles, x, y, width, height, hParent, hMenu, hInstance, lpParam));
            return CreateWindowFromHandle<TWindow>(hwnd, true);
        }

        public static TWindow CreateWindow<TWindow>(string className,
            WindowStyles styles = 0,
            WindowExStyles exStyles = 0, string text = "Window",
            int x = (int) CreateWindowFlags.CW_USEDEFAULT, int y = (int) CreateWindowFlags.CW_USEDEFAULT,
            int width = (int) CreateWindowFlags.CW_USEDEFAULT, int height = (int) CreateWindowFlags.CW_USEDEFAULT,
            IntPtr hParent = default(IntPtr), IntPtr hMenu = default(IntPtr), IntPtr hInstance = default(IntPtr),
            IntPtr lpParam = default(IntPtr))
            where TWindow : WindowCoreBase, new()
        {
            return CreateWindow<TWindow>(className, text, styles, exStyles, x, y, width, height, hParent,
                hMenu, hInstance, lpParam);
        }

        public static TWindow CreateFrameWindow<TWindow>(string className,
            WindowStyles styles =
                WindowStyles.WS_OVERLAPPEDWINDOW | WindowStyles.WS_CLIPCHILDREN | WindowStyles.WS_CLIPSIBLINGS,
            WindowExStyles exStyles = WindowExStyles.WS_EX_APPWINDOW | WindowExStyles.WS_EX_WINDOWEDGE,
            string text = "Window",
            int x = (int) CreateWindowFlags.CW_USEDEFAULT, int y = (int) CreateWindowFlags.CW_USEDEFAULT,
            int width = (int) CreateWindowFlags.CW_USEDEFAULT, int height = (int) CreateWindowFlags.CW_USEDEFAULT,
            IntPtr hParent = default(IntPtr), IntPtr hMenu = default(IntPtr), IntPtr hInstance = default(IntPtr),
            IntPtr lpParam = default(IntPtr))
            where TWindow : WindowCoreBase, new()

        {
            return CreateWindow<TWindow>(className, text, styles, exStyles, x, y, width, height, hParent,
                hMenu, hInstance, lpParam);
        }

        public static NativeWindow CreateWindow(string className, string text, WindowStyles styles,
            WindowExStyles exStyles, int x, int y,
            int width, int height, IntPtr hParent, IntPtr hMenu, IntPtr hInstance, IntPtr lpParam)
        {
            var hwnd = VerifyWindowCreation(User32Methods.CreateWindowEx(exStyles, className, text,
                styles, x, y, width, height, hParent, hMenu, hInstance, lpParam));
            return CreateWindowFromHandle(hwnd);
        }

        public static NativeWindow CreateFrameWindow(string className,
            WindowStyles styles =
                WindowStyles.WS_OVERLAPPEDWINDOW | WindowStyles.WS_CLIPCHILDREN | WindowStyles.WS_CLIPSIBLINGS,
            WindowExStyles exStyles = WindowExStyles.WS_EX_APPWINDOW | WindowExStyles.WS_EX_WINDOWEDGE,
            string text = "Window",
            int x = (int) CreateWindowFlags.CW_USEDEFAULT, int y = (int) CreateWindowFlags.CW_USEDEFAULT,
            int width = (int) CreateWindowFlags.CW_USEDEFAULT, int height = (int) CreateWindowFlags.CW_USEDEFAULT,
            IntPtr hParent = default(IntPtr), IntPtr hMenu = default(IntPtr), IntPtr hInstance = default(IntPtr),
            IntPtr lpParam = default(IntPtr))
        {
            return CreateWindow(className, text, styles, exStyles, x, y, width, height, hParent,
                hMenu, hInstance, lpParam);
        }

        public static NativeWindow CreateWindow(string className,
            WindowStyles styles = 0,
            WindowExStyles exStyles = 0, string text = "Window",
            int x = (int) CreateWindowFlags.CW_USEDEFAULT, int y = (int) CreateWindowFlags.CW_USEDEFAULT,
            int width = (int) CreateWindowFlags.CW_USEDEFAULT, int height = (int) CreateWindowFlags.CW_USEDEFAULT,
            IntPtr hParent = default(IntPtr), IntPtr hMenu = default(IntPtr), IntPtr hInstance = default(IntPtr),
            IntPtr lpParam = default(IntPtr))
        {
            return CreateWindow(className, text, styles, exStyles, x, y, width, height, hParent,
                hMenu, hInstance, lpParam);
        }

        public static TWindow CreateWindowFromHandle<TWindow>(IntPtr hwnd, bool takeOwnership = false)
            where TWindow : WindowCoreBase, new()
        {
            var win = new TWindow();
            var windowConnector = (IWindowCoreConnector) win;
            windowConnector.Attach(hwnd, takeOwnership);
            windowConnector.AttachWindowProc(IntPtr.Zero);
            return win;
        }

        public static NativeWindow CreateWindowFromHandle(IntPtr hwnd)
        {
            var win = new NativeWindow();
            var windowConnector = (INativeWindowConnector) win;
            windowConnector.Attach(hwnd);
            return win;
        }

        private static IntPtr VerifyWindowCreation(IntPtr hwnd)
        {
            if (hwnd == IntPtr.Zero) ThrowWindowCreationFailed();
            return hwnd;
        }

        private static void ThrowWindowCreationFailed()
        {
            throw new Exception("Failed to create window");
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
            User32Methods.UnregisterClass(factory.ClassName, factory.ProcessHandle);
        }

        public static void DestroyAndUnregister(WindowFactory factory)
        {
            DestroyAllWindows(factory);
            UnregisterClass(factory);
        }

        public class FactoryCache
        {
            [ThreadStatic] private static FactoryCache t_instance;

            [ThreadStatic] private static WeakReference<FactoryCache> t_weakRefInstance;

            private FactoryCache()
            {
                ProcessHandle = Kernel32Methods.GetCurrentProcess();
                AppIconHandle = User32Helpers.LoadIcon(IntPtr.Zero, SystemIcon.IDI_APPLICATION);
                ArrowCursorHandle = User32Helpers.LoadCursor(IntPtr.Zero, SystemCursor.IDC_ARROW);
                WindowClassExSize = (uint) Marshal.SizeOf<WindowClassEx>();
            }

            public IntPtr ArrowCursorHandle { get; set; }
            public IntPtr AppIconHandle { get; set; }
            public uint WindowClassExSize { get; set; }
            public IntPtr ProcessHandle { get; set; }

            public static FactoryCache Instance
            {
                get
                {
                    if (t_instance == null)
                    {
                        if ((t_weakRefInstance == null) || !t_weakRefInstance.TryGetTarget(out t_instance))
                        {
                            t_instance = new FactoryCache();
                        }
                    }
                    return t_instance;
                }
            }

            public static void TransitionCacheToWeakReference()
            {
                if (t_instance != null)
                {
                    if (t_weakRefInstance == null)
                        t_weakRefInstance = new WeakReference<FactoryCache>(t_instance);
                    else
                        t_weakRefInstance.SetTarget(t_instance);
                    t_instance = null;
                }
            }
        }
    }
}