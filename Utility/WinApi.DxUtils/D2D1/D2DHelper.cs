using System;
using SharpDX.Direct2D1;
using SharpDX.DXGI;
using WinApi.DxUtils.Core;

namespace WinApi.DxUtils.D2D1
{
    public static class D2DHelper
    {
        public static void ConnectContextToDxgiSwapChain(D2DMetaResource<IDxgi1_2ContainerWithSwapChain> d2DMetaResource)
        {
            using (var surface = d2DMetaResource.DxgiContainer.SwapChain1.GetBackBuffer<Surface>(0))
            {
                using (var bitmap = new Bitmap1(d2DMetaResource.Context, surface))
                    d2DMetaResource.Context.Target = bitmap;
            }
        }

        public static void DisconnectContextFromDxgiSwapChain(D2DMetaResource<IDxgi1_2ContainerWithSwapChain> d2DMetaResource)
        {
            var currentTarget = d2DMetaResource.Context?.Target;
            if (currentTarget == null) return;
            currentTarget.Dispose();
            d2DMetaResource.Context.Target = null;
        }
    }
}