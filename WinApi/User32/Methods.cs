using System;
using System.Runtime.InteropServices;
using System.Text;
using NetCoreEx.Geometry;

namespace WinApi.User32
{
    public static class User32Methods
    {
        public const string LibraryName = "user32";

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern IntPtr LoadIcon(IntPtr hInstance, string lpIconName);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern IntPtr LoadIcon(IntPtr hInstance, IntPtr lpIconResource);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern IntPtr LoadCursor(IntPtr hInstance, string lpCursorName);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern IntPtr LoadCursor(IntPtr hInstance, IntPtr lpCursorResource);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern IntPtr LoadImage(IntPtr hInstance, string lpszName, ResourceImageType uType,
            int cxDesired, int cyDesired, LoadResourceFlags fuLoad);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern IntPtr LoadImage(IntPtr hInstance, IntPtr resourceId, ResourceImageType uType,
            int cxDesired, int cyDesired, LoadResourceFlags fuLoad);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern IntPtr LoadBitmap(IntPtr hInstance, string lpBitmapName);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern IntPtr LoadBitmap(IntPtr hInstance, IntPtr resourceId);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr BeginPaint(IntPtr hwnd, out PaintStruct lpPaint);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern void EndPaint(IntPtr hwnd, [In] ref PaintStruct lpPaint);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr GetDesktopWindow();

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr MonitorFromPoint(Point pt, MonitorFlag dwFlags);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr MonitorFromWindow(IntPtr hwnd, MonitorFlag dwFlags);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern int DrawText(IntPtr hdc, string lpString, int nCount, [In] ref Rectangle lpRect,
            uint uFormat);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, KeyModifierFlags fsModifiers, VirtualKey vk);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern uint SendInput(uint nInputs, IntPtr pInputs, int cbSize);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern uint SendInput(uint nInputs, [In] Input[] pInputs, int cbSize);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr SetTimer(IntPtr hWnd, IntPtr nIdEvent, uint uElapseMillis, TimerProc lpTimerFunc);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool KillTimer(IntPtr hwnd, IntPtr uIdEvent);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool ValidateRect(IntPtr hWnd, [In] ref Rectangle lpRect);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool ValidateRect(IntPtr hWnd, IntPtr lpRect);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool InvalidateRect(IntPtr hWnd, [In] ref Rectangle lpRect, bool bErase);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool InvalidateRect(IntPtr hWnd, IntPtr lpRect, bool bErase);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern int GetSystemMetrics(SystemMetrics nIndex);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern bool SystemParametersInfo(uint uiAction, uint uiParam, IntPtr pvParam,
            SystemParamtersInfoFlags fWinIni);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr GetWindowDC(IntPtr hwnd);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr WindowFromDC(IntPtr hdc);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool ReleaseDC(IntPtr hwnd, IntPtr hdc);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool InvertRect(IntPtr hdc, [In] ref Rectangle lprc);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool SetRectEmpty(out Rectangle lprc);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool AdjustWindowRect([In] [Out] ref Rectangle lpRect, WindowStyles dwStyle, bool hasMenu);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool AdjustWindowRectEx([In] [Out] ref Rectangle lpRect, WindowStyles dwStyle, bool hasMenu,
            WindowExStyles dwExStyle);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool CopyRect(out Rectangle lprcDst, [In] ref Rectangle lprcSrc);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool IntersectRect(out Rectangle lprcDst, [In] ref Rectangle lprcSrc1,
            [In] ref Rectangle lprcSrc2);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool UnionRect(out Rectangle lprcDst, [In] ref Rectangle lprcSrc1,
            [In] ref Rectangle lprcSrc2);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool IsRectEmpty([In] ref Rectangle lprc);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool PtInRect([In] ref Rectangle lprc, Point pt);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool OffsetRect([In] [Out] ref Rectangle lprc, int dx, int dy);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool InflateRect([In] [Out] ref Rectangle lprc, int dx, int dy);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool FrameRect(IntPtr hdc, [In] ref Rectangle lprc, IntPtr hbr);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool FillRect(IntPtr hdc, [In] ref Rectangle lprc, IntPtr hbr);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern RegionType GetWindowRgn(IntPtr hWnd, IntPtr hRgn);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern RegionType GetWindowRgnBox(IntPtr hWnd, out Rectangle lprc);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha,
            LayeredWindowAttributeFlag dwFlags);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern bool WinHelp(IntPtr hWndMain, string lpszHelp, uint uCommand, uint dwData);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool bRedraw);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool InvalidateRgn(IntPtr hWnd, IntPtr hRgn, bool bErase);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool GetUpdateRect(IntPtr hwnd, out Rectangle rect, bool bErase);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool ValidateRgn(IntPtr hWnd, IntPtr hRgn);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr GetDCEx(IntPtr hWnd, IntPtr hrgnClip, DeviceContextFlags flags);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern void DisableProcessWindowsGhosting();

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst,
            [In] ref Point pptDst, [In] ref Size psize, IntPtr hdcSrc, [In] ref Point pptSrc, uint crKey,
            [In] ref BlendFunction pblend, uint dwFlags);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern int MapWindowPoints(IntPtr hWndFrom, IntPtr hWndTo, IntPtr lpPoints, int cPoints);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool ScreenToClient(IntPtr hWnd, [In] [Out] ref Point lpPoint);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr WindowFromPhysicalPoint(Point point);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr WindowFromPoint(Point point);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool IsWindowVisible(IntPtr hwnd);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool OpenIcon(IntPtr hwnd);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool IsWindow(IntPtr hwnd);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool IsHungAppWindow(IntPtr hwnd);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool IsZoomed(IntPtr hwnd);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool IsIconic(IntPtr hwnd);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool LogicalToPhysicalPoint(IntPtr hwnd, [In] [Out] ref Point point);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr ChildWindowFromPoint(IntPtr hwndParent, Point point);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr ChildWindowFromPointEx(IntPtr hwndParent, Point point,
            ChildWindowFromPointFlags flags);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr GetMenu(IntPtr hwnd);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern MessageBoxResult MessageBox(IntPtr hWnd, string lpText, string lpCaption, uint type);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern MessageBoxResult MessageBoxEx(IntPtr hWnd, string lpText, string lpCaption, uint type,
            ushort wLanguageId);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool SetThreadDesktop(IntPtr hDesk);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr GetThreadDesktop(uint threadId);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern bool GetMonitorInfo(IntPtr hMonitor, ref MonitorInfo lpmi);

        #region Keyboard, Mouse & Input Method Functions

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool IsWindowEnabled(IntPtr hWnd);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool ReleaseCapture();

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr SetCapture(IntPtr hWnd);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr GetFocus();

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr SetFocus(IntPtr hWnd);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr SetActiveWindow(IntPtr hWnd);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr GetActiveWindow();

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool BlockInput(bool fBlockIt);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern uint WaitForInputIdle(IntPtr hProcess, uint dwMilliseconds);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool DragDetect(IntPtr hwnd, Point point);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool ClientToScreen(IntPtr hwnd, [In] [Out] ref Point point);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool ClipCursor([In] ref Rectangle rect);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool ClipCursor(IntPtr ptr);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool TrackMouseEvent([In] [Out] ref TrackMouseEventOptions lpEventTrack);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool GetLastInputInfo(out LastInputInfo plii);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern uint MapVirtualKey(uint uCode, VirtualKeyMapType uMapType);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern uint MapVirtualKey(VirtualKey uCode, VirtualKeyMapType uMapType);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern uint MapVirtualKeyEx(uint uCode, VirtualKeyMapType uMapType, IntPtr dwhkl);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern uint MapVirtualKeyEx(VirtualKey uCode, VirtualKeyMapType uMapType, IntPtr dwhkl);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern KeyState GetAsyncKeyState(int vKey);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern KeyState GetAsyncKeyState(VirtualKey vKey);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern KeyState GetKeyState(int vKey);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern KeyState GetKeyState(VirtualKey vKey);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool GetKeyboardState(
            [MarshalAs(UnmanagedType.LPArray, SizeConst = 256)] out byte[] lpKeyState);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool GetKeyboardState(IntPtr lpKeyState);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool SetKeyboardState(
            [MarshalAs(UnmanagedType.LPArray, SizeConst = 256)] [In] byte[] lpKeyState);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool SetKeyboardState(IntPtr lpKeyState);

        /// <summary>
        ///     Retrieves information about the specified title bar.
        /// </summary>
        /// <param name="hwnd">A handle to the title bar whose information is to be retrieved.</param>
        /// <param name="pti">
        ///     A pointer to a TITLEBARINFO structure to receive the information. Note that you must set the cbSize
        ///     member to sizeof(TITLEBARINFO) before calling this function.
        /// </param>
        /// <returns></returns>
        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool GetTitleBarInfo(IntPtr hwnd, IntPtr pti);

        #endregion

        #region Cursor Functions

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool GetCursorPos(out Point point);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool SetCursorPos(int x, int y);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool SetPhysicalCursorPos(int x, int y);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool SetSystemCursor(IntPtr cursor, SystemCursor id);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern int ShowCursor(bool bShow);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr SetCursor(IntPtr hCursor);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr CopyCursor(IntPtr hCursor);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool GetCursorInfo(ref CursorInfo info);

        #endregion

        #region Window Functions

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
        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool AnimateWindow(IntPtr hwnd, int dwTime, AnimateWindowFlags dwFlags);

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
        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool ShowWindow(IntPtr hwnd, ShowWindowCommands nCmdShow);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool UpdateWindow(IntPtr hwnd);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
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


        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool GetWindowRect(IntPtr hwnd, out Rectangle lpRect);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool GetClientRect(IntPtr hwnd, out Rectangle lpRect);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass,
            string lpszWindow);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr GetTopWindow();

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr GetNextWindow(IntPtr hwnd, uint wCmd);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr GetWindow(IntPtr hwnd, uint wCmd);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool AllowSetForegroundWindow(uint dwProcessId);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool SetForegroundWindow(IntPtr hwnd);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool BringWindowToTop(IntPtr hwnd);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool SetWindowPos(IntPtr hwnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy,
            WindowPositionFlags flags);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern bool SetWindowText(IntPtr hwnd, string lpString);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, IntPtr processId);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool bRepaint);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool GetWindowInfo(IntPtr hwnd, [In] [Out] ref WindowInfo pwi);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool SetWindowPlacement(IntPtr hWnd,
            [In] ref WindowPlacement lpwndpl);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool GetWindowPlacement(IntPtr hWnd, out WindowPlacement lpwndpl);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool RedrawWindow(IntPtr hWnd, [In] ref Rectangle lprcUpdate, IntPtr hrgnUpdate,
            RedrawWindowFlags flags);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool DestroyWindow(IntPtr hwnd);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool CloseWindow(IntPtr hwnd);

        #endregion

        #region Window Class Functions

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern ushort RegisterClassEx([In] ref WindowClassEx lpwcx);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern ushort RegisterClassEx([In] ref WindowClassExBlittable lpwcx);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern bool UnregisterClass(string lpClassName, IntPtr hInstance);

        // This static method is required because Win32 does not support
        // GetWindowLongPtr directly
        public static IntPtr GetWindowLongPtr(IntPtr hwnd, int nIndex)
        {
            return IntPtr.Size > 4
                ? GetWindowLongPtr_x64(hwnd, nIndex)
                : new IntPtr(GetWindowLong(hwnd, nIndex));
        }

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        private static extern int GetWindowLong(IntPtr hwnd, int nIndex);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet, EntryPoint = "GetWindowLongPtr")]
        private static extern IntPtr GetWindowLongPtr_x64(IntPtr hwnd, int nIndex);

        // This static method is required because Win32 does not support
        // GetWindowLongPtr directly
        public static IntPtr SetWindowLongPtr(IntPtr hwnd, int nIndex, IntPtr dwNewLong)
        {
            return IntPtr.Size > 4
                ? SetWindowLongPtr_x64(hwnd, nIndex, dwNewLong)
                : new IntPtr(SetWindowLong(hwnd, nIndex, dwNewLong.ToInt32()));
        }

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        private static extern int SetWindowLong(IntPtr hwnd, int nIndex, int dwNewLong);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet, EntryPoint = "SetWindowLongPtr")]
        private static extern IntPtr SetWindowLongPtr_x64(IntPtr hwnd, int nIndex, IntPtr dwNewLong);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern bool GetClassInfoEx(IntPtr hInstance, string lpClassName,
            out WindowClassExBlittable lpWndClass);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        public static IntPtr GetClassLongPtr(IntPtr hwnd, int nIndex)
        {
            return IntPtr.Size > 4
                ? GetClassLongPtr_x64(hwnd, nIndex)
                : new IntPtr(unchecked((int) GetClassLong(hwnd, nIndex)));
        }

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        private static extern uint GetClassLong(IntPtr hWnd, int nIndex);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet, EntryPoint = "GetClassLongPtr")]
        private static extern IntPtr GetClassLongPtr_x64(IntPtr hWnd, int nIndex);


        public static IntPtr SetClassLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
        {
            return IntPtr.Size > 4
                ? SetClassLongPtr_x64(hWnd, nIndex, dwNewLong)
                : new IntPtr(unchecked((int) SetClassLong(hWnd, nIndex, dwNewLong.ToInt32())));
        }

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        private static extern uint SetClassLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet, EntryPoint = "SetClassLongPtr")]
        private static extern IntPtr SetClassLongPtr_x64(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        #endregion

        #region Window Procedure Functions

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern IntPtr DefWindowProc(IntPtr hwnd, uint uMsg, IntPtr wParam, IntPtr lParam);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern IntPtr CallWindowProc(WindowProc lpPrevWndFunc, IntPtr hWnd, uint uMsg, IntPtr wParam,
            IntPtr lParam);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern IntPtr CallWindowProc(IntPtr lpPrevWndFunc, IntPtr hWnd, uint uMsg, IntPtr wParam,
            IntPtr lParam);

        #endregion

        #region Message Functions

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern bool PeekMessage(out Message lpMsg, IntPtr hWnd, uint wMsgFilterMin,
            uint wMsgFilterMax, uint wRemoveMsg);

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
        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
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
        [DllImport(LibraryName, ExactSpelling = true)]
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
        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern int GetMessage(out Message lpMsg, IntPtr hwnd, uint wMsgFilterMin,
            uint wMsgFilterMax);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern void PostQuitMessage(int nExitCode);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr GetMessageExtraInfo();

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr SetMessageExtraInfo(IntPtr lParam);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool WaitMessage();

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern IntPtr SendMessage(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern bool SendNotifyMessage(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern bool PostMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool ReplyMessage(IntPtr lResult);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool GetInputState();

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern uint GetMessagePos();

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern uint GetMessageTime();

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool InSendMessage();

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern uint GetQueueStatus(QueueStatusFlags flags);

        [DllImport(LibraryName, CharSet = Properties.BuildCharSet)]
        public static extern bool PostThreadMessage(uint threadId, uint msg, IntPtr wParam, IntPtr lParam);

        #endregion

        #region Clipboard Functions

        /// <summary>
        /// Opens the clipboard for examination and prevents other applications from modifying the clipboard content.
        /// </summary>
        /// <param name="hWndNewOwner">A handle to the window to be associated with the open clipboard. If this parameter is NULL, the open clipboard is associated with the current task.</param>
        /// <returns>If the function succeeds, the return value is nonzero.</returns>
        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool OpenClipboard(IntPtr hWndNewOwner);
        /// <summary>
        /// Closes the clipboard.
        /// </summary>
        /// <returns>If the function succeeds, the return value is nonzero.</returns>
        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool CloseClipboard();
        /// <summary>
        /// Retrieves data from the clipboard in a specified format. The clipboard must have been opened previously.
        /// </summary>
        /// <param name="uFormat">A clipboard format.</param>
        /// <returns>If the function succeeds, the return value is the handle to a clipboard object in the specified format.</returns>
        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr GetClipboardData(uint uFormat);

        /// <summary>
        /// Places data on the clipboard in a specified clipboard format. The window must be the current clipboard owner, and the application must have called the OpenClipboard function
        /// </summary>
        /// <param name="uFormat">The clipboard format</param>
        /// <param name="handle">A handle to the data in the specified format</param>
        /// <returns>If the function succeeds, the return value is the handle to the data.</returns>
        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr SetClipboardData(uint uFormat, IntPtr handle);

        /// <summary>
        /// Empties the clipboard and frees handles to data in the clipboard. The function then assigns ownership of the clipboard to the window that currently has the clipboard open.
        /// </summary>
        /// <returns></returns>
        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool EmptyClipboard();

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern IntPtr SetClipboardViewer(IntPtr hWndNewViewer);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern bool AddClipboardFormatListener(IntPtr hwnd);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern int GetPriorityClipboardFormat(IntPtr paFormatPriorityList, int cFormats);

        [DllImport(LibraryName, ExactSpelling = true)]
        public static extern uint EnumClipboardFormats(uint format);

        #endregion
    }
}