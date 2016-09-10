# WinApi

A direct, highly opinionated CLR library for the native Win32 API.

`Work-In-Progress`

```c#
    static int Main(string[] args)
    {
        var factory = WindowFactory.Create("MainWindow");
        var win = factory.CreateWindow<MainWindow>(text: "Hello");
        win.Show();
        return new EventLoop(win).Run();
    }
```

**Goals:**

- Provide both safe (through helpers, and safety wrappers like HandleRefs, SafeHandles), and unsafe wrappers (pure with minimal performance impact), in a clean way supplemented with inline documentation.
- Provide a single DLL that can over time, be a direct equivalent of C/C++ `windows.h` header file for the CLR. Other Windows SDK wrappers may, or may not be in fragmented into separate packages.
- Sufficient base to be able to write custom toolkits over Win32 based on Direct2D, Direct3D or even an external graphics library like Skia, without depending on WPF or WinForms.
- Always retain parity with the native API when it comes to constants (Eg: `WS_OVERLAPPEDWINDOW`, will never be changed to `OverlappedWindow` to look more like C#. There is one exception to this, and that's `WM` - the message id constants for simpler usability).
- `WinApi.XWin` - See below.
- All structs, flags, should always have the names in the idiomatic C# style. (Eg: `public enum WindowStyles { .. WS_OVERLAPPEDWINDOW = 0x00.  }`). Never WINDOWSTYLE, or MARGINS or RECT. Always `Margin`, `Rectangle`, etc. (It actually is surprisingly clean once drop the usual depencendies like WinForms, or WPF which always provide alternative forms).
- Use variants such as `int` for Windows types like `BOOL` - to ensure minimum Marashalling impact. Using `bool` requires another copy, since bool in CLR is 1 byte, but the unmanaged variant could be 1, 2 or 4 bytes, depending on the context. A `bool` wrapped function can be manually provided as a helper, but not in the direct translation layer (`*Methods` class).
- `int` vs `uint` (and all similar primitives): Prefer the signed `int` and siblings unless there's its well defined for the value to be never negative. In short, use what makes the most semantic sense.

**Secondary goals:**

- Provide fully documented API (both from headers and MSDN, where-ever applicable) in the releases. Everything should be `IntelliSense capable`. No MSDN round-trips, while doing low level programming with CLR.

**WinApi.XWin:**

- Ultra-light weight, extremely simple and tiny wrappers that can be used to create, manipulate or use windows extensively.
- NativeWindow class is a very thin Window class that processes no messages, and provides no extra functionality. Great for using with custom GUI toolkits, DirectX, OpenGL games.
- NativeWindow can also be extended to work with any subclasses like Button, ComboBox, etc, with the same principles.
- A GUI wrapper for Win32 that can work with CoreCLR.
- Can be wrapped over any existing windows, just by using the handle.
- Strict pay-only-for-what-you-use model.
- Several different event loops depending on the need (For example, `RealtimeEventLoop` for games while the simple `EventLoop` is ideal for normal applications).

**Notes**:

- All methods in its minimal interop form (no SafeHandles, HandleRefs, etc) for maximum micro-optimization of interop scenarios in the class with `Methods` suffix. (`User32Methods`, `Kernel32Methods`, `DwmApiMethods`, etc). Prefered to use `int`, `uint` etc inside the `*Methods` class to ensure parity with native APIs. Enums can be used for flags only if the value is a strictly well defined constant set. Otherwise prefer int, uint, etc. Type safe wrappers are in the `Helpers`. 
- All methods with handles, enums and other supplemented types go into `Helpers` (`User32Helpers`, `Kernel32Helpers`, etc).
- Everything that uses undocumented APIs is maintained in a separate `Experimental` namespace similarly.

**Why re-invent the wheel?**

While there aren't many well defined reliable wrappers, there are a few - my favorite being Pinvoke (https://github.com/AArnott/pinvoke). While `Goals` above, should explain the reasons for re-inventing the wheel, it's also mostly a matter for coding style, and a little bit of the matter of the ability to micro-optimizate when you really need to.

**Filesystem structure:**
```
--- LibraryName
    --  Types.cs (Structs, enums and other constants)
    --  Methods.cs (All direct native methods)
    --  Helpers.cs (All the helper methods with type safety wrappers)
    ##  Constants.cs (Optionally, if there are too many types, split constants (enums) from pure structs)
```

**Example:**

A simple Win32 C++ Program (Mostly just C really):

```c
#include "stdafx.h"
#include <iostream>

int WINAPI wWinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance,
     PWSTR pCmdLine, int nCmdShow);
LRESULT CALLBACK WindowProc(HWND hwnd, UINT uMsg, WPARAM wParam,
     LPARAM lParam);

int wmain()
{
	wWinMain(nullptr, nullptr, nullptr, 0);
}

int WINAPI wWinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, 
    PWSTR pCmdLine, int nCmdShow)
{
	WNDCLASSEX wc = { 0 };
	wc.hInstance = hInstance;
	wc.lpszClassName = L"MainWindow";
	wc.cbSize = sizeof(WNDCLASSEX);
	wc.hIcon = LoadIcon(nullptr, IDI_APPLICATION);
	wc.hCursor = LoadCursor(nullptr, IDC_ARROW);
	wc.style = CS_HREDRAW | CS_VREDRAW;
	wc.lpfnWndProc = WindowProc;
	auto regRes = RegisterClassEx(&wc);
	if (!regRes)
	{
		std::cerr << "window registration failed" << std::endl;
		return regRes;
	}
	auto hwnd = CreateWindowEx(0, wc.lpszClassName, L"Hello", 
        WS_OVERLAPPEDWINDOW, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT,
        CW_USEDEFAULT, nullptr, nullptr, hInstance, nullptr);
	if (hwnd == nullptr)
	{
		std::cerr << "window couldn't be created" << std::endl;
		return -1;
	}

	ShowWindow(hwnd, SW_SHOWNORMAL);

	MSG msg = {};
	while (GetMessage(&msg, nullptr, 0, 0))
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}
	return 0;
}

LRESULT HandleDestroy(HWND hWnd)
{
	PostQuitMessage(0);
	return 0;
}

LRESULT HandlePaint(HWND hwnd)
{
	PAINTSTRUCT ps;
	auto hdc = BeginPaint(hwnd, &ps);
	FillRect(hdc, &ps.rcPaint, (HBRUSH)(GetStockObject(WHITE_BRUSH));
	EndPaint(hwnd, &ps);
	return 0;
}

LRESULT CALLBACK WindowProc(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
	switch (uMsg)
	{
	case WM_ERASEBKGND:
        return 1;
	case WM_DESTROY:
		return HandleDestroy(hwnd);
	case WM_PAINT:
		return HandlePaint(hwnd);
	}
	return DefWindowProc(hwnd, uMsg, wParam, lParam);
}
```

Now the direct C# equivalent using WinApi: 

```c#

using System;
using System.Runtime.InteropServices;
using WinApi.Core;
using WinApi.User32;
using WinApi.Gdi32;

namespace MySuperLowLevelProgram {

 internal class Program
    {
        [STAThread]
        static int Main(string[] args)
        {
            var instanceHandle = Kernel32Methods.GetModuleHandle(IntPtr.Zero);

            var wc = new WindowClassEx()
            {
                Size = (uint)Marshal.SizeOf<WindowClassEx>(),
                ClassName = "MainWindow",
                CursorHandle  = User32Helpers.LoadCursor(IntPtr.Zero, SystemCursor.IDC_ARROW),
                IconHandle = User32Helpers.LoadIcon(IntPtr.Zero, SystemIcon.IDI_APPLICATION),
                Style = WindowClassStyles.CS_HREDRAW | WindowClassStyles.CS_VREDRAW,
                BackgroundBrushHandle = new IntPtr((int)StockObject.WHITE_BRUSH),
                WindowProc = WindowProc,
                InstanceHandle = instanceHandle,
            };

            var resReg = User32Methods.RegisterClassEx(ref wc);
            if (resReg == 0)
            {
                Console.Error.WriteLine("registration failed");
                return -1;
            }

            var hwnd = User32Methods.CreateWindowEx(WindowExStyles.WS_EX_APPWINDOW, 
				wc.ClassName, "Hello", WindowStyles.WS_OVERLAPPEDWINDOW,
                (int) CreateWindowFlags.CW_USEDEFAULT, (int) CreateWindowFlags.CW_USEDEFAULT,
                (int) CreateWindowFlags.CW_USEDEFAULT, (int) CreateWindowFlags.CW_USEDEFAULT,
                IntPtr.Zero, IntPtr.Zero, instanceHandle, IntPtr.Zero);

            if (hwnd == IntPtr.Zero)
            {
                Console.Error.WriteLine("window creation failed");
                return -1;
            }

            User32Methods.ShowWindow(hwnd, ShowWindowCommands.SW_SHOWNORMAL);
            User32Methods.UpdateWindow(hwnd);

            Message msg;
            int res;
            while ((res = User32Methods.GetMessage(out msg, IntPtr.Zero, 0, 0)) != 0)
            {
                User32Methods.TranslateMessage(ref msg);
                User32Methods.DispatchMessage(ref msg);
            }

            return res;
        }

        private static IntPtr WindowProc(IntPtr hwnd, uint umsg, 
			IntPtr wParam, IntPtr lParam)
        {
            var msg = (WM) umsg;
            switch (msg)
            {
                case WM.ERASEBKGND:
                    return new IntPtr(1);
                case WM.CLOSE:
                {
                    User32Methods.PostQuitMessage(0);
                    break;
                }
                case WM.PAINT:
                {
                    PaintStruct ps;
                    var hdc = User32Methods.BeginPaint(hwnd, out ps);
                    User32Methods.FillRect(hdc, ref ps.PaintRectangle, 
						Gdi32Helpers.GetStockObject(StockObject.WHITE_BRUSH));
                    User32Methods.EndPaint(hwnd, ref ps);
                    break;
                }
            }
            return User32Methods.DefWindowProc(hwnd, umsg, wParam, lParam);
        }
    }
```

And now further using WinApi.WinX:

```c#
using System;
using WinApi.XWin;

namespace MySuperLowLevelProgram {
    internal class Program
    {
        [STAThread]
        static int Main(string[] args)
        {
            var factory = WindowFactory.Create("MainWindow");
            var win = factory.CreateWindow<AppWindow>(text: "Hello");
            win.Show();
            return new EventLoop(win).Run();
        }
    }

    public class AppWindow : MainWindow
    {
        protected override void OnPaint()
        {
            PaintStruct ps;
            var hdc = User32Methods.BeginPaint(hwnd, out ps);
            User32Methods.FillRect(hdc, ref ps.PaintRectangle, 
                Gdi32Helpers.GetStockObject(StockObject.WHITE_BRUSH));
            User32Methods.EndPaint(hwnd, ref ps);
        }

        protected override bool OnMessage(ref WindowMessage msg)
        {
            switch (msg.Id)
            {
                case WM.ERASEBKGND:
                {
                    msg.Result = new IntPtr(1);
                    return false;
                }
                case WM.PAINT:
                {
                    OnPaint();
                    return false;
                }
            }
            return base.OnMessage(ref msg);
        }
    }
}
``` 