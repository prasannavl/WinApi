using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct2D1;
using SharpDX.DXGI;
using AlphaMode = SharpDX.Direct2D1.AlphaMode;

namespace WinApi.DxUtils.D2D1
{
    public class D2DMetaFactory
    {
        public static D2DMetaResource Create(ref CreationProperties props, ref RenderTargetProperties renderTargetProps)
        {
            return CreateCore(ref props, ref renderTargetProps);
        }

        public static D2DMetaResource Create(ThreadingMode threadingMode = ThreadingMode.SingleThreaded,
            DeviceContextOptions contextOptions = DeviceContextOptions.EnableMultithreadedOptimizations,
            DebugLevel debugLevel = DebugLevel.None, RenderTargetProperties? props = null)
        {
            var creationProps = new CreationProperties
            {
                DebugLevel = debugLevel,
                ThreadingMode = threadingMode,
                Options = contextOptions
            };

            RenderTargetProperties rProps;
            if (props.HasValue)
                rProps = props.Value;
            else
                GetDefaultRenderTargetProperties(out rProps);

            return CreateCore(ref creationProps, ref rProps);
        }

        private static D2DMetaResource CreateCore(ref CreationProperties props,
            ref RenderTargetProperties renderTargetProps)
        {
#if DEBUG
            if (props.DebugLevel == 0)
                props.DebugLevel = DebugLevel.Information;
#endif
            return new D2DMetaResource(props, renderTargetProps);
        }

        public static void GetDefaultRenderTargetProperties(out RenderTargetProperties props)
        {
            props = new RenderTargetProperties
            {
                PixelFormat = new PixelFormat(Format.B8G8R8A8_UNorm, AlphaMode.Premultiplied)
            };
        }
    }
}