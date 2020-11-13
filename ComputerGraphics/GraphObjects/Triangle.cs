using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

using System.IO;

namespace ComputerGraphics.GraphObjects
{
    class Triangle : GraphObject
    {
        public float[] Vertices { get; set; }
        public Triangle(string vertexShaderPath, string fragmentShaderPath) : base(vertexShaderPath, fragmentShaderPath)
        {
            float[] temp = {-0.5f, -0.5f, 0.0f, //Bottom-left vertex
                                     0.5f, -0.5f, 0.0f, //Bottom-right vertex
                                     0.0f,  0.5f, 0.0f  //Top vertex
                                };
            Vertices = temp;
        }

        public override void OnLoad()
        {
            
            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, Vertices.Length * sizeof(float), Vertices, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            //_shader.Use();
            VertexArrayObject = GL.GenVertexArray();
            // ..:: Initialization code (done once (unless your object frequently changes)) :: ..
            // 1. bind Vertex Array Object
            GL.BindVertexArray(VertexArrayObject);
            // 2. copy our vertices array in a buffer for OpenGL to use
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, Vertices.Length * sizeof(float), Vertices, BufferUsageHint.StaticDraw);
            // 3. then set our vertex attributes pointers
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            base.OnLoad();
        }

        public override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
        }

        public override void OnUnload()
        {
            base.OnUnload();
        }
    }
}
