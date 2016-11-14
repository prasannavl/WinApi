using System;
using WinApi.DwmApi;
using WinApi.User32;

namespace WinApi.Windows.Helpers
{
    public static class MessageHelpers
    {
        public static bool RunDwmDefWindowProc(ref WindowMessage msg)
        {
            return DwmApiMethods.DwmDefWindowProc(msg.Hwnd, (uint) msg.Id, msg.WParam, msg.LParam, out msg.Result);
        }

        public static void PostQuitMessage(int exitCode = 0)
        {
            User32Methods.PostQuitMessage(exitCode);
        }

        public static void RunWindowProc(WindowCore coreWindow, ref WindowMessage msg)
        {
            msg.SetResult(coreWindow.WindowProc(msg.Hwnd, (uint) msg.Id, msg.WParam, msg.LParam));
        }
    }
}