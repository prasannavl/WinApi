using System;
using WinApi.DwmApi;
using WinApi.Extensions;
using WinApi.User32;

namespace WinApi.Windows.Helpers
{
    public static class MessageHelpers
    {
        public static void RunDwmDefWindowProc(ref WindowMessage msg)
        {
            IntPtr res;
            if (DwmApiMethods.DwmDefWindowProc(msg.Hwnd, (uint) msg.Id, msg.WParam, msg.LParam, out res))
                msg.SetResult(res);
        }

        public static void PostQuitMessage(int exitCode = 0)
        {
            User32Methods.PostQuitMessage(exitCode);
        }

        public static void RunWindowProc(WindowCore coreWindow, ref WindowMessage msg)
        {
            msg.SetResult(coreWindow.CallWindowProc(ref msg));
        }

        public static int RunWindowProcAndGetAsInt(WindowCore coreWindow, ref WindowMessage msg)
        {
            msg.SetResult(coreWindow.CallWindowProc(ref msg));
            return msg.Result.ToSafeInt32();
        }

        public static void RunWindowBaseProc(WindowCore coreWindow, ref WindowMessage msg)
        {
            msg.SetResult(coreWindow.CallWindowBaseProc(ref msg));
        }

        public static int RunWindowBaseProcAndGetAsInt(WindowCore coreWindow, ref WindowMessage msg)
        {
            msg.SetResult(coreWindow.CallWindowBaseProc(ref msg));
            return msg.Result.ToSafeInt32();
        }
    }
}