using System;

namespace WinApi.User32
{
    public static class User32Helpers
    {
        public static IntPtr GetWindowLongPtr(IntPtr hwnd, WindowLongFlags nIndex)
        {
            return User32Methods.GetWindowLongPtr(hwnd, (int) nIndex);
        }

        public static IntPtr SetWindowLongPtr(IntPtr hwnd, WindowLongFlags nIndex, IntPtr dwNewLong)
        {
            return User32Methods.SetWindowLongPtr(hwnd, (int) nIndex, dwNewLong);
        }

        public static IntPtr GetClassLongPtr(IntPtr hwnd, ClassLongFlags nIndex)
        {
            return User32Methods.GetClassLongPtr(hwnd, (int) nIndex);
        }

        public static IntPtr SetClassLongPtr(IntPtr hwnd, ClassLongFlags nIndex, IntPtr dwNewLong)
        {
            return User32Methods.SetClassLongPtr(hwnd, (int) nIndex, dwNewLong);
        }

        public static IntPtr LoadIcon(IntPtr hInstance, SystemIcon icon)
        {
            return User32Methods.LoadIcon(hInstance, new IntPtr((int) icon));
        }

        public static IntPtr LoadCursor(IntPtr hInstance, SystemCursor cursor)
        {
            return User32Methods.LoadCursor(hInstance, new IntPtr((int) cursor));
        }

        public static int DrawText(IntPtr hdc, string lpString, int nCount, ref Rectangle lpRect,
            DrawTextFormatFlags uFormat)
        {
            return User32Methods.DrawText(hdc, lpString, nCount, ref lpRect, (uint) uFormat);
        }

        public static int SetWindowPos(IntPtr hwnd, HwndZOrder order, int x, int y, int cx, int cy,
            SetWindowPosFlags flags)
        {
            return User32Methods.SetWindowPos(hwnd, new IntPtr((int) order), x, y, cx, cy, flags);
        }

        public static int PeekMessage(out Message lpMsg, IntPtr hWnd, uint wMsgFilterMin,
            uint wMsgFilterMax, PeekMessageFlags wRemoveMsg)
        {
            return User32Methods.PeekMessage(out lpMsg, hWnd, wMsgFilterMin, wMsgFilterMax, (uint) wRemoveMsg);
        }

        public static IntPtr GetNextWindow(IntPtr hwnd, GetWindowFlag cmd)
        {
            return User32Methods.GetNextWindow(hwnd, (uint) cmd);
        }

        public static IntPtr GetWindow(IntPtr hwnd, GetWindowFlag cmd)
        {
            return User32Methods.GetWindow(hwnd, (uint) cmd);
        }

        public static int SystemParametersInfo(SystemParametersAccessibilityInfo uiAction, uint uiParam, IntPtr pvParam,
            SystemParamtersInfoFlags fWinIni)
        {
            return User32Methods.SystemParametersInfo((uint) uiAction, uiParam, pvParam, fWinIni);
        }

        public static int SystemParametersInfo(SystemParametersDesktopInfo uiAction, uint uiParam, IntPtr pvParam,
            SystemParamtersInfoFlags fWinIni)
        {
            return User32Methods.SystemParametersInfo((uint) uiAction, uiParam, pvParam, fWinIni);
        }

        public static int SystemParametersInfo(SystemParametersIconInfo uiAction, uint uiParam, IntPtr pvParam,
            SystemParamtersInfoFlags fWinIni)
        {
            return User32Methods.SystemParametersInfo((uint) uiAction, uiParam, pvParam, fWinIni);
        }

        public static int SystemParametersInfo(SystemParametersInputInfo uiAction, uint uiParam, IntPtr pvParam,
            SystemParamtersInfoFlags fWinIni)
        {
            return User32Methods.SystemParametersInfo((uint) uiAction, uiParam, pvParam, fWinIni);
        }

        public static int SystemParametersInfo(SystemParametersMenuInfo uiAction, uint uiParam, IntPtr pvParam,
            SystemParamtersInfoFlags fWinIni)
        {
            return User32Methods.SystemParametersInfo((uint) uiAction, uiParam, pvParam, fWinIni);
        }

        public static int SystemParametersInfo(SystemParametersPowerInfo uiAction, uint uiParam, IntPtr pvParam,
            SystemParamtersInfoFlags fWinIni)
        {
            return User32Methods.SystemParametersInfo((uint) uiAction, uiParam, pvParam, fWinIni);
        }

        public static int SystemParametersInfo(SystemParametersScreenSaverInfo uiAction, uint uiParam, IntPtr pvParam,
            SystemParamtersInfoFlags fWinIni)
        {
            return User32Methods.SystemParametersInfo((uint) uiAction, uiParam, pvParam, fWinIni);
        }

        public static int SystemParametersInfo(SystemParametersTimeoutInfo uiAction, uint uiParam, IntPtr pvParam,
            SystemParamtersInfoFlags fWinIni)
        {
            return User32Methods.SystemParametersInfo((uint) uiAction, uiParam, pvParam, fWinIni);
        }

        public static int SystemParametersInfo(SystemParametersUiEffectsInfo uiAction, uint uiParam, IntPtr pvParam,
            SystemParamtersInfoFlags fWinIni)
        {
            return User32Methods.SystemParametersInfo((uint) uiAction, uiParam, pvParam, fWinIni);
        }

        public static int SystemParametersInfo(SystemParametersWindowInfo uiAction, uint uiParam, IntPtr pvParam,
            SystemParamtersInfoFlags fWinIni)
        {
            return User32Methods.SystemParametersInfo((uint) uiAction, uiParam, pvParam, fWinIni);
        }

        public static MessageBoxResult MessageBox(IntPtr hWnd, string lpText, string lpCaption, MessageBoxFlags type)
        {
            return (MessageBoxResult) User32Methods.MessageBox(hWnd, lpText, lpCaption, (uint) type);
        }

        public static MessageBoxResult MessageBox(string message, string title = "Info",
            MessageBoxFlags flags = MessageBoxFlags.MB_OK, IntPtr parent = default(IntPtr))
        {
            return MessageBox(parent, message, title, flags);
        }

        public static MessageBoxResult MessageBoxForError(object errorObject, string infoMessage = null,
            IntPtr parentHwnd = default(IntPtr),
            MessageBoxFlags flags = MessageBoxFlags.MB_OK | MessageBoxFlags.MB_ICONERROR | MessageBoxFlags.MB_TASKMODAL)
        {
            var ex = errorObject as Exception;
            const string defaultInfoMessage = "Oh snap! Something went wrong.";
            if (ex == null)
            {
                return MessageBox(parentHwnd,
                    infoMessage ?? defaultInfoMessage +
                    $"\n\n{errorObject?.ToString() ?? "No additional information available."}",
                    "Error", flags);
            }
            var exMessage = (ex.Message ?? "No information message available.");
            string msg;
            if (infoMessage != null)
            {
                msg = infoMessage + "\n\n" + exMessage;
            }
            else
            {
                msg = defaultInfoMessage + "\n\n" + exMessage;
            }
            return MessageBox(parentHwnd,
                $"{msg}" +
                $"\n\nStackTrace:\n\n{ex.StackTrace}",
                "Error", flags);
        }
    }
}