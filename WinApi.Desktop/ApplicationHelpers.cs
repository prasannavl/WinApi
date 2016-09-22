using System;
using WinApi.Kernel32;
using WinApi.XWin;

namespace WinApi.Desktop
{
    public static class ApplicationHelpers
    {
        public static WindowExceptionHandler DefaultWindowExceptionHandler { get; private set; }
        public static UnhandledExceptionEventHandler DefaultUnhandledExceptionHandler { get; private set; }

        static ApplicationHelpers()
        {
            DefaultUnhandledExceptionHandler =
                (sender, eventArgs) =>
                {
                    ShowCriticalError(eventArgs.ExceptionObject);
                    if (eventArgs.IsTerminating)
                    {
                        ShowCriticalInfo(
                            $"{Environment.NewLine}The application will now terminate. Press any key to do so.");
                        Console.ReadKey();
                    }
                };

            DefaultWindowExceptionHandler = (ref WindowException ex) =>
            {
                ShowCriticalError(ex.DispatchedException);
                ex.SetHandled();
            };
        }

        public static void SetupDefaultExceptionHandlers()
        {
            AppDomain.CurrentDomain.UnhandledException += DefaultUnhandledExceptionHandler;
            WindowCoreBase.UnhandledException += DefaultWindowExceptionHandler;
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