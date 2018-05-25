// ---------------------------------------------------------------
// <summary>
// A Vector 3 struct to replace the Vector 3 provided by OpenTK
// (this might be dumb - I;m jsut trying to avoid having to include a reference to OpenTK in a program that uses this library)
// </summary>
// ---------------------------------------------------------------

namespace OpenGL_Helper
{
    /// <summary>
    /// More or less a copy of the Vector 3 from OpenTK.
    /// </summary>
    public struct Vec3
    {
        /// <summary>
        /// The x component of this vector.
        /// </summary>
        public float X;

        /// <summary>
        /// The y component of this vector.
        /// </summary>
        public float Y;

        /// <summary>
        /// The z component of this vector.
        /// </summary>
        public float Z;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vec3"/> struct.
        /// </summary>
        /// <param name="x">The x component of this vector.</param>
        /// <param name="y">The y component of this vector.</param>
        /// <param name="z">The z component of this vector.</param>
        public Vec3(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        /// <summary>
        /// Gets the size of this object in bytes.
        /// </summary>
        public static int SizeInBytes
        {
            get
            {
                return sizeof(float) * 3;
            }
        }

        /// <summary>
        /// Gets a <see cref="Vec3"/> object equal to (0,0,0)
        /// </summary>
        public static Vec3 None
        {
            get
            {
                return new Vec3(0, 0, 0);
            }
        }

        /// <summary>
        /// Returns the string representation of this vector.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return string.Format("{0},{1},{2}", this.X, this.Y, this.Z);
        }
    }
}
