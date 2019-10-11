# WinApi

A simple, direct, ultra-thin CLR library for high-performance Win32 Native Interop

[![NuGet badge](https://buildstats.info/nuget/WinApi)](https://www.nuget.org/packages/WinApi)
[![NuGet badge](https://buildstats.info/nuget/WinApi.Desktop)](https://www.nuget.org/packages/WinApi.Desktop)
[![NuGet badge](https://buildstats.info/nuget/WinApi.Utils)](https://www.nuget.org/packages/WinApi.Utils)
[![NuGet badge](https://buildstats.info/nuget/WinApi.DxUtils)](https://www.nuget.org/packages/WinApi.DxUtils)
[![NuGet badge](https://buildstats.info/nuget/WinApi.Windows.Controls)](https://www.nuget.org/packages/WinApi.Windows.Controls)

```c#
static int Main(string[] args)
{
    using (var win = Window.Create(text: "Hello"))
    {
        win.Show();
        return new EventLoop().Run(win);
    }
}
```

**Nuget:**
> Install-Package WinApi

Fully supports the **CoreCLR.** Uses C# 7 features like `ref returns` to achieve performance without losing semantic value.

### Articles
- <a href="https://www.prasannavl.com/2016/10/introducing-winapi-the-evolution/">Introducing WinApi: The Evolution</a>
- <a href="https://www.prasannavl.com/2016/10/introducing-winapi-basics/">Introducing WinApi: Basics</a>
- <a href="https://www.prasannavl.com/2016/10/introducing-winapi-graphics-with-direct3d-d2d1-gdi-opengl-and-skia">Introducing WinApi: Graphics with Direct3D, D2D1, GDI, OpenGL and Skia</a>
- <a href="https://www.prasannavl.com/2016/10/introducing-winapi-comparing-gc-pressure-and-performance-with-winforms">Introducing WinApi: Comparing GC pressure and performance with WinForms</a>

#### TL;DR: WinForms Comparison
```
Direct message loop performance: 20-35% faster.
Heap allocation: 0MB vs. roughly, 0.75GB / 100k messages.
Memory page faults (Soft): 0.005% - A mere 5k vs. roughly 1 million faults/100k messages)
```

### Packages
- **`WinApi`** - The core package that contains all the methods, helpers, and the tiny `WinApi.Windows` namespace.
- **`WinApi.Desktop`** - Desktop-only helpers.
- **`WinApi.Utils`** - Provides utilities like `NativePixelBuffer`, `DwmWindow` etc.
- **`WinApi.DxUtils`** - Provides DirectX utilities that ease the version management of SharpDX factories, and provides cohesive automatic device management to write DirectX application with just a few lines of code automatically managing device loss, device errors, etc.
- **`WinApi.Windows.Controls`** [Incomplete] - A small library that implements the `EventedWindowCore` for standard classes like `Static`, `Edit` and also provides `Window`, which is just a helper to ease direct derivation of EventedWindowCore. This library is currently incomplete and just provides the implementations to serve as an example.

**Note:** - Starting from v4, all packages are of minimum `netstandard 1.4`, and `Source` nuget packages are no more. `Desktop` package is `netstandard 2.0`.

### WinApi.Windows

- Ultra-light weight, extremely simple and tiny wrappers that can be used to create, manipulate or use windows extensively.
- `Zero GC allocations` on during window messages, and event loop cycles.
- `Fundamental concepts similar to ATL/WTL`, but in a C# idiomatic way.
- NativeWindow class is a very thin Window class that processes no messages, and provides no extra functionality. Great for using with custom GUI toolkits, DirectX, OpenGL games.
- NativeWindow can also be extended to work with any subclasses like Button, ComboBox, etc, with the same principles.
- A GUI wrapper for Win32 that `can work with CoreCLR`.
- Can be wrapped over any existing windows, just by using the handle.
- Strict `pay-only-for-what-you-use model`.
- Several different event loops depending on the need (For example, `RealtimeEventLoop` for games while the simple `EventLoop` is ideal for normal applications).

### Goals

- Every single method is `hand-written from a combination of auto-generation from Windows SDK headers and MSDN`, and tested for correctness.
- Provide both safe (through helpers, and safety wrappers like SafeHandles, CriticalHandles), and unsafe wrappers (pure with minimal performance impact), in a clean way supplemented with inline documentation.
- Provide a single DLL that can over time, be a direct equivalent of C/C++ `windows.h` header file for the CLR. Other Windows SDK wrappers may, or may not be in fragmented into separate packages.
- Sufficient base to be able to write custom toolkits over Win32 based on Direct2D, Direct3D or even an external graphics library like Skia, without depending on WPF or WinForms - `Examples of usage with Direct2D, 3D, Skia, OpenGL are all in the samples`.
- Always retain parity with the native API when it comes to constants (Eg: `WS_OVERLAPPEDWINDOW`, will never be changed to `OverlappedWindow` to look more like C#. The only exceptions: `WM` and `VirtualKey` - the message id, and virtual key constants for simpler usability).
- `WinApi.Windows` - See below.
- All structs, flags, should always have the names in the idiomatic C# style. (Eg: `public enum WindowStyles { .. WS_OVERLAPPEDWINDOW = 0x00.  }`). Never WINDOWSTYLE, or MARGINS or RECT. Always `Margin`, `Rectangle`, etc. (It actually is surprisingly clean once drop the usual depencendies like WinForms, or WPF which always provide alternative forms).
- Use variants such as `int` for Windows types like `BOOL` - to ensure minimum Marashalling impact when inside a structure. Using `bool` requires another copy, since bool in CLR is 1 byte, but the unmanaged variant could be 1, 2 or 4 bytes, depending on the context. However, when it comes to functions `bool` is used directly, since int conversion there is not only tedious but is bound to loose semantic value.

### Secondary goals

- Provide fully documented API (both from headers and MSDN, where-ever applicable) in the releases. Everything should be `IntelliSense capable`. No MSDN round-trips, while doing low level programming with CLR.


### Notes

- All methods in its minimal interop form (no SafeHandles, CriticalHandles, etc) unless absolutely required, for maximum micro-optimization of interop scenarios in the class with `Methods` suffix. (`User32Methods`, `Kernel32Methods`, `DwmApiMethods`, etc). Prefered to use `int`, `uint` etc inside the `*Methods` class to ensure parity with native APIs. Enums can be used for flags only if the value is a strictly well defined constant set. Otherwise prefer int, uint, etc. However, type safe wrappers can be supplemented in the `Helpers`.
- All methods with handles, enums and other supplemented types go into `Helpers` (`User32Helpers`, `Kernel32Helpers`, etc).
- Everything that uses undocumented APIs is maintained in a separate `Experimental` namespace similarly.

### Why re-invent the wheel?

While there aren't many well defined reliable wrappers, there are a few - my favorite being Pinvoke (https://github.com/AArnott/pinvoke). While `Goals` above, should explain the reasons for re-inventing the wheel, it's also mostly a matter for coding style, and about having the ability to micro-optimize when you really need to.

### Filesystem structure
```
--- LibraryName
    --  Types.cs (Structs, enums and other constants)
    --  Methods.cs (All direct native methods)
    --  Helpers.cs (All the helper methods with type safety wrappers)
    ##  Constants.cs (Optionally, if there are too many types, split constants (enums) from pure structs)
```

## Samples


#### C/C++ Samples to serve as comparison standard:
- Win32 C - https://github.com/prasannavl/WinApi/tree/master/Samples/Sample.Native.Win32/main.cpp
- ATL/C++ - https://github.com/prasannavl/WinApi/blob/master/Samples/Sample.Native.Atl/CAppWindow.cpp

#### C# Samples using `WinApi`:

- Raw equivalent of C Sample: https://github.com/prasannavl/WinApi/blob/master/Samples/Sample.Win32/Program.cs
- Equivalent of C and ATL/C++ using `WinApi.Windows` - https://github.com/prasannavl/WinApi/blob/master/Samples/Sample.SimpleWindow/Program.cs
- DirectX using `WinApi.DxUtils` - https://github.com/prasannavl/WinApi/tree/master/Samples/Sample.DirectX
- OpenGL - https://github.com/prasannavl/WinApi/tree/master/Samples/Sample.OpenGL
- Keyboard Input Simulation with `SendInput` helpers - https://github.com/prasannavl/WinApi/tree/master/Samples/Sample.SimulateInput
- Using Skia as the primary 2D drawing backend with `SkiaSharp` - https://github.com/prasannavl/WinApi/tree/master/Samples/Sample.Skia

Contributions
---
- Please follow the file structure detailed. 
- Please avoid batching up commits in your PRs. Keep pure Win32 methods, and constants in a separate one so they can easily be merged. (Anything that usually belongs in `Methods.cs` or `Constants.cs`)
- Use your discretion to decide whether `Helpers.cs`, and/or any other library features require a separate PR as well. When in doubt, separate it out.
- Beyond that feel free to follow your usual standards - feature/bugfix, etc based batching.

Community projects using `WinApi`
---
(This section is community editable. Please help yourself)

- [Chromely](https://github.com/mattkol/Chromely) : Build .NET/.NET Core HTML5 desktop apps using cross-platform native GUI API.

Credits
---
Thanks to [JetBrains](https://www.jetbrains.com) for the OSS license of Resharper Ultimate.

Proudly developed using:

<a href="https://www.jetbrains.com/resharper/
"><img src="https://blog.jetbrains.com/wp-content/uploads/2014/04/logo_resharper.gif" alt="Resharper logo" width="100" /></a>

License
---

This project is licensed under either of the following, at your choice:

* Apache License, Version 2.0, ([LICENSE-APACHE](LICENSE-APACHE) or [https://www.apache.org/licenses/LICENSE-2.0](https://www.apache.org/licenses/LICENSE-2.0))
* GPL 3.0 license ([LICENSE-GPL](LICENSE-GPL) or [https://opensource.org/licenses/GPL-3.0](https://opensource.org/licenses/GPL-3.0))

Code of Conduct
---

Contribution to the LiquidState project is organized under the terms of the Contributor Covenant, and as such the maintainer [@prasannavl](https://github.com/prasannavl) promises to intervene to uphold that code of conduct.

