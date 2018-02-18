/// <summary>
/// Class structure for a basic OpenGL object.
/// </summary>

namespace OpenGL_Helper.Object
{
    using System.Collections.Generic;

    using OpenTK.Graphics.OpenGL;

    using Shaders;

    public class GLObject
    {
        /// <summary>
        /// Integer that holds a reference to the GL assigned handle for this object's Vertex Array Object
        /// </summary>
        private int vertex_array_object = -1;

        /// <summary>
        /// The shader program used by this object.
        /// </summary>
        private ShaderProgram shaderProgram;

        /// <summary>
        /// Initializes a new instance of the <see cref="GLObject"/> class.
        /// </summary>
        /// <param name="vertices">An array of floats with vertex information for the object.</param>
        /// <param name="shaders">A list of the shaders to be used by this object.</param>
        public GLObject(Vertex[] vertices, List<Shader> shaders)
        {
            // build and compile the shaders
            shaderProgram = ShaderProgram.LoadShaderProgram(shaders);

            // set up vertex data and buffers and configure vertex attributes
            vertex_array_object = GL.GenVertexArray();
            int vertex_buffer_object = GL.GenBuffer();

            GL.BindVertexArray(vertex_array_object);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vertex_buffer_object);
            GL.BufferData(BufferTarget.ArrayBuffer, Vertex.SizeInBytes * vertices.Length, vertices, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
        }

        /// <summary>
        /// Renders this object to the GL context.
        /// </summary>
        public void Render()
        {
            GL.UseProgram(shaderProgram.Handle);
            GL.BindVertexArray(vertex_array_object);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
        }        
    }
}
