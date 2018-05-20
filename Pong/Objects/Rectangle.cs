/// <summary>
/// Example object derived from GLObject.
/// </summary>

namespace Pong.Objects
{
    using System.Collections.Generic;
    using System.Drawing;
    using OpenGL_Helper;
    using OpenGL_Helper.Object;
    using OpenGL_Helper.Shaders;

    public class GLRectangle : GLObject
    {
        public Vec3 MyColor
        {
            get
            {
                return this.Uniforms.GetByName<Vec3>("ourColor").Value;
            }
            set
            {
                this.Uniforms.GetByName<Vec3>("ourColor").Value = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GLRectangle"/> class.
        /// </summary>
        /// <param name="dimensions">The size and location of this object.</param>
        public GLRectangle(RectangleF dimensions)
        { 
            Vertex[] vertices =
            {
                new Vertex(new Vec3(dimensions.Right,  dimensions.Bottom, 0.0f)),
                new Vertex(new Vec3(dimensions.Right, dimensions.Top, 0.0f)),
                new Vertex(new Vec3(dimensions.Left, dimensions.Top, 0.0f)),
                new Vertex(new Vec3(dimensions.Left,  dimensions.Bottom, 0.0f))
            };

            short[] indices =
            {
                0, 1, 3,  // first Triangle
                1, 2, 3   // second Triangle
            };

            Shader tri1_vertexShader = Shader.LoadVertexShader(@"Shader\Source\moreShaders.vert.glsl");
            Shader tri1_fragmentShader = Shader.LoadFragmentShader(@"Shader\Source\moreShaders.frag.glsl");

            LoadObjectData(vertices, indices, new List<Shader>() { tri1_vertexShader, tri1_fragmentShader });

            this.Uniforms.Add(new UniformVec3(Vec3.None, "ourColor", this.ShaderProgram));
        }
    }
}
