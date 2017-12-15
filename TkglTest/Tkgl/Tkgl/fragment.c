#version 440 core

in vec4 frag_color;
uniform vec3 iResolution;
out vec4 color;

void main()
{
    color = frag_color;
    if (gl_FragCoord.x < iResolution.x / 2) {
        color = vec4( gl_FragCoord.y / iResolution.y,gl_FragCoord.x / iResolution.x, 0.0, 1.0);
    }
}