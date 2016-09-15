using System;
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

        internal IntPtr WindowInstanceInitializerProc(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            if (msg == (int) WM.NCCREATE)
            {
                var windowConnector = (IWindowCoreConnector) this;
                windowConnector.Attach(hwnd, true);
                windowConnector.AttachWindowProc(wParam);
            }
            return WindowProc(hwnd, msg, wParam, lParam);
        }

        protected internal virtual IntPtr WindowProc(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            return User32Methods.CallWindowProc(m_baseWindowProcPtr, hwnd, msg, wParam, lParam);
        }
    }

    public sealed class WindowCore : WindowCoreBase {}
}