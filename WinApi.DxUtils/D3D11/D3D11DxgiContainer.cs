using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using WinApi.DxUtils.Core;
using Device = SharpDX.Direct3D11.Device;
using Device1 = SharpDX.Direct3D11.Device1;

namespace WinApi.DxUtils.D3D11
{
    public abstract class D3D11Dxgi1ContainerCore : DxgiContainerBase, IDxgi1Container
    {
        public virtual Device Device { get; protected set; }
        public virtual SharpDX.DXGI.Device DxgiDevice { get; protected set; }
        public virtual Factory DxgiFactory { get; protected set; }
        public virtual Adapter Adapter { get; protected set; }

        protected void EnsureDxgiDevice()
        {
            if (this.DxgiDevice == null) this.CreateDxgiDevice();
        }

        protected abstract void CreateDxgiDevice();

        protected void EnsureAdapter()
        {
            if (this.Adapter == null) this.CreateAdapter();
        }

        protected abstract void CreateAdapter();

        protected void EnsureDxgiFactory()
        {
            if (this.DxgiFactory == null) this.CreateDxgiFactory();
        }

        protected abstract void CreateDxgiFactory();

        protected void EnsureDevice()
        {
            if (this.Device == null) this.CreateDevice();
        }

        protected abstract void CreateDevice();
    }

    // ReSharper disable once InconsistentNaming
    public abstract class D3D11Dxgi1_2ContainerCore : D3D11Dxgi1ContainerCore, IDxgi1_2Container
    {
        public override Device Device => this.Device1;
        public virtual Device1 Device1 { get; protected set; }
        public override Factory DxgiFactory => this.DxgiFactory2;
        public override SharpDX.DXGI.Device DxgiDevice => this.DxgiDevice2;

        public virtual Factory2 DxgiFactory2 { get; protected set; }
        public virtual SharpDX.DXGI.Device2 DxgiDevice2 { get; protected set; }
    }
}