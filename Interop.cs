using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

// ReSharper disable InconsistentNaming
namespace WinApi
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Message
    {
        public IntPtr WindowHandle;
        public uint Value;
        public UIntPtr WParam;
        public UIntPtr LParam;
        public uint Time;
        public Point Point;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Point
    {
        public int X, Y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Margin
    {
        public int Left, Right, Top, Bottom;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Rectangle
    {
        public int Left, Top, Right, Bottom;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PaintStruct
    {
        public IntPtr HandleDC;
        public bool ShouldEraseBackground;
        public Rectangle PaintRectangle;
        public bool ReservedInternalRestore;
        public bool ReservedInternalIncUpdate;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] ReservedInternalRgb;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct WindowClass
    {
        public WindowClassStyle Style;
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public WindowProc WindowProc;
        public int ClassExtraBytes;
        public int WindowExtraBytes;
        public IntPtr InstanceHandle;
        public IntPtr IconHandle;
        public IntPtr CursorHandle;
        public IntPtr BackgroundBrushHandle;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string MenuName;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string ClassName;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct WindowClassEx
    {
        public int Size;
        public WindowClassStyle Style;
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public WindowProc WindowProc;
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
            obj.Size = Marshal.SizeOf<WindowClassEx>();
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct WindowThemeAttributeOptions
    {
        public WindowThemeNCAttribute Flags;
        public WindowThemeNCAttribute Mask;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct WindowInfo
    {
        public uint Size;
        public Rectangle WindowRectangle;
        public Rectangle ClientRectangle;
        public uint Style;
        public uint ExStyle;
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
    public struct WindowPlacement
    {
        public int Length;
        public WindowPlacementFlag Flags;
        public ShowWindowCommand ShowCmd;
        public Point MinPosition;
        public Point MaxPosition;
        public Rectangle NormalPosition;

        public static void Initialize(ref WindowPlacement obj)
        {
            obj.Length = (int)Marshal.SizeOf<WindowPlacement>();
        }
    }

    public delegate IntPtr WindowProc(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam);
    public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

    [SuppressUnmanagedCodeSecurity]
    public static partial class NativeMethods
    {
        [DllImport("user32.dll")]
        public static extern IntPtr DispatchMessage([In] ref Message lpMsg);

        [DllImport("user32.dll")]
        public static extern bool TranslateMessage([In] ref Message lpMsg);

        [DllImport("user32.dll")]
        public static extern int GetMessage(out Message lpMsg, IntPtr hwnd, uint wMsgFilterMin,
            uint wMsgFilterMax);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int AnimateWindow(IntPtr hwnd, int dwTime, AnimateWindowFlag dwFlags);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hwnd, ShowWindowCommand nCmdShow);

        [DllImport("user32.dll")]
        public static extern bool UpdateWindow(IntPtr hwnd);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr CreateWindowEx(
            WindowExStyle dwExStyle,
            string lpClassName,
            string lpWindowName,
            WindowStyle dwStyle,
            int x,
            int y,
            int nWidth,
            int nHeight,
            IntPtr hwndParent,
            IntPtr hMenu,
            IntPtr hInstance,
            IntPtr lpParam);

        [DllImport("user32.dll", EntryPoint = "GetWindowLong", SetLastError = true)]
        private static extern IntPtr GetWindowLongPtr32(IntPtr hwnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr", SetLastError = true)]
        private static extern IntPtr GetWindowLongPtr64(IntPtr hwnd, int nIndex);

        // This static method is required because Win32 does not support
        // GetWindowLongPtr directly
        public static IntPtr GetWindowLongPtr(IntPtr hwnd, int nIndex)
        {
            return IntPtr.Size == 8 ? GetWindowLongPtr64(hwnd, nIndex) : GetWindowLongPtr32(hwnd, nIndex);
        }

        // This static method is required because Win32 does not support
        // GetWindowLongPtr directly
        public static IntPtr SetWindowLongPtr(IntPtr hwnd, int nIndex, IntPtr dwNewLong)
        {
            return IntPtr.Size == 8
                ? SetWindowLongPtr64(hwnd, nIndex, dwNewLong)
                : new IntPtr(SetWindowLong(hwnd, nIndex, dwNewLong.ToInt32()));
        }

        [DllImport("user32.dll", EntryPoint = "SetWindowLong", SetLastError = true)]
        private static extern int SetWindowLong(IntPtr hwnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr", SetLastError = true)]
        private static extern IntPtr SetWindowLongPtr64(IntPtr hwnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern ushort RegisterClassEx([In] ref WindowClassEx lpwcx);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool UnregisterClass(string lpClassName, IntPtr hInstance);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetWindowRect(IntPtr hwnd, out Rectangle lpRect);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetClientRect(IntPtr hwnd, out Rectangle lpRect);

        [DllImport("user32.dll")]
        public static extern void PostQuitMessage(int nExitCode);

        [DllImport("user32.dll")]
        public static extern IntPtr DefWindowProc(IntPtr hwnd, uint uMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr LoadIcon(IntPtr hInstance, string lpIconName);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr LoadIcon(IntPtr hInstance, IntPtr lpIconResource);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr LoadCursor(IntPtr hInstance, string lpCursorName);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr LoadCursor(IntPtr hInstance, IntPtr lpCursorResource);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr LoadImage(IntPtr hInstance, string lpszName, uint uType,
            int cxDesired, int cyDesired, uint fuLoad);

        [DllImport("user32.dll")]
        public static extern IntPtr BeginPaint(IntPtr hwnd, out PaintStruct lpPaint);

        [DllImport("user32.dll")]
        public static extern bool EndPaint(IntPtr hwnd, [In] ref PaintStruct lpPaint);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int DrawText(IntPtr hdc, string lpString, int nCount, ref Rectangle lpRect, uint uFormat);

        [DllImport("kernel32.dll")]
        public static extern uint GetLastError();

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CloseHandle(IntPtr hObject);

        [DllImport("gdi32.dll")]
        public static extern IntPtr GetStockObject(int fnObject);

        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, DwmWindowAttribute attr, [In] ref int attrValue,
            int attrSize);

        [DllImport("dwmapi.dll")]
        public static extern void DwmIsCompositionEnabled(out bool pfEnabled);

        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hwnd, [In] ref Margin margin);

        [DllImport("uxtheme.dll")]
        public static extern int SetWindowThemeAttribute(
            IntPtr hwnd,
            WindowThemeAttributeType eAttributeType,
            [In] ref WindowThemeNCAttribute pvAttribute,
            int size);
    }
}
