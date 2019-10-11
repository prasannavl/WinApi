using NetCoreEx.BinaryExtensions;
using NetCoreEx.Geometry;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WinApi.User32
{
    [StructLayout(LayoutKind.Sequential)]
    public struct KeyboardInputState
    {
        public uint Value;

        public KeyboardInputState(uint value)
        {
            this.Value = value;
        }

        /// <summary>
        ///     The repeat count for the current message. The value is the number of times the keystroke is autorepeated as a
        ///     result of the user holding down the key. If the keystroke is held long enough, multiple messages are sent. However,
        ///     the repeat count is not cumulative.
        /// </summary>
        public uint RepeatCount
        {
            get { return this.Value & 0x0000ffff; }
            set { this.Value = this.Value.WithLow(value.Low()); }
        }

        public uint ScanCode
        {
            get { return (this.Value >> 16) & 0x000000ff; }
            set
            {
                var mask = 0x00ff0000U;
                var newValue = (value << 16) & mask;
                this.Value = this.Value & ~mask | newValue;
            }
        }

        /// <summary>
        ///     Indicates whether the key is an extended key, such as the right-hand ALT and CTRL keys that appear on an enhanced
        ///     101- or 102-key keyboard. The value is 1 if it is an extended key; otherwise, it is 0.
        /// </summary>
        public bool IsExtendedKey
        {
            get { return unchecked(((int)this.Value >> 24) & 0x1) == 1; }
            set
            {
                var mask = 0b0000_0001_0000_0000_0000_0000_0000_0000U;
                this.Value = this.Value & ~mask | (value ? mask : 0U);
            }
        }

        /// <summary>
        ///     The value is 1 if the ALT key is down while the key is pressed; it is 0 if the WM_SYSKEYDOWN message is posted to
        ///     the active window because no window has the keyboard focus.
        /// </summary>
        public bool IsContextual
        {
            get { return unchecked(((int)this.Value >> 29) & 0x1) == 1; }
            set
            {
                var mask = 0b0010_0000_0000_0000_0000_0000_0000_0000U;
                this.Value = this.Value & ~mask | (value ? mask : 0U);
            }
        }

        /// <summary>
        ///     The value is 1 if the key is down before the message is sent, or it is 0 if the key is up.
        /// </summary>
        public bool IsPreviousKeyStatePressed
        {
            get { return unchecked(((int)this.Value >> 30) & 0x1) == 1; }
            set
            {
                var mask = 0b0100_0000_0000_0000_0000_0000_0000_0000U;
                this.Value = this.Value & ~mask | (value ? mask : 0U);
            }
        }

        /// <summary>
        ///     The value is 1 if the key is being released, or it is 0 if the key is being pressed.
        /// </summary>
        public bool IsKeyUpTransition
        {
            get { return unchecked(((int)this.Value >> 31) & 0x1) == 1; }
            set
            {
                var mask = 0b1000_0000_0000_0000_0000_0000_0000_0000U;
                this.Value = this.Value & ~mask | (value ? mask : 0U);
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct HardwareInput
    {
        public uint Message;
        public ushort Low;
        public ushort High;

        public uint WParam
        {
            get { return ((uint) this.High << 16) | this.Low; }
            set
            {
                this.Low = (ushort) value;
                this.High = (ushort) (value >> 16);
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MouseInput
    {
        public int X;
        public int Y;
        public uint Data;
        public MouseInputFlags Flags;
        public uint Time;
        public IntPtr ExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct KeyboardInput
    {
        public ushort VirtualKeyCode;
        public ushort ScanCode;
        public KeyboardInputFlags Flags;
        public uint Time;
        public IntPtr ExtraInfo;

        public VirtualKey Key
        {
            get { return (VirtualKey) this.VirtualKeyCode; }
            set { this.VirtualKeyCode = (ushort) value; }
        }
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct InputPacket
    {
        [FieldOffset(0)] public MouseInput MouseInput;
        [FieldOffset(0)] public KeyboardInput KeyboardInput;
        [FieldOffset(0)] public HardwareInput HardwareInput;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Input
    {
        public InputType Type;
        public InputPacket Packet;

        public static void InitHardwareInput(out Input input, uint message, ushort low, ushort high)
        {
            input = new Input
            {
                Type = InputType.INPUT_HARDWARE,
                Packet = new InputPacket
                {
                    HardwareInput = new HardwareInput
                    {
                        Message = message,
                        Low = low,
                        High = high
                    }
                }
            };
        }

        public static void InitHardwareInput(out Input input, uint message, uint wParam)
        {
            InitHardwareInput(out input, message, (ushort) wParam, (ushort) (wParam >> 16));
        }

        public static void InitKeyboardInput(out Input input, ushort scanCode, bool isKeyUp,
            bool isExtendedKey = false, uint timestampMillis = 0)
        {
            input = new Input
            {
                Type = InputType.INPUT_KEYBOARD,
                Packet = new InputPacket
                {
                    KeyboardInput =
                    {
                        Time = timestampMillis,
                        Flags = KeyboardInputFlags.KEYEVENTF_SCANCODE,
                        ScanCode = scanCode,
                        VirtualKeyCode = 0
                    }
                }
            };
            if (isKeyUp) input.Packet.KeyboardInput.Flags |= KeyboardInputFlags.KEYEVENTF_KEYUP;
            if (isExtendedKey) input.Packet.KeyboardInput.Flags |= KeyboardInputFlags.KEYEVENTF_EXTENDEDKEY;
        }

        public static void InitKeyboardInput(out Input input, char charCode, bool isKeyUp, uint timestampMillis = 0)
        {
            input = new Input
            {
                Type = InputType.INPUT_KEYBOARD,
                Packet = new InputPacket
                {
                    KeyboardInput =
                    {
                        Time = timestampMillis,
                        Flags = KeyboardInputFlags.KEYEVENTF_UNICODE,
                        ScanCode = charCode,
                        VirtualKeyCode = 0
                    }
                }
            };
            if (isKeyUp) input.Packet.KeyboardInput.Flags |= KeyboardInputFlags.KEYEVENTF_KEYUP;
        }

        public static void InitKeyboardInput(out Input input, VirtualKey key, bool isKeyUp,
            uint timestampMillis = 0)
        {
            input = new Input
            {
                Type = InputType.INPUT_KEYBOARD,
                Packet = new InputPacket
                {
                    KeyboardInput =
                    {
                        Time = timestampMillis,
                        Key = key,
                        ScanCode = 0,
                        Flags = 0
                    }
                }
            };
            if (isKeyUp) input.Packet.KeyboardInput.Flags |= KeyboardInputFlags.KEYEVENTF_KEYUP;
        }

        public static void InitMouseInput(out Input input, int x, int y, MouseInputFlags flags, uint data = 0,
            uint timestampMillis = 0)
        {
            input = new Input
            {
                Type = InputType.INPUT_MOUSE,
                Packet = new InputPacket
                {
                    MouseInput =
                    {
                        Time = timestampMillis,
                        X = x,
                        Y = y,
                        Data = data,
                        Flags = flags
                    }
                }
            };
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct LastInputInfo
    {
        public uint Size;
        public uint Time;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct KeyState
    {
        public short Value;

        public KeyState(short value)
        {
            this.Value = value;
        }

        public bool IsPressed
        {
            // Note: The boolean check is performed on int, not short.
            get { return (this.Value & 0x8000) > 0; }
            set
            {
                if (value) this.Value = unchecked ((short) (this.Value | 0x8000));
                else this.Value = unchecked ((short) (this.Value & 0x7fff));
            }
        }

        public bool IsToggled
        {
            get { return (this.Value & 0x1) == 1; }
            set
            {
                if (value) this.Value = unchecked ((short) (this.Value | 0x1));
                else this.Value = unchecked ((short) (this.Value & 0xfffe));
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CursorInfo
    {
        public uint Size;
        public CursorInfoFlags Flags;
        public IntPtr CursorHandle;
        public Point ScreenPosition;
    }
}