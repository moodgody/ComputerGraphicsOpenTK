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
             Matrix4.CreateOrthographic(20.0f, 20.0f,  0.1f, 100.0f, out var p);
            //var p = Matrix4.Identity;
           var model = Matrix4.Identity;
            var t1 = p * model;
            _shader.SetMatrix4(Shader.ShaderMatrix.model, ref t1);
            GL.DrawArrays(PrimitiveType.LineLoop, 0, 3);
            var value = p * model * GetVertices();

            float dlta = (float)(DateTime.Now.Second) / 120.0f;
             model = Matrix4.CreateTranslation(-0.03f, 0.0f, -70.0f);
            model = model * Matrix4.CreateScale(40.0f);
            //var v = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(-180.0f));
            model = new Matrix4(3.0f, 0f, 0f, -5.0f, 0f, 1f, 0f, 0.3f, 0f, 0f, 1f, 0.0f, 0f, 0f, 0f, 1);
            var v = Matrix4.Identity;
            var t = p * v * model;
           
            _shader.SetMatrix4(Shader.ShaderMatrix.model, ref t);
            GL.DrawArrays(PrimitiveType.LineLoop, 0, 3  );
            value = p * model * GetVertices();
        }
        protected Matrix4 GetVertices()
        {
            Matrix4 res = new Matrix4();
            res.Column0 = new Vector4(LocalVertices[0], 1.0f);
            res.Column1 = new Vector4(LocalVertices[1], 1.0f);
            res.Column2 = new Vector4(LocalVertices[2], 1.0f);
            res.Column3 = new Vector4(0.0f);
            return res;
        }
        protected override void LoadVertexBufferWithStandardShape()
        {
            LocalVertices.Add(new Vector3(-0.5f, -0.5f, 0.0f));
            LocalVertices.Add(new Vector3(0.5f, -0.5f, 0.0f));
            LocalVertices.Add(new Vector3(0.0f, 0.5f, 0.0f));
        }
    }
}
