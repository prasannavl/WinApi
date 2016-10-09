using System;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using WinApi.DxUtils.D3D11_1;
using AlphaMode = SharpDX.DXGI.AlphaMode;
using Device = SharpDX.Direct3D11.Device;

namespace WinApi.DxUtils.D3D11_1
{
    public class D3DMetaFactory
    {
        public static D3DMetaResource Create3D(Adapter adapter, DeviceCreationFlags creationFlags = 0,
            SwapChainDescription1? description = null)
        {
            return Create3DCore(adapter, DriverType.Null, creationFlags, description, false);
        }

        public static D3DMetaResource Create3D(DriverType type = DriverType.Hardware,
            DeviceCreationFlags creationFlags = DeviceCreationFlags.SingleThreaded,
            SwapChainDescription1? description = null, bool allowWarpFallbackDriver = true)
        {
            return Create3DCore(null, type, creationFlags, description, allowWarpFallbackDriver);
        }

        private static D3DMetaResource Create3DCore(IntPtr hwnd, Adapter adapter, DriverType type,
            DeviceCreationFlags creationFlags,
            SwapChainDescription1? description, bool allowWarpFallback)
        {
            SwapChainDescription1 desc;
            if (description.HasValue)
                desc = description.Value;
            else
                GetDefaultSwapChainDescription1(out desc);

#if DEBUG
            creationFlags |= DeviceCreationFlags.Debug;
#endif

            Func<SharpDX.Direct3D11.Device2> deviceCreator = () => CreateD3DDevice2(adapter, type, creationFlags, allowWarpFallback);

            Func<D3DMetaResource, SwapChain1> swapChainCreator =
                rm =>
                {
                    var size = rm.Size;
                    desc.Width = size.Width;
                    desc.Height = size.Height;
                    return new SwapChain1(rm.DxgiFactory, rm.Device, hwnd, ref desc, null, null);
                };

            return new D3DMetaResource(deviceCreator, swapChainCreator, );
        }

        private static Device CreateD3DDevice(Adapter adapter, DriverType type, DeviceCreationFlags flags,
            bool allowWarpFallback)
        {
            if (adapter != null) return new Device(adapter, flags);
            try
            {
                return new Device(type, flags);
            }
            catch
            {
                if (allowWarpFallback && (type != DriverType.Warp))
                    return new Device(DriverType.Warp, flags);
                throw;
            }
        }

        private static SharpDX.Direct3D11.Device2 CreateD3DDevice2(Adapter adapter, DriverType type, DeviceCreationFlags flags,
            bool allowWarpFallback)
        {
            using (var device = CreateD3DDevice(adapter, type, flags, allowWarpFallback))
            {
                return device.QueryInterface<SharpDX.Direct3D11.Device2>();
            }
        }

        public static SwapEffect GetBestSwapEffectForPlatform(Version version = null)
        {
            if (version == null)
                version = Environment.OSVersion.Version;
            if (version.Major > 6) return SwapEffect.FlipDiscard; // Win 10+
            if ((version.Major > 5) && (version.Minor > 1)) return SwapEffect.FlipSequential; // 6.2+ - Win 8+
            return SwapEffect.Discard;
        }

        public static void GetDefaultSwapChainDescription(out SwapChainDescription1 swapChainDescription)
        {
            swapChainDescription = new SwapChainDescription1
            {
                SampleDescription = new SampleDescription(1, 0),
                Usage = Usage.RenderTargetOutput,
                BufferCount = 2,
                // OutputHandle is also set by the resource manager
                SwapEffect = GetBestSwapEffectForPlatform(),
                Scaling = Scaling.None,
                Format = Format.B8G8R8A8_UNorm,
                AlphaMode = AlphaMode.Ignore,
                Width = 0,
                Height = 0,
            };
        }
    }
}