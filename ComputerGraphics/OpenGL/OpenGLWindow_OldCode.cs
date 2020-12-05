/********************************************************************************************
 * Copyright (c) Computer Graphics Course by Fayoum University 
 * Prof. Amr M. Gody, amg00@fayoum.edu.eg
 * License: free for use and distribution for Educational purposes. It is required to keep this header comments on your code. 
 * Purpose:             Old and obsolete codes are here
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
      
        private void DrawAxies(float z)
        {


            GL.Begin(PrimitiveType.Lines);
            GL.LineWidth(4);
            GL.Color3(1.0f, 0.0f, 0.0f);
            GL.Vertex3(0, 0, z);
            GL.Vertex3(1, 0, z);
            GL.Color3(0.0f, 1.0f, 0.0f);
            GL.Vertex3(0, 0, z);
            GL.Vertex3(0, 1, z);
            GL.Color3(0.0f, 0.0f, 1.0f);
            GL.Vertex3(0, 0, z);
            GL.Vertex3(0, 0, z + 1);
            GL.End();

        }
        private void InitiateModelView()
        {
            //GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            //Matrix4 modelview = Matrix4.LookAt(Vector3.Zero,Vector3.UnitZ, Vector3.Zero);
            //GL.MatrixMode(MatrixMode.Modelview);
            //GL.LoadMatrix(ref modelview);
            //MoveTheModelAway(4);
        }

        void display_triangle()
        {

            GL.CallList(_list);
        }
        int _list;
        void initTriangleList()
        {
            _list = GL.GenLists(1);
            GL.NewList(_list, ListMode.Compile);
            GL.Begin(PrimitiveType.Triangles);
            GL.Color3(1.0f, 1.0f, 0.0f); GL.Vertex3(-1.0f, -1.0f, 4.0f);
            GL.Color3(1.0f, 0.0f, 0.0f); GL.Vertex3(1.0f, -1.0f, 4.0f);
            GL.Color3(0.2f, 0.9f, 1.0f); GL.Vertex3(0.0f, 1.0f, 4.0f);
            GL.End();
            GL.EndList();
        }

    }
}
