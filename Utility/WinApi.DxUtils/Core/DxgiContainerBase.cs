using System.Collections.Generic;
using System.Linq;

namespace WinApi.DxUtils.Core
{
    public abstract class DxgiContainerBase
    {
        private List<IDxgiConnectable> m_connectedResources;
        protected List<IDxgiConnectable> LinkedResources
        {
            get { return m_connectedResources ?? (m_connectedResources = new List<IDxgiConnectable>()); }
            set { m_connectedResources = value; }
        }

        public void AddLinkedResource(IDxgiConnectable resource)
        {
            if (LinkedResources.Contains(resource)) return;
            LinkedResources.Add(resource);
        }

        public void AddLinkedResources(IEnumerable<IDxgiConnectable> resources)
        {
            LinkedResources.AddRange(resources.Except(LinkedResources));
        }

        public void RemoveLinkedResource(IDxgiConnectable resource)
        {
            LinkedResources.Remove(resource);
        }

        public void RemoveLinkedResources(IEnumerable<IDxgiConnectable> resources)
        {
            foreach (var res in resources)
            {
                LinkedResources.Remove(res);
            }
        }

        protected void DisconnectLinkedResources()
        {
            foreach (var res in LinkedResources)
            {
                res.DisconnectFromDxgi();
            }
        }

        protected void ConnectLinkedResources()
        {
            foreach (var res in LinkedResources)
            {
                res.ConnectToDxgi();
            }
        }
    }
}