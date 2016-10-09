using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.DirectWrite;
using WinApi.Utils;

namespace WinApi.DxUtils
{
    public class DxMetaResource: IDisposable
    {
        private D2DMetaResource m_d2D;
        private D3DMetaResource m_d3D;
        private Factory m_dWrite;

        public D2DMetaResource D2D
        {
            get { return m_d2D; }
            private set { m_d2D = value; }
        }

        public D3DMetaResource D3D
        {
            get { return m_d3D; }
            private set { m_d3D = value; }
        }

        public SharpDX.DirectWrite.Factory DWrite
        {
            get { return m_dWrite; }
            private set { m_dWrite = value; }
        }

        public void Initialize()
        {
            
        }

        public void Destroy()
        {
            DisposableHelpers.DisposeAndSetNull(ref m_dWrite);
            DisposableHelpers.DisposeAndSetNull(ref m_d2D);
            DisposableHelpers.DisposeAndSetNull(ref m_d3D);
        }

        public void Dispose()
        {
            this.Destroy();
        }
    }
}
