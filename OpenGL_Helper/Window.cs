namespace OpenGL_Helper
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Timers;

    using OpenTK;
    using OpenTK.Graphics.OpenGL;

    using Shaders;

    public class Window : IDisposable
    {

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

        private ShaderProgram shaderProgram;
        private int vertex_array_object;

        
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

            // build and compile the shaders
            Shader vertexShader = Shader.LoadVertexShader(@"F:\Documents\GitHub\OpenTK\OpenGL_Helper\Shader\Source\helloTriangle.vert.glsl");
            Shader fragmentShader = Shader.LoadFragmentShader(@"F:\Documents\GitHub\OpenTK\OpenGL_Helper\Shader\Source\helloTriangle.frag.glsl");
            shaderProgram = ShaderProgram.LoadShaderProgram(new List<Shader>() { vertexShader, fragmentShader });

            // set up vertex data and buffers and configure vertex attributes
            float[] vertices =
            {
                -0.5f, -0.5f, 0.0f, // left  
                 0.5f, -0.5f, 0.0f, // right 
                 0.0f,  0.5f, 0.0f  // top  
            };

            vertex_array_object = GL.GenVertexArray();
            int vertex_buffer_object = GL.GenBuffer();

            GL.BindVertexArray(vertex_array_object);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vertex_buffer_object);
            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * vertices.Length, vertices, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
        }

        /// <summary>
        /// Called just before the window is rendered.
        /// </summary>
        /// <param name="e">Not Used.</param>
        private void OnUpdateFrame(object sender, FrameEventArgs e)
        {
        }

        /// <summary>
        /// Called when the window is rendered. 
        /// </summary>
        /// <param name="e">Not Used.</param>
        private void OnRenderFrame(object sender, FrameEventArgs e)
        {
            frameCount++;

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.UseProgram(shaderProgram.Handle);
            GL.BindVertexArray(vertex_array_object);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

            window.SwapBuffers();
        }
    }
}
