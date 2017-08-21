using System;
using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using AlphaMode = SharpDX.DXGI.AlphaMode;
using Device = SharpDX.Direct3D11.Device;
using Device1 = SharpDX.Direct3D11.Device1;

namespace WinApi.DxUtils.D3D11
{
    public class D3D11MetaFactory
    {
        public static D3D11MetaResource CreateForComposition(Adapter adapter, DeviceCreationFlags creationFlags = 0,
            SwapChainDescription1? description = null, FeatureLevel[] levels = null)
        {
            return CreateCore(adapter, DriverType.Null, creationFlags, description, levels, false,
                SwapChainTarget.Composition);
        }

        public static D3D11MetaResource CreateForComposition(DriverType type = DriverType.Hardware,
            DeviceCreationFlags creationFlags = 0,
            SwapChainDescription1? description = null, FeatureLevel[] levels = null)
        {
            return CreateCore(null, type, creationFlags, description, levels, false, SwapChainTarget.Composition);
        }

        public static D3D11MetaResource CreateForWindowTarget(Adapter adapter, DeviceCreationFlags creationFlags = 0,
            SwapChainDescription1? description = null, FeatureLevel[] levels = null)
        {
            return CreateCore(adapter, DriverType.Null, creationFlags, description, levels, false,
                SwapChainTarget.Window);
        }

        public static D3D11MetaResource CreateForWindowTarget(DriverType type = DriverType.Hardware,
            DeviceCreationFlags creationFlags = 0,
            SwapChainDescription1? description = null, FeatureLevel[] levels = null)
        {
            return CreateCore(null, type, creationFlags, description, levels, false, SwapChainTarget.Window);
        }

        public static D3D11MetaResource CreateForCoreWindowTarget(Adapter adapter, DeviceCreationFlags creationFlags = 0,
            SwapChainDescription1? description = null, FeatureLevel[] levels = null)
        {
            return CreateCore(adapter, DriverType.Null, creationFlags, description, levels, false,
                SwapChainTarget.CoreWindow);
        }

        public static D3D11MetaResource CreateForCoreWindowTarget(DriverType type = DriverType.Hardware,
            DeviceCreationFlags creationFlags = 0,
            SwapChainDescription1? description = null, FeatureLevel[] levels = null)
        {
            return CreateCore(null, type, creationFlags, description, levels, false, SwapChainTarget.CoreWindow);
        }

        private static D3D11MetaResource CreateCore(Adapter adapter, DriverType type,
            DeviceCreationFlags creationFlags,
            SwapChainDescription1? description, FeatureLevel[] levels, bool allowWarpFallbackDriver,
            SwapChainTarget target)
        {
            SwapChainDescription1 desc;
            if (description.HasValue) desc = description.Value;
            else GetDefaultSwapChainDescription(out desc, target);

#if DEBUG
            // Note: These have no impact on solution outside
            // of this project. This is only for internal testing
            // purposes
            creationFlags |= DeviceCreationFlags.Debug;
#endif

            return new D3D11MetaResource(new D3D11MetaResourceOptions
            {
                Adapter = adapter,
                SwapChainDescription = desc,
                Target = target,
                Type = type,
                WarpFallbackEnabled = allowWarpFallbackDriver,
                CreationFlags = creationFlags,
                Levels = levels
            });
        }

        public static Device1 CreateD3DDevice1(D3D11DxgiOptions creationOpts)
        {
            using (var device = CreateD3DDevice(creationOpts)) { return device.QueryInterface<Device1>(); }
        }

        public static Device CreateD3DDevice(D3D11DxgiOptions creationOpts)
        {
            var adapter = creationOpts.Adapter;
            var flags = creationOpts.CreationFlags;
            var levels = creationOpts.Levels;
            var type = creationOpts.Type;
            var allowWarpFallback = creationOpts.WarpFallbackEnabled;

            if (adapter != null) return new Device(adapter, flags);
            try { return levels != null ? new Device(type, flags, levels) : new Device(type, flags); }
            catch
            {
                const DriverType warpDriverType = DriverType.Warp;
                if (allowWarpFallback && (type != warpDriverType))
                    return levels != null
                        ? new Device(warpDriverType, flags, levels)
                        : new Device(warpDriverType, flags);
                throw;
            }
        }

        public static SwapChain1 CreateSwapChain1(D3D11MetaResourceOptions creationOpts, D3D11MetaResource resource)
        {
            creationOpts.SwapChainDescription.Width = resource.Size.Width;
            creationOpts.SwapChainDescription.Height = resource.Size.Height;

            switch (creationOpts.Target)
            {
                case SwapChainTarget.Composition:
                    return new SwapChain1(resource.DxgiFactory2, resource.Device1, ref creationOpts.SwapChainDescription);
                case SwapChainTarget.Window:
                    return new SwapChain1(resource.DxgiFactory2, resource.Device1, resource.Hwnd,
                        ref creationOpts.SwapChainDescription);
                case SwapChainTarget.CoreWindow:
                    using (var comObject = new ComObject(resource.Hwnd))
                    {
                        return new SwapChain1(resource.DxgiFactory2, resource.Device1, comObject,
                            ref creationOpts.SwapChainDescription);
                    }
            }
            return null;
        }

        public static SwapEffect GetBestSwapEffectForPlatform(Version version = null)
        {
            if (version == null) version = VersionHelpers.GetWindowsVersion();
            if (version.Major > 6) return SwapEffect.FlipDiscard; // Win 10+
            if ((version.Major > 5) && (version.Minor > 1)) return SwapEffect.FlipSequential; // 6.2+ - Win 8+
            return SwapEffect.Discard;
        }

        public static void GetDefaultSwapChainDescription(out SwapChainDescription1 swapChainDescription,
            SwapChainTarget target)
        {
            swapChainDescription = new SwapChainDescription1
            {
                SampleDescription = new SampleDescription(1, 0),
                Usage = Usage.RenderTargetOutput,
                BufferCount = 2,
                // OutputHandle is also set by the resource manager
                SwapEffect = GetBestSwapEffectForPlatform(),
                Scaling = Scaling.Stretch,
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

    public class D3D11DxgiOptions
    {
        public Adapter Adapter;
        public DeviceCreationFlags CreationFlags;
        public FeatureLevel[] Levels;
        public DriverType Type;
        public bool WarpFallbackEnabled;

        public D3D11DxgiOptions() {}

        public D3D11DxgiOptions(DriverType type,
            DeviceCreationFlags flags = DeviceCreationFlags.BgraSupport | DeviceCreationFlags.SingleThreaded,
            FeatureLevel[] levels = null, bool warpFallbackEnabled = true)
        {
            this.Type = type;
            this.CreationFlags = flags;
            this.Levels = levels;
            this.WarpFallbackEnabled = warpFallbackEnabled;
        }

        public D3D11DxgiOptions(Adapter adapter,
            DeviceCreationFlags flags = DeviceCreationFlags.BgraSupport | DeviceCreationFlags.SingleThreaded,
            FeatureLevel[] levels = null, bool warpFallbackEnabled = true)
        {
            this.Adapter = adapter;
            this.CreationFlags = flags;
            this.Levels = levels;
            this.WarpFallbackEnabled = warpFallbackEnabled;
        }
    }

    public class D3D11MetaResourceOptions : D3D11DxgiOptions
    {
        public SwapChainDescription1 SwapChainDescription;
        public SwapChainTarget Target;
    }
}