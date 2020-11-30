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
       public Triangle():base()
        {

        }
        public Triangle(Vector3 refpoint,float width, float height):base(refpoint,width,height)
        {

        }
       
       

       

        public override void OnRenderFrame(FrameEventArgs args,  OpenGLWindow parent)
        {
            base.OnRenderFrame(args, parent);
            GL.DrawArrays(PrimitiveType.LineLoop, 0, 3);
            
        }
        

        protected override bool ImportStandtradShapeData()
        {
            LocalVertices.Add(new Vector3(-0.5f, -0.5f, 0.0f));
            LocalVertices.Add(new Vector3(0.5f, -0.5f, 0.0f));
            LocalVertices.Add(new Vector3(0.0f, 0.5f, 0.0f));
            VerticesColors.Add(new Vector3(1.0f,0.0f,0.0f));
            VerticesColors.Add(new Vector3(0.0f, 1.0f, 0.0f));
            VerticesColors.Add(new Vector3(0.0f, 0.0f, 1.0f));
            return base.ImportStandtradShapeData();
        }
        
    }
}
