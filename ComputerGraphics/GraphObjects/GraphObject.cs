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
        
       
       
        public float Width { get; set; }
        public float Height { get; set; }
        public List<Vector3> LocalVertices { get; set; }
        public List<Vector3> VerticesColors { get; set; }
        public int VertexArrayObject { get; private set; }
        public int VertexBufferObject { get; private set; }
        protected Shader _shader;
        protected Vector3 _worldReferencePoint;
        protected float[] _vertices;
        Matrix4 _model;
        Matrix4 _view;
        Matrix4 _ModelView;
        bool _valid;
        public GraphObject()
        {
            LocalVertices = new List<Vector3>();
            VerticesColors = new List<Vector3>();
            this.Width = 1.0f;
            this.Height = 1.0f;
            _worldReferencePoint = Vector3.Zero;
           _valid = ImportStandtradShapeData() && UpdateModelViewMatrix();

        }
        public GraphObject(Vector3 refpoint,float width, float height)
        {
            LocalVertices = new List<Vector3>();
            VerticesColors = new List<Vector3>();
            this.Width = width;
            this.Height = height;
            _worldReferencePoint = refpoint;
            _valid = ImportStandtradShapeData() && UpdateModelViewMatrix();
           
        }

        private bool UpdateModelViewMatrix()
        {
            bool res = true;
            _model = MatrixMath.Translate(_worldReferencePoint) * MatrixMath.Scale(new Vector3(Width,Height,1.0f));
            _view = Matrix4.Identity;

            _ModelView = _view * _model;
            return res;
        }

        public virtual void OnLoad(Shader shader)
        {
            _shader = shader;
             OnLoadObject();
        }
     
        
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

           
            VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObject);
            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);
            
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), (3 * sizeof(float)));
            GL.EnableVertexAttribArray(1);
            
        }

        public virtual  void OnRenderFrame(FrameEventArgs args, OpenGLWindow parent)
        {
            Matrix4.CreateOrthographic(20.0f, 20.0f, 0.1f, 100.0f, out var p);
            var t = p * _ModelView;
            _shader.SetMatrix4(Shader.ShaderMatrix.model, ref t);

        }
       public virtual void OnUnload()
        {
           
            
        }

        

        protected virtual bool ImportStandtradShapeData()
        {
            
            if (LocalVertices.Count != VerticesColors.Count)
            {
                NormalizeColorsList();
            }
            
            List<Vector3> all = new List<Vector3>();
            for(int i=0;i< LocalVertices.Count;i++)
            {
                all.Add(LocalVertices[i]);
                all.Add(VerticesColors[i]);
            }
            _vertices = ConvertToFloatArray(all);
            bool res = _vertices.Length == LocalVertices.Count * 3 * 2;
            return res;
        }

        private void NormalizeColorsList()
        {
            Vector3 defaultColor = new Vector3(1.0f, 1.0f, 0.0f);
            for(int i=VerticesColors.Count;i<LocalVertices.Count;i++)
            {
                VerticesColors.Add(defaultColor);
            }

        }
    }
}
