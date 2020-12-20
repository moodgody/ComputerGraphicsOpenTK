/********************************************************************************************
 * Copyright (c) Computer Graphics Course by Fayoum University 
 * Prof. Amr M. Gody, amg00@fayoum.edu.eg
 * License: free for use and distribution for Educational purposes. It is required to keep this header comments on your code. 
 * Purpose:             All private functions and fields of the Game window has been provided here
 *
 * Ver  Date         By     Purpose
 * ---  ----------- -----   --------------------------------------------------------------------
 * 01   2020-12-20  AMG     Created the initial version.
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
        List<LightSource.LightObject> _lighObjects = new List<LightSource.LightObject>();
        public void AddLightSource(LightSource.LightObject source)
        {
            _lighObjects.Add(source);
        }
        /// <summary>
        /// Creates light sources
        /// </summary>
        private void OnLoadLights()
        {
            foreach (var obj in _lighObjects)
            {
                obj.OnLoad(LightingShaderProgram);
            }
        }

        private void SetAmbientLight()
        {
            ShaderProgram.SetVector3(Shader.ShaderMatrix.lightColor, new Vector3(0.5f, 0.5f, 0.5f));
        }
        private void TrunLighsOn()
        {
            SetAmbientLight();
        }
        /// <summary>
        /// Draw light sources into the scene
        /// </summary>
        private void DrawLightSources(FrameEventArgs args)
        {
            LightingShaderProgram.Use();
            foreach (var obj in _lighObjects)
            {
                obj.OnRenderFrame(args, this);
            }
        }
    }
}
