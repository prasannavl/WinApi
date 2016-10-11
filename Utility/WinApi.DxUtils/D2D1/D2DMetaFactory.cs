using SharpDX.Direct2D1;

namespace WinApi.DxUtils.D2D1
{
    public class D2DMetaFactory
    {
        public static D2DMetaResource Create(ref CreationProperties props)
        {
            return CreateCore(ref props);
        }

        public static D2DMetaResource Create(ThreadingMode threadingMode = ThreadingMode.SingleThreaded,
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

        private static D2DMetaResource CreateCore(ref CreationProperties props)
        {
#if DEBUG
            if (props.DebugLevel == 0)
                props.DebugLevel = DebugLevel.Information;
#endif
            return new D2DMetaResource(props);
        }
    }
}
