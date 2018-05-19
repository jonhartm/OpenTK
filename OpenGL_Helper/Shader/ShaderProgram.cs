namespace OpenGL_Helper.Shaders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using OpenTK.Graphics.OpenGL;

    public class ShaderProgram
    {
        private static List<ShaderProgram> loadedShaderPrograms = new List<ShaderProgram>();

        private int handle;

        internal int Handle { get { return handle; } }

        public List<Shader> Shaders { get; private set; }

        private ShaderProgram(List<Shader> shaders)
        {
            Shaders = shaders;

            // Get the next available program handle
            handle = GL.CreateProgram();

            // Attach each shader to the program
            foreach (Shader s in shaders)
            {
                GL.AttachShader(handle, s.Handle);
            }

            // Links the shaders now assigned to the program together
            GL.LinkProgram(handle);

            Console.WriteLine("Sucesfully linked Shader Program Handle " + this.handle + " with the following Shaders:");
            foreach (Shader s in shaders)
                Console.WriteLine(string.Format("  {0} - Handle: {1}", System.IO.Path.GetFileName(s.Name), s.Handle.ToString()));
        }

        public void Use()
        {
            GL.UseProgram(handle);
        }

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
                        matchedShaders++;
                }
            }

            return matchedShaders == totalShaderfiles;
        }

        public static bool operator !=(ShaderProgram program1, ShaderProgram program2)
        {
            return !(program1 == program2);
        }

        public override int GetHashCode()
        {
            return this.Shaders.GetHashCode() + this.handle.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return this == (ShaderProgram)obj;
        }
    }
}
