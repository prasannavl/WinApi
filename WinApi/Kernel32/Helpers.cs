using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using NetCoreEx.BinaryExtensions;

// ReSharper disable InconsistentNaming

namespace WinApi.Kernel32
{
    public static class Kernel32Helpers
    {
        public static Version GetVersion()
        {
            var dwVersion = Kernel32Methods.GetVersion();
            var build = dwVersion < 0x80000000 ? dwVersion.HighAsUInt() : 0; // (DWORD) (HIWORD(dwVersion))
            var v = new Version(
                (byte) dwVersion.Low(), // (DWORD)(LOBYTE(LOWORD(dwVersion)))
                (dwVersion.Low() >> 8) & 0xff,
                (int) build // (DWORD)(HIBYTE(LOWORD(dwVersion)))
            );
            return v;
        }

        public static bool IsWin8OrGreater(Version version = null)
        {
            if (version == null) version = GetVersion();
            if (version.Major > 5)
            {
                if ((version.Major > 6) || (version.Minor > 1)) return true;
            }
            return false;
        }

        public static bool IsWin8Point1OrGreater(Version version = null)
        {
            if (version == null) version = GetVersion();
            if (version.Major > 5)
            {
                if ((version.Major > 6) || (version.Minor > 2)) return true;
            }
            return false;
        }

        public static bool IsWin10OrGreater(Version version = null)
        {
            if (version == null) version = GetVersion();
            return version.Major > 6;
        }

        public static bool GetIsProcessorAMD64()
        {
            SystemInfo info;
            Kernel32Methods.GetNativeSystemInfo(out info);
            return info.ProcessorArchitecture == (uint) ProcessArchitecture.PROCESSOR_ARCHITECTURE_AMD64;
        }

        public static class ProcessUserModeExceptionFilter
        {
            const uint PROCESS_CALLBACK_FILTER_ENABLED = 0x1;

            [DllImport(Kernel32Methods.LibraryName)]
            public static extern bool SetProcessUserModeExceptionPolicy(uint dwFlags);

            [DllImport(Kernel32Methods.LibraryName)]
            public static extern bool GetProcessUserModeExceptionPolicy(out uint dwFlags);

            private static bool IsApiAvailable()
            {
                if (GetIsProcessorAMD64())
                {
                    var ver = GetVersion();
                    return (ver.Major == 6) && (ver.Minor == 1) && (ver.Build >= 7601);
                }
                return false;
            }

            public static bool Disable()
            {
                if (IsApiAvailable())
                {
                    uint dwFlags;
                    if (GetProcessUserModeExceptionPolicy(out dwFlags))
                    {
                        // Turn off the bit
                        dwFlags &= ~PROCESS_CALLBACK_FILTER_ENABLED;
                        return SetProcessUserModeExceptionPolicy(dwFlags);
                    }
                }
                return false;
            }

            public static bool Enable()
            {
                if (IsApiAvailable())
                {
                    uint dwFlags;
                    if (GetProcessUserModeExceptionPolicy(out dwFlags))
                    {
                        // Turn off the bit
                        dwFlags |= PROCESS_CALLBACK_FILTER_ENABLED;
                        return SetProcessUserModeExceptionPolicy(dwFlags);
                    }
                }
                return false;
            }
        }
    }
}