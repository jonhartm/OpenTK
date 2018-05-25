//-----------------------------------------------------------------------
// <summary>
// Class structure for a basic OpenGL object.
// </summary>
//-----------------------------------------------------------------------
namespace OpenGL_Helper.Object
{
    using System.Collections.Generic;

    using OpenTK.Graphics.OpenGL;

    using Shaders;

    /// <summary>
    /// Class Object for a Simplistic OpenGL visual. 
    /// Provided with object data (Vertex and index arrays) and shaders, will add the object to the OpenGL Context.
    /// </summary>
    public class GLObject
    {
        /// <summary>
        /// Counter to keep track of the current object
        /// </summary>
        private static int counter;

        /// <summary>
        /// Integer that holds a reference to the GL assigned handle for this object's Vertex Array Object
        /// </summary>
        private int vertexArrayObject = -1;

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
        /// <param name="indices">An array of shorts with the ordering information for the object's vertices. </param>
        /// <param name="shaders">A list of the shaders to be used by this object.</param>
        /// <param name="name">A string that uniquely identifies this object.</param>
        public GLObject(Vertex[] vertices, short[] indices, List<Shader> shaders, string name)
        {
            this.Name = name;
            this.LoadObjectData(vertices, indices, shaders);
        }

        /// <summary>
        /// Gets the Unique ID for this Object
        /// </summary>
        public int ID { get; private set; } = counter++;

        /// <summary>
        /// Gets or sets the user provided name for this object. Must be unique.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Gets the shader program used by this object.
        /// </summary>
        public ShaderProgram ShaderProgram { get; private set; }

        /// <summary>
        /// Gets the list of Uniform objects associated with this object.
        /// </summary>
        public List<Uniform> Uniforms { get; internal set; } = new List<Uniform>();

        /// <summary>
        /// Load object data into the Vertex Array Object
        /// </summary>
        /// <param name="vertices">An array of floats with vertex information for the object.</param>
        /// <param name="indices">An array of shorts with the ordering information for the object's vertices. </param>
        /// <param name="shaders">A list of the shaders to be used by this object.</param>
        public void LoadObjectData(Vertex[] vertices, short[] indices, List<Shader> shaders)
        {
            // build and compile the shaders
            this.ShaderProgram = ShaderProgram.LoadShaderProgram(shaders);

            // set up vertex data and buffers and configure vertex attributes
            this.vertexArrayObject = GL.GenVertexArray();
            int vertex_buffer_object = GL.GenBuffer();
            int element_bufer_object = GL.GenBuffer();

            GL.BindVertexArray(this.vertexArrayObject);

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
            foreach (Uniform u in this.Uniforms)
            {
                u.Update();
            }

            GL.BindVertexArray(this.vertexArrayObject);
            GL.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedShort, 0);
        }        
    }
}
