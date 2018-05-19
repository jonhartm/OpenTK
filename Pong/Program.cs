namespace Pong
{
    using System.Collections.Generic;

    using OpenGL_Helper;
    using OpenGL_Helper.Object;
    using OpenGL_Helper.Shaders;

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
            // Triangle 1
            Vertex[] tri1_vertices =
            {
                new Vertex(new Vec3(0.25f,  0.25f, 0.0f)),
                new Vertex(new Vec3(0.25f, -0.25f, 0.0f)),
                new Vertex(new Vec3(-0.25f, -0.25f, 0.0f)),
                new Vertex(new Vec3(-0.25f,  0.25f, 0.0f))
            };

            short[] tri1_indices =
            {
                0, 1, 3,  // first Triangle
                1, 2, 3   // second Triangle
            };

            Shader tri1_vertexShader = Shader.LoadVertexShader(@"Shader\Source\helloTriangle.vert.glsl");
            Shader tri1_fragmentShader = Shader.LoadFragmentShader(@"Shader\Source\helloTriangle.frag.glsl");

            ObjectManager.AddObject(new GLObject(tri1_vertices, tri1_indices, new List<Shader>() { tri1_vertexShader, tri1_fragmentShader }));

            // Triangle 2
            Vertex[] tri2_vertices =
            {
                new Vertex(new Vec3(0.75f,  0.75f, 0.0f)),
                new Vertex(new Vec3(0.75f, 0.25f, 0.0f)),
                new Vertex(new Vec3(0.25f, 0.25f, 0.0f)),
                new Vertex(new Vec3(0.25f,  0.75f, 0.0f))
            };

            short[] tri2_indices =
            {
                0, 1, 3,  // first Triangle
                1, 2, 3   // second Triangle
            };

            Shader tri2_vertexShader = Shader.LoadVertexShader(@"Shader\Source\helloTriangle.vert.glsl");
            Shader tri2_fragmentShader = Shader.LoadFragmentShader(@"Shader\Source\helloTriangle.frag.glsl");

            ObjectManager.AddObject(new GLObject(tri2_vertices, tri2_indices, new List<Shader>() { tri2_vertexShader, tri2_fragmentShader }));
        }

        private static void Win_Render()
        {
            ObjectManager.RenderAll();
        }
    }
}
