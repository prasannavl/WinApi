using System;
using System.Text;
using WinApi.User32;

namespace WinApi.XWin
{
    public interface INativeWindowConnector
    {
        void Attach(IntPtr handle);
        IntPtr Detach();
    }

    public class NativeWindowBase : INativeWindowConnector
    {
        public IntPtr Handle { get; protected set; }

        void INativeWindowConnector.Attach(IntPtr handle)
        {
            Handle = handle;
        }

        IntPtr INativeWindowConnector.Detach()
        {
            var h = Handle;
            Handle = IntPtr.Zero;
            return h;
        }

        public bool SetText(string text)
        {
            return User32Methods.SetWindowText(Handle, text) != 0;
        }

        public string GetText()
        {
            var size = User32Methods.GetWindowTextLength(Handle);
            if (size > 0)
            {
                var sb = new StringBuilder(size);
                return User32Methods.GetWindowText(Handle, sb, size) > 0 ? sb.ToString() : string.Empty;
            }
            return string.Empty;
        }

        public bool SetSize(int width, int height)
        {
            return User32Methods.SetWindowPos(Handle, IntPtr.Zero, -1, -1, width, height,
                       SetWindowPosFlags.SWP_NOACTIVATE | SetWindowPosFlags.SWP_NOMOVE | SetWindowPosFlags.SWP_NOZORDER) !=
                   0;
        }

        public bool SetPosition(int x, int y)
        {
            return User32Methods.SetWindowPos(Handle, IntPtr.Zero, x, y, -1, -1,
                       SetWindowPosFlags.SWP_NOACTIVATE | SetWindowPosFlags.SWP_NOSIZE | SetWindowPosFlags.SWP_NOZORDER) !=
                   0;
        }

        public bool SetPosition(int x, int y, int width, int height)
        {
            return User32Methods.SetWindowPos(Handle, IntPtr.Zero, x, y, width, height,
                       SetWindowPosFlags.SWP_NOACTIVATE | SetWindowPosFlags.SWP_NOZORDER) !=
                   0;
        }

        public bool SetPosition(int x, int y, int width, int height, SetWindowPosFlags flags)
        {
            return User32Methods.SetWindowPos(Handle, IntPtr.Zero, x, y, width, height, flags) !=
                   0;
        }

        public bool SetPosition(HwndZOrder order, int x, int y, int width, int height, SetWindowPosFlags flags)
        {
            return User32Helpers.SetWindowPos(Handle, order, x, y, width, height, flags) !=
                   0;
        }

        public bool SetPosition(IntPtr hWndInsertAfter, int x, int y, int width, int height, SetWindowPosFlags flags)
        {
            return User32Methods.SetWindowPos(Handle, hWndInsertAfter, x, y, width, height, flags) !=
                   0;
        }

        public bool GetPosition(out Rectangle rectangle)
        {
            return User32Methods.GetWindowRect(Handle, out rectangle) !=
                   0;
        }

        public bool GetClientRectangle(out Rectangle rectangle)
        {
            return User32Methods.GetClientRect(Handle, out rectangle) !=
                   0;
        }

        public IntPtr SetItem(WindowLongFlags index, IntPtr value)
        {
            return User32Methods.SetWindowLongPtr(Handle, (int) index, value);
        }

        public IntPtr GetItem(WindowLongFlags index)
        {
            return User32Methods.GetWindowLongPtr(Handle, (int) index);
        }

        public bool Show()
        {
            return User32Methods.ShowWindow(Handle, ShowWindowCommands.SW_SHOW) !=
                   0;
        }

        public bool Hide()
        {
            return User32Methods.ShowWindow(Handle, ShowWindowCommands.SW_HIDE) !=
                   0;
        }

        public bool SetState(ShowWindowCommands flags)
        {
            return User32Methods.ShowWindow(Handle, flags) !=
                   0;
        }

        public IntPtr Create(string className,
            WindowStyles styles = 0,
            WindowExStyles exStyles = 0, string text = "Window",
            int x = (int) CreateWindowFlags.CW_USEDEFAULT, int y = (int) CreateWindowFlags.CW_USEDEFAULT,
            int width = (int) CreateWindowFlags.CW_USEDEFAULT, int height = (int) CreateWindowFlags.CW_USEDEFAULT,
            IntPtr hParent = default(IntPtr), IntPtr hMenu = default(IntPtr), IntPtr hInstance = default(IntPtr),
            IntPtr createParams = default(IntPtr))
        {
            return User32Methods.CreateWindowEx(exStyles, className, text,
                styles, x, y, width, height, hParent, hMenu,
                hInstance == IntPtr.Zero ? WindowFactory.FactoryCache.Instance.ProcessHandle : hInstance,
                createParams);
        }

        public bool Destroy()
        {
            if (User32Methods.DestroyWindow(Handle) != 0)
            {
                Handle = IntPtr.Zero;
                return true;
            }
            return false;
        }
    }

    public sealed class NativeWindow : NativeWindowBase {}
}