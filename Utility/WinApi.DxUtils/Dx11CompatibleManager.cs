using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct2D1;
using SharpDX.Direct3D11;
using SharpDX.DirectComposition;
using SharpDX.DXGI;
using WinApi.Core;
using WinApi.DxUtils.Core;
using AlphaMode = SharpDX.Direct2D1.AlphaMode;
using Factory = SharpDX.DirectWrite.Factory;
using FactoryType = SharpDX.DirectWrite.FactoryType;

namespace WinApi.DxUtils
{
    public class Dx11CompatibleManager : IDisposable
    {
        private ID2D1MetaResourceImpl m_d2D;
        private ID3D11MetaResourceImpl m_d3D;
        private SharpDX.DirectComposition.Device2 m_dCompDevice;
        private Factory m_dWriteFactory;

        public ID2D1MetaResource D2D => m_d2D;
        public ID3D11MetaResource D3D => m_d3D;
        public Factory TextFactory => m_dWriteFactory;

        public void Dispose()
        {
            Destroy();
        }

        public void Initialize(IntPtr hwnd, ref Size size, bool useDCompositionIfPossible = true)
        {
            if (m_d3D != null)
                Destroy();
            Create(useDCompositionIfPossible);
            InitializeInternal(hwnd, ref size, useDCompositionIfPossible);
        }

        public void Resize(ref Size size)
        {
            m_d3D?.Resize(ref size);
        }

        private void InitializeInternal(IntPtr hwnd, ref Size size, bool useDCompositionIfPossible)
        {
            m_d3D.Initalize(hwnd, size);
            m_d2D.Initialize(m_d3D, true);
            if (useDCompositionIfPossible)
            {
                var target = Target.FromHwnd((DesktopDevice) m_dCompDevice, hwnd, false);
                target.Root = new Visual2(m_dCompDevice)
                {
                    Content = m_d3D.SwapChain
                };
                m_dCompDevice.Commit();
            }
        }

        private void Create(bool autoUseDComposition)
        {
            var minorVersion = PlatformHelpers.GetPlatformDefaultDx11Minor();
            var d3dCreationFlags = DeviceCreationFlags.BgraSupport | DeviceCreationFlags.SingleThreaded;
            if (minorVersion >= 1)
            {
                m_d3D = autoUseDComposition
                    ? D3D11_1.D3DMetaFactory.CreateForComposition(creationFlags: d3dCreationFlags)
                    : D3D11_1.D3DMetaFactory.CreateForWindowTarget(creationFlags: d3dCreationFlags);
                m_d2D = D2D1_1.D2DMetaFactory.Create();
            }
            else
            {
                SwapChainDescription swapChainDesc;
                D3D11.D3DMetaFactory.GetDefaultSwapChainDescription(out swapChainDesc);
                m_d3D = D3D11.D3DMetaFactory.Create(creationFlags: d3dCreationFlags, description: swapChainDesc);
                RenderTargetProperties props;
                D2D1.D2DMetaFactory.GetDefaultRenderTargetProperties(out props);
                props.PixelFormat.Format = Format.B8G8R8A8_UNorm;
                props.PixelFormat.AlphaMode = AlphaMode.Premultiplied;
                m_d2D = D2D1.D2DMetaFactory.Create(props: props);
            }
            m_dWriteFactory = new Factory(FactoryType.Shared);
            if (autoUseDComposition)
            {
                m_dCompDevice = new DesktopDevice(m_d3D.DxgiDevice);
            }
        }

        public void Destroy()
        {
            DisposableHelpers.DisposeAndSetNull(ref m_dCompDevice);
            DisposableHelpers.DisposeAndSetNull(ref m_dWriteFactory);
            DisposableHelpers.DisposeAndSetNull(ref m_d2D);
            DisposableHelpers.DisposeAndSetNull(ref m_d3D);
        }
    }

    public static class PlatformHelpers
    {
        public static int GetPlatformDefaultDx11Minor(Version platformVersion = null)
        {
            if (platformVersion == null)
                platformVersion = Environment.OSVersion.Version;
            if (platformVersion.Major > 6) return 3; // Win 10+
            if (platformVersion.Major > 5)
            {
                if (platformVersion.Minor > 2) return 2; // 6.3+ - Win 8.1
                if (platformVersion.Minor > 1) return 1; // 6.2+ - Win 8
            }
            return 0; // Win 7
            // It is assumed the version is atleast 7. Legacy OSes aren't considered.
        }
    }
}