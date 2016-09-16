using System;
using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;

namespace Sample.Win32
{
    public class MainWindow2
    {
        private DeviceContext m_dxContext;
        private Device m_dxDevice;
        private RenderTargetView m_renderTargetView;
        private SharpDX.DXGI.SwapChain m_swapChain;

        private void InitializeDxResources()
        {
            var swapChainDesc = new SharpDX.DXGI.SwapChainDescription
            {
                ModeDescription =
                    new SharpDX.DXGI.ModeDescription(0, 0, new SharpDX.DXGI.Rational(60, 1),
                        SharpDX.DXGI.Format.R8G8B8A8_UNorm),
                SampleDescription = new SharpDX.DXGI.SampleDescription(1, 0),
                Usage = SharpDX.DXGI.Usage.RenderTargetOutput,
                BufferCount = 1,
                OutputHandle = IntPtr.Zero,
                IsWindowed = true,
                SwapEffect = SharpDX.DXGI.SwapEffect.Discard
            };

            // Create device and swap chain
            Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.None,
                swapChainDesc, out m_dxDevice,
                out m_swapChain);

            // Set the device context
            m_dxContext = m_dxDevice.ImmediateContext;
            SetupRenderTargetView();
        }


        private void SetupRenderTargetView()
        {
            // Set render target
            using (var backBuffer = m_swapChain.GetBackBuffer<Texture2D>(0))
            {
                m_renderTargetView = new RenderTargetView(m_dxDevice, backBuffer);
            }
            m_dxContext.OutputMerger.SetRenderTargets(m_renderTargetView);
        }

        private void DisposeDxResources()
        {
            m_dxContext.OutputMerger.ResetTargets();
            m_renderTargetView.Dispose();
            m_dxContext.Dispose();
            ((IUnknown) m_dxDevice).Release();
            m_dxDevice.Dispose();
            m_swapChain.Dispose();
        }

        private void ResizeDxResources()
        {
            m_dxContext.OutputMerger.ResetTargets();
            m_renderTargetView.Dispose();
            m_swapChain.ResizeBuffers(1, 0, 0, SharpDX.DXGI.Format.B8G8R8A8_UNorm, SharpDX.DXGI.SwapChainFlags.None);
            SetupRenderTargetView();
        }
    }
}