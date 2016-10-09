using System.Collections.Generic;
using SharpDX.DXGI;

namespace WinApi.DxUtils.Core
{
    public interface IDxgiContainerCore
    {
        void AddLinkedResource(IDxgiConnectable resource);
        void AddLinkedResources(IEnumerable<IDxgiConnectable> resources);
        void RemoveLinkedResource(IDxgiConnectable resource);
        void RemoveLinkedResources(IEnumerable<IDxgiConnectable> resources);
    }

    public interface IDxgiConnectable
    {
        void ConnectToDxgi();
        void DisconnectFromDxgi();
    }

    public interface IDxgi1
    {
        Device DxgiDevice { get; }
        Factory DxgiFactory { get; }
        SwapChain SwapChain { get; }
    }

    // ReSharper disable once InconsistentNaming
    public interface IDxgi1_2
    {
        Device2 DxgiDevice2 { get; }
        Factory2 DxgiFactory2 { get; }
        SwapChain1 SwapChain1 { get; }
    }

    public interface IDxgi1Container : IDxgiContainerCore, IDxgi1 { }
    // ReSharper disable once InconsistentNaming
    public interface IDxgi1_2Container : IDxgi1Container, IDxgi1_2 { }
}