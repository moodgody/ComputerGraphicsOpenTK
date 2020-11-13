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
     class GraphObject 
    {
        protected Shader _shader;
        protected int VertexBufferObject;
        protected int VertexArrayObject;
        protected string _vertexShaderPath;
        protected string _fragmentShaderPath;

        public GraphObject(string vertexShaderPath, string fragmentShaderPath)
        {
            _vertexShaderPath = vertexShaderPath;
            _fragmentShaderPath = fragmentShaderPath;
            _shader = new Shader(_vertexShaderPath, _fragmentShaderPath);
        }

        public virtual void OnLoad()
        {

        }
       public virtual  void OnRenderFrame(FrameEventArgs args)
        {
            _shader.Use();
            GL.BindVertexArray(VertexArrayObject);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
        }
       public virtual void OnUnload()
        {
            _shader.Dispose();
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(VertexBufferObject);
        }
       
    }
}
