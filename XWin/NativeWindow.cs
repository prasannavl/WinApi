using System;
using System.Runtime.InteropServices;
using WinApi.User32;

namespace WinApi.XWin
{
    public interface IWindowConnector
    {
        void Connect(IntPtr handle, bool takeOwnership);
        void AttachWindowProc(IntPtr baseWindowProcPtr);
        void AttachWindowProc();
        void SetFactory(WindowFactory factory);
    }

    public class NativeWindowBase : IDisposable, IWindowConnector
    {
        private IntPtr m_baseWindowProcPtr;
        private WindowProc m_instanceWindowProc;
        public bool IsSourceOwner { get; set; }
        public IntPtr Handle { get; protected set; }
        public bool IsDisposed { get; protected set; }
        public WindowFactory Factory { get; protected set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        void IWindowConnector.Connect(IntPtr handle, bool takeOwnership)
        {
            Handle = handle;
            IsSourceOwner = takeOwnership;
        }

        void IWindowConnector.AttachWindowProc(IntPtr baseWindowProcPtr)
        {
            m_baseWindowProcPtr = baseWindowProcPtr == IntPtr.Zero
                ? Get(WindowLongFlags.GWLP_WNDPROC)
                : baseWindowProcPtr;
            m_instanceWindowProc = WindowProc;
            Set(WindowLongFlags.GWLP_WNDPROC, Marshal.GetFunctionPointerForDelegate(m_instanceWindowProc));
            OnSourceAttached();
        }

        void IWindowConnector.AttachWindowProc()
        {
            ((IWindowConnector) this).AttachWindowProc(IntPtr.Zero);
        }

        void IWindowConnector.SetFactory(WindowFactory factory)
        {
            Factory = factory;
        }

        ~NativeWindowBase()
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
            IsDisposed = true;
        }

        protected void CheckDisposed()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().Name);
        }

        public void SetText(string text)
        {
            CheckDisposed();
            User32Methods.SetWindowText(Handle, text);
        }

        public void SetSize(int width, int height)
        {
            CheckDisposed();
            User32Methods.SetWindowPos(Handle, IntPtr.Zero, -1, -1, width, height,
                SetWindowPosFlags.SWP_NOACTIVATE | SetWindowPosFlags.SWP_NOMOVE | SetWindowPosFlags.SWP_NOZORDER);
        }

        public void SetPosition(int x, int y)
        {
            CheckDisposed();
            User32Methods.SetWindowPos(Handle, IntPtr.Zero, x, y, -1, -1,
                SetWindowPosFlags.SWP_NOACTIVATE | SetWindowPosFlags.SWP_NOSIZE | SetWindowPosFlags.SWP_NOZORDER);
        }

        public void SetPosition(int x, int y, int width, int height)
        {
            CheckDisposed();
            User32Methods.SetWindowPos(Handle, IntPtr.Zero, x, y, width, height,
                SetWindowPosFlags.SWP_NOACTIVATE | SetWindowPosFlags.SWP_NOZORDER);
        }

        public void SetPosition(int x, int y, int width, int height, SetWindowPosFlags flags)
        {
            CheckDisposed();
            User32Methods.SetWindowPos(Handle, IntPtr.Zero, x, y, width, height, flags);
        }

        public void SetPosition(HwndZOrder order, int x, int y, int width, int height, SetWindowPosFlags flags)
        {
            CheckDisposed();
            User32Helpers.SetWindowPos(Handle, order, x, y, width, height, flags);
        }

        public void SetPosition(IntPtr hWndInsertAfter, int x, int y, int width, int height, SetWindowPosFlags flags)
        {
            CheckDisposed();
            User32Methods.SetWindowPos(Handle, hWndInsertAfter, x, y, width, height, flags);
        }

        public void GetPosition(out Rectangle rectangle)
        {
            CheckDisposed();
            User32Methods.GetWindowRect(Handle, out rectangle);
        }

        public void GetClientRectangle(out Rectangle rectangle)
        {
            CheckDisposed();
            User32Methods.GetClientRect(Handle, out rectangle);
        }

        public IntPtr Set(WindowLongFlags index, IntPtr value)
        {
            CheckDisposed();
            return User32Methods.SetWindowLongPtr(Handle, (int) index, value);
        }

        public IntPtr Get(WindowLongFlags index)
        {
            CheckDisposed();
            return User32Methods.GetWindowLongPtr(Handle, (int) index);
        }

        public void Show()
        {
            CheckDisposed();
            User32Methods.ShowWindow(Handle, ShowWindowCommands.SW_SHOW);
        }

        public void Hide()
        {
            CheckDisposed();
            User32Methods.ShowWindow(Handle, ShowWindowCommands.SW_HIDE);
        }

        public void SetState(ShowWindowCommands flags)
        {
            CheckDisposed();
            User32Methods.ShowWindow(Handle, flags);
        }

        protected virtual void OnSourceAttached() {}

        internal IntPtr WindowInstanceInitializerProc(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            if (msg == (int) WM.NCCREATE)
            {
                var windowConnector = (IWindowConnector) this;
                windowConnector.Connect(hwnd, true);
                windowConnector.AttachWindowProc(wParam);
            }
            return WindowProc(hwnd, msg, wParam, lParam);
        }

        protected internal virtual IntPtr WindowProc(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            return User32Methods.CallWindowProc(m_baseWindowProcPtr, hwnd, msg, wParam, lParam);
        }
    }

    public sealed class NativeWindow : NativeWindowBase {}
}