//-----------------------------------------------------------------------
// <summary>
// Stores the Objects that are added to the GL Context in a single list, indexed by the Shader Program they use
// Is used during the render operation to avoid a situation where we keep switching between shader programs 
// All of the objects that use the same shader program are rendered at once, then the next and so on.
// </summary>
//-----------------------------------------------------------------------

namespace OpenGL_Helper.Object
{
    using System.Collections.Generic;
    using System.Linq;

    using OpenTK.Graphics.OpenGL;

    /// <summary>
    /// Stores the Objects that are added to the GL Context in a single list, indexed by the Shader Program they use
    /// Is used during the render operation to avoid a situation where we keep switching between shader programs 
    /// All of the objects that use the same shader program are rendered at once, then the next and so on.
    /// </summary>
    public static class ObjectManager
    {
        /// <summary>
        /// A Dictionary of a List of GLObjects that are indexed by shader program
        /// </summary>
        private static readonly Dictionary<int, List<GLObject>> StoredObjects;

        /// <summary>
        /// Initializes static members of the <see cref="ObjectManager" /> class.
        /// </summary>
        static ObjectManager()
        {
            StoredObjects = new Dictionary<int, List<GLObject>>();
        }

        /// <summary>
        /// Add an object to the manager to be rendered in the next render cycle.
        /// </summary>
        /// <param name="obj">The GL Object to add.</param>
        public static void AddObject(GLObject obj)
        {
            if (!StoredObjects.ContainsKey(obj.ShaderProgram.Handle))
            {
                StoredObjects[obj.ShaderProgram.Handle] = new List<GLObject>();
            }

            StoredObjects[obj.ShaderProgram.Handle].Add(obj);
        }

        /// <summary>
        /// Renders all of the stored objects.
        /// </summary>
        public static void RenderAll()
        {
            foreach (int shaderProgram in StoredObjects.Keys)
            {
                GL.UseProgram(shaderProgram);
                foreach (GLObject obj in StoredObjects[shaderProgram])
                {
                    obj.Render();
                }
            }
        }

        /// <summary>
        /// Get a GL Object from the Stored List by it's numerical ID.
        /// </summary>
        /// <param name="id">The Integer ID of the object to return.</param>
        /// <returns>A GLObject if it is found, null if not.</returns>
        public static GLObject GetObjectByID(int id)
        {
            return StoredObjects.SelectMany(s => s.Value).FirstOrDefault(s => s.ID == id);
        }

        /// <summary>
        /// Get a GL Object from the Stored List by it's unique name.
        /// </summary>
        /// <param name="name">The name of the object to return.</param>
        /// <returns>A GLObject if it is found, null if not.</returns>
        public static GLObject GetObjectByName(string name)
        {
            return StoredObjects.SelectMany(s => s.Value).FirstOrDefault(s => s.Name == name);
        }
    }
}
