using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTKTut.Shapes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerGraphics.GraphObjects
{
    class Sphere : GraphObject
    {
        public float Radius { get; set; }
        public Sphere(): base()
        {
        }

        public Sphere(Vector3 refpoint,float radius ) : base(refpoint, radius, radius)
        {
            Radius = radius;
        }

        public override void OnRenderFrame(FrameEventArgs args, OpenGLWindow parent)
        {
            base.OnRenderFrame(args, parent);
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);

        }

        protected override bool ConfigureElemnetsBuffer()
        {
            return base.ConfigureElemnetsBuffer();
        }

        protected override bool ImportStandtradShapeData()
        {
            var MeshPolygons = MeshElement.Sphere(Radius);
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
