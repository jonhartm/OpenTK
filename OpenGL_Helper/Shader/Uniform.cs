// --------------------------------------------------------------
// <summary>
// Provides a way of interacting with uniform values in a given shader program.
// Locations are saved so the uniform can be updated later.
// </summary>
// --------------------------------------------------------------

namespace OpenGL_Helper.Shaders
{
    using System.Collections.Generic;

    using OpenTK.Graphics.OpenGL;

    //// TODO: Is this dumb? This might be dumb.

    /// <summary>
    /// Extension to find a specific Uniform by name.
    /// </summary>
    public static class UniformExtensionMethods
    {
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

    /// <summary>
    /// Abstract base class for Uniforms.
    /// Lets me create a list of Uniforms without having to specify a type.
    /// </summary>
    public abstract class Uniform
    {
        /// <summary>
        /// Gets the location handle for this uniform.
        /// </summary>
        public int Handle { get; internal set; } = -1;

        /// <summary>
        /// Gets the name of this uniform in the shader.
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// Method for updating the value of this Uniform.
        /// </summary>
        public abstract void Update();
    }

    /// <summary>
    /// Abstract generic class for holding basic Uniform data.
    /// </summary>
    /// <typeparam name="T">The type of the Uniform. Must be a valid GLSL type.</typeparam>
    public abstract class Uniform<T> : Uniform
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Uniform{T}"/> class.
        /// </summary>
        /// <param name="name">The name of this uniform in the shader.</param>
        /// <param name="shaderProgram">The shader program object to look in.</param>
        protected Uniform(string name, ShaderProgram shaderProgram)
        {
            this.Name = name;
            shaderProgram.Use();
            this.Handle = GL.GetUniformLocation(shaderProgram.Handle, name);
        }

        /// <summary>
        /// Gets or sets the value of this Uniform.
        /// </summary>
        public virtual T Value { get; set; }
    }

    /// <summary>
    /// Uniform object for <see cref="Vec3"/> variables.
    /// </summary>
    public class UniformVec3 : Uniform<Vec3>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UniformVec3"/> class.
        /// </summary>
        /// <param name="intitialValue">Starting value for this uniform.</param>
        /// <param name="name">The name this uniform is given in the shader.</param>
        /// <param name="shaderProgram">The shader program this uniform is a part of.</param>
        public UniformVec3(Vec3 intitialValue, string name, ShaderProgram shaderProgram)
            : base(name, shaderProgram)
        {
            this.Value = intitialValue;
        }

        /// <summary>
        /// Sets the value for this object, both in this class and in the GL context.
        /// </summary>
        /// <param name="value">The value to assign to this uniform.</param>
        public void SetValue(Vec3 value)
        {
            GL.Uniform3(this.Handle, value.X, value.Y, value.Z);
            this.Value = value;
            System.Console.WriteLine("New Vec3 Value for Uniform {0} (ID {1}): {2}", this.Name, this.Handle, value);
        }

        /// <summary>
        /// Update the value of this Uniform in the shader program with it's current value.
        /// </summary>
        public override void Update()
        {
            GL.Uniform3(this.Handle, this.Value.X, this.Value.Y, this.Value.Z);
        }
    }
}
