using System;
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

        public static bool ShouldResetD2DForError(ResultDescriptor resultDescriptor)
        {
            return ShouldResetD2DForError(resultDescriptor.Code);
        }

        public static bool ShouldResetD2DForError(int code)
        {
            return code == SharpDX.Direct2D1.ResultCode.RecreateTarget.Code;
        }
    }

    internal static class VersionHelpers {
        public static Version GetWindowsVersion()
        {
            var desc = System.Runtime.InteropServices.RuntimeInformation.OSDescription;
            if (desc.Contains(" 10."))
            {
                return new Version(10, 0, 0, 0);
            }
            else if (desc.Contains(" 6.3"))
            {
                return new Version(6, 3, 0, 0);
            }
            else if (desc.Contains(" 6.2"))
            {
                return new Version(6, 2, 0, 0);
            }
            return new Version(6, 1, 0, 0);
        }
    }
}