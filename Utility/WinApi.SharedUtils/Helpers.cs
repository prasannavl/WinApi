using System;

namespace WinApi.Utils
{
    public class DisposableHelpers
    {
        public static void DisposeAndSetNull<T>(ref T obj)
            where T : class, IDisposable
        {
            if (obj != null)
            {
                obj.Dispose();
                obj = null;
            }
        }
    }
}
