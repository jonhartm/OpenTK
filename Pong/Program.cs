namespace Pong
{
    using System.Collections.Generic;
    using System.Drawing;

    using OpenGL_Helper;
    using OpenGL_Helper.Object;
    using OpenGL_Helper.Shaders;

    using Objects;

    class Program
    {
        static void Main(string[] args)
        {
            using (Window win = new Window("Pong"))
            {
                win.Load += Win_Load;
                win.Render += Win_Render;

                win.Run();
            }
        }

        private static void Win_Load()
        {
            ObjectManager.AddObject(new GLRectangle(new RectangleF(new PointF(-1.0f, -1.0f), new SizeF(2f, 2f))));
        }

        private static void Win_Render()
        {
            ObjectManager.RenderAll();
        }
    }
}
