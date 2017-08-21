using System;
using SharpDX;
using SharpDX.DirectComposition;
using SharpDX.DXGI;
using WinApi.Core;
using Device = SharpDX.DirectComposition.Device;
using Surface = SharpDX.DirectComposition.Surface;

namespace WinApi.DxUtils.Composition
{
    public static class CompositionHelper
    {
        public static int GetVariantForPlatform(Version platformVersion = null)
        {
            if (platformVersion == null) platformVersion = VersionHelpers.GetWindowsVersion();
            if (platformVersion.Major > 6) return 2;
            if (platformVersion.Major == 6)
            {
                if (platformVersion.Minor > 2) return 2;
                if (platformVersion.Minor > 1) return 1;
            }
            return 0;
        }

        public static ComObject CreateDevice(SharpDX.DXGI.Device dxgiDevice, int variant = -1)
        {
            if (variant == -1) variant = 2;
            if (variant > 1) { return new DesktopDevice(dxgiDevice); }
            return variant == 1 ? new Device(dxgiDevice) : null;
        }

        public static void Commit(ComObject device, int variant)
        {
            if (variant > 1) ((DesktopDevice) device).Commit();
            else if (variant == 1) ((Device) device).Commit();
        }

        public static Surface CreateSurface(ComObject device, int variant, int width, int height, Format pixelFormat,
            AlphaMode alphaMode)
        {
            if (variant > 1) { return new Surface((DesktopDevice) device, width, height, pixelFormat, alphaMode); }
            return variant == 1 ? new Surface((Device) device, width, height, pixelFormat, alphaMode) : null;
        }
    }
}