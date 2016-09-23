using System;
using WinApi.DwmApi;
using WinApi.User32;

namespace WinApi.XWin
{
    public static class MessageHelpers
    {
        public static void RunDwmDefWindowProc(ref WindowMessage msg, IntPtr windowHandle)
        {
            IntPtr res;
            if (DwmApiMethods.DwmDefWindowProc(windowHandle, (uint) msg.Id, msg.WParam, msg.LParam, out res))
            {
                msg.Result = res;
                msg.SetHandled();
            }
        }

        public static void PostQuitMessage(int exitCode = 0)
        {
            User32Methods.PostQuitMessage(exitCode);
        }
    }
}