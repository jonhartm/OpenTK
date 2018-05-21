namespace Pong
{
    using System;
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
            ObjectManager.AddObject(
                new GLRectangle(
                    new RectangleF(
                        new PointF(-.2f, -.2f), 
                        new SizeF(.2f, .2f)
                        ),
                        "Rectangle 1"
                    )
                );
            ObjectManager.AddObject(new GLRectangle(new RectangleF(new PointF(.1f, .1f), new SizeF(.2f, .2f)), "Rectangle 2"));
        }
         
        private static void Win_Update()
        {
            ((GLRectangle)ObjectManager.GetObjectByName("Rectangle 1")).MyColor = new Vec3(((float)DateTime.Now.Millisecond / 1000), ((float)DateTime.Now.Millisecond / 1000), ((float)DateTime.Now.Millisecond / 1000));
            ((GLRectangle)ObjectManager.GetObjectByName("Rectangle 2")).MyColor = new Vec3(1f, ((float)DateTime.Now.Millisecond / 1000), .5f);
        }

        private static void Win_Render()
        {
            ObjectManager.RenderAll();
        }
    }
}
