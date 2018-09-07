using System;
using NetCoreEx.Geometry;

namespace WinApi.Windows
{
    public static class PacketizerExtensions
    {
        public static int ToInt32(this Size size)
        {
            return (size.Height << 16) | size.Width;
        }

        public static int ToInt32(this Point point)
        {
            return (point.Y << 16) | point.X;
        }
    }

    public static class Packetizer
    {
        public static unsafe void ProcessNcPaint(ref WindowMessage msg, EventedWindowCore window)
        {
            fixed (WindowMessage* ptr = &msg)
            {
                var packet = new NcPaintPacket(ptr);
                window.OnNcPaint(ref packet);
            }
        }

        public static unsafe void ProcessNcActivate(ref WindowMessage msg, EventedWindowCore window)
        {
            fixed (WindowMessage* ptr = &msg)
            {
                var packet = new NcActivatePacket(ptr);
                window.OnNcActivate(ref packet);
            }
        }

        public static unsafe void ProcessNcCalcSize(ref WindowMessage msg, EventedWindowCore window)
        {
            fixed (WindowMessage* ptr = &msg)
            {
                var packet = new NcCalcSizePacket(ptr);
                window.OnNcCalcSize(ref packet);
            }
        }

        public static unsafe void ProcessNcMouseMove(ref WindowMessage msg, EventedWindowCore window)
        {
            fixed (WindowMessage* ptr = &msg)
            {
                var packet = new NcMouseMovePacket(ptr);
                window.OnNcMouseMove(ref packet);
            }
        }

        public static unsafe void ProcessGetMinMaxInfo(ref WindowMessage msg, EventedWindowCore window)
        {
            fixed (WindowMessage* ptr = &msg)
            {
                var packet = new MinMaxInfoPacket(ptr);
                window.OnGetMinMaxInfo(ref packet);
            }
        }

        public static unsafe void ProcessSize(ref WindowMessage msg, EventedWindowCore window)
        {
            fixed (WindowMessage* ptr = &msg)
            {
                var packet = new SizePacket(ptr);
                window.OnSize(ref packet);
            }
        }

        public static unsafe void ProcessMove(ref WindowMessage msg, EventedWindowCore window)
        {
            fixed (WindowMessage* ptr = &msg)
            {
                var packet = new MovePacket(ptr);
                window.OnMove(ref packet);
            }
        }

        public static unsafe void ProcessPaint(ref WindowMessage msg, EventedWindowCore window)
        {
            fixed (WindowMessage* ptr = &msg)
            {
                var packet = new PaintPacket(ptr);
                window.OnPaint(ref packet);
            }
        }

        public static unsafe void ProcessEraseBkgnd(ref WindowMessage msg, EventedWindowCore window)
        {
            fixed (WindowMessage* ptr = &msg)
            {
                var packet = new EraseBkgndPacket(ptr);
                window.OnEraseBkgnd(ref packet);
            }
        }

        public static unsafe void ProcessWindowPositionChanged(ref WindowMessage msg, EventedWindowCore window)
        {
            fixed (WindowMessage* ptr = &msg)
            {
                var packet = new WindowPositionPacket(ptr);
                window.OnPositionChanged(ref packet);
            }
        }

        public static unsafe void ProcessWindowPositionChanging(ref WindowMessage msg, EventedWindowCore window)
        {
            fixed (WindowMessage* ptr = &msg)
            {
                var packet = new WindowPositionPacket(ptr);
                window.OnPositionChanging(ref packet);
            }
        }

        public static unsafe void ProcessShowWindow(ref WindowMessage msg, EventedWindowCore window)
        {
            fixed (WindowMessage* ptr = &msg)
            {
                var packet = new ShowWindowPacket(ptr);
                window.OnShow(ref packet);
            }
        }

        public static unsafe void ProcessQuit(ref WindowMessage msg, EventedWindowCore window)
        {
            fixed (WindowMessage* ptr = &msg)
            {
                var packet = new QuitPacket(ptr);
                window.OnQuit(ref packet);
            }
        }

        public static unsafe void ProcessCreate(ref WindowMessage msg, EventedWindowCore window)
        {
            fixed (WindowMessage* ptr = &msg)
            {
                var packet = new CreateWindowPacket(ptr);
                window.OnCreate(ref packet);
            }
        }

        public static unsafe void ProcessActivate(ref WindowMessage msg, EventedWindowCore window)
        {
            fixed (WindowMessage* ptr = &msg)
            {
                var packet = new ActivatePacket(ptr);
                window.OnActivate(ref packet);
            }
        }

        public static unsafe void ProcessKey(ref WindowMessage msg, EventedWindowCore window)
        {
            fixed (WindowMessage* ptr = &msg)
            {
                var packet = new KeyPacket(ptr);
                window.OnKey(ref packet);
            }
        }

        public static unsafe void ProcessKeyChar(ref WindowMessage msg, EventedWindowCore window)
        {
            fixed (WindowMessage* ptr = &msg)
            {
                var packet = new KeyCharPacket(ptr);
                window.OnKeyChar(ref packet);
            }
        }

        public static unsafe void ProcessMouseDoubleClick(ref WindowMessage msg, EventedWindowCore window)
        {
            fixed (WindowMessage* ptr = &msg)
            {
                var packet = new MouseDoubleClickPacket(ptr);
                window.OnMouseDoubleClick(ref packet);
            }
        }

        public static unsafe void ProcessMouseMove(ref WindowMessage msg, EventedWindowCore window)
        {
            fixed (WindowMessage* ptr = &msg)
            {
                var packet = new MousePacket(ptr);
                window.OnMouseMove(ref packet);
            }
        }

        public static unsafe void ProcessMouseHover(ref WindowMessage msg, EventedWindowCore window)
        {
            fixed (WindowMessage* ptr = &msg)
            {
                var packet = new MousePacket(ptr);
                window.OnMouseHover(ref packet);
            }
        }

        public static unsafe void ProcessMouseButton(ref WindowMessage msg, EventedWindowCore window)
        {
            fixed (WindowMessage* ptr = &msg)
            {
                var packet = new MouseButtonPacket(ptr);
                window.OnMouseButton(ref packet);
            }
        }

        public static unsafe void ProcessMouseActivate(ref WindowMessage msg, EventedWindowCore window)
        {
            fixed (WindowMessage* ptr = &msg)
            {
                var packet = new MouseActivatePacket(ptr);
                window.OnMouseActivate(ref packet);
            }
        }

        public static unsafe void ProcessMouseWheel(ref WindowMessage msg, EventedWindowCore window)
        {
            fixed (WindowMessage* ptr = &msg)
            {
                var packet = new MouseWheelPacket(ptr);
                window.OnMouseWheel(ref packet);
            }
        }

        public static unsafe void ProcessActivateApp(ref WindowMessage msg, EventedWindowCore window)
        {
            fixed (WindowMessage* ptr = &msg)
            {
                var packet = new ActivateAppPacket(ptr);
                window.OnActivateApp(ref packet);
            }
        }

        public static unsafe void ProcessDisplayChange(ref WindowMessage msg, EventedWindowCore window)
        {
            fixed (WindowMessage* ptr = &msg)
            {
                var packet = new DisplayChangePacket(ptr);
                window.OnDisplayChange(ref packet);
            }
        }

        public static unsafe void ProcessCommand(ref WindowMessage msg, EventedWindowCore window)
        {
            fixed (WindowMessage* ptr = &msg)
            {
                var packet = new CommandPacket(ptr);
                window.OnCommand(ref packet);
            }
        }

        public static unsafe void ProcessSysCommand(ref WindowMessage msg, EventedWindowCore window)
        {
            fixed (WindowMessage* ptr = &msg)
            {
                var packet = new SysCommandPacket(ptr);
                window.OnSysCommand(ref packet);
            }
        }

        public static unsafe void ProcessMenuCommand(ref WindowMessage msg, EventedWindowCore window)
        {
            fixed (WindowMessage* ptr = &msg)
            {
                var packet = new MenuCommandPacket(ptr);
                window.OnMenuCommand(ref packet);
            }
        }

        public static unsafe void ProcessAppCommand(ref WindowMessage msg, EventedWindowCore window)
        {
            fixed (WindowMessage* ptr = &msg)
            {
                var packet = new AppCommandPacket(ptr);
                window.OnAppCommand(ref packet);
            }
        }


        public static unsafe void ProcessHotKey(ref WindowMessage msg, EventedWindowCore window)
        {
            fixed (WindowMessage* ptr = &msg)
            {
                var packet = new HotKeyPacket(ptr);
                window.OnHotKey(ref packet);
            }
        }


        public static unsafe void ProcessGotFocus(ref WindowMessage msg, EventedWindowCore window)
        {
            fixed (WindowMessage* ptr = &msg)
            {
                var packet = new FocusPacket(ptr);
                window.OnGotFocus(ref packet);
            }
        }

        public static unsafe void ProcessLostFocus(ref WindowMessage msg, EventedWindowCore window)
        {
            fixed (WindowMessage* ptr = &msg)
            {
                var packet = new FocusPacket(ptr);
                window.OnLostFocus(ref packet);
            }
        }

        public static unsafe void ProcessCaptureChanged(ref WindowMessage msg, EventedWindowCore window)
        {
            fixed (WindowMessage* ptr = &msg)
            {
                var packet = new CaptureChangedPacket(ptr);
                window.OnInputCaptureChanged(ref packet);
            }
        }

        public static unsafe void ProcessNcHitTest(ref WindowMessage msg, EventedWindowCore window)
        {
            fixed (WindowMessage* ptr = &msg)
            {
                var packet = new NcHitTestPacket(ptr);
                window.OnNcHitTest(ref packet);
            }
        }

        public static unsafe void ProcessNcDestroy(ref WindowMessage msg, EventedWindowCore window)
        {
            fixed (WindowMessage* ptr = &msg)
            {
                var packet = new Packet(ptr);
                window.OnNcDestroy(ref packet);
            }
        }

        public static unsafe void ProcessClose(ref WindowMessage msg, EventedWindowCore window)
        {
            fixed (WindowMessage* ptr = &msg)
            {
                var packet = new Packet(ptr);
                window.OnClose(ref packet);
            }
        }

        public static unsafe void ProcessSystemTimeChange(ref WindowMessage msg, EventedWindowCore window)
        {
            fixed (WindowMessage* ptr = &msg)
            {
                var packet = new Packet(ptr);
                window.OnSystemTimeChange(ref packet);
            }
        }

        public static unsafe void ProcessMouseLeave(ref WindowMessage msg, EventedWindowCore window)
        {
            fixed (WindowMessage* ptr = &msg)
            {
                var packet = new Packet(ptr);
                window.OnMouseLeave(ref packet);
            }
        }

        public static unsafe void ProcessDestroy(ref WindowMessage msg, EventedWindowCore window)
        {
            fixed (WindowMessage* ptr = &msg)
            {
                var packet = new Packet(ptr);
                window.OnDestroy(ref packet);
            }
        }
    }
}