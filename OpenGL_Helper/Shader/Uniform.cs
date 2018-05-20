/// <summary>
/// Provides a way of interacting with uniform values in a given shader program.
/// Locations are saved so the uniform can be updated later.
/// </summary>

namespace OpenGL_Helper.Shaders
{
    using System.Collections.Generic;

    using OpenTK.Graphics.OpenGL;

    /// <summary>
    /// Abstract base class for Uniforms.
    /// Lets me create a list of Uniforms without having to specify a type.
    /// </summary>
    public abstract class Uniform
    {
        /// <summary>
        /// The name of this uniform in the shader
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// The location handle for this uniform.
        /// </summary>
        internal int handle = -1;
    }

    /// <summary>
    /// Abstract generic class for holding basic Uniform data.
    /// </summary>
    /// <typeparam name="T">The type of the Uniform. Must be a valid GLSL type.</typeparam>
    public abstract class Uniform<T> : Uniform
    {
        /// <summary>
        /// The value of this Uniform.
        /// </summary>
        public virtual T Value { get; set; }

        /// <summary>
        /// Creates a new instance of the <see cref="Uniform{T}"/> Class.
        /// </summary>
        /// <param name="name">The name of this uniform in the shader.</param>
        /// <param name="shaderProgram">The shader program object to look in.</param>
        public Uniform(string name, ShaderProgram shaderProgram)
        {
            this.Name = name;
            shaderProgram.Use();
            this.handle = GL.GetUniformLocation(shaderProgram.Handle, name);
        }
    }

    /// <summary>
    /// Uniform object for Vec3 variables
    /// </summary>
    public class UniformVec3 : Uniform<Vec3>
    {
        /// <summary>
        /// Sets the value for this object, both in this class and in the GL context.
        /// </summary>
        public override Vec3 Value
        {
            set
            {
                GL.Uniform3(this.handle, value.x, value.y, value.z);
                System.Console.WriteLine("New Vec3 Value for Uniform {0} (ID {1}): {2}", this.Name, this.handle, value);
            }
        }

        /// <summary>
        /// Creates a new instance of the <see cref="UniformVec3"/> class.
        /// </summary>
        /// <param name="intitialValue"></param>
        /// <param name="name"></param>
        /// <param name="shaderProgram"></param>
        public UniformVec3(Vec3 intitialValue, string name, ShaderProgram shaderProgram)
            : base(name, shaderProgram)
        {
            Value = intitialValue;
        }
    }

    public static class UniformExtensionMethods
    {
        // TODO: Is this dumb? This might be dumb.
        /// <summary>
        /// Get a uniform from a list of uniforms by it's name.
        /// </summary>
        /// <typeparam name="T">The type of Uniform to return.</typeparam>
        /// <param name="uniforms">The list of uniforms to search.</param>
        /// <param name="name">The name to look for.</param>
        /// <returns>A Uniform object of the requested type.</returns>
        public static Uniform<T> GetByName<T>(this List<Uniform> uniforms, string name)
        {
            return (Uniform<T>)uniforms.Find(x => x.Name == name);
        }
    }
}
