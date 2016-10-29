using System;
using SharpDX.Direct2D1;
using SharpDX.DXGI;
using WinApi.DxUtils.Core;

namespace WinApi.DxUtils.D2D1
{
    public static class D2D1Helper
    {
        public static void ConnectContextToDxgiSwapChain(
            D2D1MetaResource<IDxgi1_2ContainerWithSwapChain> d2D1MetaResource)
        {
            using (var surface = d2D1MetaResource.DxgiContainer.SwapChain1.GetBackBuffer<Surface>(0))
            {
                using (var bitmap = new Bitmap1(d2D1MetaResource.Context, surface)) d2D1MetaResource.Context.Target = bitmap;
            }
        }

        public static void DisconnectContextFromDxgiSwapChain(
            D2D1MetaResource<IDxgi1_2ContainerWithSwapChain> d2D1MetaResource)
        {
            var currentTarget = d2D1MetaResource.Context?.Target;
            if (currentTarget == null) return;
            currentTarget.Dispose();
            d2D1MetaResource.Context.Target = null;
        }
    }
}