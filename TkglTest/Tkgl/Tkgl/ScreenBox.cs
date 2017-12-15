using System;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace Tkgl
{
    /// <summary>
    /// Vertex buffer to cover the screen
    /// </summary>
    public class ScreenBox
    {
        protected Vector3[] vertexBufferData;
        protected int[] vertexBufferIndices;
        
        protected int vertexAttribId;
        protected int positionBufferId;
        protected int normalBufferId;
        protected int elementBufferId;

        public ScreenBox()
        {
            vertexBufferData = new[] {
                new Vector3(-1f, -1f, 0.0f),
                new Vector3(1.0f, -1.0f, 0.0f),
                new Vector3(1.0f, 1.0f, 0.0f),
                new Vector3(-1.0f, 1.0f, 0.0f)
            };

            vertexBufferIndices = new[] { 0, 1, 2, 0, 2, 3 };
        }
        
        public void Bind(int shaderId){
            CreateVertexBuffers();
            CreateVertexAttributes(shaderId);
        }

        public void Draw() {
            GL.BindVertexArray(vertexAttribId);
            GL.DrawElements(BeginMode.Triangles, vertexBufferIndices.Length, DrawElementsType.UnsignedInt, 0);
            GL.BindVertexArray(0);
        }

        protected void CreateVertexBuffers()
        {
            GL.GenBuffers(1, out positionBufferId);
            GL.BindBuffer(BufferTarget.ArrayBuffer, positionBufferId);
            GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(vertexBufferData.Length * Vector3.SizeInBytes), vertexBufferData, BufferUsageHint.StaticDraw);

            GL.GenBuffers(1, out normalBufferId);
            GL.BindBuffer(BufferTarget.ArrayBuffer, normalBufferId);
            GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(vertexBufferData.Length * Vector3.SizeInBytes), vertexBufferData, BufferUsageHint.StaticDraw);

            GL.GenBuffers(1, out elementBufferId);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferId);
            GL.BufferData(BufferTarget.ElementArrayBuffer, new IntPtr(sizeof(uint) * vertexBufferIndices.Length), vertexBufferIndices, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }

        protected void CreateVertexAttributes(int shaderId)
        {
            GL.GenVertexArrays(1, out vertexAttribId);
            GL.BindVertexArray(vertexAttribId);

            GL.EnableVertexAttribArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, positionBufferId);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, true, Vector3.SizeInBytes, 0);
            GL.BindAttribLocation(shaderId, 0, "in_position");

            GL.EnableVertexAttribArray(1);
            GL.BindBuffer(BufferTarget.ArrayBuffer, normalBufferId);
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, true, Vector3.SizeInBytes, 0);
            GL.BindAttribLocation(shaderId, 1, "in_normal");

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferId);

            GL.BindVertexArray(0);
        }
    }
}