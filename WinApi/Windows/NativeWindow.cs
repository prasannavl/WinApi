using System;
using System.Text;
using NetCoreEx.BinaryExtensions;
using NetCoreEx.Geometry;
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
            this.Handle = handle;
        }

        IntPtr INativeAttachable.Detach()
        {
            var h = this.Handle;
            this.Handle = IntPtr.Zero;
            return h;
        }

        public bool SetText(string text)
        {
            return User32Methods.SetWindowText(this.Handle, text);
        }

        public string GetText()
        {
            var size = User32Methods.GetWindowTextLength(this.Handle);
            if (size > 0)
            {
                var len = size + 1;
                var sb = new StringBuilder(len);
                return User32Methods.GetWindowText(this.Handle, sb, len) > 0 ? sb.ToString() : string.Empty;
            }
            return string.Empty;
        }

        public bool SetSize(Size size)
        {
            return this.SetSize(ref size);
        }

        public bool SetSize(ref Size size)
        {
            return this.SetSize(size.Width, size.Height);
        }

        public bool SetSize(int width, int height)
        {
            return User32Methods.SetWindowPos(this.Handle, IntPtr.Zero, -1, -1, width, height,
                WindowPositionFlags.SWP_NOACTIVATE | WindowPositionFlags.SWP_NOMOVE | WindowPositionFlags.SWP_NOZORDER);
        }

        public bool SetPosition(int x, int y)
        {
            return User32Methods.SetWindowPos(this.Handle, IntPtr.Zero, x, y, -1, -1,
                WindowPositionFlags.SWP_NOACTIVATE | WindowPositionFlags.SWP_NOSIZE | WindowPositionFlags.SWP_NOZORDER);
        }

        public bool SetPosition(int x, int y, int width, int height)
        {
            return User32Methods.SetWindowPos(this.Handle, IntPtr.Zero, x, y, width, height,
                WindowPositionFlags.SWP_NOACTIVATE | WindowPositionFlags.SWP_NOZORDER);
        }

        public bool SetPosition(int x, int y, int width, int height, WindowPositionFlags flags)
        {
            return User32Methods.SetWindowPos(this.Handle, IntPtr.Zero, x, y, width, height, flags);
        }

        public bool SetPosition(HwndZOrder order, int x, int y, int width, int height, WindowPositionFlags flags)
        {
            return User32Helpers.SetWindowPos(this.Handle, order, x, y, width, height, flags);
        }

        public bool SetPosition(IntPtr hWndInsertAfter, int x, int y, int width, int height, WindowPositionFlags flags)
        {
            return User32Methods.SetWindowPos(this.Handle, hWndInsertAfter, x, y, width, height, flags);
        }

        public bool SetPosition(Rectangle rect)
        {
            return this.SetPosition(ref rect);
        }

        public bool SetPosition(Rectangle rect, WindowPositionFlags flags)
        {
            return this.SetPosition(ref rect, flags);
        }

        public bool SetPosition(ref Rectangle rect)
        {
            return this.SetPosition(rect.Left, rect.Top, rect.Width, rect.Height,
                WindowPositionFlags.SWP_NOACTIVATE | WindowPositionFlags.SWP_NOZORDER);
        }

        public bool SetPosition(ref Rectangle rect, WindowPositionFlags flags)
        {
            return this.SetPosition(rect.Left, rect.Top, rect.Width, rect.Height, flags);
        }

        public bool GetWindowRect(out Rectangle rectangle)
        {
            return User32Methods.GetWindowRect(this.Handle, out rectangle);
        }

        public Rectangle GetWindowRect()
        {
            Rectangle rectangle;
            User32Methods.GetWindowRect(this.Handle, out rectangle);
            return rectangle;
        }

        public bool GetClientRect(out Rectangle rectangle)
        {
            return User32Methods.GetClientRect(this.Handle, out rectangle);
        }

        public Rectangle GetClientRect()
        {
            Rectangle rectangle;
            User32Methods.GetClientRect(this.Handle, out rectangle);
            return rectangle;
        }

        public Size GetClientSize()
        {
            Rectangle rect;
            this.GetClientRect(out rect);
            return new Size {Width = rect.Width, Height = rect.Height};
        }

        public Size GetWindowSize()
        {
            Rectangle rect;
            this.GetWindowRect(out rect);
            return new Size {Width = rect.Width, Height = rect.Height};
        }

        public bool IsVisible()
        {
            return User32Methods.IsWindowVisible(this.Handle);
        }

        public bool IsEnabled()
        {
            return User32Methods.IsWindowEnabled(this.Handle);
        }

        public IntPtr SetParam(WindowLongFlags index, IntPtr value)
        {
            return User32Methods.SetWindowLongPtr(this.Handle, (int) index, value);
        }

        public IntPtr GetParam(WindowLongFlags index)
        {
            return User32Methods.GetWindowLongPtr(this.Handle, (int) index);
        }

        public WindowStyles GetStyles()
        {
            return (WindowStyles) this.GetParam(WindowLongFlags.GWL_STYLE).ToSafeInt32();
        }

        public WindowExStyles GetExStyles()
        {
            return (WindowExStyles) this.GetParam(WindowLongFlags.GWL_EXSTYLE).ToSafeInt32();
        }

        public WindowStyles SetStyle(WindowStyles styles)
        {
            return (WindowStyles) this.SetParam(WindowLongFlags.GWL_STYLE, new IntPtr((int) styles));
        }

        public WindowExStyles SetExStyles(WindowExStyles exStyles)
        {
            return (WindowExStyles) this.SetParam(WindowLongFlags.GWL_EXSTYLE, new IntPtr((int) exStyles));
        }

        public bool Show()
        {
            return User32Methods.ShowWindow(this.Handle, ShowWindowCommands.SW_SHOW);
        }

        public bool Hide()
        {
            return User32Methods.ShowWindow(this.Handle, ShowWindowCommands.SW_HIDE);
        }

        public IntPtr BeginPaint(out PaintStruct ps)
        {
            return User32Methods.BeginPaint(this.Handle, out ps);
        }

        public void EndPaint(ref PaintStruct ps)
        {
            User32Methods.EndPaint(this.Handle, ref ps);
        }

        public void RedrawFrame()
        {
            this.SetPosition(new Rectangle(),
                WindowPositionFlags.SWP_FRAMECHANGED | WindowPositionFlags.SWP_NOMOVE |
                WindowPositionFlags.SWP_NOSIZE);
        }

        public IntPtr GetDc()
        {
            return User32Methods.GetDC(this.Handle);
        }

        public IntPtr GetWindowDc()
        {
            return User32Methods.GetWindowDC(this.Handle);
        }

        public bool ReleaseDc(IntPtr hdc)
        {
            return User32Methods.ReleaseDC(this.Handle, hdc);
        }

        public bool SetState(ShowWindowCommands flags)
        {
            return User32Methods.ShowWindow(this.Handle, flags);
        }

        public bool Validate(ref Rectangle rect)
        {
            return User32Methods.ValidateRect(this.Handle, ref rect);
        }

        public bool Validate()
        {
            return User32Methods.ValidateRect(this.Handle, IntPtr.Zero);
        }

        public bool Invalidate(ref Rectangle rect, bool shouldErase = false)
        {
            return User32Methods.InvalidateRect(this.Handle, ref rect, shouldErase);
        }

        public bool Invalidate(bool shouldErase = false)
        {
            return User32Methods.InvalidateRect(this.Handle, IntPtr.Zero, shouldErase);
        }

        public void SetFocus()
        {
            User32Methods.SetFocus(this.Handle);
        }

        public Rectangle ClientToScreen(ref Rectangle clientRect)
        {
            Rectangle rect;
            this.ClientToScreen(ref clientRect, out rect);
            return rect;
        }

        public virtual void ClientToScreen(ref Rectangle clientRect, out Rectangle screenRect)
        {
            screenRect = clientRect;
            User32Helpers.MapWindowPoints(this.Handle, IntPtr.Zero, ref screenRect);
            User32Methods.AdjustWindowRectEx(ref screenRect, this.GetStyles(),
                User32Methods.GetMenu(this.Handle) != IntPtr.Zero, this.GetExStyles());
        }

        public Rectangle ScreenToClient(ref Rectangle screenRect)
        {
            Rectangle rect;
            this.ScreenToClient(ref screenRect, out rect);
            return rect;
        }

        public virtual void ScreenToClient(ref Rectangle screenRect, out Rectangle clientRect)
        {
            clientRect = screenRect;
            User32Helpers.MapWindowPoints(IntPtr.Zero, this.Handle, ref clientRect);
            User32Helpers.InverseAdjustWindowRectEx(ref clientRect, this.GetStyles(),
                User32Methods.GetMenu(this.Handle) != IntPtr.Zero, this.GetExStyles());
        }

        public void CenterToScreen(bool useWorkArea = true)
        {
            var monitor = User32Methods.MonitorFromWindow(this.Handle,
                MonitorFlag.MONITOR_DEFAULTTONEAREST);
            MonitorInfo monitorInfo;
            User32Helpers.GetMonitorInfo(monitor, out monitorInfo);
            var screenRect = useWorkArea ? monitorInfo.WorkRect : monitorInfo.MonitorRect;
            var midX = screenRect.Width/2;
            var midY = screenRect.Height/2;
            var size = this.GetWindowSize();
            this.SetPosition(midX - size.Width/2, midY - size.Height/2);
        }

        public bool Destroy()
        {
            if (User32Methods.DestroyWindow(this.Handle))
            {
                this.Handle = IntPtr.Zero;
                return true;
            }
            return false;
        }
    }
}