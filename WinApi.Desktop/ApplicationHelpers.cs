using System;
using WinApi.XWin;

namespace WinApi.Desktop
{
    public static class ApplicationHelpers
    {
        public static void InitializeCriticalErrorDisplay()
        {
            AppDomain.CurrentDomain.UnhandledException +=
                (sender, eventArgs) =>
                {
                    ShowCriticalError(eventArgs.ExceptionObject);
                };

            WindowCoreBase.UnhandledException +=
                (ref WindowException ex) =>
                {
                    ShowCriticalError(ex.DispatchedException);
                };
        }

        public static void ShowCriticalError(object exceptionObj, string message = null, string title = null)
        {
            const string defaultMessage = "Oh snap! Something went wrong.";
            if (message == null) message = defaultMessage;

            ConsoleHelpers.EnsureConsole();
            if (!string.IsNullOrEmpty(title)) Console.Title = title;
            var exception = exceptionObj as Exception;
            string msg;
            if (exception != null)
            {
                msg = $"{Environment.NewLine}[CRITICAL] {message}{Environment.NewLine}{Environment.NewLine}" +
                      $"{exception.Message}{Environment.NewLine}" +
                      $"{exception.StackTrace}";
            }
            else
            {
                msg = $"{Environment.NewLine}[CRITICAL] {message}{Environment.NewLine}{Environment.NewLine}" +
                      $"{exceptionObj.ToString()}{Environment.NewLine}";
            }
            Console.Error.WriteLine(msg);
        }

        public static void ShowCriticalInfo(string message, string title = null, bool suffixNewLine = true)
        {
            ConsoleHelpers.EnsureConsole();
            if (!string.IsNullOrEmpty(title)) Console.Title = title;
            if (suffixNewLine) Console.Error.WriteLine(message);
            else Console.Error.Write(message);
        }
    }
}