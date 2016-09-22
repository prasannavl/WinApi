using System;
using System.Runtime.InteropServices;
using WinApi.Core;

namespace WinApi.User32
{
    public delegate IntPtr WindowProc(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam);

    public delegate int EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

    public delegate IntPtr GetMsgProc(int code, IntPtr wParam, IntPtr lParam);

    [StructLayout(LayoutKind.Sequential)]
    public struct Message
    {
        public IntPtr WindowHandle;
        public uint Value;
        public IntPtr WParam;
        public IntPtr LParam;
        public uint Time;
        public Point Point;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Rectangle
    {
        public Rectangle(int left = 0, int top = 0, int right = 0, int bottom = 0)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public int Left, Top, Right, Bottom;

        public int Width
        {
            get { return Right - Left; }
            set { Right = Left + value; }
        }

        public int Height
        {
            get { return Bottom - Top; }
            set { Bottom = Top + value; }
        }

        public Size GetSize()
        {
            return new Size(Width, Height);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PaintStruct
    {
        public IntPtr HandleDC;
        public int EraseBackgroundValue;
        public Rectangle PaintRectangle;
        public int ReservedInternalRestore;
        public int ReservedInternalIncUpdate;
        public fixed byte ReservedInternalRgb [32];

        public bool ShouldEraseBackground
        {
            get { return EraseBackgroundValue > 0; }
            set { EraseBackgroundValue = value ? 1 : 0; }
        }
    }

    /// <summary>
    ///     Note: Marshalled
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct WindowClass
    {
        public WindowClassStyles Styles;
        [MarshalAs(UnmanagedType.FunctionPtr)] public WindowProc WindowProc;
        public int ClassExtraBytes;
        public int WindowExtraBytes;
        public IntPtr InstanceHandle;
        public IntPtr IconHandle;
        public IntPtr CursorHandle;
        public IntPtr BackgroundBrushHandle;
        public string MenuName;
        public string ClassName;
    }

    /// <summary>
    ///     Note: Marshalled
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct WindowClassEx
    {
        public uint Size;
        public WindowClassStyles Styles;
        [MarshalAs(UnmanagedType.FunctionPtr)] public WindowProc WindowProc;
        public int ClassExtraBytes;
        public int WindowExtraBytes;
        public IntPtr InstanceHandle;
        public IntPtr IconHandle;
        public IntPtr CursorHandle;
        public IntPtr BackgroundBrushHandle;
        public string MenuName;
        public string ClassName;
        public IntPtr SmallIconHandle;

        public static void Initialize(ref WindowClassEx obj)
        {
            obj.Size = (uint) Marshal.SizeOf<WindowClassEx>();
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct WindowInfo
    {
        public uint Size;
        public Rectangle WindowRectangle;
        public Rectangle ClientRectangle;
        public WindowStyles Styles;
        public WindowExStyles ExStyles;
        public uint WindowStatus;
        public uint BorderX;
        public uint BorderY;
        public ushort WindowType;
        public ushort CreatorVersion;

        public static void Initialize(ref WindowInfo obj)
        {
            obj.Size = (uint) Marshal.SizeOf<WindowInfo>();
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CreateStruct
    {
        public IntPtr lpCreateParams;
        public IntPtr hInstance;
        public IntPtr hMenu;
        public IntPtr hwndParent;
        public int cy;
        public int cx;
        public int y;
        public int x;
        public WindowStyles style;
        public IntPtr lpszName;
        public IntPtr lpszClass;
        public int dwExStyle;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct WindowPlacement
    {
        public int Length;
        public WindowPlacementFlags Flags;
        public ShowWindowCommands ShowCmd;
        public Point MinPosition;
        public Point MaxPosition;
        public Rectangle NormalPosition;

        public static void Initialize(ref WindowPlacement obj)
        {
            obj.Length = Marshal.SizeOf<WindowPlacement>();
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct BlendFunction
    {
        public byte BlendOp;
        public byte BlendFlags;
        public byte SourceConstantAlpha;
        public AlphaFormat AlphaFormat;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct AnimationInfo
    {
        /// <summary>
        ///     Creates an AMINMATIONINFO structure.
        /// </summary>
        /// <param name="iMinAnimate">If non-zero and SPI_SETANIMATION is specified, enables minimize/restore animation.</param>
        public AnimationInfo(int iMinAnimate)
        {
            Size = (uint) Marshal.SizeOf<AnimationInfo>();
            MinAnimate = iMinAnimate;
        }

        /// <summary>
        ///     Always must be set to (System.UInt32)Marshal.SizeOf(typeof(ANIMATIONINFO)).
        /// </summary>
        public uint Size;

        /// <summary>
        ///     If non-zero, minimize/restore animation is enabled, otherwise disabled.
        /// </summary>
        public int MinAnimate;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MinimizedMetrics
    {
        public uint Size;
        public int Width;
        public int HorizontalGap;
        public int VerticalGap;
        public ArrangeFlags Arrange;

        public static void Initialize(ref MinimizedMetrics obj)
        {
            obj.Size = (uint) Marshal.SizeOf<MinimizedMetrics>();
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TrackMouseEventOptions
    {
        public uint Size;
        public TrackMouseEventFlags Flags;
        public IntPtr TrackedWindowHandle;
        public uint HoverTime;

        public static void Initialize(ref TrackMouseEventOptions obj)
        {
            obj.Size = (uint) Marshal.SizeOf<TrackMouseEventOptions>();
        }

        public const uint DefaultHoverTime = 0xFFFFFFFF;
    }

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
        public int RepeatCount => unchecked((int) Value & 0x000000ff);

        public int ScanCode => unchecked (((int) Value >> 16) & 0x0000000f);

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
}