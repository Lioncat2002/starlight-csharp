#version 400 core

in vec3 position;
in vec2 textureCoords;

out vec2 pass_textureCoords;

uniform mat4 transformationMatrix;
uniform mat4 projectionMatrix;
uniform mat4 viewMatrix;

void main(){
    gl_Position=projectionMatrix*viewMatrix*transformationMatrix*vec4(position,1.0);//the matrices must be multiplied in this order
    pass_textureCoords=textureCoords;//pass it on to the fragment shader
}