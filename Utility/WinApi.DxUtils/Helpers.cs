using System;
using System.Net.Configuration;
using SharpDX;
using WinApi.DxUtils.Core;

namespace WinApi.DxUtils
{
    public static class DisposableHelpers
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

    public static class ErrorHelpers
    {
        public static bool ShouldResetDxgiForError(ResultDescriptor resultDescriptor)
        {
            return ShouldResetDxgiForError(resultDescriptor.Code);
        }

        public static bool ShouldResetDxgiForError(int code)
        {
            return (code == SharpDX.DXGI.ResultCode.DeviceRemoved.Code) ||
                   (code == SharpDX.DXGI.ResultCode.DeviceReset.Code);
        }
    }
}