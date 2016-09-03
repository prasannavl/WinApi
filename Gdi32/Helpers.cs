using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinApi.Gdi32
{
    public static class Gdi32Helpers
    {
        public static IntPtr GetStockObject(StockObject fnObject)
        {
            return Gdi32Methods.GetStockObject((int)fnObject);
        }
    }
}
