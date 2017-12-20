﻿using System;
using System.IO;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;

namespace Tkgl
{
    public class MainWindow : GameWindow
    {
        private int shaderId;
        private int vertexArrayId;
        private double cumlTime;

        private ScreenBox screenBox;

        public MainWindow()
            : base(800, 600,
                  GraphicsMode.Default,
                  "SdfCad",
                  GameWindowFlags.Default,
                  DisplayDevice.Default,
                  4, 0, // OpenGL version
                  GraphicsContextFlags.ForwardCompatible)
        {
            Title += ", OpenGL v"+GL.GetString(StringName.Version);
        }

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
            GL.VertexAttrib1(0, cumlTime);

            var position = new Vector4
            {
                X = (float) Math.Sin(cumlTime) * 0.5f,
                Y = (float) Math.Cos(cumlTime) * 0.5f,
                Z = 0.0f,
                W = 1.0f
            };
            GL.VertexAttrib4(1, position);

            GL.Uniform3(0, Width, Height, 0.0f);                         // iResolution
            //GL.Uniform4(1, Width / 2.0f, Height / 2.0f, 0.0f, 0.0f);     // iMouse
            GL.Uniform4(1, 0.0f, 0.0f, 0.0f, 0.0f);     // iMouse / position

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
    }
}