using OpenTK.Windowing.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerGraphics.GraphObjects
{
    interface IGraphObject
    {
        
        protected  void OnUnload();
        protected void OnLoad();
        protected void OnRenderFrame(FrameEventArgs args);
    }
}
