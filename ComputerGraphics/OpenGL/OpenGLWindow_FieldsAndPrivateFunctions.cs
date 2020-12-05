/********************************************************************************************
 * Copyright (c) Computer Graphics Course by Fayoum University 
 * Prof. Amr M. Gody, amg00@fayoum.edu.eg
 * License: free for use and distribution for Educational purposes. It is required to keep this header comments on your code. 
 * Purpose:             All private functions and fields of the Game window has been provided here
 *
 * Ver  Date         By     Purpose
 * ---  ----------- -----   --------------------------------------------------------------------
 * 01   2020-12-05  AMG     Created the initial version.
 *************************************************************************************************/

using ComputerGraphics.GraphObjects;
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
        List<GraphObjects.GraphObject> _graphObjects = new List<ComputerGraphics.GraphObjects.GraphObject>();
        private void DrawAllObjects(FrameEventArgs args)
        {

            foreach (var obj in _graphObjects)
            {
                obj.OnRenderFrame(args, this);
            }
        }
        private void LoadVertexArrayInAllObjects()
        {

            foreach (var obj in _graphObjects)
            {
                obj.OnLoad(ShaderProgram);
            }



        }

    }
}
