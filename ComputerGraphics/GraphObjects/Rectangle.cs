/********************************************************************************************
 * Copyright (c) Computer Graphics Course by Fayoum University 
 * Prof. Amr M. Gody, amg00@fayoum.edu.eg
 * License: free for use and distribution for Educational purposes. It is required to keep this header comments on your code. 
 * Purpose:             Rectangle Object
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
    class Rectangle : GraphObject
    {
        public Rectangle() : base()
        {

        }
        public Rectangle(Vector3 refpoint, float width, float height) : base(refpoint, width, height)
        {

        }
        public override void OnRenderFrame(FrameEventArgs args, OpenGLWindow parent)
        {
            base.OnRenderFrame(args, parent);
           


        }
        protected override bool ConfigureElemnetsBuffer()
        {
            uint[] temp = { 0, 1, 3,
                            
                            1,2,3 };
            _indices = temp;

            return base.ConfigureElemnetsBuffer();
        }
        protected override bool ImportStandtradShapeData()
        {
            LocalVertices.Add(new Vector3(0.5f, 0.5f, 0.0f));    // Top Right
            LocalVertices.Add(new Vector3(0.5f, -0.5f, 0.0f));   // Bottom Right
            LocalVertices.Add(new Vector3(-0.5f, -0.5f, 0.0f));  // Bottom Left
            LocalVertices.Add(new Vector3(-0.5f, 0.5f, 0.0f));   // Top Left

            
            VerticesColors.Add(new Vector3(1.0f, 0.0f, 0.0f));
            VerticesColors.Add(new Vector3(0.0f, 1.0f, 0.0f));
            VerticesColors.Add(new Vector3(0.0f, 0.0f, 1.0f));
            return base.ImportStandtradShapeData();
        }

    }
}
