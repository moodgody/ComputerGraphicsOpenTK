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
        
        protected int _start;
        protected int _count;
        protected int _v0; //First element index.
        protected uint _refpoint;
        public List<Vector3> Vertices { get; set; }
        public GraphObject()
        {
            Vertices = new List<Vector3>();
            LoadVertexBufferWithStandardShape();
        }

        public virtual void OnLoad(List<Vector3> vertices)
        {
            _start = vertices.Count;
            vertices.AddRange(Vertices);
            _count = Vertices.Count;
        }
        protected Matrix4 model;
        protected Matrix4 view;
       public virtual  void OnRenderFrame(FrameEventArgs args)
        {

            model = Matrix4.Identity;
            view = Matrix4.Identity;
           

        }
       public virtual void OnUnload()
        {
           
            
        }

        public virtual void OnLoadElementIndices(List<uint> indicesBuffer)
        {
            _v0 = indicesBuffer.Count;
            _refpoint = (uint)_start;
        }

        protected virtual void LoadVertexBufferWithStandardShape()
        {

        }
    }
}
