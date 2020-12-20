﻿/********************************************************************************************
 * Copyright (c) Computer Graphics Course by Fayoum University 
 * Prof. Amr M. Gody, amg00@fayoum.edu.eg
 * License: free for use and distribution for Educational purposes. It is required to keep this header comments on your code. 
 * Purpose:             Shader interface logic has been provided here
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
using System.Text;

using System.IO;


namespace ComputerGraphics
{
    public class Shader : IDisposable
    {
        public enum ShaderMatrix
        {
            model,
            view,
            projection,
            lightColor
        }
        int Handle;
        private bool disposedValue = false;
        public string ShaderInfoLog { get; private set; }
        public Shader(string vertexPath, string fragmentPath) 
        {
            string VertexShaderSource;

            using (StreamReader reader = new StreamReader(vertexPath, Encoding.UTF8))
            {
                VertexShaderSource = reader.ReadToEnd();
            }

            string FragmentShaderSource;

            using (StreamReader reader = new StreamReader(fragmentPath, Encoding.UTF8))
            {
                FragmentShaderSource = reader.ReadToEnd();
            }
            //Then, we generate our shaders, and bind the source code to the shaders.
            var VertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(VertexShader, VertexShaderSource);

            var FragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            
            GL.ShaderSource(FragmentShader, FragmentShaderSource);
            //Then, we compile the shaders and check for errors.
            GL.CompileShader(VertexShader);

            string infoLogVert = GL.GetShaderInfoLog(VertexShader);
            if (infoLogVert != System.String.Empty)
                System.Console.WriteLine(infoLogVert);

            GL.CompileShader(FragmentShader);

            string infoLogFrag = GL.GetShaderInfoLog(FragmentShader);

            if (infoLogFrag == System.String.Empty && infoLogVert == string.Empty)
            {
                ShaderInfoLog = infoLogFrag;
                 Handle = GL.CreateProgram();

                GL.AttachShader(Handle, VertexShader);
                GL.AttachShader(Handle, FragmentShader);

                GL.LinkProgram(Handle);
                //Before we leave the constructor, we should do a little cleanup. The individual vertex and fragment shaders are useless now that they've been linked; the compiled data is copied to the shader program when you link it. You also don't need to have those individual shaders attached to the program; let's detach and then delete them.
                GL.DetachShader(Handle, VertexShader);
                GL.DetachShader(Handle, FragmentShader);
                GL.DeleteShader(FragmentShader);
                GL.DeleteShader(VertexShader);
            }
        }
        /// <summary>
        /// Bind matrix to shader matrix
        /// </summary>
        /// <param name="nameInShader"></param>
        /// <param name="matrix"></param>
        public void SetMatrix4(ShaderMatrix nameInShader,ref Matrix4 matrix)
        {
            int location = GL.GetUniformLocation(Handle, nameInShader.ToString());
            GL.UniformMatrix4(location, true, ref matrix);
        }
        public void SetVector3(ShaderMatrix nameInShader,  Vector3 vec3)
        {
            int location = GL.GetUniformLocation(Handle, nameInShader.ToString());
            GL.Uniform3(location,  vec3.X, vec3.Y, vec3.Z);
        }
        public void Use()
        {
            
            GL.UseProgram(Handle);
        }

        public int GetAttribLocation(string attribName)
        {
            return GL.GetAttribLocation(Handle, attribName);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                GL.DeleteProgram(Handle);

                disposedValue = true;
            }
        }

        ~Shader()
        {
            GL.DeleteProgram(Handle);
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
