using System;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using Device = SharpDX.Direct3D11.Device;

namespace WinApi.DxUtils.D3D11
{
    public class D3DMetaFactory
    {
        public static D3DMetaResource Create(Adapter adapter, DeviceCreationFlags creationFlags = 0,
            SwapChainDescription? description = null)
        {
            return CreateCore(adapter, DriverType.Null, creationFlags, description, false);
        }

        public static D3DMetaResource Create(DriverType type = DriverType.Hardware,
            DeviceCreationFlags creationFlags = DeviceCreationFlags.SingleThreaded,
            SwapChainDescription? description = null, bool allowWarpFallbackDriver = true)
        {
            return CreateCore(null, type, creationFlags, description, allowWarpFallbackDriver);
        }

        private static D3DMetaResource CreateCore(Adapter adapter, DriverType type,
            DeviceCreationFlags creationFlags,
            SwapChainDescription? description, bool allowWarpFallbackDriver)
        {
            SwapChainDescription desc;
            if (description.HasValue)
                desc = description.Value;
            else
                GetDefaultSwapChainDescription(out desc);

#if DEBUG
            creationFlags |= DeviceCreationFlags.Debug;
#endif

            Func<Device> deviceCreator = () => CreateD3DDevice(adapter, type, creationFlags, allowWarpFallbackDriver);

            Func<D3DMetaResource, SwapChain> swapChainCreator =
                rm =>
                {
                    var size = rm.Size;
                    desc.ModeDescription.Width = size.Width;
                    desc.ModeDescription.Height = size.Height;
                    desc.OutputHandle = rm.Hwnd;
                    return new SwapChain(rm.DxgiFactory, rm.Device, desc);
                };

            return new D3DMetaResource(deviceCreator, swapChainCreator);
        }

        public static Device CreateD3DDevice(Adapter adapter, DriverType type, DeviceCreationFlags flags,
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

        public static SwapEffect GetBestSwapEffectForPlatform(Version version = null)
        {
            return SwapEffect.Discard;
        }

        public static void GetDefaultSwapChainDescription(out SwapChainDescription swapChainDescription)
        {
            swapChainDescription = new SwapChainDescription
            {
                // Mode description height and size is correctly set by the resource manager
                ModeDescription = new ModeDescription(0, 0, new Rational(60, 1), Format.B8G8R8A8_UNorm)
                { Scaling = DisplayModeScaling.Unspecified },
                SampleDescription = new SampleDescription(1, 0),
                Usage = Usage.RenderTargetOutput,
                BufferCount = 2,
                // OutputHandle is also set by the resource manager
                OutputHandle = IntPtr.Zero,
                IsWindowed = true,
                SwapEffect = GetBestSwapEffectForPlatform()
            };
        }
    }
}