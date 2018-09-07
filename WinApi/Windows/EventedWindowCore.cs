using System;
using System.Runtime.CompilerServices;
using WinApi.Core;
using WinApi.User32;
using WinApi.Windows.Helpers;

namespace WinApi.Windows
{
    /// <summary>
    ///     Derives from the WindowCore, and provides all the life cycle, and input events.
    ///     It doesn't really handle them in any way but just provides the events with the
    ///     correct decoded parameters. All the processing and decoding are done
    ///     transparently with `Packets`.
    ///     If in certain high-performance requirements, you need only a few events, this
    ///     can be manually implemented only for the events required using the correspoding
    ///     Packet structure, and `Packetizer`.
    /// </summary>
    public abstract class EventedWindowCore : WindowCore
    {
        protected override void OnMessage(ref WindowMessage msg)
        {
            switch (msg.Id)
            {
                case WM.PAINT:
                    {
                        Packetizer.ProcessPaint(ref msg, this);
                        break;
                    }
                case WM.NCDESTROY:
                    {
                        Packetizer.ProcessNcDestroy(ref msg, this);
                        break;
                    }
                case WM.CLOSE:
                    {
                        Packetizer.ProcessClose(ref msg, this);
                        break;
                    }
                case WM.TIMECHANGE:
                    {
                        Packetizer.ProcessSystemTimeChange(ref msg, this);
                        break;
                    }
                case WM.DESTROY:
                    {
                        Packetizer.ProcessDestroy(ref msg, this);
                        break;
                    }
                case WM.MOUSELEAVE:
                    {
                        Packetizer.ProcessMouseLeave(ref msg, this);
                        break;
                    }
                case WM.NCACTIVATE:
                    {
                        Packetizer.ProcessNcActivate(ref msg, this);
                        break;
                    }
                case WM.NCCALCSIZE:
                    {
                        Packetizer.ProcessNcCalcSize(ref msg, this);
                        break;
                    }
                case WM.SHOWWINDOW:
                    {
                        Packetizer.ProcessShowWindow(ref msg, this);
                        break;
                    }
                case WM.QUIT:
                    {
                        Packetizer.ProcessQuit(ref msg, this);
                        break;
                    }
                case WM.CREATE:
                    {
                        Packetizer.ProcessCreate(ref msg, this);
                        break;
                    }
                case WM.SIZE:
                    {
                        Packetizer.ProcessSize(ref msg, this);
                        break;
                    }
                case WM.MOVE:
                    {
                        Packetizer.ProcessMove(ref msg, this);
                        break;
                    }
                case WM.WINDOWPOSCHANGED:
                    {
                        Packetizer.ProcessWindowPositionChanged(ref msg, this);
                        break;
                    }
                case WM.WINDOWPOSCHANGING:
                    {
                        Packetizer.ProcessWindowPositionChanging(ref msg, this);
                        break;
                    }
                case WM.ACTIVATE:
                    {
                        Packetizer.ProcessActivate(ref msg, this);
                        break;
                    }
                case WM.ERASEBKGND:
                    {
                        Packetizer.ProcessEraseBkgnd(ref msg, this);
                        break;
                    }
                case WM.ACTIVATEAPP:
                    {
                        Packetizer.ProcessActivateApp(ref msg, this);
                        break;
                    }
                case WM.DISPLAYCHANGE:
                    {
                        Packetizer.ProcessDisplayChange(ref msg, this);
                        break;
                    }
                case WM.MOUSEMOVE:
                    {
                        Packetizer.ProcessMouseMove(ref msg, this);
                        break;
                    }
                case WM.LBUTTONUP:
                    {
                        Packetizer.ProcessMouseButton(ref msg, this);
                        break;
                    }
                case WM.LBUTTONDOWN:
                    {
                        Packetizer.ProcessMouseButton(ref msg, this);
                        break;
                    }
                case WM.LBUTTONDBLCLK:
                    {
                        Packetizer.ProcessMouseDoubleClick(ref msg, this);
                        break;
                    }
                case WM.RBUTTONUP:
                    {
                        Packetizer.ProcessMouseButton(ref msg, this);
                        break;
                    }
                case WM.RBUTTONDOWN:
                    {
                        Packetizer.ProcessMouseButton(ref msg, this);
                        break;
                    }
                case WM.RBUTTONDBLCLK:
                    {
                        Packetizer.ProcessMouseDoubleClick(ref msg, this);
                        break;
                    }
                case WM.MBUTTONUP:
                    {
                        Packetizer.ProcessMouseButton(ref msg, this);
                        break;
                    }
                case WM.MBUTTONDOWN:
                    {
                        Packetizer.ProcessMouseButton(ref msg, this);
                        break;
                    }
                case WM.MBUTTONDBLCLK:
                    {
                        Packetizer.ProcessMouseDoubleClick(ref msg, this);
                        break;
                    }
                case WM.XBUTTONUP:
                    {
                        Packetizer.ProcessMouseButton(ref msg, this);
                        break;
                    }
                case WM.XBUTTONDOWN:
                    {
                        Packetizer.ProcessMouseButton(ref msg, this);
                        break;
                    }
                case WM.XBUTTONDBLCLK:
                    {
                        Packetizer.ProcessMouseDoubleClick(ref msg, this);
                        break;
                    }
                case WM.MOUSEACTIVATE:
                    {
                        Packetizer.ProcessMouseActivate(ref msg, this);
                        break;
                    }
                case WM.MOUSEHOVER:
                    {
                        Packetizer.ProcessMouseHover(ref msg, this);
                        break;
                    }
                case WM.MOUSEWHEEL:
                    {
                        Packetizer.ProcessMouseWheel(ref msg, this);
                        break;
                    }
                case WM.MOUSEHWHEEL:
                    {
                        Packetizer.ProcessMouseWheel(ref msg, this);
                        break;
                    }
                case WM.CHAR:
                    {
                        Packetizer.ProcessKeyChar(ref msg, this);
                        break;
                    }
                case WM.SYSCHAR:
                    {
                        Packetizer.ProcessKeyChar(ref msg, this);
                        break;
                    }
                case WM.DEADCHAR:
                    {
                        Packetizer.ProcessKeyChar(ref msg, this);
                        break;
                    }
                case WM.SYSDEADCHAR:
                    {
                        Packetizer.ProcessKeyChar(ref msg, this);
                        break;
                    }
                case WM.KEYUP:
                    {
                        Packetizer.ProcessKey(ref msg, this);
                        break;
                    }
                case WM.KEYDOWN:
                    {
                        Packetizer.ProcessKey(ref msg, this);
                        break;
                    }
                case WM.SYSKEYUP:
                    {
                        Packetizer.ProcessKey(ref msg, this);
                        break;
                    }
                case WM.SYSKEYDOWN:
                    {
                        Packetizer.ProcessKey(ref msg, this);
                        break;
                    }
                case WM.COMMAND:
                    {
                        Packetizer.ProcessCommand(ref msg, this);
                        break;
                    }
                case WM.SYSCOMMAND:
                    {
                        Packetizer.ProcessSysCommand(ref msg, this);
                        break;
                    }
                case WM.MENUCOMMAND:
                    {
                        Packetizer.ProcessMenuCommand(ref msg, this);
                        break;
                    }
                case WM.APPCOMMAND:
                    {
                        Packetizer.ProcessAppCommand(ref msg, this);
                        break;
                    }
                case WM.KILLFOCUS:
                    {
                        Packetizer.ProcessLostFocus(ref msg, this);
                        break;
                    }
                case WM.SETFOCUS:
                    {
                        Packetizer.ProcessGotFocus(ref msg, this);
                        break;
                    }
                case WM.CAPTURECHANGED:
                    {
                        Packetizer.ProcessCaptureChanged(ref msg, this);
                        break;
                    }
                case WM.NCHITTEST:
                    {
                        Packetizer.ProcessNcHitTest(ref msg, this);
                        break;
                    }
                case WM.HOTKEY:
                    {
                        Packetizer.ProcessHotKey(ref msg, this);
                        break;
                    }
                case WM.GETMINMAXINFO:
                    {
                        Packetizer.ProcessGetMinMaxInfo(ref msg, this);
                        break;
                    }
                case WM.NCPAINT:
                    {
                        Packetizer.ProcessNcPaint(ref msg, this);
                        break;
                    }
                case WM.NCMOUSEMOVE:
                    {
                        Packetizer.ProcessNcMouseMove(ref msg, this);
                        break;
                    }
                default:
                    {
                        this.OnMessageDefault(ref msg);
                        return;
                    }
            }
        }

        protected internal virtual void OnPositionChanged(ref WindowPositionPacket packet)
        {
            unsafe { this.OnMessageDefault(ref ((WindowMessageWrapper*) packet.Message)->Value); }
        }

        protected internal virtual void OnPositionChanging(ref WindowPositionPacket packet)
        {
            unsafe { this.OnMessageDefault(ref ((WindowMessageWrapper*) packet.Message)->Value); }
        }

        protected internal virtual void OnClose(ref Packet packet)
        {
            unsafe { this.OnMessageDefault(ref ((WindowMessageWrapper*) packet.Message)->Value); }
        }

        protected internal virtual void OnEraseBkgnd(ref EraseBkgndPacket packet)
        {
            unsafe { this.OnMessageDefault(ref ((WindowMessageWrapper*) packet.Message)->Value); }
        }

        protected internal virtual void OnGetMinMaxInfo(ref MinMaxInfoPacket packet)
        {
            unsafe { this.OnMessageDefault(ref ((WindowMessageWrapper*) packet.Message)->Value); }
        }

        protected internal virtual void OnDestroy(ref Packet packet)
        {
            unsafe { this.OnMessageDefault(ref ((WindowMessageWrapper*) packet.Message)->Value); }
        }

        protected internal virtual void OnSystemTimeChange(ref Packet packet)
        {
            unsafe { this.OnMessageDefault(ref ((WindowMessageWrapper*) packet.Message)->Value); }
        }

        protected internal virtual void OnSize(ref SizePacket packet)
        {
            unsafe { this.OnMessageDefault(ref ((WindowMessageWrapper*) packet.Message)->Value); }
        }

        protected internal virtual void OnMove(ref MovePacket packet)
        {
            unsafe { this.OnMessageDefault(ref ((WindowMessageWrapper*) packet.Message)->Value); }
        }

        protected internal virtual void OnCreate(ref CreateWindowPacket packet)
        {
            unsafe { this.OnMessageDefault(ref ((WindowMessageWrapper*) packet.Message)->Value); }
        }

        protected internal virtual void OnActivate(ref ActivatePacket packet)
        {
            unsafe { this.OnMessageDefault(ref ((WindowMessageWrapper*) packet.Message)->Value); }
        }

        protected internal virtual void OnPaint(ref PaintPacket packet)
        {
            unsafe { this.OnMessageDefault(ref ((WindowMessageWrapper*) packet.Message)->Value); }
        }

        protected internal virtual void OnDisplayChange(ref DisplayChangePacket packet)
        {
            unsafe { this.OnMessageDefault(ref ((WindowMessageWrapper*) packet.Message)->Value); }
        }

        protected internal virtual void OnActivateApp(ref ActivateAppPacket packet)
        {
            unsafe { this.OnMessageDefault(ref ((WindowMessageWrapper*) packet.Message)->Value); }
        }

        protected internal virtual void OnMouseMove(ref MousePacket packet)
        {
            unsafe { this.OnMessageDefault(ref ((WindowMessageWrapper*) packet.Message)->Value); }
        }

        protected internal virtual void OnNcHitTest(ref NcHitTestPacket packet)
        {
            unsafe { this.OnMessageDefault(ref ((WindowMessageWrapper*) packet.Message)->Value); }
        }

        protected internal virtual void OnMouseActivate(ref MouseActivatePacket packet)
        {
            unsafe { this.OnMessageDefault(ref ((WindowMessageWrapper*) packet.Message)->Value); }
        }

        protected internal virtual void OnMouseWheel(ref MouseWheelPacket packet)
        {
            unsafe { this.OnMessageDefault(ref ((WindowMessageWrapper*) packet.Message)->Value); }
        }

        protected internal virtual void OnMouseLeave(ref Packet packet)
        {
            unsafe { this.OnMessageDefault(ref ((WindowMessageWrapper*) packet.Message)->Value); }
        }

        protected internal virtual void OnMouseHover(ref MousePacket packet)
        {
            unsafe { this.OnMessageDefault(ref ((WindowMessageWrapper*) packet.Message)->Value); }
        }

        protected internal virtual void OnInputCaptureChanged(ref CaptureChangedPacket packet)
        {
            unsafe { this.OnMessageDefault(ref ((WindowMessageWrapper*) packet.Message)->Value); }
        }

        protected internal virtual void OnGotFocus(ref FocusPacket packet)
        {
            unsafe { this.OnMessageDefault(ref ((WindowMessageWrapper*) packet.Message)->Value); }
        }

        protected internal virtual void OnLostFocus(ref FocusPacket packet)
        {
            unsafe { this.OnMessageDefault(ref ((WindowMessageWrapper*) packet.Message)->Value); }
        }

        protected internal virtual void OnMenuCommand(ref MenuCommandPacket packet)
        {
            unsafe { this.OnMessageDefault(ref ((WindowMessageWrapper*) packet.Message)->Value); }
        }

        protected internal virtual void OnSysCommand(ref SysCommandPacket packet)
        {
            unsafe { this.OnMessageDefault(ref ((WindowMessageWrapper*) packet.Message)->Value); }
        }

        protected internal virtual void OnCommand(ref CommandPacket packet)
        {
            unsafe { this.OnMessageDefault(ref ((WindowMessageWrapper*) packet.Message)->Value); }
        }

        protected internal virtual void OnKey(ref KeyPacket packet)
        {
            unsafe { this.OnMessageDefault(ref ((WindowMessageWrapper*) packet.Message)->Value); }
        }

        protected internal virtual void OnKeyChar(ref KeyCharPacket packet)
        {
            unsafe { this.OnMessageDefault(ref ((WindowMessageWrapper*) packet.Message)->Value); }
        }

        protected internal virtual void OnHotKey(ref HotKeyPacket packet)
        {
            unsafe { this.OnMessageDefault(ref ((WindowMessageWrapper*) packet.Message)->Value); }
        }

        protected internal virtual void OnAppCommand(ref AppCommandPacket packet)
        {
            unsafe { this.OnMessageDefault(ref ((WindowMessageWrapper*) packet.Message)->Value); }
        }

        protected internal virtual void OnMouseButton(ref MouseButtonPacket packet)
        {
            unsafe { this.OnMessageDefault(ref ((WindowMessageWrapper*) packet.Message)->Value); }
        }

        protected internal virtual void OnNcCalcSize(ref NcCalcSizePacket packet)
        {
            unsafe { this.OnMessageDefault(ref ((WindowMessageWrapper*) packet.Message)->Value); }
        }

        protected internal virtual void OnShow(ref ShowWindowPacket packet)
        {
            unsafe { this.OnMessageDefault(ref ((WindowMessageWrapper*) packet.Message)->Value); }
        }

        protected internal virtual void OnQuit(ref QuitPacket packet)
        {
            unsafe { this.OnMessageDefault(ref ((WindowMessageWrapper*) packet.Message)->Value); }
        }

        protected internal virtual void OnNcActivate(ref NcActivatePacket packet)
        {
            unsafe { this.OnMessageDefault(ref ((WindowMessageWrapper*) packet.Message)->Value); }
        }

        protected internal virtual void OnNcDestroy(ref Packet packet)
        {
            unsafe { this.OnMessageDefault(ref ((WindowMessageWrapper*) packet.Message)->Value); }
        }

        protected internal virtual void OnNcPaint(ref NcPaintPacket packet)
        {
            unsafe { this.OnMessageDefault(ref ((WindowMessageWrapper*) packet.Message)->Value); }
        }

        protected internal virtual void OnNcMouseMove(ref NcMouseMovePacket packet)
        {
            unsafe { this.OnMessageDefault(ref ((WindowMessageWrapper*)packet.Message)->Value); }
        }

        protected internal virtual void OnMouseDoubleClick(ref MouseDoubleClickPacket packet)
        {
            unsafe { this.OnMessageDefault(ref ((WindowMessageWrapper*) packet.Message)->Value); }
        }
    }

    public sealed class EventedWindow : EventedWindowCore {}
}