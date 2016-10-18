using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.DirectComposition;
using WinApi.DxUtils.Core;

namespace WinApi.DxUtils.DComp
{
    public static class DCompHelper
    {
        public static int GetVariantForPlatform(Version platformVersion = null)
        {
            if (platformVersion == null)
                platformVersion = Environment.OSVersion.Version;
            if (platformVersion.Major > 6) return 2;
            if (platformVersion.Major == 6)
            {
                if (platformVersion.Minor > 2) return 2;
                if (platformVersion.Minor > 1) return 1;
            }
            return 0;
        }

        public static object CreateDevice(SharpDX.DXGI.Device dxgiDevice, int variant = -1)
        {
            if (variant == -1) variant = GetVariantForPlatform();
            if (variant > 1)
            {
                return new DesktopDevice(dxgiDevice);
            }
            return variant == 1 ? new Device(dxgiDevice) : null;
        }
    }
}
