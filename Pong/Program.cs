namespace Pong
{
    using System.Collections.Generic;

    using OpenGL_Helper;
    using OpenGL_Helper.Object;
    using OpenGL_Helper.Shaders;

    class Program
    {
        private static GLObject triangle;

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
            float[] vertices =
            {
                -0.5f, -0.5f, 0.0f, // left  
                 0.5f, -0.5f, 0.0f, // right 
                 0.0f,  0.5f, 0.0f  // top  
            };

            Shader vertexShader = Shader.LoadVertexShader(@"F:\Documents\GitHub\OpenTK\OpenGL_Helper\Shader\Source\helloTriangle.vert.glsl");
            Shader fragmentShader = Shader.LoadFragmentShader(@"F:\Documents\GitHub\OpenTK\OpenGL_Helper\Shader\Source\helloTriangle.frag.glsl");

            triangle = new GLObject(vertices, new List<Shader>() { vertexShader, fragmentShader });
        }

        private static void Win_Render()
        {
            triangle.Render();
        }
    }
}
