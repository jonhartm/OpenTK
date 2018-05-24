#version 330 core
layout (location = 0) in vec3 aPos;

layout(std140) uniform GlobalCamera
{
	mat4 cameraView;
	mat4 cameraPerspective;
};

void main()
{
	mat4 mvpMatrix = cameraPerspective * cameraView;
    gl_Position = mvpMatrix * vec4(aPos, 1.0);
}