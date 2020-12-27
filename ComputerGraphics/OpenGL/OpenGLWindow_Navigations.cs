/********************************************************************************************
 * Copyright (c) Computer Graphics Course by Fayoum University 
 * Prof. Amr M. Gody, amg00@fayoum.edu.eg
 * License: free for use and distribution for Educational purposes. It is required to keep this header comments on your code. 
 * Purpose:             Navigation functions have been developed here
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
        Camera.StandardCamera _camera = new Camera.StandardCamera();
        KeyboardState input;
        delegate void NavigationFunction(float value);
        enum Navigations
        {
            Forward,
            Backward,
            Left,
            Right,
            None
        }

        private void ProcessNavigation(KeyboardState input)
        {
            if(input.IsKeyDown(Keys.W))
            {
                _camera.Forward();
            }
            else if(input.IsKeyDown(Keys.S))
            {
                _camera.Backward();
            }
            else if (input.IsKeyDown(Keys.A))
            {
                _camera.MoveLeft();
            }
            else if (input.IsKeyDown(Keys.D))
            {
                _camera.MoveRight();
            }
            else if (input.IsKeyDown(Keys.Space))
            {
                _camera.MoveUp();
            }
            else if (input.IsKeyDown(Keys.B))
            {
                _camera.MoveDown();
            }
            else if(input.IsKeyDown(Keys.M))
            {
                _camera.Reset();
            }
            else
            {
               
            }
        }
        private void _camera_OnCameraMove()
        {
            this.Projection = _camera.View;// OriginalProjection * _camera.View;
        }
        Dictionary<Navigations, NavigationFunction> _navigationFunction = new Dictionary<Navigations, NavigationFunction>();
        private void LoadNavigationFunctions()
        {
            _camera.OnCameraMove += _camera_OnCameraMove;
        }

       
    }
}
