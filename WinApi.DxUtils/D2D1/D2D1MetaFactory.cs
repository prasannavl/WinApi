using SharpDX.Direct2D1;
using WinApi.DxUtils.Core;

namespace WinApi.DxUtils.D2D1
{
    public class D2D1MetaFactory
    {
        public static D2D1MetaResource<IDxgi1Container> Create(ref CreationProperties props)
        {
            return CreateCore(ref props);
        }

        public static D2D1MetaResource<IDxgi1Container> Create(
            ThreadingMode threadingMode = ThreadingMode.SingleThreaded,
            DeviceContextOptions contextOptions = DeviceContextOptions.EnableMultithreadedOptimizations,
            DebugLevel debugLevel = DebugLevel.None)
        {
            var props = new CreationProperties
            {
                DebugLevel = debugLevel,
                ThreadingMode = threadingMode,
                Options = contextOptions
            };
            return CreateCore(ref props);
        }

        private static D2D1MetaResource<IDxgi1Container> CreateCore(
            ref CreationProperties props)
        {
#if DEBUG
            // Note: These have no impact on solution outside
            // of this project. This is only for internal testing
            // purposes
            if (props.DebugLevel == 0) props.DebugLevel = DebugLevel.Information;
#endif
            return new D2D1MetaResource<IDxgi1Container>(props, null, null);
        }

        public static D2D1MetaResource<IDxgi1_2ContainerWithSwapChain> CreateForSwapChain(ref CreationProperties props)
        {
            return CreateForSwapChainCore(ref props);
        }

        public static D2D1MetaResource<IDxgi1_2ContainerWithSwapChain> CreateForSwapChain(
            ThreadingMode threadingMode = ThreadingMode.SingleThreaded,
            DeviceContextOptions contextOptions = DeviceContextOptions.EnableMultithreadedOptimizations,
            DebugLevel debugLevel = DebugLevel.None)
        {
            var props = new CreationProperties
            {
                DebugLevel = debugLevel,
                ThreadingMode = threadingMode,
                Options = contextOptions
            };
            return CreateForSwapChainCore(ref props);
        }

        private static D2D1MetaResource<IDxgi1_2ContainerWithSwapChain> CreateForSwapChainCore(
            ref CreationProperties props)
        {
#if DEBUG
            // Note: These have no impact on solution outside
            // of this project. This is only for internal testing
            // purposes
            if (props.DebugLevel == 0) props.DebugLevel = DebugLevel.Information;
#endif
            return new D2D1MetaResource<IDxgi1_2ContainerWithSwapChain>(props, D2D1Helper.ConnectContextToDxgiSwapChain,
                D2D1Helper.DisconnectContextFromDxgiSwapChain);
        }
    }
}