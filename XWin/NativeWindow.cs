using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using WinApi.User32;

namespace WinApi.XWin
{
    public interface IWindowInitializable
    {
        void Initialize(IntPtr windowHandle, IntPtr baseWindowProcPtr);
    }

    public class NativeWindow : IDisposable, IWindowInitializable
    {
        private IntPtr m_baseWindowProcPtr;
        private bool m_disposed;
        private WindowProc m_instanceWindowProc;
        public WindowFactory Factory { get; set; }

        public IntPtr Handle { get; private set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        void IWindowInitializable.Initialize(IntPtr windowHandle, IntPtr baseWindowProcPtr)
        {
            Handle = windowHandle;
            m_baseWindowProcPtr = baseWindowProcPtr == IntPtr.Zero
                ? Get(WindowLongFlags.GWLP_WNDPROC)
                : baseWindowProcPtr;
            m_instanceWindowProc = WindowProc;
            Set(WindowLongFlags.GWLP_WNDPROC, Marshal.GetFunctionPointerForDelegate(m_instanceWindowProc));
            OnSourceInitialized();
        }

        ~NativeWindow()
        {
            Dispose(false);
        }

        public void SetText(string text)
        {
            User32Methods.SetWindowText(Handle, text);
        }

        public void SetSize(int width, int height)
        {
            User32Methods.SetWindowPos(Handle, IntPtr.Zero, -1, -1, width, height,
                SetWindowPosFlags.SWP_NOACTIVATE | SetWindowPosFlags.SWP_NOMOVE | SetWindowPosFlags.SWP_NOZORDER);
        }

        public void SetPosition(int x, int y)
        {
            User32Methods.SetWindowPos(Handle, IntPtr.Zero, x, y, -1, -1,
                SetWindowPosFlags.SWP_NOACTIVATE | SetWindowPosFlags.SWP_NOSIZE | SetWindowPosFlags.SWP_NOZORDER);
        }

        public void SetPosition(int x, int y, int width, int height)
        {
            User32Methods.SetWindowPos(Handle, IntPtr.Zero, x, y, width, height,
                SetWindowPosFlags.SWP_NOACTIVATE | SetWindowPosFlags.SWP_NOZORDER);
        }

        public void SetPosition(int x, int y, int width, int height, SetWindowPosFlags flags)
        {
            User32Methods.SetWindowPos(Handle, IntPtr.Zero, x, y, width, height, flags);
        }

        public void SetPosition(HwndZOrder order, int x, int y, int width, int height, SetWindowPosFlags flags)
        {
            User32Helpers.SetWindowPos(Handle, order, x, y, width, height, flags);
        }

        public void SetPosition(IntPtr hWndInsertAfter, int x, int y, int width, int height, SetWindowPosFlags flags)
        {
            User32Methods.SetWindowPos(Handle, hWndInsertAfter, x, y, width, height, flags);
        }

        public void GetPosition(out Rectangle rectangle)
        {
            User32Methods.GetWindowRect(Handle, out rectangle);
        }

        public void GetClientRectangle(out Rectangle rectangle)
        {
            User32Methods.GetClientRect(Handle, out rectangle);
        }

        public IntPtr Set(WindowLongFlags index, IntPtr value)
        {
            return User32Methods.SetWindowLongPtr(Handle, (int) index, value);
        }

        public IntPtr Get(WindowLongFlags index)
        {
            return User32Methods.GetWindowLongPtr(Handle, (int) index);
        }

        public void Show()
        {
            User32Methods.ShowWindow(Handle, ShowWindowCommands.SW_SHOW);
        }

        public void Hide()
        {
            User32Methods.ShowWindow(Handle, ShowWindowCommands.SW_HIDE);
        }

        public void SetState(ShowWindowCommands flags)
        {
            User32Methods.ShowWindow(Handle, flags);
        }

        protected virtual void OnSourceInitialized() {}

        protected virtual void Dispose(bool disposing)
        {
            if (m_disposed) return;
            User32Methods.DestroyWindow(Handle);
            m_disposed = true;
        }

        internal IntPtr WindowInstanceInitializerProc(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            if (msg == (int) WM.NCCREATE)
            {
                ((IWindowInitializable) this).Initialize(hwnd, wParam);
            }
            return WindowProc(hwnd, msg, wParam, lParam);
        }

        protected internal virtual IntPtr WindowProc(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            return User32Methods.CallWindowProc(m_baseWindowProcPtr, hwnd, msg, wParam, lParam);
        }
    }
}