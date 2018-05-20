/// <summary>
/// Stores the Objects that are added to the GL Context in a single list, indexed by the Shader Program they use
/// Is used during the render operation to avoid a situation where we keep switching between shader programs 
/// All of the objects that use the same shader program are rendered at once, then the next and so on.
/// </summary>

namespace OpenGL_Helper.Object
{
    using System.Collections.Generic;
    using System.Linq;

    using OpenTK.Graphics.OpenGL;

    public static class ObjectManager
    {
        /// <summary>
        /// A Dictionary of List<GLObject> that are indexed by shader program
        /// </summary>
        private static Dictionary<int, List<GLObject>> storedObjects;

        /// <summary>
        /// Constructor.
        /// </summary>
        static ObjectManager()
        {
            storedObjects = new Dictionary<int, List<GLObject>>();
        }

        /// <summary>
        /// Add an object to the manager to be rendered in the next render cycle.
        /// </summary>
        /// <param name="obj">The GL Object to add.</param>
        public static void AddObject(GLObject obj)
        {
            if (!storedObjects.ContainsKey(obj.ShaderProgram.Handle))
                storedObjects[obj.ShaderProgram.Handle] = new List<GLObject>();

            storedObjects[obj.ShaderProgram.Handle].Add(obj);
        }

        /// <summary>
        /// Renders all of the stored objects.
        /// </summary>
        public static void RenderAll()
        {
            foreach (int shaderProgram in storedObjects.Keys)
            {
                GL.UseProgram(shaderProgram);
                foreach (GLObject obj in storedObjects[shaderProgram])
                {
                    obj.Render();
                }
            }
        }

        public static GLObject GetObjectByID(int ID)
        {
            return storedObjects.SelectMany(s => s.Value).Where(s => s.ID == ID).FirstOrDefault();
        }
    }
}
