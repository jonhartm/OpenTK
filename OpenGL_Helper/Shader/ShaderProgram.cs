//---------------------------------------------------------------
// <summary>
// Create and save Shader Programs. Makes sure we don't load in the
// same shader program more than once. 
// </summary>
//---------------------------------------------------------------

namespace OpenGL_Helper.Shaders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using OpenTK.Graphics.OpenGL;

    /// <summary>
    /// Represents a shader program stored in the OpenGL Context. 
    /// Essentially nothing more than an integer handle at it's most basic. 
    /// </summary>
    public class ShaderProgram
    {
        /// <summary>
        /// A list of <see cref="ShaderProgram"/> that have already been loaded.
        /// </summary>
        private static List<ShaderProgram> loadedShaderPrograms = new List<ShaderProgram>();

        /// <summary>
        /// The OpenGL provided integer handle for this Shader Program.
        /// </summary>
        private readonly int handle;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShaderProgram"/> class.
        /// </summary>
        /// <param name="shaders">A list of <see cref="Shader"/> that will make up this program.</param>
        private ShaderProgram(List<Shader> shaders)
        {
            this.Shaders = shaders;

            // Get the next available program handle
            this.handle = GL.CreateProgram();

            // Attach each shader to the program
            foreach (Shader s in shaders)
            {
                GL.AttachShader(this.handle, s.Handle);
            }

            // Links the shaders now assigned to the program together
            GL.LinkProgram(this.handle);

            Console.WriteLine("Sucesfully linked Shader Program Handle " + this.handle + " with the following Shaders:");
            foreach (Shader s in shaders)
            {
                Console.WriteLine(string.Format("  {0} - Handle: {1}", System.IO.Path.GetFileName(s.Name), s.Handle.ToString()));
            }
        }

        /// <summary>
        /// Gets the list of shaders that make up this shader program.
        /// </summary>
        public List<Shader> Shaders { get; private set; }

        /// <summary>
        /// Gets the OpenGL provided integer handle for this object.
        /// </summary>
        internal int Handle
        {
            get
            {
                return this.handle;
            }
        }

        /// <summary>
        /// Creates a <see cref="ShaderProgram"/> using the list of <see cref="Shader"/> provided.
        /// Checks to see if a program with these shaders has already been created.
        /// </summary>
        /// <param name="shaders">A list of shaders to include.</param>
        /// <returns>A <see cref="ShaderProgram"/></returns>
        public static ShaderProgram LoadShaderProgram(List<Shader> shaders)
        {
            // Check to see if this set of shaders has already been loaded, and return the cooresponding program handle if so
            foreach (ShaderProgram program in loadedShaderPrograms)
            {
                // Do they have the same number of shader files?
                if (new HashSet<Shader>(shaders).SetEquals(program.Shaders))
                {
                    Console.WriteLine("Program ID {0} has the same shaders. Assigning that program.", program.Handle);
                    return program;
                }
            }

            // Add this program to the dictionary for later use, if needed.
            ShaderProgram newProgram = new ShaderProgram(shaders);
            loadedShaderPrograms.Add(newProgram);

            return newProgram;
        }

        /// <summary>
        /// Checks to see if two <see cref="ShaderProgram"/> are the same.
        /// </summary>
        /// <param name="program1">The first program to compare.</param>
        /// <param name="program2">The second program to compare.</param>
        /// <returns>True if they contain the same shaders, false if not.</returns>
        public static bool operator ==(ShaderProgram program1, ShaderProgram program2)
        {
            // Get the average, in case there are an unequal number of shaders in each program
            float totalShaderfiles = ((float)program1.Shaders.Count + (float)program2.Shaders.Count) / 2f;
            int matchedShaders = 0;

            foreach (Shader p1shader in program1.Shaders)
            {
                foreach (Shader p2shader in program2.Shaders)
                {
                    if (p1shader.Name == p2shader.Name)
                    {
                        matchedShaders++;
                    }
                }
            }

            return matchedShaders == totalShaderfiles;
        }

        /// <summary>
        /// Checks to see if two <see cref="ShaderProgram"/> are not the same.
        /// </summary>
        /// <param name="program1">The first program to compare.</param>
        /// <param name="program2">The second program to compare.</param>
        /// <returns>False if they contain the same shaders, True if not.</returns>
        public static bool operator !=(ShaderProgram program1, ShaderProgram program2)
        {
            return !(program1 == program2);
        }

        /// <summary>
        /// Tells the OpenGL Context to use this shader program.
        /// </summary>
        public void Use()
        {
            GL.UseProgram(this.handle);
        }

        /// <summary>
        /// Gets the HashCode for this ShaderProgram. Needed for checking equivalence. 
        /// </summary>
        /// <returns>An integer HashCode that should be unique.</returns>
        public override int GetHashCode()
        {
            return this.Shaders.GetHashCode() + this.handle.GetHashCode();
        }

        /// <summary>
        /// Checks if this <see cref="ShaderProgram"/> is equal to another object.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns>True if the object is equal.</returns>
        public override bool Equals(object obj)
        {
            return this == (ShaderProgram)obj;
        }
    }
}
