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
        KeyboardState _input;
        Matrix4 _transformation;
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
            _transformation = Matrix4.Identity;
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
            _input = KeyboardState;

            if (_input.IsKeyDown(Keys.Escape))
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
            return _input.IsKeyDown(Keys.Up) ? Navigations.Forward :
                    _input.IsKeyDown(Keys.Down) ? Navigations.Backward :
                    _input.IsKeyDown(Keys.Left) ? Navigations.Left :
                    _input.IsKeyDown(Keys.Right) ? Navigations.Right :
                    Navigations.None;
        }

        private void MoveTheModelAway(float v)
        {
           // _transformation = Transformation(Vector3.UnitZ * v, Vector3.Zero, Vector3.One);
            //GL.MatrixMode(MatrixMode.Modelview);
            //GL.Translate(Vector3.UnitZ * v);
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
            _transformation *=  Matrix4.CreateTranslation(0.0f,0.01f,0.0f);
            //GL.MatrixMode(MatrixMode.Modelview);
            //GL.Translate(-Vector3.UnitZ * v);
        }

        private void MoveModelRight(float v)
        {
           // _transformation = Transformation(Vector3.UnitX * v, Vector3.Zero, Vector3.One);
            _transformation =_transformation * Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(5.0f));
            //GL.MatrixMode(MatrixMode.Modelview);
            //GL.Translate(Vector3.UnitX * v);
        }

        private void MoveModelLeft(float v)
        {
            _transformation = _transformation * Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(-5.0f));
           
        }

        private Matrix4 Transformation(Vector3 translationVector, Vector3 rotationVector,Vector3 scaleVector)
        {
            
            Matrix4 rotationZ = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotationVector.Z));
            Matrix4 rotationX = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotationVector.X));
            Matrix4 rotationY = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotationVector.Y));
            Matrix4 scale = Matrix4.CreateScale(scaleVector);
            Matrix4 translation = Matrix4.CreateTranslation(translationVector);
            Matrix4 res = rotationX * rotationY * rotationZ * scale * translation;
            
            return res;
        }
    }
}
