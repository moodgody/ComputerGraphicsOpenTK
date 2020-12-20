/********************************************************************************************
 * Copyright (c) Computer Graphics Course by Fayoum University 
 * Prof. Amr M. Gody, amg00@fayoum.edu.eg
 * License: free for use and distribution for Educational purposes. It is required to keep this header comments on your code. 
 * Purpose:             shader program 
 *
 * Ver  Date         By     Purpose
 * ---  ----------- -----   --------------------------------------------------------------------
 * 01   2020-12-05  AMG     Created the initial version.
 *************************************************************************************************/

#version 330 core
layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec3 aColor; // the color variable has attribute position 1
uniform mat4 model;
uniform vec3 lightColor;
out vec3 objectColor; // output a color to the fragment shader
out vec3 light;
void main()
{
    gl_Position =  model *  vec4(aPosition, 1.0f) ;
    objectColor = aColor; // set objectColor to the input color we got from the vertex data
    light = lightColor;
}