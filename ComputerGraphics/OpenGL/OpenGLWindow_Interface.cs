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
            Height = nativeWindowSettings.Size.Y;
            Width = nativeWindowSettings.Size.X;
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

        public Matrix4 Model;
        public Matrix4 View;
        public Matrix4 Projection;
        protected override void OnLoad()
        {
            base.OnLoad();
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
            GL.Enable(EnableCap.DepthTest);
            // SetModelViewProjectionMatrices();
            

        }

        private void SetModelViewProjectionMatrices()
        {
            //Model = Matrix4.Identity;
            //_shader.SetMatrix4("model", ref Model);
           // _shader.SetMatrix4("view", ref _view);
            //_shader.SetMatrix4("projection", ref _projection);
            //_model = Matrix4.Identity;
            //_view = Matrix4.CreateTranslation(0f, 0f, -0.3f);
            //_projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), Width / Height, 0.1f, 100.0f);
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
        public int Width { get; private set; }
        public int Height { get; private set; }

        internal void AddGraph(GraphObject obj)
        {
            _graphObjects.Add(obj);
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            Height = e.Height;
            Width = e.Width;
            GL.Viewport(0, 0, e.Width, e.Height);
            float width = (float)Width;
            float height = (float)Height;
            View = Matrix4.Identity;//  Matrix4.CreateTranslation(0.0f,0.0f,5.0f);
            Model = Matrix4.Identity;
            Projection =   Matrix4.CreateOrthographicOffCenter(0.0f, width, 0.0f, height, 0.1f, 100.0f);
            
            base.OnResize(e);



        }
        
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            _shader.Use();
            //Code goes here.

          
            GL.BindVertexArray(VertexArrayObject);
            DrawAllObjects(args);
            _shader.SetMatrix4(Shader.ShaderMatrix.model, ref Model);
            _shader.SetMatrix4(Shader.ShaderMatrix.view, ref View);
            _shader.SetMatrix4(Shader.ShaderMatrix.projection, ref Projection);
            Context.SwapBuffers();
            base.OnRenderFrame(args);


        }

        private void DrawAllObjects(FrameEventArgs args)
        {
            
            foreach (var obj in _graphObjects)
            {
                
               
                obj.OnRenderFrame(args,this);

            }
        }
    }
}
