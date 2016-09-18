using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenGL;
using WinApi.User32;
using WinApi.XWin;

namespace Sample.OpenGL
{
    class Program
    {
        static int Main(string[] args)
        {
            Gl.Initialize();
            var factory = WindowFactory.Create("MainWindow");
            using (var win = factory.CreateFrameWindow<MainWindow>(text: "Hello"))
            {
                win.Show();
                return new EventLoop(win).Run();
            }
        }
    }

    public class MainWindow : MainWindowBase
    {
        private bool m_isInit;

        protected IDeviceContext DeviceContext;
        protected IntPtr RenderContext;

        protected override void OnCreate(ref WindowMessage msg, ref CreateStruct createStruct)
        {
            base.OnCreate(ref msg, ref createStruct);
            Show();
        }

        private void Init()
        {
            // Create device/render context
            CreateDeviceContext();
            CreateContext();
            // The context is made current unconditionally: it will be current also on OnPaint, avoiding
            // rendundant calls to glMakeCurrent ini nominal implementations
            MakeCurrentContext();
            OnContextCreated();
        }

        private void OnContextCreated()
        {
            Gl.MatrixMode(MatrixMode.Projection);
            Gl.LoadIdentity();
            Gl.Ortho(0.0, 1.0f, 0.0, 1.0, 0.0, 1.0);

            Gl.MatrixMode(MatrixMode.Modelview);
            Gl.LoadIdentity();
        }

        protected override void OnPaint(ref WindowMessage msg, IntPtr hdc)
        {
            if (!m_isInit)
            {
                m_isInit = true;
                Init();
            }

            Rectangle rect;
            GetClientRectangle(out rect);

            Gl.Viewport(0, 0, rect.Width, rect.Height);
            Gl.Clear(ClearBufferMask.ColorBufferBit);

            Gl.Begin(PrimitiveType.Triangles);
            Gl.Color3(1.0f, 0.0f, 0.0f);
            Gl.Vertex2(0.0f, 0.0f);
            Gl.Color3(0.0f, 1.0f, 0.0f);
            Gl.Vertex2(0.5f, 1.0f);
            Gl.Color3(0.0f, 0.0f, 1.0f);
            Gl.Vertex2(1.0f, 0.0f);
            Gl.End();
            DeviceContext.SwapBuffers();

            Validate();
            base.OnPaint(ref msg, hdc);
        }

        protected override void OnDestroy(ref WindowMessage msg)
        {
            DeleteContext();
            base.OnDestroy(ref msg);
        }

        #region OpenGL Related Properties

        /// <summary>
        ///     Attribute permission.
        /// </summary>
        public enum AttributePermission
        {
            /// <summary>
            ///     Requires the attribute. Throw an error is not supported.
            /// </summary>
            Required,

            /// <summary>
            ///     Enable the attribute to be set, if supported.
            /// </summary>
            Enabled,

            /// <summary>
            ///     Requires the attrbute to be set, but only when a debugger is attached.
            /// </summary>
            EnabledInDebugger,

            /// <summary>
            ///     Do not specify the attribute.
            /// </summary>
            DontCare
        }

        /// <summary>
        ///     Profile permission.
        /// </summary>
        public enum ProfileType
        {
            /// <summary>
            ///     Requires the core profile
            /// </summary>
            Core,

            /// <summary>
            ///     Requires the compatibility profile.
            /// </summary>
            Compatibility
        }


        public KhronosVersion Version { get; set; }

        public uint ColorBits { get; set; } = 24;

        /// <summary>
        ///     Get or set the OpenGL minimum depth buffer bits.
        /// </summary>
        public uint DepthBits { get; set; }

        /// <summary>
        ///     Get or set the OpenGL minimum stencil buffer bits.
        /// </summary>
        public uint StencilBits { get; set; }

        /// <summary>
        ///     Get or set the OpenGL minimum multisample buffer "bits".
        /// </summary>
        public uint MultisampleBits { get; set; }

        /// <summary>
        ///     Get or set the OpenGL double buffering flag.
        /// </summary>
        public bool DoubleBuffer { get; set; } = true;

        /// <summary>
        ///     Get or set the OpenGL swap buffers interval.
        /// </summary>
        public int SwapInterval { get; set; } = 1;

        /// <summary>
        ///     Get or set the permission to create a debug context.
        /// </summary>
        public ProfileType ContextProfile { get; set; } = ProfileType.Compatibility;

        /// <summary>
        ///     Get or set the permission to create a debug context.
        /// </summary>
        public AttributePermission DebugContext { get; set; } = AttributePermission.EnabledInDebugger;

        /// <summary>
        ///     Get or set the permission to create a forward compatible context.
        /// </summary>
        public AttributePermission ForwardCompatibleContext { get; set; } = AttributePermission.DontCare;

        /// <summary>
        ///     Get or set the permission to create a context with the compatibility profile.
        /// </summary>
        public AttributePermission RobustContext { get; set; } = AttributePermission.DontCare;

        #endregion

        #region OpenGL Context Methods

        private void CreateDeviceContext()
        {
            DeviceContext = DeviceContextFactory.Create(Handle);
            DeviceContext.IncRef();

            // Set pixel format
            var pixelFormats = DeviceContext.PixelsFormats;
            var controlReqFormat = new DevicePixelFormat
            {
                ColorBits = (int) ColorBits,
                DepthBits = (int) DepthBits,
                StencilBits = (int) StencilBits,
                MultisampleBits = (int) MultisampleBits,
                DoubleBuffer = DoubleBuffer
            };

            var matchingPixelFormats = pixelFormats.Choose(controlReqFormat);
            if (matchingPixelFormats.Count == 0)
                throw new InvalidOperationException("unable to find a suitable pixel format");

            DeviceContext.SetPixelFormat(matchingPixelFormats[0]);
        }

        protected virtual void CreateContext()
        {
            if (RenderContext != IntPtr.Zero)
                throw new InvalidOperationException("context already created");

            var currentVersion = Gl.CurrentVersion;
            var currentExtensions = Gl.CurrentExtensions;

            var hasCreateContext = true;
            var hasCreateContextProfile = true;
            var hasCreateContextRobustness = true;

            hasCreateContextProfile &= (currentVersion.Api == KhronosVersion.ApiGl) &&
                                       (currentVersion >= Gl.Version_300);
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                var currentWglExtensions = Wgl.CurrentExtensions;
                hasCreateContext &= currentWglExtensions.CreateContext_ARB ||
                                    currentWglExtensions.CreateContextProfile_ARB;
                hasCreateContextProfile &= currentWglExtensions.CreateContextProfile_ARB;
                hasCreateContextRobustness = currentWglExtensions.CreateContextRobustness_ARB;
            }
            else if (Environment.OSVersion.Platform == PlatformID.Unix)
            {
                var currentGlxExtensions = Glx.CurrentExtensions;
                hasCreateContext &= currentGlxExtensions.CreateContext_ARB;
                hasCreateContextProfile &= currentGlxExtensions.CreateContextProfile_ARB;
                hasCreateContextRobustness = currentGlxExtensions.CreateContextRobustness_ARB;
            }

            if (hasCreateContext)
            {
                var attributes = new List<int>();
                uint contextProfile = 0, contextFlags = 0;
                var debuggerAttached = Debugger.IsAttached;

                #region WGL_ARB_create_context|GLX_ARB_create_context

                // The default values for WGL_CONTEXT_MAJOR_VERSION_ARB and WGL_CONTEXT_MINOR_VERSION_ARB are 1 and 0 respectively. In this
                // case, implementations will typically return the most recent version of OpenGL they support which is backwards compatible with OpenGL 1.0
                // (e.g. 3.0, 3.1 + GL_ARB_compatibility, or 3.2 compatibility profile) [from WGL_ARB_create_context spec]
                Debug.Assert(Wgl.CONTEXT_MAJOR_VERSION_ARB == Glx.CONTEXT_MAJOR_VERSION_ARB);
                Debug.Assert(Wgl.CONTEXT_MINOR_VERSION_ARB == Glx.CONTEXT_MINOR_VERSION_ARB);
                if (Version != null)
                {
                    attributes.AddRange(new[]
                    {
                        Wgl.CONTEXT_MAJOR_VERSION_ARB, Version.Major,
                        Wgl.CONTEXT_MINOR_VERSION_ARB, Version.Minor
                    });
                }
                else
                {
                    attributes.AddRange(new[]
                    {
                        Wgl.CONTEXT_MAJOR_VERSION_ARB, currentVersion.Major,
                        Wgl.CONTEXT_MINOR_VERSION_ARB, currentVersion.Minor
                    });
                }

                if ((DebugContext == AttributePermission.Enabled) ||
                    (debuggerAttached && (DebugContext == AttributePermission.EnabledInDebugger)))
                {
                    Debug.Assert(Wgl.CONTEXT_DEBUG_BIT_ARB == Glx.CONTEXT_DEBUG_BIT_ARB);
                    contextFlags |= Wgl.CONTEXT_DEBUG_BIT_ARB;
                }

                if ((ForwardCompatibleContext == AttributePermission.Enabled) ||
                    (debuggerAttached && (ForwardCompatibleContext == AttributePermission.EnabledInDebugger)))
                {
                    Debug.Assert(Wgl.CONTEXT_FORWARD_COMPATIBLE_BIT_ARB == Glx.CONTEXT_FORWARD_COMPATIBLE_BIT_ARB);
                    contextFlags |= Wgl.CONTEXT_FORWARD_COMPATIBLE_BIT_ARB;
                }

                #endregion

                #region WGL_ARB_create_context_profile|GLX_ARB_create_context_profile

                if (hasCreateContextProfile)
                {
                    if (ContextProfile == ProfileType.Core)
                    {
                        Debug.Assert(Wgl.CONTEXT_CORE_PROFILE_BIT_ARB == Glx.CONTEXT_CORE_PROFILE_BIT_ARB);
                        contextProfile |= Wgl.CONTEXT_CORE_PROFILE_BIT_ARB;
                    }
                    else if (ContextProfile == ProfileType.Compatibility)
                    {
                        Debug.Assert(Wgl.CONTEXT_COMPATIBILITY_PROFILE_BIT_ARB ==
                                     Glx.CONTEXT_COMPATIBILITY_PROFILE_BIT_ARB);
                        contextProfile |= Wgl.CONTEXT_COMPATIBILITY_PROFILE_BIT_ARB;
                    }
                }

                #endregion

                #region WGL_ARB_create_context_robustness|GLX_ARB_create_context_robustness

                if (hasCreateContextRobustness)
                {
                    if ((RobustContext == AttributePermission.Enabled) ||
                        (debuggerAttached && (RobustContext == AttributePermission.EnabledInDebugger)))
                    {
                        Debug.Assert(Wgl.CONTEXT_ROBUST_ACCESS_BIT_ARB == Glx.CONTEXT_ROBUST_ACCESS_BIT_ARB);
                        contextFlags |= Wgl.CONTEXT_ROBUST_ACCESS_BIT_ARB;
                    }
                }

                #endregion

                Debug.Assert(Wgl.CONTEXT_FLAGS_ARB == Glx.CONTEXT_FLAGS_ARB);
                if (contextFlags != 0)
                    attributes.AddRange(new[] {Wgl.CONTEXT_FLAGS_ARB, unchecked((int) contextFlags)});

                Debug.Assert(Wgl.CONTEXT_PROFILE_MASK_ARB == Glx.CONTEXT_PROFILE_MASK_ARB);
                if (contextProfile != 0)
                    attributes.AddRange(new[] {Wgl.CONTEXT_PROFILE_MASK_ARB, unchecked((int) contextProfile)});

                attributes.Add(0);

                if ((RenderContext = DeviceContext.CreateContextAttrib(IntPtr.Zero, attributes.ToArray())) ==
                    IntPtr.Zero)
                    throw new InvalidOperationException($"unable to create render context ({Gl.GetError()})");
            }
            else
            {
                // Create OpenGL context using compatibility profile
                if ((RenderContext = DeviceContext.CreateContext(IntPtr.Zero)) == IntPtr.Zero)
                    throw new InvalidOperationException("unable to create render context");
            }
        }

        /// <summary>
        ///     Make the GlControl context current.
        /// </summary>
        protected virtual void MakeCurrentContext()
        {
            // Make context current
            if (DeviceContext.MakeCurrent(RenderContext) == false)
                throw new InvalidOperationException("unable to make context current");
        }

        /// <summary>
        ///     Delete the GlControl context.
        /// </summary>
        protected virtual void DeleteContext()
        {
            // Delete OpenGL context
            DeviceContext.DeleteContext(RenderContext);
            RenderContext = IntPtr.Zero;
        }

        #endregion
    }
}