/********************************************************************************************
 * Copyright (c) Computer Graphics Course by Fayoum University 
 * Prof. Amr M. Gody, amg00@fayoum.edu.eg
 * License: free for use and distribution for Educational purposes. It is required to keep this header comments on your code. 
 * Purpose:             Triangle Object
 *
 * Ver  Date         By     Purpose
 * ---  ----------- -----   --------------------------------------------------------------------
 * 01   2020-12-05  AMG     Created the initial version.
 *************************************************************************************************/
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTKTut.Shapes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerGraphics.LightSource
{
    class PointSource : LightObject
    {
        public float Radius { get; set; }
        public PointSource() : base()
        {
        }

        public PointSource(Vector3 refpoint, float radius) : base(refpoint, radius, radius)
        {
            Radius = radius;
        }

        public override void OnRenderFrame(FrameEventArgs args, OpenGLWindow parent)
        {
            base.OnRenderFrame(args, parent);
            if (_useElements)
            {
                GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
            }
            else
            {
                GL.DrawArrays(PrimitiveType.LineStrip, 0, LocalVertices.Count);
            }

        }



        protected override bool ImportStandtradShapeData()
        {
            var MeshPolygons = MeshElement.Sphere(0.5);
            for (int i = 0; i < MeshPolygons.Length; i++)
            {
                //GL.Normal3(MeshPolygons[i].Normal);
                for (int j = 0; j < MeshPolygons[i].Vertices.Length; j++)
                {
                    LocalVertices.Add(MeshPolygons[i].Vertices[j]);

                }

            }
            return base.ImportStandtradShapeData();
        }
    }
}
