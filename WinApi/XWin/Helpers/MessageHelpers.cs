using System;
using WinApi.DwmApi;
using WinApi.User32;

namespace WinApi.XWin.Helpers
{
    public static class MessageHelpers
    {
        public static void RunDwmDefWindowProc(ref WindowMessage msg)
        {
            IntPtr res;
            if (DwmApiMethods.DwmDefWindowProc(msg.Hwnd, (uint) msg.Id, msg.WParam, msg.LParam, out res))
                msg.SetHandledWithResult(res);
        }

        public static void PostQuitMessage(int exitCode = 0)
        {
            User32Methods.PostQuitMessage(exitCode);
        }
    }
}