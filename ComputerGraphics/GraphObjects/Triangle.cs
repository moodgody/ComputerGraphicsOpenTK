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
       
        public Triangle() : base()
        {
            LoadVertexBufferWithStandardShape();

        }

        private void LoadVertexBufferWithStandardShape()
        {
            Vertices.Add(new Vector3(-0.5f, -0.5f, 0.0f));
            Vertices.Add(new Vector3(0.5f, -0.5f, 0.0f));
            Vertices.Add(new Vector3(0.0f, 0.5f, 0.0f));
        }

        public override void OnLoad(List<Vector3> verticesBuffer)
        {
            base.OnLoad(verticesBuffer);
        }

        public override void OnRenderFrame(FrameEventArgs args)
        {
            GL.DrawArrays(PrimitiveType.LineLoop, _start, _count);
            base.OnRenderFrame(args);
        }

        public override void OnUnload()
        {
            base.OnUnload();
        }
    }
}
