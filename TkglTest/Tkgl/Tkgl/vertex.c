﻿#version 440 core

layout (location = 0) in vec4 position;

out vec4 frag_color;

void main(void)
{
    gl_Position = position;
    frag_color = vec4(gl_Position.x, gl_Position.y, 0.0, 1.0);
}