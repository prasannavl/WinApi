using NetCoreEx.Geometry;
using System;
using System.Collections.Generic;
using WinApi.Core;

namespace WinApi.DxUtils.Core
{
    public interface IDxgi1
    {
        SharpDX.DXGI.Device DxgiDevice { get; }
        SharpDX.DXGI.Factory DxgiFactory { get; }
        SharpDX.DXGI.Adapter Adapter { get; }
    }

    public interface IDxgi1WithSwapChain : IDxgi1
    {
        SharpDX.DXGI.SwapChain SwapChain { get; }
    }

    // ReSharper disable once InconsistentNaming
    public interface IDxgi1_2
    {
        SharpDX.DXGI.Device2 DxgiDevice2 { get; }
        SharpDX.DXGI.Factory2 DxgiFactory2 { get; }
    }

    // ReSharper disable once InconsistentNaming
    public interface IDxgi1_2WithSwapChain : IDxgi1_2
    {
        SharpDX.DXGI.SwapChain1 SwapChain1 { get; }
    }

    public interface IDxgi1Container : IDxgiContainerCore, IDxgi1, INotifyOnInitDestroy {}

    public interface IDxgi1ContainerWithSwapChain : IDxgi1Container, IDxgi1WithSwapChain {}


    // ReSharper disable once InconsistentNaming
    public interface IDxgi1_2Container : IDxgi1Container, IDxgi1_2 {}

    // ReSharper disable once InconsistentNaming
    public interface IDxgi1_2ContainerWithSwapChain : IDxgi1ContainerWithSwapChain, IDxgi1_2WithSwapChain {}


    public interface IDxgi1MetaResource : IDxgi1Container
    {
        void Initialize();
        void EnsureInitialized();
        void Destroy();
    }

    public interface ID3D11MetaResourceCore : IDxgi1ContainerWithSwapChain, IDisposable
    {
        SharpDX.Direct3D11.Device Device { get; }
        SharpDX.Direct3D11.DeviceContext Context { get; }
        SharpDX.Direct3D11.RenderTargetView RenderTargetView { get; }
    }

    // ReSharper disable once InconsistentNaming
    public interface ID3D11_1MetaResourceCore : ID3D11MetaResourceCore, IDxgi1_2ContainerWithSwapChain {}

    public interface ID3D11MetaResource : ID3D11MetaResourceCore
    {
        void Initialize(IntPtr hwnd, Size size);
        void EnsureInitialized();
        void Resize(Size size);
        void Destroy();
    }

    // ReSharper disable once InconsistentNaming
    public interface ID3D11_1MetaResource : ID3D11MetaResource, ID3D11_1MetaResourceCore {}

    // ReSharper disable once InconsistentNaming
    public interface ID2D1_1MetaResourceCore : IDxgiConnectable, INotifyOnInitDestroy, IDisposable
    {
        SharpDX.Direct2D1.Device Device { get; }
        SharpDX.Direct2D1.DeviceContext Context { get; }
        SharpDX.Direct2D1.Factory1 Factory1 { get; }
    }

    // ReSharper disable once InconsistentNaming
    public interface ID2D1_1MetaResource<in TDxgi1Container> : ID2D1_1MetaResourceCore
    {
        void Initialize(TDxgi1Container dxgiContainer, bool autoLink = true);
        void EnsureInitialized();
        void Destroy();
    }

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

    public interface INotifyOnInitDestroy
    {
        event Action Initialized;
        event Action Destroyed;
    }
}