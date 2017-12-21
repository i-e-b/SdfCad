using System;
using System.IO;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;

namespace Tkgl
{
    public class PreviewWindow : GameWindow
    {
        private int shaderId;
        private int vertexArrayId;
        private double cumlTime;

        private ScreenBox screenBox;

        public PreviewWindow()
            : base(640, 480,
                  GraphicsMode.Default,
                  "SdfCad - Preview",
                  GameWindowFlags.Default,
                  DisplayDevice.Default,
                  4, 0, // OpenGL version
                  GraphicsContextFlags.ForwardCompatible)
        {
            shaderId = -1;
            Title += ", OpenGL v"+GL.GetString(StringName.Version);
        }

        public Vector4 Camera;
        private bool useAmbientOcclusion = true;
        private bool useShadows;
        private bool useReflections;

        /// <summary>
        /// Initial set up
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CursorVisible = true;
            CompileShaders();
            
            GL.GenVertexArrays(1, out vertexArrayId);
            GL.BindVertexArray(vertexArrayId);

            screenBox = new ScreenBox();
            screenBox.Bind(shaderId);

            Closed += OnClosed;
        }

        private void CompileShaders()
        {
            if (shaderId < 0) {
                GL.DeleteProgram(shaderId);
                shaderId = -1;
            }

            var vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, File.ReadAllText(@"vertex.c"));
            GL.CompileShader(vertexShader);

            var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            var fragSource = File.ReadAllText(@"fragment.c");
            if ( ! useAmbientOcclusion) {
                fragSource = fragSource.Replace("#define AMB_OCC 1", "#define AMB_OCC 0");
            }
            if ( useShadows) {
                fragSource = fragSource.Replace("#define SHADOWS 0", "#define SHADOWS 1");
            }
            if ( useReflections) {
                fragSource = fragSource.Replace("#define REFLECTIONS 0", "#define REFLECTIONS 1");
            }
            GL.ShaderSource(fragmentShader, fragSource);
            GL.CompileShader(fragmentShader);

            var program = GL.CreateProgram();
            GL.AttachShader(program, vertexShader);
            GL.AttachShader(program, fragmentShader);
            GL.LinkProgram(program);

            var log = GL.GetShaderInfoLog(vertexShader);
            log += GL.GetShaderInfoLog(fragmentShader);

            GL.DetachShader(program, vertexShader);
            GL.DetachShader(program, fragmentShader);
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);

            if (! string.IsNullOrWhiteSpace(log)) {
                GL.DeleteProgram(program);
                throw new Exception(log);
            }

            shaderId =  program;
        }

        public void RebindShader(bool ambOcc, bool shadows, bool reflections)
        {
            useAmbientOcclusion = ambOcc;
            useShadows = shadows;
            useReflections = reflections;
            CompileShaders();
        }

        /// <summary>
        /// Final clear up
        /// </summary>
        private void OnClosed(object sender, EventArgs e)
        {
            Exit();
        }

        /// <summary>
        /// Core rendering
        /// </summary>
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            cumlTime += e.Time;

            GL.ClearColor(new Color4(0, 0, 0, 1.0f));
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            GL.UseProgram(shaderId);

            // Shader attributes
            float aspectRatio = 1.0f + ((Width - Height) / (float)Height);

            // NOTE: it is *CRITICAL* that the types on the .Net side are the same as in the shader program.
            GL.Uniform4(2, ref Camera);         // iCamPosition
            GL.Uniform3(3, 0.0f, 0.0f, 0.0f);   // iTargetPosition
            GL.Uniform1(4, aspectRatio);        // iAspect
            GL.Uniform1(5, (float)cumlTime);    // iTime

            // Draw commands
            screenBox.Draw();

            SwapBuffers();
        }

        /// <summary>
        /// Non-rendering updates
        /// </summary>
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            ReadKeyboard();
        }

        private void ReadKeyboard()
        {
            if (Keyboard[Key.Escape]) Exit();
        }

        public override void Exit()
        {
            GL.DeleteVertexArrays(1, ref vertexArrayId);
            GL.DeleteProgram(shaderId);
            base.Exit();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Width, Height);
        }

        /// <summary>
        /// Set the camera position as a rotation and distance from the stage centre
        /// </summary>
        /// <param name="theta">rotation around stage</param>
        /// <param name="elevation">height above stage</param>
        /// <param name="distance">distance from stage centre</param>
        public void CameraPosition(float theta, float elevation, float distance)
        {
            var oldFov = (Camera.W < 1.0f) ? 2.0f : Camera.W;
            Camera = new Vector4((float) -Math.Cos(theta) * distance, 1.0f + elevation, (float)Math.Sin(theta) * distance, oldFov);
        }

        public void Fov(float fov)
        {
            Camera.W = fov;
        }
    }
}