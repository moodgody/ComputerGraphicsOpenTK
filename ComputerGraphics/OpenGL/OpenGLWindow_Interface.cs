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
        Shader _shader;
        int VertexBufferObject;
        int VertexArrayObject;
        public OpenGLWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
           
            LoadNavigationFunctions();
        }

        

        protected override void OnUnload()
        {
            _shader.Dispose();
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(VertexBufferObject);
            foreach (var obj in _graphObjects)
            {
                obj.OnUnload();
            }
            base.OnUnload();
        }

  
        protected override void OnLoad()
        {
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            //Code goes here
            _shader = new Shader("shader.vert", "shader.frag");

            float[] vertices = LoadVertexArrayFromAllObjects();
            uint[] indices = LoadElementIndicesFromAllObjects();
            VertexArrayObject = GL.GenVertexArray();
            //// ..:: Initialization code (done once (unless your object frequently changes)) :: ..
            //// 1. bind Vertex Array Object
            GL.BindVertexArray(VertexArrayObject);
            //// 2. copy our vertices array in a buffer for OpenGL to use
            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
            //// 3. then set our vertex attributes pointers
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
            base.OnLoad();

        }

        private uint[] LoadElementIndicesFromAllObjects()
        {
            List<uint> indicesBuffer = new List<uint>();
            foreach (var obj in _graphObjects)
            {
                obj.OnLoadElementIndices(indicesBuffer);
            }

           
            return indicesBuffer.ToArray();
        }

        private float[] LoadVertexArrayFromAllObjects()
        {
            List<Vector3> verticesBuffer = new List<Vector3>();
            foreach (var obj in _graphObjects)
            {
                obj.OnLoad(verticesBuffer);
            }

            float[] vertices = ConvertToFloatArray(verticesBuffer);
            return vertices;
        }

        private float[] ConvertToFloatArray(List<Vector3> verticesBuffer)
        {
            List<float> buffer = new List<float>();
            foreach(var vertice in verticesBuffer)
            {
                buffer.Add(vertice.X);
                buffer.Add(vertice.Y);
                buffer.Add(vertice.Z);
            }
            return buffer.ToArray();
        }

        List<GraphObjects.GraphObject> _graphObjects = new List<ComputerGraphics.GraphObjects.GraphObject>();

        public int ElementBufferObject { get; private set; }

        internal void AddGraph(GraphObject obj)
        {
            _graphObjects.Add(obj);
        }

        protected override void OnResize(ResizeEventArgs e)
        {

            GL.Viewport(0, 0, e.Width, e.Height);
            base.OnResize(e);



        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            _shader.Use();
            //Code goes here.
           
            GL.BindVertexArray(VertexArrayObject);
            DrawAllObjects(args);
            Context.SwapBuffers();
            base.OnRenderFrame(args);


        }

        private void DrawAllObjects(FrameEventArgs args)
        {
            foreach (var obj in _graphObjects)
            {
                obj.OnRenderFrame(args);

            }
        }
    }
}
