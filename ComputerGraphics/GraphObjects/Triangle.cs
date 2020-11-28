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
            _worldReferencePoint = Vector3.Zero;
            

        }
        public Triangle(Vector3 worldCoordinateReferencePoint ) : base()
        {
            _worldReferencePoint = worldCoordinateReferencePoint;
            

        }

       

       

        public override void OnRenderFrame(FrameEventArgs args,  OpenGLWindow parent)
        {
            base.OnRenderFrame(args, parent);
           
            GL.DrawArrays(PrimitiveType.LineLoop, 0, 3  );
            
        }

        protected override void LoadVertexBufferWithStandardShape()
        {
            LocalVertices.Add(new Vector3(-0.5f, -0.5f, -0.5f));
            LocalVertices.Add(new Vector3(0.5f, -0.5f, -0.5f));
            LocalVertices.Add(new Vector3(0.0f, 0.5f, -0.5f));
        }
    }
}
