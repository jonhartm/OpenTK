namespace OpenGL_Helper
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Timers;

    using OpenTK;
    using OpenTK.Graphics.OpenGL;
    using OpenTK.Input;

    using Shaders;

    public class Window : IDisposable
    {
        public delegate void WindowEventHandler();
        public delegate void WindowInputEventHandler(string k);

        public event WindowEventHandler Load;
        public event WindowEventHandler Update;
        public event WindowEventHandler Render;

        public event WindowInputEventHandler KeyDown;

        /// <summary>
        /// Gets the title of the window, it's current FPS, and dimensions.
        /// </summary>
        public string FormattedTitle
        {
            get
            {
                return string.Format("{0} - FPS: {1} @ {2}x{3}", title, frameCount, window.Width, window.Height);
            }
        }

        /// <summary>
        /// A timer to track the FPS of the window.
        /// </summary>
        private Timer fpsUpdate = new Timer(1000);
        /// <summary>
        /// Counter used in conjunction with <see cref="fpsUpdate"/> to track FPS.
        /// Every frame render, is increased by one until a second has elapsed in the timer when it is reset to 0.
        /// </summary>
        private int frameCount = 0;
        /// <summary>
        /// The title of the window.
        /// </summary>
        private string title;

        private GameWindow window = new GameWindow();

        
        /// <summary>
        /// Initialize a new instance of the <see cref="Window"/> class.
        /// </summary>
        /// <param name="title">The title for this window.</param>
        public Window(string title)
        {
            this.title = title;
            window.Title = FormattedTitle;
            window.Location = new Point(10, 10);
            window.ClientSize = new Size(800, 600);
            window.WindowBorder = WindowBorder.Fixed;

            this.fpsUpdate.Elapsed += FpsUpdate_Elapsed;

            window.Load += OnLoad;
            window.UpdateFrame += OnUpdateFrame;
            window.RenderFrame += OnRenderFrame;

            window.KeyDown += OnKeyDown;
        }

        public void Dispose()
        {
            window.Dispose();
        }

        public void Run()
        {
            window.Run();
        }


        /// <summary>
        /// Called when <see cref="fpsUpdate"/> Timer is elapsed (every second).
        /// Updates the title with the current FPS and resets the counter to 0.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FpsUpdate_Elapsed(object sender, ElapsedEventArgs e)
        {
            window.Title = FormattedTitle;
            this.frameCount = 0;
        }

        /// <summary>
        /// Called when the window is loaded for the first time.
        /// </summary>
        /// <param name="e">Not Used.</param>
        private void OnLoad(object sender, EventArgs e)
        {
            GL.ClearColor(Color.CornflowerBlue);

            fpsUpdate.Enabled = true;

            // print OpenGL information to the Console for reference
            Console.WriteLine("OpenGL Version: " + GL.GetString(StringName.Version));
            Console.WriteLine("GLSL Version: " + GL.GetString(StringName.ShadingLanguageVersion));
            Console.WriteLine(GL.GetString(StringName.Vendor) + " - " + GL.GetString(StringName.Renderer));

            this.Load();
        }

        /// <summary>
        /// Called just before the window is rendered.
        /// </summary>
        /// <param name="e">Not Used.</param>
        private void OnUpdateFrame(object sender, FrameEventArgs e)
        {
            this.Update();
        }

        /// <summary>
        /// Called when the window is rendered. 
        /// </summary>
        /// <param name="e">Not Used.</param>
        private void OnRenderFrame(object sender, FrameEventArgs e)
        {
            frameCount++;

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            this.Render();

            window.SwapBuffers();
        }

        private void OnKeyDown(object senter, KeyboardKeyEventArgs e)
        {
            this.KeyDown(e.Key.ToString());
        }
    }
}
