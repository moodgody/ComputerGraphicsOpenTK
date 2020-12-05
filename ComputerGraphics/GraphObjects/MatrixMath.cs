﻿/********************************************************************************************
 * Copyright (c) Computer Graphics Course by Fayoum University 
 * Prof. Amr M. Gody, amg00@fayoum.edu.eg
 * License: free for use and distribution for Educational purposes. It is required to keep this header comments on your code. 
 * Purpose:             Math helper Object
 *
 * Ver  Date         By     Purpose
 * ---  ----------- -----   --------------------------------------------------------------------
 * 01   2020-12-05  AMG     Created the initial version.
 *************************************************************************************************/

using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerGraphics.GraphObjects
{
    public static class MatrixMath
    {
        public static Matrix4 Scale(Vector3 scale)
        {
            Matrix4 res = new Matrix4();
            res[0, 0] = scale.X > 0 ? scale.X:1.0f;
            res[1, 1] = scale.Y > 0 ? scale.Y : 1.0f;
            res[2, 2] = scale.Z > 0 ? scale.Z : 1.0f;
            res[3, 3] = 1.0f;
            return res;
        }
        public static Matrix4 Sclae(float scale)
        {
            
            return Scale(new Vector3(scale));
        }
        public static Matrix4 Translate(Vector3 value)
        {
            Matrix4 res = new Matrix4();
            res.Diagonal = new Vector4(1.0f);
            res.Column3 = new Vector4(value, 1.0f);
           
            return res;
        }
        public static Matrix4 Translate(float value)
        {
          

            return Translate(new Vector3(value));
        }
        public static Matrix4 TranslateX(float x)
        {
            

            return Translate(new Vector3(x,0.0f,0.0f));
        }
        public static Matrix4 TranslateY(float y)
        {


            return Translate(new Vector3(0.0f, y, 0.0f));
        }
        public static Matrix4 TranslateZ(float z)
        {


            return Translate(new Vector3(0.0f, 0.0f, z));
        }
        public static Matrix4 RotateZ(float degrees)
        {
            Matrix4 res = new Matrix4();
            res.Diagonal = new Vector4(1.0f);
            float theta = MathHelper.DegreesToRadians(degrees);
            float cos_Theta =(float) MathHelper.Cos(theta);
            float sin_Theta = (float) MathHelper.Sin(theta);
            res[0, 0] = cos_Theta;
            res[0, 1] = -sin_Theta;
            res[1, 0] = sin_Theta;
            res[1, 1] = cos_Theta;
            
            return res;
        }
        public static Matrix4 RotateX(float degrees)
        {
            Matrix4 res = new Matrix4();
            res.Diagonal = new Vector4(1.0f);
            float theta = MathHelper.DegreesToRadians(degrees);
            float cos_Theta = (float)MathHelper.Cos(theta);
            float sin_Theta = (float)MathHelper.Sin(theta);
            res[1, 1] = cos_Theta;
            res[1, 2] = -sin_Theta;
            res[2, 1] = sin_Theta;
            res[2, 2] = cos_Theta;
            
            return res;
        }
        public static Matrix4 Rotate(float degrees)
        {
            

            return RotateX(degrees) * RotateY(degrees) * RotateZ(degrees);
        }
        public static Matrix4 RotateY(float degrees)
        {
            Matrix4 res = new Matrix4();
            res.Diagonal = new Vector4(1.0f);
            float theta = MathHelper.DegreesToRadians(degrees);
            float cos_Theta = (float)MathHelper.Cos(theta);
            float sin_Theta = (float)MathHelper.Sin(theta);
            res[0, 0] = cos_Theta;
            res[0, 2] = sin_Theta;
            res[2,0] = -sin_Theta;
            res[2, 2] = cos_Theta;

            return res;
        }
    }
}
