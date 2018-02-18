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
            Vertex[] vertices =
            {
                new Vertex(new Vec3(0.5f,  0.5f, 0.0f)),
                new Vertex(new Vec3(0.5f, -0.5f, 0.0f)),
                new Vertex(new Vec3(-0.5f, -0.5f, 0.0f)),
                new Vertex(new Vec3(-0.5f,  0.5f, 0.0f))
            };

            short[] indices =
            {
                0, 1, 3,  // first Triangle
                1, 2, 3   // second Triangle
            };

            Shader vertexShader = Shader.LoadVertexShader(@"Shader\Source\helloTriangle.vert.glsl");
            Shader fragmentShader = Shader.LoadFragmentShader(@"Shader\Source\helloTriangle.frag.glsl");

            triangle = new GLObject(vertices, indices, new List<Shader>() { vertexShader, fragmentShader });
        }

        private static void Win_Render()
        {
            triangle.Render();
        }
    }
}
