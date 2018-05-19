namespace OpenGL_Helper.Shaders
{
    using OpenTK.Graphics.OpenGL;

    public abstract class Uniform<T>
    {
        public virtual T Value { get; set; }

        internal int handle = -1;

        public Uniform(string name, int shaderProgram)
        {
            GL.UseProgram(shaderProgram);
            this.handle = GL.GetUniformLocation(shaderProgram, name);
        }
    }

    public class UniformVec3 : Uniform<Vec3>
    {
        public override Vec3 Value
        {
            set
            {
                GL.Uniform3(this.handle, value.x, value.y, value.z);
                System.Console.WriteLine("New Value for Uniform ID {0}: {1}", this.handle, value);
            }
        }
        public UniformVec3(Vec3 intitialValue, string name, int shaderProgram)
            : base(name, shaderProgram)
        {
            Value = intitialValue;
        }
    }
}
