/********************************************************************************************
 * Copyright (c) Computer Graphics Course by Fayoum University 
 * Prof. Amr M. Gody, amg00@fayoum.edu.eg
 * License: free for use and distribution for Educational purposes. It is required to keep this header comments on your code. 
 * Purpose:            main window logic and user interface logic are here
 *
 * Ver  Date         By     Purpose
 * ---  ----------- -----   --------------------------------------------------------------------
 * 01   2020-12-05  AMG     Created the initial version.
 *************************************************************************************************/

using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ComputerGraphics
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BTN_start_Click(object sender, RoutedEventArgs e)
        {
            GameWindowSettings v = new GameWindowSettings()
            {
                IsMultiThreaded = false,
                RenderFrequency = 24,
                UpdateFrequency = 24
            };
            NativeWindowSettings n = new NativeWindowSettings();
             n.Size = new OpenTK.Mathematics.Vector2i(800, 600);
           using( OpenGLWindow ogl = new OpenGLWindow(GameWindowSettings.Default , NativeWindowSettings.Default))
            {
                ConstructScene(ogl);

                ogl.Run();
            }
        }

        private static void ConstructScene(OpenGLWindow ogl)
        {
           
            ogl.AddGraph(new GraphObjects.Rectangle(new Vector3(0.0f, 0.0f, -0.5f), 1.0f, 1.0f));
            ogl.AddGraph(new GraphObjects.Triangle(new Vector3(5.0f, 5.0f, -5.0f), 10.0f, 5.0f));
            ogl.AddGraph(new GraphObjects.Sphere(new Vector3(-3.0f, -0.3f, 0.0f), 5.0f));
            ogl.AddGraph(new GraphObjects.Sphere(new Vector3(0.5f, 0.5f, -0.5f), 1.0f));

        }
    }
}
