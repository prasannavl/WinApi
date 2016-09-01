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
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)] public byte[] ReservedInternalRgb;
    }

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
        [MarshalAs(UnmanagedType.LPTStr)] public string MenuName;
        [MarshalAs(UnmanagedType.LPTStr)] public string ClassName;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct WindowClassEx
    {
        public int Size;
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

    public delegate IntPtr WindowProc(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam);

    public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

    [SuppressUnmanagedCodeSecurity]
    public static partial class NativeMethods
    {
        /// <summary>
        ///     Dispatches a message to a window procedure. It is typically used to dispatch a message retrieved by the GetMessage
        ///     function.
        /// </summary>
        /// <param name="lpMsg">
        ///     [Type: const MSG*]
        ///     A pointer to a structure that contains the message.
        /// </param>
        /// <returns>
        ///     [Type: LRESULT]
        ///     The return value specifies the value returned by the window procedure.Although its meaning depends on the message
        ///     being dispatched, the return value generally is ignored.
        /// </returns>
        /// <remarks>
        ///     The MSG structure must contain valid message values. If the lpmsg parameter points to a WM_TIMER message and the
        ///     lParam parameter of the WM_TIMER message is not NULL, lParam points to a function that is called instead of the
        ///     window procedure.
        ///     Note that the application is responsible for retrieving and dispatching input messages to the dialog box.Most
        ///     applications use the main message loop for this. However, to permit the user to move to and to select controls by
        ///     using the keyboard, the application must call IsDialogMessage.For more information, see Dialog Box Keyboard
        ///     Interface.
        /// </remarks>
        [DllImport("user32.dll")]
        public static extern IntPtr DispatchMessage([In] ref Message lpMsg);

        /// <summary>
        ///     Translates virtual-key messages into character messages. The character messages are posted to the calling thread's
        ///     message queue, to be read the next time the thread calls the GetMessage or PeekMessage function.
        /// </summary>
        /// <param name="lpMsg">
        ///     [Type: const MSG*]
        ///     A pointer to an MSG structure that contains message information retrieved from the calling thread's message queue
        ///     by using the GetMessage or PeekMessage function.
        /// </param>
        /// <returns>
        ///     [Type: BOOL]
        ///     If the message is translated(that is, a character message is posted to the thread's message queue), the return
        ///     value is nonzero. If the message is WM_KEYDOWN, WM_KEYUP, WM_SYSKEYDOWN, or WM_SYSKEYUP, the return value is
        ///     nonzero, regardless of the translation. If the message is not translated (that is, a character message is not
        ///     posted to the thread's message queue), the return value is zero.
        /// </returns>
        /// <remarks>
        ///     The TranslateMessage function does not modify the message pointed to by the lpMsg parameter.
        ///     WM_KEYDOWN and WM_KEYUP combinations produce a WM_CHAR or WM_DEADCHAR message.WM_SYSKEYDOWN and WM_SYSKEYUP
        ///     combinations produce a WM_SYSCHAR or WM_SYSDEADCHAR message.
        ///     TranslateMessage produces WM_CHAR messages only for keys that are mapped to ASCII characters by the keyboard
        ///     driver.
        ///     If applications process virtual-key messages for some other purpose, they should not call TranslateMessage.For
        ///     instance, an application should not call TranslateMessage if the TranslateAccelerator function returns a nonzero
        ///     value.Note that the application is responsible for retrieving and dispatching input messages to the dialog box.Most
        ///     applications use the main message loop for this. However, to permit the user to move to and to select controls by
        ///     using the keyboard, the application must call IsDialogMessage.For more information, see Dialog Box Keyboard
        ///     Interface.
        /// </remarks>
        [DllImport("user32.dll")]
        public static extern bool TranslateMessage([In] ref Message lpMsg);

        /// <summary>
        ///     Retrieves a message from the calling thread's message queue. The function dispatches incoming sent messages until a
        ///     posted message is available for retrieval. Unlike GetMessage, the PeekMessage function does not wait for a message
        ///     to be posted before returning.
        /// </summary>
        /// <param name="lpMsg">
        ///     [Type: LPMSG]
        ///     A pointer to an MSG structure that receives message information from the thread's message queue.
        /// </param>
        /// <param name="hwnd">
        ///     [Type: HWND]
        ///     A handle to the window whose messages are to be retrieved.The window must belong to the current thread.
        ///     If hWnd is NULL, GetMessage retrieves messages for any window that belongs to the current thread, and any messages
        ///     on the current thread's message queue whose hwnd value is NULL (see the MSG structure). Therefore if hWnd is NULL,
        ///     both window messages and thread messages are processed.
        ///     If hWnd is -1, GetMessage retrieves only messages on the current thread's message queue whose hwnd value is NULL,
        ///     that is, thread messages as posted by PostMessage (when the hWnd parameter is NULL) or PostThreadMessage.
        /// </param>
        /// <param name="wMsgFilterMin">
        ///     [Type: UINT] The integer value of the lowest message value to be retrieved.Use WM_KEYFIRST (0x0100) to specify the
        ///     first keyboard message or WM_MOUSEFIRST(0x0200) to specify the first mouse message.
        ///     Use WM_INPUT here and in wMsgFilterMax to specify only the WM_INPUT messages.
        ///     If wMsgFilterMin and wMsgFilterMax are both zero, GetMessage returns all available messages (that is, no range
        ///     filtering is performed).
        /// </param>
        /// <param name="wMsgFilterMax">
        ///     [Type: UINT]
        ///     The integer value of the highest message value to be retrieved.Use WM_KEYLAST to specify the last keyboard message
        ///     or WM_MOUSELAST to specify the last mouse message.
        ///     Use WM_INPUT here and in wMsgFilterMin to specify only the WM_INPUT messages.
        ///     If wMsgFilterMin and wMsgFilterMax are both zero, GetMessage returns all available messages (that is, no range
        ///     filtering is performed).
        /// </param>
        /// <returns>
        ///     [Type: BOOL]
        ///     If the function retrieves a message other than WM_QUIT, the return value is nonzero.
        ///     If the function retrieves the WM_QUIT message, the return value is zero.
        ///     If there is an error, the return value is -1. For example, the function fails if hWnd is an invalid window handle
        ///     or lpMsg is an invalid pointer.To get extended error information, call GetLastError.
        /// </returns>
        /// <returns>
        ///     An application typically uses the return value to determine whether to end the main message loop and exit the
        ///     program.
        ///     The GetMessage function retrieves messages associated with the window identified by the hWnd parameter or any of
        ///     its children, as specified by the IsChild function, and within the range of message values given by the
        ///     wMsgFilterMin and wMsgFilterMax parameters. Note that an application can only use the low word in the wMsgFilterMin
        ///     and wMsgFilterMax parameters; the high word is reserved for the system.
        ///     Note that GetMessage always retrieves WM_QUIT messages, no matter which values you specify for wMsgFilterMin and
        ///     wMsgFilterMax.
        ///     During this call, the system delivers pending, nonqueued messages, that is, messages sent to windows owned by the
        ///     calling thread using the SendMessage, SendMessageCallback, SendMessageTimeout, or SendNotifyMessage function. Then
        ///     the first queued message that matches the specified filter is retrieved. The system may also process internal
        ///     events. If no filter is specified, messages are processed in the following order:
        ///     Sent messages
        ///     Posted messages
        ///     Input (hardware) messages and system internal events
        ///     Sent messages (again)
        ///     WM_PAINT messages
        ///     WM_TIMER messages
        ///     To retrieve input messages before posted messages, use the wMsgFilterMin and wMsgFilterMax parameters.
        ///     GetMessage does not remove WM_PAINT messages from the queue. The messages remain in the queue until processed.
        ///     If a top-level window stops responding to messages for more than several seconds, the system considers the window
        ///     to be not responding and replaces it with a ghost window that has the same z-order, location, size, and visual
        ///     attributes. This allows the user to move it, resize it, or even close the application. However, these are the only
        ///     actions available because the application is actually not responding. When in the debugger mode, the system does
        ///     not generate a ghost window.
        /// </returns>
        [DllImport("user32.dll")]
        public static extern int GetMessage(out Message lpMsg, IntPtr hwnd, uint wMsgFilterMin,
            uint wMsgFilterMax);

        /// <summary>
        ///     Enables you to produce special effects when showing or hiding windows. There are four types of animation: roll,
        ///     slide, collapse or expand, and alpha-blended fade.
        /// </summary>
        /// <param name="hwnd">
        ///     [Type: HWND]
        ///     A handle to the window to animate.The calling thread must own this window.
        /// </param>
        /// <param name="dwTime">
        ///     [Type: DWORD]
        ///     The time it takes to play the animation, in milliseconds.Typically, an animation takes 200 milliseconds to play.
        /// </param>
        /// <param name="dwFlags">
        ///     [Type: DWORD]
        ///     The type of animation.This parameter can be one or more of the following values. Note that, by default, these flags
        ///     take effect when showing a window. To take effect when hiding a window, use AW_HIDE and a logical OR operator with
        ///     the appropriate flags.
        /// </param>
        /// <returns>
        ///     [Type: BOOL]
        ///     If the function succeeds, the return value is nonzero.
        ///     If the function fails, the return value is zero. The function will fail in the following situations:
        ///     If the window is already visible and you are trying to show the window.
        ///     If the window is already hidden and you are trying to hide the window.
        ///     If there is no direction specified for the slide or roll animation.
        ///     When trying to animate a child window with AW_BLEND.
        ///     If the thread does not own the window. Note that, in this case, AnimateWindow fails but GetLastError returns
        ///     ERROR_SUCCESS.
        ///     To get extended error information, call the GetLastError function.
        /// </returns>
        /// <remarks>
        ///     To show or hide a window without special effects, use ShowWindow.
        ///     When using slide or roll animation, you must specify the direction. It can be either AW_HOR_POSITIVE,
        ///     AW_HOR_NEGATIVE, AW_VER_POSITIVE, or AW_VER_NEGATIVE.
        ///     You can combine AW_HOR_POSITIVE or AW_HOR_NEGATIVE with AW_VER_POSITIVE or AW_VER_NEGATIVE to animate a window
        ///     diagonally.
        ///     The window procedures for the window and its child windows should handle any WM_PRINT or WM_PRINTCLIENT messages.
        ///     Dialog boxes, controls, and common controls already handle WM_PRINTCLIENT. The default window procedure already
        ///     handles WM_PRINT.
        ///     If a child window is displayed partially clipped, when it is animated it will have holes where it is clipped.
        ///     AnimateWindow supports RTL windows.
        ///     Avoid animating a window that has a drop shadow because it produces visually distracting, jerky animations.
        /// </remarks>
        [DllImport("user32.dll")]
        public static extern int AnimateWindow(IntPtr hwnd, int dwTime, AnimateWindowFlags dwFlags);

        /// <summary>
        ///     Retrieves a handle to the top-level window whose class name and window name match the specified strings. This
        ///     function does not search child windows. This function does not perform a case-sensitive search.
        ///     To search child windows, beginning with a specified child window, use the FindWindowEx function.
        /// </summary>
        /// <param name="lpClassName">
        ///     [Type: LPCTSTR]
        ///     The class name or a class atom created by a previous call to the RegisterClass or RegisterClassEx function. The
        ///     atom must be in the low-order word of lpClassName; the high-order word must be zero.
        ///     If lpClassName points to a string, it specifies the window class name. The class name can be any name registered
        ///     with RegisterClass or RegisterClassEx, or any of the predefined control-class names.
        ///     If lpClassName is NULL, it finds any window whose title matches the lpWindowName parameter.
        /// </param>
        /// <param name="lpWindowName">
        ///     [Type: LPCTSTR]
        ///     The window name (the window's title). If this parameter is NULL, all window names match.
        /// </param>
        /// <returns>
        ///     [Type: HWND]
        ///     If the function succeeds, the return value is a handle to the window that has the specified class name and window
        ///     name.
        ///     If the function fails, the return value is NULL. To get extended error information, call GetLastError.
        /// </returns>
        /// <remarks>
        ///     If the lpWindowName parameter is not NULL, FindWindow calls the GetWindowText function to retrieve the window name
        ///     for comparison. For a description of a potential problem that can arise, see the Remarks for GetWindowText.
        /// </remarks>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hwnd, ShowWindowCommands nCmdShow);

        [DllImport("user32.dll")]
        public static extern bool UpdateWindow(IntPtr hwnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr CreateWindowEx(
            WindowExStyles dwExStyle,
            string lpClassName,
            string lpWindowName,
            WindowStyles dwStyle,
            int x,
            int y,
            int nWidth,
            int nHeight,
            IntPtr hwndParent,
            IntPtr hMenu,
            IntPtr hInstance,
            IntPtr lpParam);

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowLong(IntPtr hwnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr")]
        private static extern IntPtr GetWindowLongPtrNative(IntPtr hwnd, int nIndex);

        // This static method is required because Win32 does not support
        // GetWindowLongPtr directly
        public static IntPtr GetWindowLongPtr(IntPtr hwnd, int nIndex)
        {
            return IntPtr.Size == 8 ? GetWindowLongPtrNative(hwnd, nIndex) : GetWindowLong(hwnd, nIndex);
        }

        // This static method is required because Win32 does not support
        // GetWindowLongPtr directly
        public static IntPtr SetWindowLongPtr(IntPtr hwnd, int nIndex, IntPtr dwNewLong)
        {
            return IntPtr.Size == 8
                ? SetWindowLongPtrNative(hwnd, nIndex, dwNewLong)
                : new IntPtr(SetWindowLong(hwnd, nIndex, dwNewLong.ToInt32()));
        }

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hwnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
        private static extern IntPtr SetWindowLongPtrNative(IntPtr hwnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll")]
        public static extern ushort RegisterClassEx([In] ref WindowClassEx lpwcx);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool UnregisterClass(string lpClassName, IntPtr hInstance);

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hwnd, out Rectangle lpRect);

        [DllImport("user32.dll")]
        public static extern bool GetClientRect(IntPtr hwnd, out Rectangle lpRect);

        [DllImport("user32.dll")]
        public static extern void PostQuitMessage(int nExitCode);

        [DllImport("user32.dll")]
        public static extern IntPtr DefWindowProc(IntPtr hwnd, uint uMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr LoadIcon(IntPtr hInstance, string lpIconName);

        [DllImport("user32.dll")]
        public static extern IntPtr LoadIcon(IntPtr hInstance, IntPtr lpIconResource);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr LoadCursor(IntPtr hInstance, string lpCursorName);

        [DllImport("user32.dll")]
        public static extern IntPtr LoadCursor(IntPtr hInstance, IntPtr lpCursorResource);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
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

        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(IntPtr hObject);

        [DllImport("gdi32.dll")]
        public static extern IntPtr GetStockObject(int fnObject);

        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, DwmWindowAttributeType attr, [In] ref int attrValue,
            int attrSize);

        [DllImport("dwmapi.dll")]
        public static extern void DwmIsCompositionEnabled(out bool pfEnabled);

        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hwnd, [In] ref Margin margin);

        [DllImport("user32.dll")]
        public static extern void DisableProcessWindowsGhosting();


        [DllImport("dwmapi.dll")]
        public static extern bool DwmDefWindowProc(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam,
            out IntPtr lResult);

        [DllImport("uxtheme.dll")]
        public static extern int SetWindowThemeAttribute(
            IntPtr hwnd,
            WindowThemeAttributeType eAttributeType,
            [In] ref WindowThemeNCAttribute pvAttribute,
            int size);

        [DllImport("user32.dll")]
        public static extern bool ValidateRect(IntPtr hWnd, ref Rectangle lpRect);

        [DllImport("user32.dll")]
        public static extern bool InvalidateRect(IntPtr hWnd, IntPtr lpRect, bool bErase);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PeekMessage(out Message lpMsg, IntPtr hWnd, uint wMsgFilterMin,
            uint wMsgFilterMax, uint wRemoveMsg);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromDC(IntPtr hdc);

        [DllImport("user32.dll")]
        public static extern bool ReleaseDC(IntPtr hwnd, IntPtr hdc);

        [DllImport("user32.dll")]
        public static extern bool InvertRect(IntPtr hdc, [In] ref Rectangle lprc);

        [DllImport("user32.dll")]
        public static extern bool SetRectEmpty(out Rectangle lprc);

        [DllImport("user32.dll")]
        public static extern bool CopyRect(out Rectangle lprcDst, [In] ref Rectangle lprcSrc);

        [DllImport("user32.dll")]
        public static extern bool IntersectRect(out Rectangle lprcDst, [In] ref Rectangle lprcSrc1,
            [In] ref Rectangle lprcSrc2);

        [DllImport("user32.dll")]
        public static extern bool UnionRect(out Rectangle lprcDst, [In] ref Rectangle lprcSrc1,
            [In] ref Rectangle lprcSrc2);

        [DllImport("user32.dll")]
        public static extern bool IsRectEmpty([In] ref Rectangle lprc);

        [DllImport("user32.dll")]
        public static extern bool PtInRect([In] ref Rectangle lprc, Point pt);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern bool OffsetRect(ref Rectangle lprc, int dx, int dy);

        [DllImport("user32.dll")]
        public static extern bool InflateRect(ref Rectangle lprc, int dx, int dy);

        [DllImport("user32.dll")]
        public static extern int FrameRect(IntPtr hdc, [In] ref Rectangle lprc, IntPtr hbr);

        [DllImport("user32.dll")]
        public static extern int FillRect(IntPtr hdc, [In] ref Rectangle lprc, IntPtr hbr);

        [DllImport("gdi32.dll")]
        public static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass,
            string lpszWindow);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hwnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy,
            SetWindowPosFlags flags);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, IntPtr processId);

        [DllImport("user32.dll")]
        internal static extern bool MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool bRepaint);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll")]
        public static extern bool GetWindowInfo(IntPtr hwnd, ref WindowInfo pwi);

        [DllImport("user32.dll")]
        public static extern WindowRegionType GetWindowRgn(IntPtr hWnd, IntPtr hRgn);


        [DllImport("user32.dll")]
        public static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

        [DllImport("user32.dll")]
        internal static extern bool WinHelp(IntPtr hWndMain, string lpszHelp, uint uCommand, uint dwData);

        [DllImport("user32.dll")]
        public static extern int SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool bRedraw);


        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject(IntPtr hObject);

        [DllImport("gdi32.dll")]
        public static extern bool DeleteDC(IntPtr hdc);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateRectRgnIndirect([In] ref Rectangle lprc);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateEllipticRgn(int nLeftRect, int nTopRect,
            int nRightRect, int nBottomRect);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateEllipticRgnIndirect([In] ref Rectangle lprc);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateRoundRectRgn(int x1, int y1, int x2, int y2,
            int cx, int cy);

        [DllImport("gdi32.dll")]
        public static extern int CombineRgn(IntPtr hrgnDest, IntPtr hrgnSrc1,
            IntPtr hrgnSrc2, int fnCombineMode);

        [DllImport("gdi32.dll")]
        public static extern bool OffsetViewportOrgEx(IntPtr hdc, int nXOffset, int nYOffset, out Point lpPoint);

        [DllImport("gdi32.dll")]
        public static extern bool SetViewportOrgEx(IntPtr hdc, int x, int y, out Point lpPoint);

        [DllImport("gdi32.dll")]
        public static extern int SetMapMode(IntPtr hdc, int fnMapMode);

        [DllImport("gdi32.dll")]
        public static extern int SelectClipRgn(IntPtr hdc, IntPtr hrgn);

        [DllImport("gdi32.dll")]
        public static extern int ExtSelectClipRgn(IntPtr hdc, IntPtr hrgn, int fnMode);

        [DllImport("user32.dll")]
        public static extern bool InvalidateRgn(IntPtr hWnd, IntPtr hRgn, bool bErase);

        [DllImport("user32.dll")]
        public static extern bool ValidateRgn(IntPtr hWnd, IntPtr hRgn);

        [DllImport("gdi32.dll")]
        public static extern bool FillRgn(IntPtr hdc, IntPtr hrgn, IntPtr hbr);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateSolidBrush(uint crColor);

        [DllImport("gdi32.dll")]
        public static extern bool FrameRgn(IntPtr hdc, IntPtr hrgn, IntPtr hbr, int nWidth,
            int nHeight);

        [DllImport("gdi32.dll")]
        public static extern bool PaintRgn(IntPtr hdc, IntPtr hrgn);

        [DllImport("gdi32.dll")]
        public static extern bool InvertRgn(IntPtr hdc, IntPtr hrgn);

        [DllImport("gdi32.dll")]
        public static extern bool LineTo(IntPtr hdc, int nXEnd, int nYEnd);

        [DllImport("gdi32.dll")]
        public static extern bool MoveToEx(IntPtr hdc, int x, int y, IntPtr lpPoint);

        [DllImport("gdi32.dll")]
        public static extern bool RoundRect(IntPtr hdc, int nLeftRect, int nTopRect,
            int nRightRect, int nBottomRect, int nWidth, int nHeight);

        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        public static extern bool TextOut(IntPtr hdc, int nXStart, int nYStart,
            string lpString, int cbString);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPlacement(IntPtr hWnd,
            [In] ref WindowPlacement lpwndpl);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowPlacement(IntPtr hWnd, out WindowPlacement lpwndpl);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDCEx(IntPtr hWnd, IntPtr hrgnClip, DeviceContextFlags flags);

        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool BitBlt(IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight,
            IntPtr hdcSrc,
            int nXSrc, int nYSrc, BitBltFlags dwRop);

        [DllImport("gdi32.dll")]
        public static extern bool StretchBlt(IntPtr hdcDest, int nXOriginDest, int nYOriginDest,
            int nWidthDest, int nHeightDest,
            IntPtr hdcSrc, int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int nHeightSrc,
            BitBltFlags dwRop);

        [DllImport("gdi32.dll")]
        public static extern IntPtr PathToRegion(IntPtr hdc);
    }
}