using System;
using System.Collections.Generic;
using WinApi.Core;

namespace WinApi.DxUtils.Core
{
    public interface IDxgi1
    {
        SharpDX.DXGI.Device DxgiDevice { get; }
        SharpDX.DXGI.Factory DxgiFactory { get; }
        SharpDX.DXGI.SwapChain SwapChain { get; }
    }

    // ReSharper disable once InconsistentNaming
    public interface IDxgi1_2
    {
        SharpDX.DXGI.Device2 DxgiDevice2 { get; }
        SharpDX.DXGI.Factory2 DxgiFactory2 { get; }
        SharpDX.DXGI.SwapChain1 SwapChain1 { get; }
    }

    public interface IDxgi1Container : IDxgiContainerCore, IDxgi1 {}

    public interface ID3D11MetaResource : IDxgi1Container, IDisposable
    {
        SharpDX.DXGI.Adapter Adapter { get; }
        SharpDX.Direct3D11.DeviceContext Context { get; }
        SharpDX.Direct3D11.RenderTargetView RenderTargetView { get; }
    }

    public interface ID3D11MetaResourceImpl : ID3D11MetaResource
    {
        void Initalize(IntPtr hwnd, Size size);
        void Resize(ref Size size);
    }

    // ReSharper disable once InconsistentNaming
    public interface IDxgi1_2Container : IDxgi1Container, IDxgi1_2 {}

    // ReSharper disable once InconsistentNaming
    public interface ID3D11_1MetaResource : IDxgi1_2Container, ID3D11MetaResource
    {
        SharpDX.Direct3D11.DeviceContext1 Context1 { get; }
    }

    // ReSharper disable once InconsistentNaming
    public interface ID3D11_1MetaResourceImpl : ID3D11MetaResourceImpl, ID3D11_1MetaResource {}

    public interface ID2D1MetaResource : IDxgiConnectable, IDisposable
    {
        SharpDX.Direct2D1.RenderTarget RenderTarget { get; }
        SharpDX.Direct2D1.Factory Factory { get; }
    }

    public interface ID2D1MetaResourceImpl : ID2D1MetaResource
    {
        void Initialize(IDxgi1Container dxgiContainer, bool autoLink = false);
    }

    // ReSharper disable once InconsistentNaming
    public interface ID2D1_1MetaResource : ID2D1MetaResource
    {
        SharpDX.Direct2D1.Device Device { get; }
        SharpDX.Direct2D1.DeviceContext Context { get; }
        SharpDX.Direct2D1.Factory1 Factory1 { get; }
    }

    // ReSharper disable once InconsistentNaming
    public interface ID2D1_1MetaResourceImpl : ID2D1_1MetaResource, ID2D1MetaResourceImpl {}

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
}