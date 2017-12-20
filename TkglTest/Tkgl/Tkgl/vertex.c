#version 440 core

layout (location = 0) in float time;
layout (location = 1) in vec4 position;

out vec4 frag_color;

void main(void)
{
    gl_Position = position;
    frag_color = vec4((1 + position.x)/2, (1 + position.y)/2, 0.0, 1.0);
}