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
        public Shader _shader;
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

       
        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            //Code goes here
            _shader = new Shader("shader.vert", "shader.frag");
            LoadVertexArrayInAllObjects();
           

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

       
        private void LoadVertexArrayInAllObjects()
        {
           
            foreach (var obj in _graphObjects)
            {
                obj.OnLoad(_shader);
            }

        
           
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
            base.OnResize(e);
            Height = e.Height;
            Width = e.Width;
            GL.Viewport(0, 0, e.Width, e.Height);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            



        }
        
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            _shader.Use();


            //Code goes here.
            DrawAllObjects(args);
            
            GL.Flush();
           
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
