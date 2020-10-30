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
        Dictionary<Navigations, NavigationFunction> _navigationFunction = new Dictionary<Navigations, NavigationFunction>();
        private void LoadNavigationFunctions()
        {
            _navigationFunction.Add(Navigations.Forward, MoveTheModeltome);
            _navigationFunction.Add(Navigations.Backward, MoveTheModelAway);
            _navigationFunction.Add(Navigations.Left, MoveModelLeft);
            _navigationFunction.Add(Navigations.Right, MoveModelRight);
            _navigationFunction.Add(Navigations.None, NoNavigation);
        }

        private void NoNavigation(float value)
        {
            //Todo: do nothing
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            input = KeyboardState;

            if (input.IsKeyDown(Keys.Escape))
            {
                Dispose();
            }
            else
            {
               _navigationFunction[GetNavigation()](1);
            }
           
            base.OnUpdateFrame(args);
        }

        private Navigations GetNavigation()
        {
            return input.IsKeyDown(Keys.Up) ? Navigations.Forward :
                    input.IsKeyDown(Keys.Down) ? Navigations.Backward :
                    input.IsKeyDown(Keys.Left) ? Navigations.Left :
                    input.IsKeyDown(Keys.Right) ? Navigations.Right :
                    Navigations.None;
        }

        private void MoveTheModelAway(float v)
        {
           
            GL.MatrixMode(MatrixMode.Modelview);
            GL.Translate(Vector3.UnitZ * v);
        }



        private void RotateModelRight(float v)
        {
            
            GL.MatrixMode(MatrixMode.Modelview);
            GL.Rotate(v, -Vector3.UnitY);
        }

        private void RotateModelLeft(float v)
        {
                        GL.MatrixMode(MatrixMode.Modelview);
            GL.Rotate(v, Vector3.UnitY);
        }



        private void MoveTheModeltome(float v)
        {
           
            GL.MatrixMode(MatrixMode.Modelview);
            GL.Translate(-Vector3.UnitZ * v);
        }

        private void MoveModelRight(float v)
        {
           
            GL.MatrixMode(MatrixMode.Modelview);
            GL.Translate(Vector3.UnitX * v);
        }

        private void MoveModelLeft(float v)
        {
            
            GL.MatrixMode(MatrixMode.Modelview);
            GL.Translate(-Vector3.UnitX * v);
        }
    }
}
