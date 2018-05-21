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
        /// Unique ID for this Object
        /// </summary>
        public int ID { get; private set; } = Counter++;

        /// <summary>
        /// Counter to keep track of the current object
        /// </summary>
        private static int Counter = 0;

        public string Name { get; set; }

        /// <summary>
        /// Integer that holds a reference to the GL assigned handle for this object's Vertex Array Object
        /// </summary>
        private int vertex_array_object = -1;

        /// <summary>
        /// The shader program used by this object.
        /// </summary>
        public ShaderProgram ShaderProgram { get; private set; }

        public List<Uniform> Uniforms { get; internal set; } = new List<Uniform>();

        /// <summary>
        /// Initializes a new instance of the <see cref="GLObject"/> class.
        /// Empty Constructor.
        /// </summary>
        public GLObject()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GLObject"/> class.
        /// </summary>
        /// <param name="vertices">An array of floats with vertex information for the object.</param>
        /// <param name="indices">An array of shorts with the ordering information for the object's verticies. </param>
        /// <param name="shaders">A list of the shaders to be used by this object.</param>
        public GLObject(Vertex[] vertices, short[] indices, List<Shader> shaders, string name)
        {
            this.Name = name;
            this.LoadObjectData(vertices, indices, shaders);
        }

        /// <summary>
        /// Load object data into the Vertex Array Object
        /// </summary>
        /// <param name="vertices">An array of floats with vertex information for the object.</param>
        /// <param name="indices">An array of shorts with the ordering information for the object's verticies. </param>
        /// <param name="shaders">A list of the shaders to be used by this object.</param>
        public void LoadObjectData(Vertex[] vertices, short[] indices, List<Shader> shaders)
        {
            // build and compile the shaders
            ShaderProgram = ShaderProgram.LoadShaderProgram(shaders);

            // set up vertex data and buffers and configure vertex attributes
            vertex_array_object = GL.GenVertexArray();
            int vertex_buffer_object = GL.GenBuffer();
            int element_bufer_object = GL.GenBuffer();

            GL.BindVertexArray(vertex_array_object);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vertex_buffer_object);
            GL.BufferData(BufferTarget.ArrayBuffer, Vertex.SizeInBytes * vertices.Length, vertices, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, element_bufer_object);
            GL.BufferData(BufferTarget.ElementArrayBuffer, sizeof(short) * indices.Length, indices, BufferUsageHint.StaticDraw);

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
            foreach (Uniform u in Uniforms)
                u.Update();

            GL.BindVertexArray(vertex_array_object);
            GL.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedShort, 0);
        }        
    }
}
