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
            Title += ", OpenGL v"+GL.GetString(StringName.Version);
            CameraPosition(0.0f, 1.0f);
            Fov(2.0f);
        }

        public Vector4 Camera;


        /// <summary>
        /// Initial set up
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CursorVisible = true;
            shaderId = CompileShaders();
            
            GL.GenVertexArrays(1, out vertexArrayId);
            GL.BindVertexArray(vertexArrayId);

            screenBox = new ScreenBox();
            screenBox.Bind(shaderId);

            Closed += OnClosed;
        }

        private int CompileShaders()
        {
            var vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, File.ReadAllText(@"vertex.c"));
            GL.CompileShader(vertexShader);

            var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, File.ReadAllText(@"fragment.c"));
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

            return program;
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
            //GL.VertexAttrib1(0, cumlTime);

            var position = new Vector4
            {
                X = (float) Math.Sin(cumlTime) * 0.5f,
                Y = (float) Math.Cos(cumlTime) * 0.5f,
                Z = 0.0f,
                W = 1.0f
            };
            GL.VertexAttrib4(0, position);

            float aspectRatio = 1.0f + ((Width - Height) / (float)Height);

            // NOTE: it is *CRITICAL* that the types on the .Net side are the same as in the shader program.
            //GL.Uniform3(2, Width, Height, 0.0f);                         // iResolution
            GL.Uniform4(2, ref Camera);                         // iResolution
            GL.Uniform4(3, (float)Mouse.X, (float)Mouse.Y, 0.0f, 0.0f);  // iMouse
            GL.Uniform1(4, aspectRatio);                                 // iAspect
            GL.Uniform1(5, (float)cumlTime);                             // iTime

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

        public void CameraPosition(float theta, float elevation)
        {
            //vec3( -0.5+3.5*cos(0.1*time + 6.0*mo.x), 1.0 + 2.0*mo.y, 0.5 + 4.0*sin(0.1*time + 6.0*mo.x) );
            Camera = new Vector4((float) -Math.Cos(theta), 1.0f + elevation, (float)Math.Sin(theta), 2.0f);
        }

        public void Fov(float fov)
        {
            Camera.W = fov;
        }
    }
}