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
       

       

       

        protected override bool ImportStandtradShapeData()
        {
            bool res =  base.ImportStandtradShapeData();

            LocalVertices.Add(new Vector3(0.5f, 0.5f, 0.0f));    // Top Right
            LocalVertices.Add(new Vector3(0.5f, -0.5f, 0.0f));   // Bottom Right
            LocalVertices.Add(new Vector3(-0.5f, -0.5f, 0.0f));  // Bottom Left
            LocalVertices.Add(new Vector3(-0.5f, 0.5f, 0.0f));   // Top Left
            return res;
        }
    }
}
