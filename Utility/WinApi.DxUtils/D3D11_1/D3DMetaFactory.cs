using System;
using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using WinApi.Core;
using WinApi.DxUtils.D3D11_1;
using AlphaMode = SharpDX.DXGI.AlphaMode;
using Device = SharpDX.Direct3D11.Device;
using Device2 = SharpDX.Direct3D11.Device2;

namespace WinApi.DxUtils.D3D11_1
{
    public class D3DMetaFactory
    {
        public static D3DMetaResource CreateForComposition(Adapter adapter, DeviceCreationFlags creationFlags = 0,
            SwapChainDescription1? description = null, FeatureLevel[] levels = null)
        {
            return CreateCore(adapter, DriverType.Null, creationFlags, description, levels, false,
                SwapChainTarget.Composition);
        }

        public static D3DMetaResource CreateForComposition(DriverType type = DriverType.Hardware,
            DeviceCreationFlags creationFlags = 0,
            SwapChainDescription1? description = null, FeatureLevel[] levels = null)
        {
            return CreateCore(null, type, creationFlags, description, levels, false, SwapChainTarget.Composition);
        }

        public static D3DMetaResource CreateForWindowTarget(Adapter adapter, DeviceCreationFlags creationFlags = 0,
            SwapChainDescription1? description = null, FeatureLevel[] levels = null)
        {
            return CreateCore(adapter, DriverType.Null, creationFlags, description, levels, false,
                SwapChainTarget.Window);
        }

        public static D3DMetaResource CreateForWindowTarget(DriverType type = DriverType.Hardware,
            DeviceCreationFlags creationFlags = 0,
            SwapChainDescription1? description = null, FeatureLevel[] levels = null)
        {
            return CreateCore(null, type, creationFlags, description, levels, false, SwapChainTarget.Window);
        }

        public static D3DMetaResource CreateForCoreWindowTarget(Adapter adapter, DeviceCreationFlags creationFlags = 0,
            SwapChainDescription1? description = null, FeatureLevel[] levels = null)
        {
            return CreateCore(adapter, DriverType.Null, creationFlags, description, levels, false,
                SwapChainTarget.CoreWindow);
        }

        public static D3DMetaResource CreateForCoreWindowTarget(DriverType type = DriverType.Hardware,
            DeviceCreationFlags creationFlags = 0,
            SwapChainDescription1? description = null, FeatureLevel[] levels = null)
        {
            return CreateCore(null, type, creationFlags, description, levels, false, SwapChainTarget.CoreWindow);
        }

        public static D3DMetaResource Create(Adapter adapter, DeviceCreationFlags creationFlags = 0,
            SwapChainDescription1? description = null, FeatureLevel[] levels = null, SwapChainTarget target = 0)
        {
            return CreateCore(adapter, DriverType.Null, creationFlags, description, levels, false, target);
        }

        public static D3DMetaResource Create(DriverType type = DriverType.Hardware,
            DeviceCreationFlags creationFlags = DeviceCreationFlags.SingleThreaded,
            SwapChainDescription1? description = null, FeatureLevel[] levels = null, bool allowWarpFallbackDriver = true,
            SwapChainTarget target = 0)
        {
            return CreateCore(null, type, creationFlags, description, levels, allowWarpFallbackDriver, target);
        }

        private static D3DMetaResource CreateCore(Adapter adapter, DriverType type,
            DeviceCreationFlags creationFlags,
            SwapChainDescription1? description, FeatureLevel[] levels, bool allowWarpFallbackDriver,
            SwapChainTarget target)
        {
            SwapChainDescription1 desc;
            if (description.HasValue)
                desc = description.Value;
            else
                GetDefaultSwapChainDescription(out desc, target);

#if DEBUG
            creationFlags |= DeviceCreationFlags.Debug;
#endif

            Func<Device2> deviceCreator =
                () => CreateD3DDevice2(adapter, type, creationFlags, levels, allowWarpFallbackDriver);

            Func<D3DMetaResource, SwapChain1> swapChainCreator = null;
            switch (target)
            {
                case SwapChainTarget.Composition:
                {
                    swapChainCreator = rm =>
                    {
                        var s = rm.Size;
                        desc.Width = s.Width;
                        desc.Height = s.Height;
                        return new SwapChain1(rm.DxgiFactory2, rm.Device1, ref desc);
                    };
                    break;
                }
                case SwapChainTarget.Window:
                {
                    swapChainCreator = rm => {
                        var s = rm.Size;
                        desc.Width = s.Width;
                        desc.Height = s.Height;
                        return new SwapChain1(rm.DxgiFactory2, rm.Device1, rm.Hwnd, ref desc);
                    };
                        break;
                }
                case SwapChainTarget.CoreWindow:
                {
                    swapChainCreator = rm =>
                    {
                        var s = rm.Size;
                        desc.Width = s.Width;
                        desc.Height = s.Height;
                        using (var comObject = new ComObject(rm.Hwnd))
                            return new SwapChain1(rm.DxgiFactory2, rm.Device1, comObject, ref desc);
                    };
                    break;
                }
            }
            return new D3DMetaResource(deviceCreator, swapChainCreator);
        }

        public static Device2 CreateD3DDevice2(Adapter adapter, DriverType type, DeviceCreationFlags flags,
            FeatureLevel[] levels, bool allowWarpFallback)
        {
            using (var device = D3D11.D3DMetaFactory.CreateD3DDevice(adapter, type, flags, levels, allowWarpFallback))
            {
                return device.QueryInterface<Device2>();
            }
        }

        public static SwapEffect GetBestSwapEffectForPlatform(Version version = null)
        {
            return D3D11.D3DMetaFactory.GetBestSwapEffectForPlatform(version);
        }

        public static void GetDefaultSwapChainDescription(out SwapChainDescription1 swapChainDescription, SwapChainTarget target)
        {
            swapChainDescription = new SwapChainDescription1
            {
                SampleDescription = new SampleDescription(1, 0),
                Usage = Usage.RenderTargetOutput,
                BufferCount = 2,
                // OutputHandle is also set by the resource manager
                SwapEffect = GetBestSwapEffectForPlatform(),
                Scaling = target == SwapChainTarget.Composition ? Scaling.Stretch : Scaling.None,
                Format = Format.B8G8R8A8_UNorm,
                AlphaMode = target == SwapChainTarget.Window ? AlphaMode.Ignore : AlphaMode.Premultiplied,
                Width = 0,
                Height = 0
            };
        }
    }

    public enum SwapChainTarget
    {
        Composition,
        Window,
        CoreWindow
    }
}