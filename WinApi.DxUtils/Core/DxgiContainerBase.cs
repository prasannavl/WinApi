using System;
using System.Collections.Generic;
using System.Linq;
using SharpDX.DXGI;

namespace WinApi.DxUtils.Core
{
    public abstract class DxgiContainerBase
    {
        private List<IDxgiConnectable> m_connectedResources;

        protected List<IDxgiConnectable> LinkedResources
        {
            get { return this.m_connectedResources ?? (this.m_connectedResources = new List<IDxgiConnectable>()); }
            set { this.m_connectedResources = value; }
        }

        public event Action Initialized;
        public event Action Destroyed;

        protected void InvokeInitializedEvent()
        {
            this.Initialized?.Invoke();
        }

        protected void InvokeDestroyedEvent()
        {
            this.Destroyed?.Invoke();
        }

        public void AddLinkedResource(IDxgiConnectable resource)
        {
            if (this.LinkedResources.Contains(resource)) return;
            this.LinkedResources.Add(resource);
        }

        public void AddLinkedResources(IEnumerable<IDxgiConnectable> resources)
        {
            this.LinkedResources.AddRange(resources.Except(this.LinkedResources));
        }

        public void RemoveLinkedResource(IDxgiConnectable resource)
        {
            this.LinkedResources.Remove(resource);
        }

        public void RemoveLinkedResources(IEnumerable<IDxgiConnectable> resources)
        {
            foreach (var res in resources) { this.LinkedResources.Remove(res); }
        }

        protected void DisconnectLinkedResources()
        {
            foreach (var res in this.LinkedResources) { res.DisconnectFromDxgi(); }
        }

        protected void ConnectLinkedResources()
        {
            foreach (var res in this.LinkedResources) { res.ConnectToDxgi(); }
        }
    }
}