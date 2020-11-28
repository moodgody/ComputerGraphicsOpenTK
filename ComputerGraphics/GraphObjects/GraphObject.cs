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
        
       
       
        protected Vector3 _worldReferencePoint;
        public List<Vector3> LocalVertices { get; set; }
        public int VertexArrayObject { get; private set; }
        public int VertexBufferObject { get; private set; }

        public GraphObject()
        {
            LocalVertices = new List<Vector3>();
            LoadVertexBufferWithStandardShape();
        }
        public virtual void OnLoad(Shader shader)
        {
            _shader = shader;
             OnLoadObject();
        }
     
        Shader _shader;
        private float[] ConvertToFloatArray(List<Vector3> verticesBuffer)
        {
            List<float> buffer = new List<float>();
            foreach (var vertice in verticesBuffer)
            {
                buffer.Add(vertice.X);
                buffer.Add(vertice.Y);
                buffer.Add(vertice.Z);
            }
            return buffer.ToArray();
        }

        protected void OnLoadObject()
        {
//            float[] vertices = {
//    -0.5f, -0.5f, -0.5f,  0.0f, 0.0f,
//     0.5f, -0.5f, -0.5f,  1.0f, 0.0f,
//     0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
//     0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
//    -0.5f,  0.5f, -0.5f,  0.0f, 1.0f,
//    -0.5f, -0.5f, -0.5f,  0.0f, 0.0f,

//    -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
//     0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
//     0.5f,  0.5f,  0.5f,  1.0f, 1.0f,
//     0.5f,  0.5f,  0.5f,  1.0f, 1.0f,
//    -0.5f,  0.5f,  0.5f,  0.0f, 1.0f,
//    -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,

//    -0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
//    -0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
//    -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
//    -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
//    -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
//    -0.5f,  0.5f,  0.5f,  1.0f, 0.0f,

//     0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
//     0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
//     0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
//     0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
//     0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
//     0.5f,  0.5f,  0.5f,  1.0f, 0.0f,

//    -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
//     0.5f, -0.5f, -0.5f,  1.0f, 1.0f,
//     0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
//     0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
//    -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
//    -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,

//    -0.5f,  0.5f, -0.5f,  0.0f, 1.0f,
//     0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
//     0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
//     0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
//    -0.5f,  0.5f,  0.5f,  0.0f, 0.0f,
//    -0.5f,  0.5f, -0.5f,  0.0f, 1.0f
//};
            float[] vertices =  ConvertToFloatArray( LocalVertices);
           // uint[] indices = LoadElementIndicesFromAllObjects();
            VertexArrayObject = GL.GenVertexArray();
            //// ..:: Initialization code (done once (unless your object frequently changes)) :: ..
            //// 1. bind Vertex Array Object
            GL.BindVertexArray(VertexArrayObject);
            //// 2. copy our vertices array in a buffer for OpenGL to use
            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
            //// 3. then set our vertex attributes pointers
             GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

           
            GL.Enable(EnableCap.DepthTest);
           
        }

        public virtual  void OnRenderFrame(FrameEventArgs args, OpenGLWindow parent)
        {
           // Matrix4 model = Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(DateTime.Now.Second));
            float dlta = (float)(DateTime.Now.Second) / 120.0f;
           var  model =  Matrix4.CreateTranslation(0.0f, -0.5f , 1.0f);
           // var Model = Matrix4.CreateTranslation( _worldReferencePoint);
            _shader.SetMatrix4(Shader.ShaderMatrix.model, ref model);

        }
       public virtual void OnUnload()
        {
           
            
        }

        

        protected virtual void LoadVertexBufferWithStandardShape()
        {

        }
    }
}
