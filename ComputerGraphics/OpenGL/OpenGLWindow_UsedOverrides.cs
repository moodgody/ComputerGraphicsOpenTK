﻿/********************************************************************************************
 * Copyright (c) Computer Graphics Course by Fayoum University 
 * Prof. Amr M. Gody, amg00@fayoum.edu.eg
 * License: free for use and distribution for Educational purposes. It is required to keep this header comments on your code. 
 * Purpose:             Utilized overrides are developed here
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
using System.Reflection;
using System.Text;



namespace ComputerGraphics
{
    internal partial class OpenGLWindow : GameWindow
    {
       

        protected override void OnUnload()
        {
            base.OnUnload();
            ShaderProgram.Dispose();

            foreach (var obj in _graphObjects)
            {
                obj.OnUnload();
            }

        }
        protected override void OnClosed()
        {
            OnUnload();
            base.OnClosed();

        }
        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            //Code goes here
            ShaderProgram = new Shader("shader.vert", "shader.frag");
            LoadVertexArrayInAllObjects();


        }
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            float height = e.Height;
            float width = e.Width;
            float viewport_world_width_units = 30.0f;
            float viewport_world_Height_units = 20.0f;

            float aspectRatio =(viewport_world_width_units/ width) / (viewport_world_Height_units/ height);
            GL.Viewport(0, 0, e.Width, e.Height);
            
            this.Projection = GraphObjects.MatrixMath.PerspectiveProjection(MathHelper.DegreesToRadians(45.0), aspectRatio, viewport_world_width_units, viewport_world_Height_units, 0.1f, 100.0f); 
            //this.Projection = GraphObjects.MatrixMath.OrthogonalProjection(30.0f,20.0f,0.1f,100.0f);




        }
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            ShaderProgram.Use();
           //Code goes here.
            DrawAllObjects(args);
            GL.Flush();
            Context.SwapBuffers();
            base.OnRenderFrame(args);


        }
    }
}
