// ---------------------------------------------------------------
// <summary>
// The important bit - creates and maintains a windown with an OpenGL context. 
// </summary>
// ---------------------------------------------------------------

namespace OpenGL_Helper
{
    using System;
    using System.Drawing;
    using System.Timers;

    using OpenTK;
    using OpenTK.Graphics.OpenGL;
    using OpenTK.Input;

    /// <summary>
    /// Create a window.
    /// </summary>
    public class Window : IDisposable
    {
        /// <summary>
        /// A timer to track the FPS of the window.
        /// </summary>
        private readonly Timer fpsUpdate = new Timer(1000);

        /// <summary>
        /// The title of the window.
        /// </summary>
        private readonly string title;

        /// <summary>
        /// The OpenTK Game Window that really does the work.
        /// </summary>
        private readonly GameWindow window = new GameWindow();

        /// <summary>
        /// Counter used in conjunction with <see cref="fpsUpdate"/> to track FPS.
        /// Every frame render, is increased by one until a second has elapsed in the timer when it is reset to 0.
        /// </summary>
        private int frameCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="Window"/> class.
        /// </summary>
        /// <param name="title">The title for this window.</param>
        public Window(string title)
        {
            this.title = title;
            this.window.Title = this.FormattedTitle;
            this.window.Location = new Point(10, 10);
            this.window.ClientSize = new Size(800, 600);
            this.window.WindowBorder = WindowBorder.Fixed;

            this.fpsUpdate.Elapsed += this.FpsUpdate_Elapsed;

            this.window.Load += this.OnLoad;
            this.window.Resize += this.OnResize;
            this.window.UpdateFrame += this.OnUpdateFrame;
            this.window.RenderFrame += this.OnRenderFrame;

            this.window.KeyDown += this.OnKeyDown;
        }

        /// <summary>
        /// Delegate for General Window Events.
        /// </summary>
        public delegate void WindowEventHandler();

        /// <summary>
        /// Delegate for Input events on the window.
        /// </summary>
        /// <param name="k">The name of the key that was pressed.</param>
        public delegate void WindowInputEventHandler(string k);

        /// <summary>
        /// Event called when the window is loaded.
        /// </summary>
        public event WindowEventHandler Load;

        /// <summary>
        /// Event called when the window is updated.
        /// </summary>
        public event WindowEventHandler Update;

        /// <summary>
        /// Event called when the window is rendered.
        /// </summary>
        public event WindowEventHandler Render;

        /// <summary>
        /// Event called when a key is pressed.
        /// </summary>
        public event WindowInputEventHandler KeyDown;

        /// <summary>
        /// Gets the title of the window, it's current FPS, and dimensions.
        /// </summary>
        public string FormattedTitle
        {
            get
            {
                return string.Format("{0} - FPS: {1} @ {2}x{3}", this.title, this.frameCount, this.window.Width, this.window.Height);
            }
        }

        /// <summary>
        /// Dispose of the window when we're done.
        /// </summary>
        void IDisposable.Dispose()
        {
            this.window.Dispose();
        }

        /// <summary>
        /// Start the window.
        /// </summary>
        public void Run()
        {
            this.window.Run();
        }

        /// <summary>
        /// Called when <see cref="fpsUpdate"/> Timer is elapsed (every second).
        /// Updates the title with the current FPS and resets the counter to 0.
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="e">The parameter is not used.</param>
        private void FpsUpdate_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.window.Title = this.FormattedTitle;
            this.frameCount = 0;
        }

        /// <summary>
        /// Called when the window is loaded for the first time.
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="e">The parameter is not used.</param>
        private void OnLoad(object sender, EventArgs e)
        {
            GL.ClearColor(Color.CornflowerBlue);

            this.fpsUpdate.Enabled = true;

            // print OpenGL information to the Console for reference
            Console.WriteLine("OpenGL Version: " + GL.GetString(StringName.Version));
            Console.WriteLine("GLSL Version: " + GL.GetString(StringName.ShadingLanguageVersion));
            Console.WriteLine(GL.GetString(StringName.Vendor) + " - " + GL.GetString(StringName.Renderer));

            Camera.Initialize(this.window.ClientSize, 0.1f, 100f, new Vector3(0.0f, 0.0f, 30.0f), Vector3.Zero);

            this.Load();
        }

        /// <summary>
        /// Called when the window is resized. Makes sure the viewport is maintained.
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="e">The parameter is not used.</param>
        private void OnResize(object sender, EventArgs e)
        {
            // Create the Viewport
            GL.Viewport(0, 0, 800, 600);
        }

        /// <summary>
        /// Called just before the window is rendered.
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="e">The parameter is not used.</param>
        private void OnUpdateFrame(object sender, FrameEventArgs e)
        {
            this.Update();
        }

        /// <summary>
        /// Called when the window is rendered. 
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="e">The parameter is not used.</param>
        private void OnRenderFrame(object sender, FrameEventArgs e)
        {
            this.frameCount++;

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            this.Render();

            this.window.SwapBuffers();
        }

        /// <summary>
        /// Called when the user presses a key.
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="e">The parameter is not used.</param>
        private void OnKeyDown(object sender, KeyboardKeyEventArgs e)
        {
            this.KeyDown(e.Key.ToString());
        }
    }
}
