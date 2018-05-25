// ---------------------------------------------------------------
// <summary>
// A uniform object for Uniform Blocks
// </summary>
// ---------------------------------------------------------------

namespace OpenGL_Helper.Shaders
{
    using OpenTK.Graphics.OpenGL;

    /// <summary>
    /// Object for Uniform Block Objects.
    /// </summary>
    public class UniformBlock : Uniform
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UniformBlock"/> class.
        /// </summary>
        /// <param name="globalBindingIndex">The binding index for this Uniform</param>
        /// <param name="shaderProgram">The shader program to search.</param>
        public UniformBlock(int globalBindingIndex, ShaderProgram shaderProgram)
        {
            //// TODO: Error catching if the block isn't found.
            this.Handle = GL.GetUniformBlockIndex(shaderProgram.Handle, "GlobalCamera");
            GL.UniformBlockBinding(shaderProgram.Handle, this.Handle, globalBindingIndex);
        }

        /// <summary>
        /// This Method is not used.
        /// </summary>
        public override void Update()
        {
        }
    }
}
