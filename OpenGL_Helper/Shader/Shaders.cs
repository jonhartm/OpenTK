namespace OpenGL_Helper.Shaders
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using OpenTK.Graphics.OpenGL;

    public class Shader
    {
        private static List<Shader> loadedShaders = new List<Shader>();

        /// <summary>
        /// The OpenGL assigned handle for this particular shader.
        /// </summary>
        public int Handle { get; protected set; }

        /// <summary>
        /// The filename this shader was loaded from.
        /// </summary>
        public string Name { get; private set; }

        private Shader(ShaderType type, string shaderName, string shaderSource = null)
        {
            // Check Loaded Shaders to see if this one has already been read in.
            object S = loadedShaders.Where(x => x.Name.Equals(shaderName)).FirstOrDefault();
            if (S != null)
            {
                // If it has, grab the handle and name and ignore everything else here
                this.Handle = ((Shader)S).Handle;
                this.Name = ((Shader)S).Name;
                return;
            }

            // Save the file name so we can use it to compare against later
            this.Name = shaderName;

            // Get the next available GL handler for a shader
            Handle = GL.CreateShader(type);

            // If there was no source provided, lets see if the shader name is actually a filename
            if (shaderSource == null)
                shaderSource = LoadSourceFromFile(shaderName);

            // Locate the shader source from the file
            GL.ShaderSource(Handle, shaderSource);

            // Compile the shader
            GL.CompileShader(Handle);

            // Error Checking
            string infolog = GL.GetShaderInfoLog(Handle);
            if (infolog.Length > 0)
            {
                Console.WriteLine(type.ToString() + " Compile Error: " + infolog);
                throw new ShaderException(type.ToString() + " Compile Error: " + infolog);
            }

            Console.WriteLine("Sucessfully Compiled " + type.ToString() + " " + this.Name);
            Console.WriteLine("Assigned Shader Handle " + Handle.ToString());
            loadedShaders.Add(this);
        }

        /// <summary>
        /// Exception Specific to this class. 
        /// </summary>
        public class ShaderException : Exception
        {
            public ShaderException(string message)
                : base(message)
            {
            }
        }

        public static Shader LoadVertexShader(string filename) { return new Shader(ShaderType.VertexShader, filename); }

        public static Shader LoadVertexShader(string sourceCode, string resourceName) { return new Shader(ShaderType.VertexShader, resourceName, sourceCode); }

        public static Shader LoadFragmentShader(string filename) { return new Shader(ShaderType.FragmentShader, filename); }

        public static Shader LoadFragmentShader(string sourceCode, string resourceName) { return new Shader(ShaderType.FragmentShader, resourceName, sourceCode); }

        public static bool operator ==(Shader shader1, Shader shader2)
        {
            return (shader1.Name == shader2.Name);
        }

        public static bool operator !=(Shader shader1, Shader shader2)
        {
            return !(shader1 == shader2);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode() + this.Handle.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return this == (Shader)obj;
        }

        private static string LoadSourceFromFile(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine("Unable to locate Shader file " + filename);
                throw new FileNotFoundException("Unable to load Shader File.", filename);
            }

            return File.ReadAllText(filename);
        }

    }
}
