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
                win.Update += Win_Update;
                win.Render += Win_Render;

                win.Run();
            }
        }

        private static void Win_Load()
        {
            GLRectangle myRect = new GLRectangle(new RectangleF(new PointF(-.1f, -.1f), new SizeF(2f, .2f)));
            myRect.MyColor = new Vec3(1f, 1f, .5f);
            ObjectManager.AddObject(myRect);
        }

        private static void Win_Update()
        {
        }

        private static void Win_Render()
        {
            ObjectManager.RenderAll();
        }
    }
}
