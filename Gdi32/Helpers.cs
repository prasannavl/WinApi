using System;

namespace WinApi.Gdi32
{
    public static class Gdi32Helpers
    {
        public static IntPtr GetStockObject(StockObject fnObject)
        {
            return Gdi32Methods.GetStockObject((int) fnObject);
        }
    }
}