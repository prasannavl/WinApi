using System;
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
            Value = value;
        }

        /// <summary>
        ///     The repeat count for the current message. The value is the number of times the keystroke is autorepeated as a
        ///     result of the user holding down the key. If the keystroke is held long enough, multiple messages are sent. However,
        ///     the repeat count is not cumulative.
        /// </summary>
        public int RepeatCount => unchecked((int) Value & 0x000000ff);

        public int ScanCode => unchecked(((int) Value >> 16) & 0x0000000f);

        /// <summary>
        ///     Indicates whether the key is an extended key, such as the right-hand ALT and CTRL keys that appear on an enhanced
        ///     101- or 102-key keyboard. The value is 1 if it is an extended key; otherwise, it is 0.
        /// </summary>
        public bool IsExtendedKey => unchecked(((int) Value >> 24) & 0x1) == 1;

        /// <summary>
        ///     The value is 1 if the ALT key is down while the key is pressed; it is 0 if the WM_SYSKEYDOWN message is posted to
        ///     the active window because no window has the keyboard focus.
        /// </summary>
        public bool IsContextual => unchecked(((int) Value >> 29) & 0x1) == 1;

        /// <summary>
        ///     The value is 1 if the key is down before the message is sent, or it is 0 if the key is up.
        /// </summary>
        public bool IsPreviousKeyStatePressed => unchecked(((int) Value >> 30) & 0x1) == 1;

        /// <summary>
        ///     The value is 1 if the key is being released, or it is 0 if the key is being pressed.
        /// </summary>
        public bool IsKeyUpTransition => unchecked(((int) Value >> 31) & 0x1) == 1;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct HardwareInput
    {
        public uint Message;
        public ushort Low;
        public ushort High;

        public uint WParam
        {
            get { return ((uint) High << 16) | Low; }
            set
            {
                Low = (ushort) value;
                High = (ushort) (value >> 16);
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
        public ushort KeyCode;
        public uint ScanCode;
        public KeyboardInputFlags Flags;
        public uint Time;
        public IntPtr ExtraInfo;

        public VirtualKey VKey
        {
            get { return (VirtualKey) KeyCode; }
            set { KeyCode = (ushort) value; }
        }
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct Input
    {
        [FieldOffset(0)] public InputType Type;
        [FieldOffset(4)] public MouseInput MouseInput;
        [FieldOffset(4)] public KeyboardInput KeyboardInput;
        [FieldOffset(4)] public HardwareInput HardwareInput;

        public static void InitializeHardwareInput(out Input input, uint message, ushort low, ushort high)
        {
            input = new Input
            {
                Type = InputType.INPUT_HARDWARE,
                HardwareInput =
                {
                    Message = message,
                    Low = low,
                    High = high
                }
            };
        }

        public static void InitializeHardwareInput(out Input input, uint message, uint wParam)
        {
            input = new Input
            {
                Type = InputType.INPUT_HARDWARE,
                HardwareInput =
                {
                    Message = message,
                    WParam = wParam
                }
            };
        }

        public static void InitializeKeyboardInput(out Input input, ushort scanCode, KeyEvent keyEvent,
            bool isExtendedKey = false, uint? timestampMillis = null)
        {
            input = new Input
            {
                Type = InputType.INPUT_KEYBOARD,
                KeyboardInput =
                {
                    Time = timestampMillis ?? (uint) DateTime.Now.Millisecond,
                    Flags = KeyboardInputFlags.KEYEVENTF_SCANCODE,
                    ScanCode = scanCode
                }
            };
            if (keyEvent == KeyEvent.Up)
                input.KeyboardInput.Flags |= KeyboardInputFlags.KEYEVENTF_KEYUP;
            if (isExtendedKey)
                input.KeyboardInput.Flags |= KeyboardInputFlags.KEYEVENTF_EXTENDEDKEY;
        }

        public static void InitializeKeyboardInput(out Input input, char charCode, uint? timestampMillis = null)
        {
            input = new Input
            {
                Type = InputType.INPUT_KEYBOARD,
                KeyboardInput =
                {
                    Time = timestampMillis ?? (uint) DateTime.Now.Millisecond,
                    Flags = KeyboardInputFlags.KEYEVENTF_UNICODE | KeyboardInputFlags.KEYEVENTF_KEYUP,
                    ScanCode = charCode
                }
            };
        }

        public static void InitializeKeyboardInput(out Input input, VirtualKey key, KeyEvent keyEvent,
            uint? timestampMillis = null)
        {
            input = new Input
            {
                Type = InputType.INPUT_KEYBOARD,
                KeyboardInput =
                {
                    Time = timestampMillis ?? (uint) DateTime.Now.Millisecond,
                    VKey = key
                }
            };
            if (keyEvent == KeyEvent.Up)
                input.KeyboardInput.Flags |= KeyboardInputFlags.KEYEVENTF_KEYUP;
        }

        public static void InitializeMouseInput(out Input input, int x, int y, MouseInputFlags flags, uint data = 0,
            uint? timestampMillis = null)
        {
            input = new Input
            {
                Type = InputType.INPUT_KEYBOARD,
                MouseInput =
                {
                    Time = timestampMillis ?? (uint) DateTime.Now.Millisecond,
                    X = x,
                    Y = y,
                    Data = data,
                    Flags = flags
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
            Value = value;
        }

        public bool IsPressed
        {
            // Note: The boolean check is performed on int, not short.
            get { return (Value & 0x8000) > 0; }
            set
            {
                if (value)
                    Value = unchecked ((short) (Value | 0x8000));
                else
                    Value = unchecked ((short) (Value & 0x7fff));
            }
        }

        public bool IsToggled
        {
            get { return (Value & 0x1) == 1; }
            set
            {
                if (value)
                    Value = unchecked ((short) (Value | 0x1));
                else
                    Value = unchecked ((short) (Value & 0xfffe));
            }
        }
    }
}