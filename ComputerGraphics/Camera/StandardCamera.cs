/********************************************************************************************
 * Copyright (c) Computer Graphics Course by Fayoum University 
 * Prof. Amr M. Gody, amg00@fayoum.edu.eg
 * License: free for use and distribution for Educational purposes. It is required to keep this header comments on your code. 
 * Purpose:             Triangle Object
 *
 * Ver  Date         By     Purpose
 * ---  ----------- -----   --------------------------------------------------------------------
 * 01   2020-12-20  AMG     Created the initial version.
 *************************************************************************************************/
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTKTut.Shapes;
using System;
using System.Collections.Generic;
using System.Text;


namespace ComputerGraphics.Camera
{
    public delegate void CameraMove();
    class StandardCamera
    {
        public event CameraMove OnCameraMove;
        public void RefreshParent()
        {
            if(OnCameraMove!= null)
            {
                OnCameraMove.Invoke();
            }
        }
        public Vector3 CameraPosition { get; set; }
        public Vector3 Direction { get; set; }
        public Vector3 Right { get; set; }
        public Vector3 Up { get; set; }
        public Vector3 CameraTarget { get; set; }
        public Matrix4 View { get; private set; }
        public float Speed { get; set; }
        private Vector3 _front;
        public StandardCamera(Vector3 position,Vector3 cameraTarget,Vector3 up)
        {
            CameraPosition = position;
            CameraTarget = cameraTarget;
            Up = Vector3.Normalize(up);
            Speed = 0.001f;

            UpdateConfigurations();

        }

        

        public StandardCamera()
        {
            Reset();
            UpdateConfigurations();

        }

        internal void MoveLeft()
        {
            CameraPosition -= Vector3.Normalize(Vector3.Cross(_front, Up)) * Speed; //Left
            UpdateConfigurations();
        }

        internal void Reset()
        {
            CameraPosition = new Vector3(0.0f, 0.0f, 0.1f);
            CameraTarget = new Vector3(0.0f, 0.0f, -10.0f);
            Up = Vector3.UnitY;
            Speed = 0.0001f;
            UpdateConfigurations();
        }

        internal void MoveRight()
        {
            CameraPosition += Vector3.Normalize(Vector3.Cross(_front, Up)) * Speed; //Left
            UpdateConfigurations();
        }

        internal void MoveUp()
        {
            CameraPosition += Up * Speed; //Up 
            UpdateConfigurations();
        }

        internal void MoveDown()
        {
            CameraPosition -= Up * Speed; //Up 
            UpdateConfigurations();
        }

        internal void Backward()
        {
            CameraPosition -= _front * Speed;
            UpdateConfigurations();
        }
        internal void Forward()
        {
            CameraPosition += _front * Speed;
            UpdateConfigurations();
        }
        private void UpdateConfigurations()
        {
            Direction = Vector3.Normalize(CameraPosition - CameraTarget); // Camera view is in reverse of camera 
            Right = Vector3.Cross(Up, Direction);
            View = Matrix4.LookAt(CameraPosition, CameraTarget, Up);

            _front = -Direction;
            RefreshParent();
        }

        
    }
}
