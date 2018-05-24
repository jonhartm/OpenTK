
namespace OpenGL_Helper.Shaders
{
    using OpenTK.Graphics.OpenGL;

    public class UniformBlock : Uniform
    {
        public UniformBlock(int globalBindingIndex, ShaderProgram shaderProgram)
        {
            this.handle = GL.GetUniformBlockIndex(shaderProgram.Handle, "GlobalCamera");
            GL.UniformBlockBinding(shaderProgram.Handle, this.handle, globalBindingIndex);
        }

        public override void Update()
        {

        }
    }
}
