//-----------------------------------------------------------------------
// <summary>
// Simple implementation of Pong using the OpenGL Helper library.
// </summary>
//-----------------------------------------------------------------------

namespace Pong
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    using Objects;

    using OpenGL_Helper;
    using OpenGL_Helper.Object;
    using OpenGL_Helper.Shaders;

    /// <summary>
    /// Contains the Main loop and event handlers.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main loop.
        /// </summary>
        public static void Main()
        {
            using (Window win = new Window("Pong"))
            {
                win.Load += Win_Load;
                win.Update += Win_Update;
                win.Render += Win_Render;

                win.KeyDown += Win_KeyDown;

                win.Run();
            }
        }

        /// <summary>
        /// Function that runs the first time the window is created.
        /// </summary>
        private static void Win_Load()
        {
            ObjectManager.AddObject(new GLRectangle(new RectangleF(new PointF(-.2f, -.2f), new SizeF(.2f, .2f)), "Rectangle 1"));
            ObjectManager.AddObject(new GLRectangle(new RectangleF(new PointF(.1f, .1f), new SizeF(.2f, .2f)), "Rectangle 2"));
        }
         
        /// <summary>
        /// Function that is called every time the window is refreshed, but before it renders.
        /// </summary>
        private static void Win_Update()
        {
        }

        /// <summary>
        /// Function that is called each time the window is rendered.
        /// </summary>
        private static void Win_Render()
        {
            ObjectManager.RenderAll();
        }

        /// <summary>
        /// Function that is called whenever the user presses a key.
        /// </summary>
        /// <param name="key">The name of the key which was pressed.</param>
        private static void Win_KeyDown(string key)
        {
            Console.WriteLine(key);
        }
    }
}
