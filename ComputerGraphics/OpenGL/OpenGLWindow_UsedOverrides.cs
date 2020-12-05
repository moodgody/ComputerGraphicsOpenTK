/********************************************************************************************
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
            Height = e.Height;
            Width = e.Width;
            GL.Viewport(0, 0, e.Width, e.Height);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();




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
