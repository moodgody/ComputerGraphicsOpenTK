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
        
        protected int VertexBufferObject;
        protected int VertexArrayObject;
        protected int _start;
        protected int _count;
        public List<Vector3> Vertices { get; set; }
        public GraphObject()
        {
            Vertices = new List<Vector3>();
        }

        public virtual void OnLoad(List<Vector3> vertices)
        {
            _start = vertices.Count;
            vertices.AddRange(Vertices);
            _count = Vertices.Count;
        }
       public virtual  void OnRenderFrame(FrameEventArgs args)
        {
           
            
           
        }
       public virtual void OnUnload()
        {
           
            
        }
       
    }
}
