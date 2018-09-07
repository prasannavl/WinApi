using System;
using System.Runtime.InteropServices;
using NetCoreEx.BinaryExtensions;
using NetCoreEx.Geometry;
using WinApi.User32;

namespace WinApi.Windows
{
    public struct Packet
    {
        public unsafe WindowMessage* Message;

        public unsafe Packet(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe IntPtr Hwnd { get { return this.Message->Hwnd; } set { this.Message->Hwnd = value; } }
    }

    public struct SizePacket
    {
        public unsafe WindowMessage* Message;

        public unsafe SizePacket(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe IntPtr Hwnd { get { return this.Message->Hwnd; } set { this.Message->Hwnd = value; } }

        public unsafe WindowSizeFlag Flag
        {
            get { return (WindowSizeFlag) this.Message->WParam.ToSafeInt32(); }
            set { this.Message->WParam = new IntPtr((int) value); }
        }

        public unsafe Size Size
        {
            get
            {
                Size size;
                this.Message->LParam.BreakSafeInt32To16Signed(out size.Height, out size.Width);
                return size;
            }

            set { this.Message->LParam = new IntPtr(value.ToInt32()); }
        }
    }

    public struct MovePacket
    {
        public unsafe WindowMessage* Message;

        public unsafe MovePacket(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe IntPtr Hwnd { get { return this.Message->Hwnd; } set { this.Message->Hwnd = value; } }

        public unsafe Point Point
        {
            get
            {
                Point point;
                this.Message->LParam.BreakSafeInt32To16Signed(out point.Y, out point.X);
                return point;
            }

            set { this.Message->LParam = new IntPtr(value.ToInt32()); }
        }
    }

    public struct PaintPacket
    {
        public unsafe WindowMessage* Message;

        public unsafe PaintPacket(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe IntPtr Hwnd { get { return this.Message->Hwnd; } set { this.Message->Hwnd = value; } }

        public unsafe IntPtr Hdc { get { return this.Message->WParam; } set { this.Message->WParam = value; } }
    }

    public struct MinMaxInfoPacket
    {
        public unsafe WindowMessage* Message;

        public unsafe MinMaxInfoPacket(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe IntPtr Hwnd { get { return this.Message->Hwnd; } set { this.Message->Hwnd = value; } }
        public unsafe ref MinMaxInfo Info => ref ((MinMaxInfoWrapper*)Message->LParam)->Value;

    }

    public struct EraseBkgndPacket
    {
        public unsafe WindowMessage* Message;

        public unsafe EraseBkgndPacket(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe IntPtr Hwnd { get { return this.Message->Hwnd; } set { this.Message->Hwnd = value; } }
        public unsafe IntPtr Hdc { get { return this.Message->WParam; } set { this.Message->WParam = value; } }

        public unsafe EraseBackgroundResult Result
        {
            get { return (EraseBackgroundResult) this.Message->Result.ToSafeInt32(); }
            set { this.Message->Result = new IntPtr((int) value); }
        }
    }

    public struct WindowPositionPacket
    {
        public unsafe WindowMessage* Message;

        public unsafe WindowPositionPacket(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe IntPtr Hwnd { get { return this.Message->Hwnd; } set { this.Message->Hwnd = value; } }
        public unsafe ref WindowPosition Position => ref ((WindowPositionWrapper*)Message->LParam)->Value;

    }

    public struct ShowWindowPacket
    {
        public unsafe WindowMessage* Message;

        public unsafe ShowWindowPacket(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe IntPtr Hwnd { get { return this.Message->Hwnd; } set { this.Message->Hwnd = value; } }

        public unsafe ShowWindowStatusFlags Flags
        {
            get { return (ShowWindowStatusFlags) this.Message->LParam.ToSafeInt32(); }
            set { this.Message->LParam = new IntPtr((int) value); }
        }

        public unsafe bool IsShown
        {
            get { return this.Message->WParam.ToSafeInt32() > 0; }
            set { this.Message->WParam = (IntPtr) (value ? 1 : 0); }
        }
    }

    public struct QuitPacket
    {
        public unsafe WindowMessage* Message;

        public unsafe QuitPacket(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe IntPtr Hwnd { get { return this.Message->Hwnd; } set { this.Message->Hwnd = value; } }

        public unsafe int Code
        {
            get { return this.Message->WParam.ToSafeInt32(); }
            set { this.Message->WParam = (IntPtr) value; }
        }
    }

    public struct CreateWindowPacket
    {
        public unsafe WindowMessage* Message;

        public unsafe CreateWindowPacket(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe IntPtr Hwnd { get { return this.Message->Hwnd; } set { this.Message->Hwnd = value; } }
        public unsafe ref CreateStruct CreateStruct => ref ((CreateStructWrapper*)Message->LParam)->Value;


        // Return 0 to continue creation. -1 to destroy and prevent
        public unsafe CreateWindowResult Result
        {
            get { return (CreateWindowResult) this.Message->Result.ToSafeInt32(); }
            set { this.Message->Result = new IntPtr((int) value); }
        }
    }

    public struct ActivatePacket
    {
        public unsafe WindowMessage* Message;

        public unsafe ActivatePacket(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe IntPtr Hwnd { get { return this.Message->Hwnd; } set { this.Message->Hwnd = value; } }
        public unsafe IntPtr ConverseHwnd { get { return this.Message->LParam; } set { this.Message->LParam = value; } }

        public unsafe bool IsMinimized
        {
            get { return this.GetWParamAsInt().High() != 0; }
            set { this.Message->WParam = new IntPtr(this.GetWParamAsInt().WithHigh((short) (value ? 1 : 0))); }
        }

        public unsafe WindowActivateFlag Flag
        {
            get { return (WindowActivateFlag) this.GetWParamAsInt().LowAsInt(); }
            set { this.Message->WParam = new IntPtr(this.GetWParamAsInt().WithLow(unchecked((short) value))); }
        }


        private unsafe int GetWParamAsInt()
        {
            return this.Message->WParam.ToSafeInt32();
        }
    }

    public struct ActivateAppPacket
    {
        public unsafe WindowMessage* Message;
        public unsafe IntPtr Hwnd { get { return this.Message->Hwnd; } set { this.Message->Hwnd = value; } }

        public unsafe ActivateAppPacket(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe uint ConverseThreadId
        {
            get { return this.Message->LParam.ToSafeUInt32(); }
            set { this.Message->LParam = new IntPtr(unchecked((int) value)); }
        }

        public unsafe bool IsActive
        {
            get { return this.Message->WParam.ToSafeInt32() != 0; }
            set { this.Message->WParam = new IntPtr(value ? 1 : 0); }
        }
    }

    public struct KeyCharPacket
    {
        public unsafe WindowMessage* Message;

        public unsafe KeyCharPacket(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe IntPtr Hwnd { get { return this.Message->Hwnd; } set { this.Message->Hwnd = value; } }

        public unsafe char Key
        {
            get { return (char) this.Message->WParam.ToSafeInt32(); }
            set { this.Message->WParam = new IntPtr(value); }
        }

        public unsafe KeyboardInputState InputState
        {
            get { return new KeyboardInputState(this.Message->LParam.ToSafeUInt32()); }
            set { this.Message->LParam = new IntPtr(unchecked((int) value.Value)); }
        }

        public unsafe bool IsDeadChar
        {
            get
            {
                var id = this.Message->Id;
                return (id == WM.DEADCHAR) || (id == WM.SYSDEADCHAR);
            }
            set
            {
                this.Message->Id = value
                    ? (this.IsSystemContext ? WM.SYSDEADCHAR : WM.DEADCHAR)
                    : (this.IsSystemContext ? WM.SYSCHAR : WM.CHAR);
            }
        }

        public unsafe bool IsSystemContext
        {
            get
            {
                var id = this.Message->Id;
                return (id == WM.SYSCHAR) || (id == WM.SYSDEADCHAR);
            }
            set
            {
                this.Message->Id = value
                    ? (this.IsDeadChar ? WM.SYSDEADCHAR : WM.SYSCHAR)
                    : (this.IsDeadChar ? WM.DEADCHAR : WM.CHAR);
            }
        }
    }

    public struct KeyPacket
    {
        public unsafe WindowMessage* Message;

        public unsafe KeyPacket(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe IntPtr Hwnd { get { return this.Message->Hwnd; } set { this.Message->Hwnd = value; } }

        public unsafe VirtualKey Key
        {
            get { return (VirtualKey) this.Message->WParam.ToSafeInt32(); }
            set { this.Message->WParam = new IntPtr((int) value); }
        }

        public unsafe KeyboardInputState InputState
        {
            get { return new KeyboardInputState(this.Message->LParam.ToSafeUInt32()); }
            set { this.Message->LParam = new IntPtr(unchecked((int) value.Value)); }
        }

        public unsafe bool IsKeyDown
        {
            get
            {
                var id = this.Message->Id;
                return (id == WM.KEYDOWN) || (id == WM.SYSKEYDOWN);
            }
            set
            {
                this.Message->Id = value
                    ? (this.IsSystemContext ? WM.SYSKEYDOWN : WM.KEYDOWN)
                    : (this.IsSystemContext ? WM.SYSKEYUP : WM.KEYUP);
            }
        }

        public unsafe bool IsSystemContext
        {
            get
            {
                var id = this.Message->Id;
                return (id == WM.SYSKEYDOWN) || (id == WM.SYSKEYUP);
            }
            set
            {
                this.Message->Id = value
                    ? (this.IsKeyDown ? WM.SYSKEYDOWN : WM.SYSKEYUP)
                    : (this.IsKeyDown ? WM.KEYDOWN : WM.KEYUP);
            }
        }
    }

    public struct MousePacket
    {
        public unsafe WindowMessage* Message;

        public unsafe MousePacket(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe IntPtr Hwnd { get { return this.Message->Hwnd; } set { this.Message->Hwnd = value; } }

        public unsafe Point Point
        {
            get
            {
                Point point;
                this.Message->LParam.BreakSafeInt32To16Signed(out point.Y, out point.X);
                return point;
            }

            set { this.Message->LParam = new IntPtr(value.ToInt32()); }
        }

        public unsafe MouseInputKeyStateFlags InputState
        {
            get { return (MouseInputKeyStateFlags) this.GetWParamAsInt(); }
            set { this.Message->WParam = new IntPtr((int) value); }
        }

        private unsafe int GetWParamAsInt()
        {
            return this.Message->WParam.ToSafeInt32();
        }
    }

    public struct MouseButtonPacket
    {
        public unsafe WindowMessage* Message;
        public unsafe IntPtr Hwnd { get { return this.Message->Hwnd; } set { this.Message->Hwnd = value; } }

        public unsafe MouseButtonPacket(WindowMessage* message)
        {
            this.Message = message;
        }

        private unsafe int GetWParamAsInt()
        {
            return this.Message->WParam.ToSafeInt32();
        }

        public unsafe Point Point
        {
            get
            {
                Point point;
                this.Message->LParam.BreakSafeInt32To16Signed(out point.Y, out point.X);
                return point;
            }

            set { this.Message->LParam = new IntPtr(value.ToInt32()); }
        }

        public MouseButton Button { get { return this.GetButton(); } set { this.SetButton(value); } }

        public unsafe bool IsButtonDown
        {
            get
            {
                var id = this.Message->Id;
                // Unfortunately, there's no better way than to do a full check here, since the numerical
                // values don't have any valid pattern to do it in one-go.
                return (id == WM.LBUTTONDOWN) || (id == WM.RBUTTONDOWN) || (id == WM.MBUTTONDOWN) ||
                       (id == WM.XBUTTONDOWN);
            }

            set
            {
                var button = this.GetButton();
                switch (button)
                {
                    case MouseButton.Left:
                        this.Message->Id = value ? WM.LBUTTONDOWN : WM.LBUTTONUP;
                        return;
                    case MouseButton.Right:
                        this.Message->Id = value ? WM.RBUTTONDOWN : WM.RBUTTONUP;
                        return;
                    case MouseButton.Middle:
                        this.Message->Id = value ? WM.MBUTTONDOWN : WM.MBUTTONUP;
                        return;
                    default:
                        this.Message->Id = value ? WM.XBUTTONDOWN : WM.XBUTTONUP;
                        return;
                }
            }
        }

        public unsafe MouseInputKeyStateFlags InputState
        {
            get { return (MouseInputKeyStateFlags) this.GetWParamAsInt().LowAsInt(); }
            set { this.Message->WParam = new IntPtr(this.GetWParamAsInt().WithLow(unchecked((short) value))); }
        }

        public unsafe MouseButtonResult Result
        {
            get { return (MouseButtonResult) this.Message->Result; }
            set { this.Message->Result = new IntPtr((int) value); }
        }

        private unsafe void SetButton(MouseButton value)
        {
            switch (value)
            {
                case MouseButton.Left:
                    this.Message->Id = this.IsButtonDown ? WM.LBUTTONDOWN : WM.LBUTTONUP;
                    return;
                case MouseButton.Right:
                    this.Message->Id = this.IsButtonDown ? WM.RBUTTONDOWN : WM.RBUTTONUP;
                    return;
                case MouseButton.Middle:
                    this.Message->Id = this.IsButtonDown ? WM.MBUTTONDOWN : WM.MBUTTONUP;
                    return;
                default:
                    this.Message->Id = this.IsButtonDown ? WM.XBUTTONDOWN : WM.XBUTTONUP;
                    this.Message->WParam = new IntPtr(this.GetWParamAsInt().WithHigh(unchecked((short) value)));
                    return;
            }
        }

        private unsafe MouseButton GetButton()
        {
            var id = (int) this.Message->Id;
            // Unfortunately, there's no better way than to do a full check here, since the numerical
            // values don't have any valid pattern to do it in one-go.
            if((id > 0x200) && (id < 0x204)) { return MouseButton.Left; }
            if((id > 0x203) && (id < 0x207)) { return MouseButton.Right; }
            if((id > 0x206) && (id <= 0x209)) { return MouseButton.Middle; }
            return (MouseInputXButtonFlag) this.GetWParamAsInt().HighAsInt() == MouseInputXButtonFlag.XBUTTON1
                ? MouseButton.XButton1
                : MouseButton.XButton2;
        }
    }

    public struct MouseDoubleClickPacket
    {
        public unsafe WindowMessage* Message;

        public unsafe MouseDoubleClickPacket(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe IntPtr Hwnd { get { return this.Message->Hwnd; } set { this.Message->Hwnd = value; } }

        private unsafe int GetWParamAsInt()
        {
            return this.Message->WParam.ToSafeInt32();
        }

        public unsafe Point Point
        {
            get
            {
                Point point;
                this.Message->LParam.BreakSafeInt32To16Signed(out point.Y, out point.X);
                return point;
            }

            set { this.Message->LParam = new IntPtr(value.ToInt32()); }
        }

        public MouseButton Button { get { return this.GetButton(); } set { this.SetButton(value); } }

        public unsafe MouseInputKeyStateFlags InputState
        {
            get { return (MouseInputKeyStateFlags) this.GetWParamAsInt().LowAsInt(); }
            set { this.Message->WParam = new IntPtr(this.GetWParamAsInt().WithLow(unchecked((short) value))); }
        }

        public unsafe MouseButtonResult Result
        {
            get { return (MouseButtonResult) this.Message->Result; }
            set { this.Message->Result = new IntPtr((int) value); }
        }

        private unsafe void SetButton(MouseButton value)
        {
            switch (value)
            {
                case MouseButton.Left:
                    this.Message->Id = WM.LBUTTONDBLCLK;
                    return;
                case MouseButton.Right:
                    this.Message->Id = WM.RBUTTONDBLCLK;
                    return;
                case MouseButton.Middle:
                    this.Message->Id = WM.MBUTTONDBLCLK;
                    return;
                default:
                    this.Message->Id = WM.XBUTTONDBLCLK;
                    this.Message->WParam = new IntPtr(this.GetWParamAsInt().WithHigh(unchecked((short) value)));
                    return;
            }
        }

        private unsafe MouseButton GetButton()
        {
            var id = this.Message->Id;
            switch (id)
            {
                case WM.LBUTTONDBLCLK:
                    return MouseButton.Left;
                case WM.RBUTTONDBLCLK:
                    return MouseButton.Right;
                case WM.MBUTTONDBLCLK:
                    return MouseButton.Middle;
                default:
                {
                    return (MouseInputXButtonFlag) this.GetWParamAsInt().HighAsInt()
                           == MouseInputXButtonFlag.XBUTTON1
                        ? MouseButton.XButton1
                        : MouseButton.XButton2;
                }
            }
        }
    }

    public struct MouseWheelPacket
    {
        public unsafe WindowMessage* Message;

        public unsafe MouseWheelPacket(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe IntPtr Hwnd { get { return this.Message->Hwnd; } set { this.Message->Hwnd = value; } }

        private unsafe int GetWParamAsInt()
        {
            return this.Message->WParam.ToSafeInt32();
        }

        public unsafe Point Point
        {
            get
            {
                Point point;
                this.Message->LParam.BreakSafeInt32To16Signed(out point.Y, out point.X);
                return point;
            }

            set { this.Message->LParam = new IntPtr(value.ToInt32()); }
        }

        public unsafe MouseInputKeyStateFlags InputState
        {
            get { return (MouseInputKeyStateFlags) this.GetWParamAsInt().LowAsInt(); }
            set { this.Message->WParam = new IntPtr(this.GetWParamAsInt().WithLow(unchecked((short) value))); }
        }

        // Multiple or divisons of (WHEEL_DELTA = 120)
        public unsafe short WheelDelta
        {
            get { return this.GetWParamAsInt().High(); }
            set { this.Message->WParam = new IntPtr(this.GetWParamAsInt().WithHigh(value)); }
        }

        public unsafe bool IsVertical
        {
            get { return this.Message->Id == WM.MOUSEWHEEL; }
            set { this.Message->Id = value ? WM.MOUSEWHEEL : WM.MOUSEHWHEEL; }
        }
    }

    public struct MouseActivatePacket
    {
        public unsafe WindowMessage* Message;

        public unsafe MouseActivatePacket(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe IntPtr Hwnd { get { return this.Message->Hwnd; } set { this.Message->Hwnd = value; } }

        private unsafe int GetLParamAsInt()
        {
            return this.Message->LParam.ToSafeInt32();
        }

        public unsafe IntPtr TopLevelActiveParentHwnd
        {
            get { return this.Message->WParam; }
            set { this.Message->WParam = value; }
        }

        public unsafe HitTestResult HitTestResult
        {
            get { return (HitTestResult) this.GetLParamAsInt().LowAsInt(); }
            set { this.Message->LParam = new IntPtr(this.GetLParamAsInt().WithLow(unchecked((short) value))); }
        }

        public unsafe ushort MouseMessageId
        {
            get { return (ushort) this.GetLParamAsInt().High(); }
            set { this.Message->LParam = new IntPtr(this.GetLParamAsInt().WithHigh(unchecked((short) value))); }
        }

        public unsafe MouseActivationResult Result
        {
            get { return (MouseActivationResult) this.Message->Result; }
            set { this.Message->Result = new IntPtr((int) value); }
        }
    }

    public struct DisplayChangePacket
    {
        public unsafe WindowMessage* Message;

        public unsafe DisplayChangePacket(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe IntPtr Hwnd { get { return this.Message->Hwnd; } set { this.Message->Hwnd = value; } }

        public unsafe uint ImageDepthAsBitsPerPixel
        {
            get { return (uint) this.Message->WParam.ToPointer(); }
            set { this.Message->WParam = new IntPtr(unchecked((int) value)); }
        }

        public unsafe Size Size
        {
            get
            {
                Size size;
                this.Message->LParam.BreakSafeInt32To16Signed(out size.Height, out size.Width);
                return size;
            }

            set { this.Message->LParam = new IntPtr(value.ToInt32()); }
        }
    }

    public struct CommandPacket
    {
        public unsafe WindowMessage* Message;

        public unsafe CommandPacket(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe IntPtr Hwnd { get { return this.Message->Hwnd; } set { this.Message->Hwnd = value; } }
        public unsafe IntPtr CommandHwnd { get { return this.Message->LParam; } set { this.Message->LParam = value; } }

        public unsafe short Id
        {
            get { return this.GetWParamAsInt().Low(); }
            set { this.Message->WParam = new IntPtr(this.GetWParamAsInt().WithLow(value)); }
        }

        public unsafe CommandSource CommandSource
        {
            get { return (CommandSource) this.GetWParamAsInt().HighAsInt(); }
            set { this.Message->WParam = new IntPtr(this.GetWParamAsInt().WithHigh(unchecked((short) value))); }
        }

        private unsafe int GetWParamAsInt()
        {
            return this.Message->WParam.ToSafeInt32();
        }
    }

    public struct SysCommandPacket
    {
        public unsafe WindowMessage* Message;

        public unsafe SysCommandPacket(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe IntPtr Hwnd { get { return this.Message->Hwnd; } set { this.Message->Hwnd = value; } }

        public unsafe short X
        {
            get { return this.GetLParamAsInt().Low(); }
            set { this.Message->LParam = new IntPtr(this.GetLParamAsInt().WithLow(value)); }
        }

        public unsafe short Y
        {
            get { return this.GetLParamAsInt().High(); }
            set { this.Message->LParam = new IntPtr(this.GetLParamAsInt().WithHigh(value)); }
        }

        public unsafe SysCommand Command
        {
            get { return (SysCommand) this.Message->WParam.ToSafeInt32(); }
            set { this.Message->WParam = new IntPtr((int) value); }
        }

        public bool IsAccelerator
        {
            get { return this.Y == -1; }
            set
            {
                if (value) this.Y = -1;
            }
        }

        public unsafe bool IsMnemonic
        {
            get { return this.GetLParamAsInt() == 0; }
            set
            {
                if (value) this.Message->LParam = IntPtr.Zero;
            }
        }

        private unsafe int GetLParamAsInt()
        {
            return this.Message->LParam.ToSafeInt32();
        }
    }

    public struct MenuCommandPacket
    {
        public unsafe WindowMessage* Message;

        public unsafe MenuCommandPacket(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe IntPtr Hwnd { get { return this.Message->Hwnd; } set { this.Message->Hwnd = value; } }

        public unsafe int MenuIndex
        {
            get { return this.Message->WParam.ToSafeInt32(); }
            set { this.Message->WParam = new IntPtr(value); }
        }

        public unsafe IntPtr MenuHandle { get { return this.Message->LParam; } set { this.Message->LParam = value; } }
    }

    public struct AppCommandPacket
    {
        public unsafe WindowMessage* Message;

        public unsafe AppCommandPacket(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe IntPtr Hwnd { get { return this.Message->Hwnd; } set { this.Message->Hwnd = value; } }

        public unsafe IntPtr CommandHwnd { get { return this.Message->WParam; } set { this.Message->WParam = value; } }

        public unsafe KeyboardInputState InputState
        {
            get { return new KeyboardInputState((uint) this.GetLParamAsInt().LowAsInt()); }
            set { this.Message->LParam = new IntPtr(this.GetLParamAsInt().WithLow(unchecked((short) value.Value))); }
        }

        public unsafe AppCommand Command
        {
            //GET_APPCOMMAND_LPARAM(lParam) ((short)(HIWORD(lParam) & ~FAPPCOMMAND_MASK))
            get { return (AppCommand) (this.GetLParamAsInt().HighAsInt() & ~(uint) AppCommandDevice.FAPPCOMMAND_MASK); }
            set
            {
                var high16 = this.GetLParamAsInt().High();
                var mask = ~(uint) AppCommandDevice.FAPPCOMMAND_MASK;
                var final = (high16 & ~mask) | ((int) value & mask);
                this.Message->LParam = new IntPtr(unchecked((int) final));
            }
        }

        public unsafe AppCommandDevice Device
        {
            //GET_DEVICE_LPARAM(lParam)     ((WORD)(HIWORD(lParam) & FAPPCOMMAND_MASK))
            get
            {
                return (AppCommandDevice) (this.GetLParamAsInt().HighAsInt() & (uint) AppCommandDevice.FAPPCOMMAND_MASK);
            }
            set
            {
                var high16 = this.GetLParamAsInt().High();
                var mask = (uint) AppCommandDevice.FAPPCOMMAND_MASK;
                var final = (high16 & ~mask) | ((int) value & mask);
                this.Message->LParam = new IntPtr(unchecked((int) final));
            }
        }

        public unsafe AppCommandResult Result
        {
            get { return (AppCommandResult) this.Message->Result; }
            set { this.Message->Result = new IntPtr((int) value); }
        }

        private unsafe int GetLParamAsInt()
        {
            return this.Message->LParam.ToSafeInt32();
        }
    }

    public struct CaptureChangedPacket
    {
        public unsafe WindowMessage* Message;

        public unsafe CaptureChangedPacket(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe IntPtr Hwnd { get { return this.Message->Hwnd; } set { this.Message->Hwnd = value; } }

        public unsafe IntPtr CapturingHwnd
        {
            get { return this.Message->LParam; }
            set { this.Message->LParam = value; }
        }
    }

    public struct FocusPacket
    {
        public unsafe WindowMessage* Message;

        public unsafe FocusPacket(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe IntPtr Hwnd { get { return this.Message->Hwnd; } set { this.Message->Hwnd = value; } }
        public unsafe IntPtr ConverseHwnd { get { return this.Message->WParam; } set { this.Message->WParam = value; } }
    }

    public struct HotKeyPacket
    {
        public unsafe WindowMessage* Message;

        public unsafe HotKeyPacket(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe IntPtr Hwnd { get { return this.Message->Hwnd; } set { this.Message->Hwnd = value; } }

        private unsafe int GetLParamAsInt()
        {
            return this.Message->LParam.ToSafeInt32();
        }

        public unsafe ScreenshotHotKey ScreenshotHotKey
        {
            get { return (ScreenshotHotKey) this.Message->WParam.ToSafeInt32(); }
            set { this.Message->WParam = new IntPtr((int) value); }
        }

        public unsafe HotKeyInputState KeyState
        {
            get { return (HotKeyInputState) this.GetLParamAsInt().Low(); }
            set { this.Message->LParam = new IntPtr(this.GetLParamAsInt().WithLow(unchecked((short) value))); }
        }

        public unsafe VirtualKey Key
        {
            get { return (VirtualKey) this.GetLParamAsInt().High(); }
            set { this.Message->LParam = new IntPtr(this.GetLParamAsInt().WithHigh(unchecked((short) value))); }
        }
    }

    public struct NcHitTestPacket
    {
        public unsafe WindowMessage* Message;

        public unsafe NcHitTestPacket(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe IntPtr Hwnd { get { return this.Message->Hwnd; } set { this.Message->Hwnd = value; } }

        public unsafe Point Point
        {
            get
            {
                Point point;
                this.Message->LParam.BreakSafeInt32To16Signed(out point.Y, out point.X);
                return point;
            }

            set { this.Message->LParam = new IntPtr(value.ToInt32()); }
        }

        public unsafe HitTestResult Result
        {
            get { return (HitTestResult) this.Message->Result; }
            set { this.Message->Result = new IntPtr((int) value); }
        }
    }

    public struct NcPaintPacket
    {
        public unsafe WindowMessage* Message;

        public unsafe NcPaintPacket(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe IntPtr Hwnd { get { return this.Message->Hwnd; } set { this.Message->Hwnd = value; } }
        public unsafe IntPtr UpdateRegion { get { return this.Message->WParam; } set { this.Message->WParam = value; } }
    }

    public struct NcMouseMovePacket
    {
        public unsafe WindowMessage* Message;

        public unsafe NcMouseMovePacket(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe IntPtr Hwnd { get { return this.Message->Hwnd; } set { this.Message->Hwnd = value; } }

        public unsafe HitTestResult HitTestValue
        {
            get { return (HitTestResult) this.Message->WParam; }
            set { this.Message->WParam = new IntPtr((int) value); }
        }

        public unsafe Point Point
        {
            get
            {
                Point point;
                this.Message->LParam.BreakSafeInt32To16Signed(out point.Y, out point.X);
                return point;
            }

            set { this.Message->LParam = new IntPtr(value.ToInt32()); }
        }
    }

    public struct NcActivatePacket
    {
        public unsafe WindowMessage* Message;

        public unsafe NcActivatePacket(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe IntPtr Hwnd { get { return this.Message->Hwnd; } set { this.Message->Hwnd = value; } }

        public unsafe bool IsActive
        {
            get { return this.Message->WParam.ToSafeInt32() > 0; }
            set { this.Message->WParam = (IntPtr) (value ? 1 : 0); }
        }

        // lParam is used only if visual styles are disabled
        public unsafe IntPtr UpdateRegion { get { return this.Message->LParam; } set { this.Message->LParam = value; } }

        public void PreventRegionUpdate()
        {
            // To prevent Nc region update in DefWndProc, set (updateRegion)LParam = -1;
            this.UpdateRegion = new IntPtr(-1);
        }

        // When wParam == TRUE, result is ignored
        public bool CanPreventDefault => !this.IsActive;

        public unsafe NcActivateResult Result
        {
            get { return (NcActivateResult) this.Message->Result.ToSafeInt32(); }
            set { this.Message->Result = new IntPtr((int) value); }
        }
    }

    public struct NcCalcSizePacket
    {
        public unsafe WindowMessage* Message;

        public unsafe NcCalcSizePacket(WindowMessage* message)
        {
            this.Message = message;
        }

        public unsafe IntPtr Hwnd { get { return this.Message->Hwnd; } set { this.Message->Hwnd = value; } }

        public unsafe bool ShouldCalcValidRects
        {
            get { return this.Message->WParam.ToSafeInt32() > 0; }
            set { this.Message->WParam = (IntPtr) (value ? 1 : 0); }
        }

        public unsafe ref NcCalcSizeParams Params => ref ((NcCalcSizeParamsWrapper*)this.Message->LParam)->Value;


        public unsafe WindowViewRegionFlags Result
        {
            get { return (WindowViewRegionFlags) this.Message->Result.ToSafeInt32(); }
            set { this.Message->Result = new IntPtr((int) value); }
        }
    }

    #region Support Types

    public enum MouseButton
    {
        Left = 0x1,
        Right = 0x2,
        Middle = 0x4,
        Other = 0x8,
        XButton1 = 0x10 | Other,
        XButton2 = 0x20 | Other
    }

    public enum MouseButtonResult
    {
        Default = 0,
        // Required to be set when application that processes XButton in order help simulation softwares
        // determine if the result was processed by application or DefWndProc
        Handled = 1
    }

    public enum EraseBackgroundResult
    {
        // 1 - prevent default erase.
        // 0 - Let DefWndProc erase the background with the window class's brush.
        Default = 0,
        DisableDefaultErase = 1
    }

    public enum CreateWindowResult
    {
        Default = 0,
        PreventCreation = -1
    }

    public enum AppCommandResult
    {
        Default = 0,
        Handled = 1
    }

    public enum NcActivateResult
    {
        // var result = TRUE // Default processing;
        // var result = FALSE // Prevent changes.
        Default = 1,
        PreventDefault = 0
    }

    #endregion

    #region Ref-Wrapper 

    [StructLayout(LayoutKind.Sequential)]
    internal struct WindowMessageWrapper
    {
        public WindowMessage Value;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct WindowPositionWrapper
    {
        public WindowPosition Value;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct CreateStructWrapper
    {
        public CreateStruct Value;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct MinMaxInfoWrapper
    {
        public MinMaxInfo Value;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct NcCalcSizeParamsWrapper
    {
        public NcCalcSizeParams Value;
    }

    #endregion
}
