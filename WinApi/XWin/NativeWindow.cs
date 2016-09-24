using System;
using System.Text;
using WinApi.Core;
using WinApi.User32;

namespace WinApi.XWin
{
    public interface INativeAttachable
    {
        void Attach(IntPtr handle);
        IntPtr Detach();
    }

    public class NativeWindow : INativeAttachable
    {
        public IntPtr Handle { get; protected set; }

        void INativeAttachable.Attach(IntPtr handle)
        {
            Handle = handle;
        }

        IntPtr INativeAttachable.Detach()
        {
            var h = Handle;
            Handle = IntPtr.Zero;
            return h;
        }

        public bool SetText(string text)
        {
            return User32Methods.SetWindowText(Handle, text);
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

        public bool SetSize(Size size)
        {
            return SetSize(ref size);
        }

        public bool SetSize(ref Size size)
        {
            return SetSize(size.Width, size.Height);
        }

        public bool SetSize(int width, int height)
        {
            return User32Methods.SetWindowPos(Handle, IntPtr.Zero, -1, -1, width, height,
                SetWindowPosFlags.SWP_NOACTIVATE | SetWindowPosFlags.SWP_NOMOVE | SetWindowPosFlags.SWP_NOZORDER);
        }

        public bool SetPosition(int x, int y)
        {
            return User32Methods.SetWindowPos(Handle, IntPtr.Zero, x, y, -1, -1,
                SetWindowPosFlags.SWP_NOACTIVATE | SetWindowPosFlags.SWP_NOSIZE | SetWindowPosFlags.SWP_NOZORDER);
        }

        public bool SetPosition(int x, int y, int width, int height)
        {
            return User32Methods.SetWindowPos(Handle, IntPtr.Zero, x, y, width, height,
                SetWindowPosFlags.SWP_NOACTIVATE | SetWindowPosFlags.SWP_NOZORDER);
        }

        public bool SetPosition(int x, int y, int width, int height, SetWindowPosFlags flags)
        {
            return User32Methods.SetWindowPos(Handle, IntPtr.Zero, x, y, width, height, flags);
        }

        public bool SetPosition(HwndZOrder order, int x, int y, int width, int height, SetWindowPosFlags flags)
        {
            return User32Helpers.SetWindowPos(Handle, order, x, y, width, height, flags);
        }

        public bool SetPosition(IntPtr hWndInsertAfter, int x, int y, int width, int height, SetWindowPosFlags flags)
        {
            return User32Methods.SetWindowPos(Handle, hWndInsertAfter, x, y, width, height, flags);
        }

        public bool SetPosition(Rectangle rect)
        {
            return SetPosition(ref rect);
        }

        public bool SetPosition(Rectangle rect, SetWindowPosFlags flags)
        {
            return SetPosition(ref rect, flags);
        }

        public bool SetPosition(ref Rectangle rect)
        {
            return SetPosition(rect.Left, rect.Top, rect.Width, rect.Height,
                SetWindowPosFlags.SWP_NOACTIVATE | SetWindowPosFlags.SWP_NOZORDER);
        }

        public bool SetPosition(ref Rectangle rect, SetWindowPosFlags flags)
        {
            return SetPosition(rect.Left, rect.Top, rect.Width, rect.Height, flags);
        }

        public bool GetWindowRect(out Rectangle rectangle)
        {
            return User32Methods.GetWindowRect(Handle, out rectangle);
        }

        public Rectangle GetWindowRect()
        {
            Rectangle rectangle;
            User32Methods.GetWindowRect(Handle, out rectangle);
            return rectangle;
        }

        public bool GetClientRect(out Rectangle rectangle)
        {
            return User32Methods.GetClientRect(Handle, out rectangle);
        }

        public Rectangle GetClientRect()
        {
            Rectangle rectangle;
            User32Methods.GetClientRect(Handle, out rectangle);
            return rectangle;
        }

        public Size GetClientSize()
        {
            Rectangle rect;
            GetClientRect(out rect);
            return new Size {Width = rect.Width, Height = rect.Height};
        }

        public Size GetWindowSize()
        {
            Rectangle rect;
            GetWindowRect(out rect);
            return new Size {Width = rect.Width, Height = rect.Height};
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
            return User32Methods.ShowWindow(Handle, ShowWindowCommands.SW_SHOW);
        }

        public bool Hide()
        {
            return User32Methods.ShowWindow(Handle, ShowWindowCommands.SW_HIDE);
        }

        public bool SetState(ShowWindowCommands flags)
        {
            return User32Methods.ShowWindow(Handle, flags);
        }

        public bool Validate(ref Rectangle rect)
        {
            return User32Methods.ValidateRect(Handle, ref rect);
        }

        public bool Validate()
        {
            return User32Methods.ValidateRect(Handle, IntPtr.Zero);
        }

        public bool Invalidate(ref Rectangle rect, bool shouldErase = false)
        {
            return User32Methods.InvalidateRect(Handle, ref rect, shouldErase);
        }

        public bool Invalidate(bool shouldErase = false)
        {
            return User32Methods.InvalidateRect(Handle, IntPtr.Zero, shouldErase);
        }

        public void SetFocus()
        {
            User32Methods.SetFocus(Handle);
        }

        public bool Destroy()
        {
            if (User32Methods.DestroyWindow(Handle))
            {
                Handle = IntPtr.Zero;
                return true;
            }
            return false;
        }
    }
}