using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using WinApi.User32;

namespace WinApi.XWin
{
    public interface IWindowCoreConnector : INativeWindowConnector
    {
        void Attach(IntPtr handle, bool takeOwnership);
        void AttachWindowProc();
        void AttachWindowProc(IntPtr baseWindowProcPtr);
        void DetachWindowProc();
        void SetFactory(WindowFactory factory);
    }

    public struct WindowMessage
    {
        public WM Id;
        public IntPtr WParam;
        public IntPtr LParam;
        public IntPtr Result;
        public bool Handled;

        public void SetHandled(bool handled = true)
        {
            Handled = handled;
        }
    }

    public struct WindowException
    {
        public bool IsHandled { get; set; }
        public WindowCoreBase Window { get; set; }
        public Exception DispatchedException { get; set; }

        public WindowException(Exception ex) : this(ex, null) {}

        public WindowException(Exception ex, WindowCoreBase window)
        {
            DispatchedException = ex;
            Window = window;
            IsHandled = false;
        }

        public void SetHandled(bool value = true)
        {
            IsHandled = value;
        }

        public override string ToString()
        {
            return DispatchedException.ToString();
        }
    }

    public delegate void WindowExceptionHandler(ref WindowException windowException);

    public class WindowCoreBase : NativeWindowBase, IWindowCoreConnector, IDisposable
    {
        private IntPtr m_baseWindowProcPtr;
        private WindowProc m_instanceWindowProc;
        public WindowFactory Factory { get; protected set; }
        public bool IsSourceOwner { get; protected set; }

        public bool IsDisposed { get; protected set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        void INativeWindowConnector.Attach(IntPtr handle)
        {
            ThrowIfDisposed();
            Handle = handle;
            IsSourceOwner = false;
        }

        void IWindowCoreConnector.Attach(IntPtr handle, bool takeOwnership)
        {
            ThrowIfDisposed();
            Handle = handle;
            IsSourceOwner = takeOwnership;
        }

        IntPtr INativeWindowConnector.Detach()
        {
            ((IWindowCoreConnector) this).DetachWindowProc();
            var h = Handle;
            Handle = IntPtr.Zero;
            IsSourceOwner = false;
            return h;
        }

        void IWindowCoreConnector.AttachWindowProc(IntPtr baseWindowProcPtr)
        {
            m_baseWindowProcPtr = baseWindowProcPtr == IntPtr.Zero
                ? GetItem(WindowLongFlags.GWLP_WNDPROC)
                : baseWindowProcPtr;
            m_instanceWindowProc = WindowProc;
            SetItem(WindowLongFlags.GWLP_WNDPROC, Marshal.GetFunctionPointerForDelegate(m_instanceWindowProc));
            OnSourceConnected();
        }

        void IWindowCoreConnector.AttachWindowProc()
        {
            ((IWindowCoreConnector) this).AttachWindowProc(IntPtr.Zero);
        }

        void IWindowCoreConnector.DetachWindowProc()
        {
            if (m_baseWindowProcPtr != IntPtr.Zero)
            {
                SetItem(WindowLongFlags.GWLP_WNDPROC, m_baseWindowProcPtr);
                m_baseWindowProcPtr = IntPtr.Zero;
            }
        }

        void IWindowCoreConnector.SetFactory(WindowFactory factory)
        {
            Factory = factory;
        }

        protected void ThrowIfDisposed()
        {
            if (IsDisposed) throw new ObjectDisposedException(nameof(WindowCoreBase));
        }

        ~WindowCoreBase()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed) return;
            if (IsSourceOwner)
            {
                User32Methods.DestroyWindow(Handle);
            }
            else
            {
                ((IWindowCoreConnector) this).Detach();
            }
            IsDisposed = true;
        }

        protected virtual void OnSourceConnected() {}

        // The WindowInstanceInitializerProc is only executed once, where it initializes the instance
        // with the appropriate values, and WndProc chain, after which the WindowProc method is 
        // directly called by Windows.
        internal IntPtr WindowInstanceInitializerProc(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            Debug.WriteLine("[WindowInstanceInitializerProc]: " + hwnd);
            var windowConnector = (IWindowCoreConnector) this;
            windowConnector.Attach(hwnd, true);
            windowConnector.AttachWindowProc(wParam);
            return WindowProc(hwnd, msg, IntPtr.Zero, lParam);
        }

        protected virtual void OnMessageProcessDefault(ref WindowMessage msg) {}

        protected virtual void OnMessage(ref WindowMessage msg)
        {
            if (!msg.Handled)
            {
                OnMessageProcessDefault(ref msg);
            }
        }

        protected virtual IntPtr WindowProc(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            var wmsg = new WindowMessage
            {
                Id = (WM) msg,
                WParam = wParam,
                LParam = lParam,
                Result = IntPtr.Zero,
                Handled = false
            };
            try
            {
                OnMessage(ref wmsg);
                return wmsg.Handled ? wmsg.Result : WindowClassProc(hwnd, msg, wParam, lParam);
            }
            catch (Exception ex)
            {
                if (!HandleException(ex, this)) throw;
                return IntPtr.Zero;
            }
        }

        public event WindowExceptionHandler Exception;
        public static event WindowExceptionHandler UnhandledException;

        public static bool HandleException(Exception ex, WindowCoreBase window)
        {
            var windowException = new WindowException(ex, window);
            window?.Exception?.Invoke(ref windowException);
            if (!windowException.IsHandled) UnhandledException?.Invoke(ref windowException);
            return windowException.IsHandled;
        }

        protected virtual IntPtr WindowClassProc(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            return User32Methods.CallWindowProc(m_baseWindowProcPtr, hwnd, msg, wParam, lParam);
        }
    }

    public sealed class WindowCore : WindowCoreBase {}
}