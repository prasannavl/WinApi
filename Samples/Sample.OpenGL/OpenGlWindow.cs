using System;
using System.Collections.Generic;
using System.Diagnostics;
using OpenGL;
using WinApi.User32;
using WinApi.Windows;
using WinApi.Windows.Controls;

namespace Sample.OpenGL
{
    public class OpenGlWindow : Window
    {
        protected IDeviceContext DeviceContext;
        private bool m_isInit;
        protected IntPtr RenderContextHandle;

        private void Init()
        {
            // Create device/render context
            this.CreateDeviceContext();
            this.CreateContext();
            // The context is made current unconditionally: it will be current also on OnPaint, avoiding
            // rendundant calls to glMakeCurrent ini nominal implementations
            this.MakeCurrentContext();
            this.OnGlContextCreated();
        }

        protected virtual void OnGlContextCreated() {}

        protected virtual void OnGlPaint(ref PaintStruct ps) {}

        protected override void OnPaint(ref PaintPacket packet)
        {
            PaintStruct ps;
            var hdc = this.BeginPaint(out ps);
            if (!this.m_isInit)
            {
                this.m_isInit = true;
                this.Init();
            }
            this.OnGlPaint(ref ps);
            this.EndPaint(ref ps);
        }

        protected override void OnDestroy(ref Packet packet)
        {
            this.DeleteContext();
            base.OnDestroy(ref packet);
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
            this.DeviceContext = DeviceContextFactory.Create(this.Handle);
            this.DeviceContext.IncRef();

            // Set pixel format
            var pixelFormats = this.DeviceContext.PixelsFormats;
            var controlReqFormat = new DevicePixelFormat
            {
                ColorBits = (int) this.ColorBits,
                DepthBits = (int) this.DepthBits,
                StencilBits = (int) this.StencilBits,
                MultisampleBits = (int) this.MultisampleBits,
                DoubleBuffer = this.DoubleBuffer
            };

            var matchingPixelFormats = pixelFormats.Choose(controlReqFormat);
            if (matchingPixelFormats.Count == 0) throw new InvalidOperationException("unable to find a suitable pixel format");

            this.DeviceContext.SetPixelFormat(matchingPixelFormats[0]);
        }

        protected virtual void CreateContext()
        {
            if (this.RenderContextHandle != IntPtr.Zero) throw new InvalidOperationException("context already created");

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
                if (this.Version != null)
                {
                    attributes.AddRange(new[]
                    {
                        Wgl.CONTEXT_MAJOR_VERSION_ARB, this.Version.Major,
                        Wgl.CONTEXT_MINOR_VERSION_ARB, this.Version.Minor
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

                if ((this.DebugContext == AttributePermission.Enabled) ||
                    (debuggerAttached && (this.DebugContext == AttributePermission.EnabledInDebugger)))
                {
                    Debug.Assert(Wgl.CONTEXT_DEBUG_BIT_ARB == Glx.CONTEXT_DEBUG_BIT_ARB);
                    contextFlags |= Wgl.CONTEXT_DEBUG_BIT_ARB;
                }

                if ((this.ForwardCompatibleContext == AttributePermission.Enabled) ||
                    (debuggerAttached && (this.ForwardCompatibleContext == AttributePermission.EnabledInDebugger)))
                {
                    Debug.Assert(Wgl.CONTEXT_FORWARD_COMPATIBLE_BIT_ARB == Glx.CONTEXT_FORWARD_COMPATIBLE_BIT_ARB);
                    contextFlags |= Wgl.CONTEXT_FORWARD_COMPATIBLE_BIT_ARB;
                }

                #endregion

                #region WGL_ARB_create_context_profile|GLX_ARB_create_context_profile

                if (hasCreateContextProfile)
                {
                    if (this.ContextProfile == ProfileType.Core)
                    {
                        Debug.Assert(Wgl.CONTEXT_CORE_PROFILE_BIT_ARB == Glx.CONTEXT_CORE_PROFILE_BIT_ARB);
                        contextProfile |= Wgl.CONTEXT_CORE_PROFILE_BIT_ARB;
                    }
                    else if (this.ContextProfile == ProfileType.Compatibility)
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
                    if ((this.RobustContext == AttributePermission.Enabled) ||
                        (debuggerAttached && (this.RobustContext == AttributePermission.EnabledInDebugger)))
                    {
                        Debug.Assert(Wgl.CONTEXT_ROBUST_ACCESS_BIT_ARB == Glx.CONTEXT_ROBUST_ACCESS_BIT_ARB);
                        contextFlags |= Wgl.CONTEXT_ROBUST_ACCESS_BIT_ARB;
                    }
                }

                #endregion

                Debug.Assert(Wgl.CONTEXT_FLAGS_ARB == Glx.CONTEXT_FLAGS_ARB);
                if (contextFlags != 0) attributes.AddRange(new[] {Wgl.CONTEXT_FLAGS_ARB, unchecked((int) contextFlags)});

                Debug.Assert(Wgl.CONTEXT_PROFILE_MASK_ARB == Glx.CONTEXT_PROFILE_MASK_ARB);
                if (contextProfile != 0) attributes.AddRange(new[] {Wgl.CONTEXT_PROFILE_MASK_ARB, unchecked((int) contextProfile)});

                attributes.Add(0);

                if (
                    (this.RenderContextHandle =
                        this.DeviceContext.CreateContextAttrib(IntPtr.Zero, attributes.ToArray())) ==
                    IntPtr.Zero) throw new InvalidOperationException($"unable to create render context ({Gl.GetError()})");
            }
            else
            {
                // Create OpenGL context using compatibility profile
                if ((this.RenderContextHandle = this.DeviceContext.CreateContext(IntPtr.Zero)) == IntPtr.Zero) throw new InvalidOperationException("unable to create render context");
            }
        }

        /// <summary>
        ///     Make the GlControl context current.
        /// </summary>
        protected virtual void MakeCurrentContext()
        {
            // Make context current
            if (this.DeviceContext.MakeCurrent(this.RenderContextHandle) == false) throw new InvalidOperationException("unable to make context current");
        }

        /// <summary>
        ///     Delete the GlControl context.
        /// </summary>
        protected virtual void DeleteContext()
        {
            // Delete OpenGL context
            this.DeviceContext.DeleteContext(this.RenderContextHandle);
            this.RenderContextHandle = IntPtr.Zero;
        }

        #endregion
    }
}