using System;
using System.Text;
using WinApi.Core;
using WinApi.Extensions;
using WinApi.Helpers;
using WinApi.Kernel32;
using WinApi.User32;

namespace WinApi.Windows
{
    public interface INativeAttachable
    {
        void Attach(IntPtr handle);
        IntPtr Detach();
    }

    /// <summary>
    ///     A simple wrapper around the Win32 window. It has nothing except
    ///     the handle of the window. All functions here are direct api calls.
    /// </summary>
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
                WindowPositionFlags.SWP_NOACTIVATE | WindowPositionFlags.SWP_NOMOVE | WindowPositionFlags.SWP_NOZORDER);
        }

        public bool SetPosition(int x, int y)
        {
            return User32Methods.SetWindowPos(Handle, IntPtr.Zero, x, y, -1, -1,
                WindowPositionFlags.SWP_NOACTIVATE | WindowPositionFlags.SWP_NOSIZE | WindowPositionFlags.SWP_NOZORDER);
        }

        public bool SetPosition(int x, int y, int width, int height)
        {
            return User32Methods.SetWindowPos(Handle, IntPtr.Zero, x, y, width, height,
                WindowPositionFlags.SWP_NOACTIVATE | WindowPositionFlags.SWP_NOZORDER);
        }

        public bool SetPosition(int x, int y, int width, int height, WindowPositionFlags flags)
        {
            return User32Methods.SetWindowPos(Handle, IntPtr.Zero, x, y, width, height, flags);
        }

        public bool SetPosition(HwndZOrder order, int x, int y, int width, int height, WindowPositionFlags flags)
        {
            return User32Helpers.SetWindowPos(Handle, order, x, y, width, height, flags);
        }

        public bool SetPosition(IntPtr hWndInsertAfter, int x, int y, int width, int height, WindowPositionFlags flags)
        {
            return User32Methods.SetWindowPos(Handle, hWndInsertAfter, x, y, width, height, flags);
        }

        public bool SetPosition(Rectangle rect)
        {
            return SetPosition(ref rect);
        }

        public bool SetPosition(Rectangle rect, WindowPositionFlags flags)
        {
            return SetPosition(ref rect, flags);
        }

        public bool SetPosition(ref Rectangle rect)
        {
            return SetPosition(rect.Left, rect.Top, rect.Width, rect.Height,
                WindowPositionFlags.SWP_NOACTIVATE | WindowPositionFlags.SWP_NOZORDER);
        }

        public bool SetPosition(ref Rectangle rect, WindowPositionFlags flags)
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

        public IntPtr SetParam(WindowLongFlags index, IntPtr value)
        {
            return User32Methods.SetWindowLongPtr(Handle, (int) index, value);
        }

        public IntPtr GetParam(WindowLongFlags index)
        {
            return User32Methods.GetWindowLongPtr(Handle, (int) index);
        }

        public WindowStyles GetStyles()
        {
            return (WindowStyles) GetParam(WindowLongFlags.GWL_STYLE).ToSafeInt32();
        }

        public WindowExStyles GetExStyles()
        {
            return (WindowExStyles) GetParam(WindowLongFlags.GWL_EXSTYLE).ToSafeInt32();
        }

        public WindowStyles SetStyle(WindowStyles styles)
        {
            return (WindowStyles) SetParam(WindowLongFlags.GWL_STYLE, new IntPtr((int) styles));
        }

        public WindowExStyles SetExStyles(WindowExStyles exStyles)
        {
            return (WindowExStyles) SetParam(WindowLongFlags.GWL_EXSTYLE, new IntPtr((int) exStyles));
        }

        public bool Show()
        {
            return User32Methods.ShowWindow(Handle, ShowWindowCommands.SW_SHOW);
        }

        public bool Hide()
        {
            return User32Methods.ShowWindow(Handle, ShowWindowCommands.SW_HIDE);
        }

        public IntPtr BeginPaint(out PaintStruct ps)
        {
            return User32Methods.BeginPaint(Handle, out ps);
        }

        public void EndPaint(ref PaintStruct ps)
        {
            User32Methods.EndPaint(Handle, ref ps);
        }

        public void RedrawFrame()
        {
            SetPosition(new Rectangle(),
                WindowPositionFlags.SWP_FRAMECHANGED | WindowPositionFlags.SWP_NOMOVE |
                WindowPositionFlags.SWP_NOSIZE);
        }

        public IntPtr GetDc()
        {
            return User32Methods.GetDC(Handle);
        }

        public IntPtr GetWindowDc()
        {
            return User32Methods.GetWindowDC(Handle);
        }

        public bool ReleaseDc(IntPtr hdc)
        {
            return User32Methods.ReleaseDC(Handle, hdc);
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

        public Rectangle ClientToWindow(ref Rectangle clientRect)
        {
            Rectangle rect;
            ClientToWindow(ref clientRect, out rect);
            return rect;
        }

        public virtual void ClientToWindow(ref Rectangle clientRect, out Rectangle windowRect)
        {
            windowRect = clientRect;
            User32Helpers.MapWindowPoints(Handle, IntPtr.Zero, ref windowRect);
            User32Methods.AdjustWindowRectEx(ref windowRect, GetStyles(), User32Methods.GetMenu(Handle) != IntPtr.Zero,
                GetExStyles());
        }

        public Rectangle WindowToClient(ref Rectangle windowRect)
        {
            Rectangle rect;
            WindowToClient(ref windowRect, out rect);
            return rect;
        }

        public virtual void WindowToClient(ref Rectangle windowRect, out Rectangle clientRect)
        {
            clientRect = windowRect;
            User32Helpers.MapWindowPoints(IntPtr.Zero, Handle, ref clientRect);
            User32Helpers.InverseAdjustWindowRectEx(ref clientRect, GetStyles(),
                User32Methods.GetMenu(Handle) != IntPtr.Zero,
                GetExStyles());
        }

        public void CenterToScreen(bool useWorkArea = true)
        {
            var monitor = User32Methods.MonitorFromWindow(Handle,
                MonitorFlag.MONITOR_DEFAULTTONEAREST);
            MonitorInfo monitorInfo;
            User32Helpers.GetMonitorInfo(monitor, out monitorInfo);
            var screenRect = useWorkArea ? monitorInfo.WorkRect : monitorInfo.MonitorRect;
            var midX = screenRect.Width/2;
            var midY = screenRect.Height/2;
            var size = GetWindowSize();
            SetPosition(midX - size.Width/2, midY - size.Height/2);
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