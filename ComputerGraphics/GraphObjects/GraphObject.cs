/********************************************************************************************
 * Copyright (c) Computer Graphics Course by Fayoum University 
 * Prof. Amr M. Gody, amg00@fayoum.edu.eg
 * License: free for use and distribution for Educational purposes. It is required to keep this header comments on your code. 
 * Purpose:             Parent Graph Object. This object has been developed for providing all basic functionalities for graph object. This object should be used as parent of any graph object. 
 *
 * Ver  Date         By     Purpose
 * ---  ----------- -----   --------------------------------------------------------------------
 * 01   2020-12-05  AMG     Created the initial version.
 *************************************************************************************************/

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
        public int ElementBufferObject { get; private set; }

        protected Shader _shader;
        protected Vector3 _worldReferencePoint;
        protected float[] _vertices;
        protected uint[] _indices;
        Matrix4 _model;
        Matrix4 _view;
        Matrix4 _ModelView;
        bool _valid;
        protected bool _useElements;

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
            _useElements = ConfigureElemnetsBuffer();
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), (3 * sizeof(float)));
            GL.EnableVertexAttribArray(1);
            
        }

        public virtual  void OnRenderFrame(FrameEventArgs args, OpenGLWindow parent)
        {
            GL.BindVertexArray(VertexArrayObject);
            Matrix4.CreateOrthographic(20.0f, 20.0f, 0.1f, 100.0f, out var p);
            var p2 = MatrixMath.OrthogonalProjection(20.0f, 20.0f, 0.1f, 100.0f);
            var p3 = MatrixMath.PerspectiveProjection(MathHelper.DegreesToRadians(90.0), 1.0f, 0.1f, 100.0f);
            var t = p3 * _ModelView;
            _shader.SetMatrix4(Shader.ShaderMatrix.model, ref t);
            

        }
       public virtual void OnUnload()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.DeleteBuffer(VertexBufferObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, ElementBufferObject);
            GL.DeleteBuffer(ElementBufferObject);
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
        protected virtual bool ConfigureElemnetsBuffer()
        {
            bool res = true;
            if(_indices!= null && _indices.Length>0)
            {
                ElementBufferObject = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
                GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);
            }
            else
            {
                res = false;
            }
            
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
