using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.DirectX.Helpers
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
