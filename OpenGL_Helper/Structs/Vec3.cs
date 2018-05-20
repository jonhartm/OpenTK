/// <summary>
/// A Vector 3 struct to replace the Vector 3 provided by OpenTK
/// (this might be dumb - I;m jsut trying to avoid having to include a reference to OpenTK in a program that uses this library)
/// </summary>

namespace OpenGL_Helper
{
    public struct Vec3
    {
        public float x;
        public float y;
        public float z;

        /// <summary>
        /// Gets the size of this object in bytes.
        /// </summary>
        public static int SizeInBytes
        {
            get
            {
                return sizeof(float)*3;
            }
        }

        public static Vec3 None
        {
            get
            {
                return new Vec3(0, 0, 0);
            }
        }

        public Vec3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public override string ToString()
        {
            return string.Format("{0},{1},{2}",x,y,z);
        }
    }
}
