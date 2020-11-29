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
           // var p = Matrix4.CreateOrthographicOffCenter(0.0f, 5.0f, 0.0f, 5.0f, 0.1f, 100.0f);
            var p = Matrix4.Identity;
           var model = Matrix4.Identity;
            var t1 = p * model;
            _shader.SetMatrix4(Shader.ShaderMatrix.model, ref t1);
            GL.DrawArrays(PrimitiveType.LineLoop, 0, 3);

            float dlta = (float)(DateTime.Now.Second) / 120.0f;
             model = Matrix4.CreateTranslation(0.0f, 0.0f, 0.0f);
            var v = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(-180.0f));
          
            var t = p * v * model;
            _shader.SetMatrix4(Shader.ShaderMatrix.model, ref t);
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
