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
            GraphObjects.Triangle t2 = new GraphObjects.Triangle();
            t2.Vertices.Clear();
            t2.Vertices.Add(new OpenTK.Mathematics.Vector3(-1.0f, -0.6f, 0.0f));
            t2.Vertices.Add(new OpenTK.Mathematics.Vector3(-0.5f, -1.0f, 0.0f));
            t2.Vertices.Add(new OpenTK.Mathematics.Vector3(0.0f, 0.6f, 0.0f));
            ogl.AddGraph(t2);
            ogl.AddGraph(new GraphObjects.Triangle());
           
            ogl.AddGraph(new GraphObjects.Rectangle(0f,0f,0.7f,0.3f));
        }
    }
}
