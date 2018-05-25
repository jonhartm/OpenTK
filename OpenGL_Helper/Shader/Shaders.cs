// --------------------------------------------------------------
// <summary>
// Compiles Shaders from GLSL files.
// </summary>
// --------------------------------------------------------------

namespace OpenGL_Helper.Shaders
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using OpenTK.Graphics.OpenGL;

    /// <summary>
    /// Class object that creates and stores Shader files for use by <see cref="ShaderProgram"/>.
    /// </summary>
    public class Shader
    {
        /// <summary>
        /// A list of the shaders which have already been loaded. 
        /// Prevents loading a shader multiple times.
        /// </summary>
        private static List<Shader> loadedShaders = new List<Shader>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Shader"/> class.
        /// </summary>
        /// <param name="type">The type of this shader.</param>
        /// <param name="shaderName">The name of the shader. Typically the filename.</param>
        /// <param name="shaderSource">(Optional) The GLSL source for this shader. </param>
        private Shader(ShaderType type, string shaderName, string shaderSource = null)
        {
            // Check Loaded Shaders to see if this one has already been read in.
            object s = loadedShaders.FirstOrDefault(x => x.Name.Equals(shaderName));
            if (s != null)
            {
                // If it has, grab the handle and name and ignore everything else here
                this.Handle = ((Shader)s).Handle;
                this.Name = ((Shader)s).Name;
                return;
            }

            // Save the file name so we can use it to compare against later
            this.Name = shaderName;

            // Get the next available GL handler for a shader
            this.Handle = GL.CreateShader(type);

            // If there was no source provided, lets see if the shader name is actually a filename
            if (shaderSource == null)
            {
                shaderSource = LoadSourceFromFile(shaderName);
            }

            // Locate the shader source from the file
            GL.ShaderSource(this.Handle, shaderSource);

            // Compile the shader
            GL.CompileShader(this.Handle);

            // Error Checking
            string infolog = GL.GetShaderInfoLog(this.Handle);
            if (infolog.Length > 0)
            {
                Console.WriteLine(type.ToString() + " Compile Error: " + infolog);
                throw new ShaderException(type.ToString() + " Compile Error: " + infolog);
            }

            Console.WriteLine("Sucessfully Compiled " + type.ToString() + " " + this.Name);
            Console.WriteLine("Assigned Shader Handle " + this.Handle.ToString());
            loadedShaders.Add(this);
        }

        /// <summary>
        /// Gets or sets the OpenGL assigned handle for this particular shader.
        /// </summary>
        public int Handle { get; protected set; }

        /// <summary>
        /// Gets the filename this shader was loaded from.
        /// </summary>
        public string Name { get; private set; }

        //// TODO: this can be done better.

        /// <summary>
        /// Loads a Vertex shader from the provided file.
        /// </summary>
        /// <param name="filename">The file path of the shader file.</param>
        /// <returns>A <see cref="Shader"/> object.</returns>
        public static Shader LoadVertexShader(string filename)
        {
            return new Shader(ShaderType.VertexShader, filename);
        }

        /// <summary>
        /// Loads a Vertex shader from the provided resource
        /// </summary>
        /// <param name="sourceCode">The GLSL source for this shader.</param>
        /// <param name="resourceName">The name we should remember for this shader. Should be unique.</param>
        /// <returns>A <see cref="Shader"/> object.</returns>
        public static Shader LoadVertexShader(string sourceCode, string resourceName)
        {
            return new Shader(ShaderType.VertexShader, resourceName, sourceCode);
        }

        /// <summary>
        /// Loads a fragment shader from the provided file.
        /// </summary>
        /// <param name="filename">The file path of the shader file.</param>
        /// <returns>A <see cref="Shader"/> object.</returns>
        public static Shader LoadFragmentShader(string filename)
        {
            return new Shader(ShaderType.FragmentShader, filename);
        }

        /// <summary>
        /// Loads a Fragment shader from the provided resource
        /// </summary>
        /// <param name="sourceCode">The GLSL source for this shader.</param>
        /// <param name="resourceName">The name we should remember for this shader. Should be unique.</param>
        /// <returns>A <see cref="Shader"/> object.</returns>
        public static Shader LoadFragmentShader(string sourceCode, string resourceName)
        {
            return new Shader(ShaderType.FragmentShader, resourceName, sourceCode);
        }

        /// <summary>
        /// Determines if two shaders are equal by checking their names.
        /// </summary>
        /// <param name="shader1">The first shader to check.</param>
        /// <param name="shader2">The second shader to check.</param>
        /// <returns>True if both shaders are the same.</returns>
        public static bool operator ==(Shader shader1, Shader shader2)
        {
            return shader1.Name == shader2.Name;
        }

        /// <summary>
        /// Determines if two shaders are not equal by checking their names.
        /// </summary>
        /// <param name="shader1">The first shader to check.</param>
        /// <param name="shader2">The second shader to check.</param>
        /// <returns>True if the shaders are not the same.</returns>
        public static bool operator !=(Shader shader1, Shader shader2)
        {
            return !(shader1 == shader2);
        }

        /// <summary>
        /// Gets the Hash Code for this shader. Should be unique.
        /// </summary>
        /// <returns>An integer Hash Code.</returns>
        public override int GetHashCode()
        {
            return this.Name.GetHashCode() + this.Handle.GetHashCode();
        }

        /// <summary>
        /// Checks if this shader is equal to a given object.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns>True if the object is the same.</returns>
        public override bool Equals(object obj)
        {
            return this == (Shader)obj;
        }

        /// <summary>
        /// Loads GLSL code from the given file.
        /// </summary>
        /// <param name="filename">The file to load.</param>
        /// <returns>A string representation of the GLSL code.</returns>
        private static string LoadSourceFromFile(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine("Unable to locate Shader file " + filename);
                throw new FileNotFoundException("Unable to load Shader File.", filename);
            }

            return File.ReadAllText(filename);
        }

        /// <summary>
        /// Exception Specific to this class. 
        /// </summary>
        [Serializable]
        public class ShaderException : Exception
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="ShaderException"/> class.
            /// </summary>
            /// <param name="message">The message to display for this exception.</param>
            public ShaderException(string message)
                : base(message)
            {
            }
        }
    }
}
