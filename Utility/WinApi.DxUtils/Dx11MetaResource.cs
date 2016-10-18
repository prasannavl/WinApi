using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using WinApi.Core;
using WinApi.DxUtils.Core;
using WinApi.DxUtils.D2D1;
using WinApi.DxUtils.D3D11;
using WinApi.DxUtils.DComp;
using Factory = SharpDX.DirectWrite.Factory;
using FactoryType = SharpDX.DirectWrite.FactoryType;

namespace WinApi.DxUtils
{
    public class Dx11MetaResource : IDisposable
    {
        public IntPtr Hwnd;
        private ID2D1_1MetaResourceImpl<IDxgi1_2ContainerWithSwapChain> m_d2D;
        private ID3D11_1MetaResourceImpl m_d3D;
        private WindowSwapChainCompositor m_windowSwapChainCompositor;
        private Factory m_dWriteFactory;
        public Size Size;
        private int m_dCompVariant;

        public ID2D1_1MetaResource D2D => m_d2D;
        public ID3D11MetaResource D3D => m_d3D;
        public Factory TextFactory => m_dWriteFactory;

        public void Dispose()
        {
            Destroy();
        }

        public void Initialize(IntPtr hwnd, Size size, int directCompositionVariant = -1)
        {
            if (m_d3D != null)
                Destroy();
            Hwnd = hwnd;
            Size = size;
            m_dCompVariant = directCompositionVariant != -1
                ? directCompositionVariant
                : DCompHelper.GetVariantForPlatform();
            Create();
            InitializeInternal();
        }

        public void EnsureInitialized()
        {
            if (m_d3D?.Device == null)
                Initialize(Hwnd, Size, m_dCompVariant);
        }

        public void Resize(Size size)
        {
            if (m_dCompVariant > 0 && (size.Width <= 0 || size.Height <= 0)) return; 
            Size = size;
            m_d3D?.Resize(size);
        }

        private void InitializeInternal()
        {
            m_d3D.Initalize(Hwnd, Size);
            m_d2D.Initialize(m_d3D);
            if (m_dCompVariant > 0) m_windowSwapChainCompositor.Initialize(Hwnd, m_d3D, false);
        }

        private void Create()
        {
            var d3dCreationFlags = DeviceCreationFlags.BgraSupport | DeviceCreationFlags.SingleThreaded;
            m_d3D = m_dCompVariant > 0
                ? D3DMetaFactory.CreateForComposition(creationFlags: d3dCreationFlags)
                : D3DMetaFactory.CreateForWindowTarget(creationFlags: d3dCreationFlags);
            m_d2D = D2DMetaFactory.CreateForSwapChain();
            m_dWriteFactory = new Factory(FactoryType.Shared);
            m_windowSwapChainCompositor = new WindowSwapChainCompositor(m_dCompVariant);
        }

        public bool PerformResetOnException(SharpDXException ex)
        {
            if (ErrorHelpers.ShouldResetDxgiForError(ex.Descriptor)
                || ErrorHelpers.ShouldResetD2DForError(ex.Descriptor))
            {
                Debug.WriteLine("Dx11MetaManger: ResetOnException => ", ex.Message);
                Destroy();
                return true;
            }
            return false;
        }

        public void Destroy()
        {
            DisposableHelpers.DisposeAndSetNull(ref m_windowSwapChainCompositor);
            DisposableHelpers.DisposeAndSetNull(ref m_dWriteFactory);
            DisposableHelpers.DisposeAndSetNull(ref m_d2D);
            DisposableHelpers.DisposeAndSetNull(ref m_d3D);
        }
    }
}