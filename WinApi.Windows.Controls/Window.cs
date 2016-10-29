using System;
using WinApi.User32;

namespace WinApi.Windows.Controls
{
    public class Window : EventedWindowCore, IConstructionParamsProvider
    {
        public static Lazy<WindowFactory> ClassFactory = new Lazy<WindowFactory>(() => WindowFactory.Create());
        protected Window() {}

        IConstructionParams IConstructionParamsProvider.GetConstructionParams() => new FrameWindowConstructionParams();

        /// <summary>
        ///     The window creator for standard windows.
        ///     This is the default methods to create.
        /// </summary>
        public static Window Create(string text = null,
            WindowStyles? styles = null,
            WindowExStyles? exStyles = null, int? x = null, int? y = null,
            int? width = null, int? height = null, IntPtr? hParent = null, IntPtr? hMenu = null,
            WindowFactory factory = null)
        {
            return (factory ?? ClassFactory.Value).CreateWindowEx(() => new Window(), text, styles, exStyles, x, y,
                width,
                height, hParent, hMenu);
        }

        /// <summary>
        ///     The type `Window` is conceptually slightly special, since it forms the base of
        ///     all fully functional evented windows. So, this is a helper that makes
        ///     it easy to create any other type of window easily, without the need
        ///     to create a custom class to provide the protected constructor and
        ///     IConstructionParamsProvider implementation.
        ///     This is so that its easy to quickly write other types without having a class for
        ///     it. But its application's responsibility to make sure that `new` of the Controls
        ///     aren't called in an attempt to instantiate the window itself, as they'll only
        ///     create the CLR object and not the actual windows.
        /// </summary>
        public static TWindow Create<TWindow>(string text = null,
            WindowStyles? styles = null,
            WindowExStyles? exStyles = null, int? x = null, int? y = null,
            int? width = null, int? height = null, IntPtr? hParent = null, IntPtr? hMenu = null,
            WindowFactory factory = null, uint? controlStyles = null)
            where TWindow : WindowCore, IConstructionParamsProvider, new()
        {
            return (factory ?? ClassFactory.Value).CreateWindowEx(() => new TWindow(), text, styles, exStyles, x, y,
                width, height, hParent,
                hMenu, controlStyles);
        }

        /// <summary>
        ///     Another helper for `Window` class, since its conceptually slightly special.
        ///     This allows any TWindow type to be created, whether or not they implement the
        ///     IConstructionParamsProvider to help with construction. This is purely a helper
        ///     to facilitate such a scenario.
        /// </summary>
        public static TWindow CreateWith<TWindow>(IConstructionParams constructionParams = null, string text = null,
            WindowStyles? styles = null,
            WindowExStyles? exStyles = null, int? x = null, int? y = null,
            int? width = null, int? height = null, IntPtr? hParent = null, IntPtr? hMenu = null,
            WindowFactory factory = null, uint? controlStyles = null)
            where TWindow : WindowCore, new()
        {
            return (factory ?? ClassFactory.Value).CreateWindow(() => new TWindow(),
                text, styles, exStyles, x, y,
                width, height, hParent,
                hMenu, controlStyles, constructionParams);
        }
    }
}