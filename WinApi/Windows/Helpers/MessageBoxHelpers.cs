using System;
using WinApi.User32;

namespace WinApi.Windows.Helpers
{
    public class MessageBoxHelpers
    {
        public static MessageBoxResult ShowError(object errorObject, string infoMessage = null, string title = null,
            IntPtr parentHwnd = default(IntPtr),
            MessageBoxFlags flags =
                MessageBoxFlags.MB_OK | MessageBoxFlags.MB_ICONERROR | MessageBoxFlags.MB_SYSTEMMODAL)
        {
            const string defaultTitle = "Error";
            const string defaultInfoMessage = "Oh snap! Something went wrong.";

            title = title ?? defaultTitle;
            var ex = errorObject as Exception;
            if (ex == null)
            {
                return User32Helpers.MessageBox(parentHwnd,
                    infoMessage ?? defaultInfoMessage +
                    $"\n\n{errorObject?.ToString() ?? "No additional information available."}",
                    title, flags);
            }
            var exMessage = ex.Message ?? "No information message available.";
            string msg;
            if (infoMessage != null) { msg = infoMessage + "\n\n" + exMessage; }
            else
            { msg = defaultInfoMessage + "\n\n" + exMessage; }
            return User32Helpers.MessageBox(parentHwnd,
                $"{msg}" +
                $"\n\nStackTrace:\n\n{ex.StackTrace}",
                title, flags);
        }

        public static MessageBoxResult Show(string message, string title = null,
            IntPtr parentHwnd = default(IntPtr),
            MessageBoxFlags flags =
                MessageBoxFlags.MB_OK | MessageBoxFlags.MB_ICONINFORMATION)
        {
            const string defaultTitle = "Information";
            title = title ?? defaultTitle;
            return User32Helpers.MessageBox(parentHwnd,
                message, title, flags);
        }
    }
}