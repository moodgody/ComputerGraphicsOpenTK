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
    class Rectangle : GraphObject
    {
        public Rectangle(float left, float bottom, float width, float height) : base()
        {
            Left = left;
            Bottom = bottom;
            Width = width;
            Height = height;
        }

        public float Left { get; set; }
        public float Bottom { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
       

        public override void OnRenderFrame(FrameEventArgs args,  OpenGLWindow parent)
        {
            base.OnRenderFrame(args,parent);
            GL.DrawElements(PrimitiveType.LineLoop, 6, DrawElementsType.UnsignedInt,_v0);
            
            
        }

        public override void OnLoadElementIndices(List<uint> indicesBuffer)
        {
            base.OnLoadElementIndices(indicesBuffer);
           
            uint[] indices = {  // note that we start from 0!
                                    _refpoint,_refpoint+1 , _refpoint+3,   // first triangle
                                    _refpoint+1, _refpoint+2, _refpoint+3    // second triangle
                                };
            indicesBuffer.AddRange(indices);
            
        }

        protected override void LoadVertexBufferWithStandardShape()
        {
            base.LoadVertexBufferWithStandardShape();

            Vertices.Add(new Vector3(0.5f, 0.5f, 0.0f));    // Top Right
            Vertices.Add(new Vector3(0.5f, -0.5f, 0.0f));   // Bottom Right
            Vertices.Add(new Vector3(-0.5f, -0.5f, 0.0f));  // Bottom Left
            Vertices.Add(new Vector3(-0.5f, 0.5f, 0.0f));   // Top Left
        }
    }
}
