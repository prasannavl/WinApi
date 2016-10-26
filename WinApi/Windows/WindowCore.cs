using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using WinApi.User32;

namespace WinApi.Windows
{
    /// <summary>
    ///     The minimum core for all windows. It derives from the NativeWindow,
    ///     and provides connectivity to the Window procedure. Again,
    ///     it does nothing more than controlling the message loop
    ///     connection, destruction, and error handling across native
    ///     boundary. It provides the `OnMessage` method that's processes
    ///     the message loop. Any classes that derive from this can
    ///     create life cycle events from handling the message loop.
    /// </summary>
    public class WindowCore : NativeWindow, INativeConnectable, IDisposable
    {
        private IntPtr m_baseWindowProcPtr;
        private WindowProc m_instanceWindowProc;
        public WindowFactory Factory { get; private set; }
        public bool IsSourceOwner { get; protected set; }
        public bool IsDisposed { get; protected set; }

        public void Dispose()
        {
            Dispose(true);
        }

        void INativeAttachable.Attach(IntPtr handle)
        {
            ((INativeConnectable) this).Attach(handle, false);
        }

        void INativeConnectable.Attach(IntPtr handle, bool takeOwnership)
        {
            ThrowIfDisposed();
            Handle = handle;
            IsSourceOwner = takeOwnership;
        }

        IntPtr INativeAttachable.Detach()
        {
            ((INativeConnectable) this).DisconnectWindowProc();
            var h = Handle;
            Handle = IntPtr.Zero;
            IsSourceOwner = false;
            return h;
        }

        void INativeConnectable.ConnectWindowProc(IntPtr baseWindowProcPtr)
        {
            m_baseWindowProcPtr = baseWindowProcPtr == IntPtr.Zero
                ? GetParam(WindowLongFlags.GWLP_WNDPROC)
                : baseWindowProcPtr;
            m_instanceWindowProc = WindowProc;
            SetParam(WindowLongFlags.GWLP_WNDPROC, Marshal.GetFunctionPointerForDelegate(m_instanceWindowProc));
            OnSourceConnected();
        }

        void INativeConnectable.ConnectWindowProc()
        {
            ((INativeConnectable) this).ConnectWindowProc(IntPtr.Zero);
        }

        void INativeConnectable.DisconnectWindowProc()
        {
            if (m_baseWindowProcPtr != IntPtr.Zero)
            {
                SetParam(WindowLongFlags.GWLP_WNDPROC, m_baseWindowProcPtr);
                m_baseWindowProcPtr = IntPtr.Zero;
            }
        }

        void INativeConnectable.SetFactory(WindowFactory factory)
        {
            Factory = factory;
        }

        public event Action Destroyed;
        public event Action Created;

        protected void ThrowIfDisposed()
        {
            if (IsDisposed) throw new ObjectDisposedException(nameof(WindowCore));
        }

        ~WindowCore()
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
                ((INativeConnectable) this).Detach();
            }
            IsDisposed = true;
            GC.SuppressFinalize(this);
        }

        protected virtual void OnSourceConnected() {}

        // The WindowInstanceInitializerProc is only executed once, where it initializes the instance
        // with the appropriate values, and WndProc chain, after which the WindowProc method is 
        // directly called by Windows.
        internal IntPtr WindowInstanceInitializerProc(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            Debug.WriteLine("[WindowInstanceInitializerProc]: " + hwnd);
            var windowConnector = (INativeConnectable) this;
            windowConnector.Attach(hwnd, true);
            windowConnector.ConnectWindowProc(wParam);
            return WindowProc(hwnd, msg, IntPtr.Zero, lParam);
        }

        protected virtual void OnMessage(ref WindowMessage msg)
        {
            OnMessageDefault(ref msg);
        }

        protected virtual void OnMessageDefault(ref WindowMessage msg)
        {
            msg.SetResult(User32Methods.CallWindowProc(m_baseWindowProcPtr, msg.Hwnd, (uint) msg.Id, msg.WParam,
                msg.LParam));
        }

        protected internal virtual IntPtr WindowProc(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            var wmsg = new WindowMessage
            {
                Id = (WM) msg,
                WParam = wParam,
                LParam = lParam,
                Result = IntPtr.Zero,
                Hwnd = hwnd
            };
            try
            {
                OnMessage(ref wmsg);
                return wmsg.Result;
            }
            catch (Exception ex)
            {
                if (!HandleException(ex, this)) throw;
                return IntPtr.Zero;
            }
            finally
            {
                if (wmsg.Id == WM.CREATE)
                {
                    Created?.Invoke();
                }
                else if (wmsg.Id == WM.NCDESTROY)
                {
                    Destroyed?.Invoke();
                    IsSourceOwner = false;
                }
            }
        }

        public event WindowExceptionHandler Exception;
        public static event WindowExceptionHandler UnhandledException;

        public static bool HandleException(Exception ex, WindowCore window)
        {
            var windowException = new WindowException(ex, window);
            window?.Exception?.Invoke(windowException);
            if (!windowException.IsHandled) UnhandledException?.Invoke(windowException);
            return windowException.IsHandled;
        }
    }

    public sealed class SealedWindowCore : WindowCore {}

    public interface INativeConnectable : INativeAttachable
    {
        void Attach(IntPtr handle, bool takeOwnership);
        void ConnectWindowProc();
        void ConnectWindowProc(IntPtr baseWindowProcPtr);
        void DisconnectWindowProc();
        void SetFactory(WindowFactory factory);
    }

    public struct WindowMessage
    {
        public IntPtr Hwnd;
        public WM Id;
        public IntPtr WParam;
        public IntPtr LParam;
        public IntPtr Result;

        public WindowMessage(IntPtr hwnd, uint id, IntPtr wParam, IntPtr lParam)
        {
            Hwnd = hwnd;
            Id = (WM) id;
            WParam = wParam;
            LParam = lParam;
            Result = IntPtr.Zero;
        }

        public void SetResult(IntPtr result)
        {
            Result = result;
        }

        public void SetResult(int result)
        {
            SetResult(new IntPtr(result));
        }
    }

    public delegate void WindowExceptionHandler(WindowException windowException);

    public class WindowException : Exception
    {
        public WindowException(Exception ex) : this(ex, null) {}

        public WindowException(Exception ex, WindowCore window) : this(ex, window, null) {}

        public WindowException(Exception ex, WindowCore window, string message) : base(message, ex)
        {
            Window = window;
            IsHandled = false;
        }

        public bool IsHandled { get; set; }
        public WindowCore Window { get; set; }

        public void SetHandled(bool value = true)
        {
            IsHandled = value;
        }
    }
}