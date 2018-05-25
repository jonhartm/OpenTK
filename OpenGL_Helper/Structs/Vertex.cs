// ---------------------------------------------------------------
// <summary>
// Vertex Object for sending compact Vertex data.
// </summary>
// ---------------------------------------------------------------

namespace OpenGL_Helper
{
    /// <summary>
    /// Basic Vertex Object.
    /// </summary>
    public struct Vertex
    {
        /// <summary> The location of the vertex in 3d space. </summary>
        private Vec3 position;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vertex"/> struct.
        /// </summary>
        /// <param name="positionData">The Location of this Vertex in Model Space.</param>
        public Vertex(Vec3 positionData)
        {
            this.position = positionData;
        }

        /// <summary>
        /// Gets the total size of the Vertex object in bytes.
        /// </summary>
        public static int SizeInBytes
        {
            get
            {
                return Vec3.SizeInBytes;
            }
        }

        /// <summary>
        /// Gets or sets the Position for this Vertex in model space.
        /// </summary>
        public Vec3 Position
        {
            get
            {
                return this.position;
            }

            set
            {
                this.position = value;
            }
        }
    }
}
