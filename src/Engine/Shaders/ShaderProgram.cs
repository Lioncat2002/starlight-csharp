using System.Numerics;
using Silk.NET.OpenGL;

namespace StarLight.Engine.Shaders;

public abstract class ShaderProgram
{
    private uint programID;
    private uint vertexShaderID;
    private uint fragmentShaderID;
    private  GL Gl;

    public ShaderProgram(GL Gl,string vertexFile,string fragmentFile )
    {
        this.Gl = Gl;
        vertexShaderID = loadShader(Gl,vertexFile, GLEnum.VertexShader);
        fragmentShaderID = loadShader(Gl, fragmentFile, GLEnum.FragmentShader);
        programID = Gl.CreateProgram();
        Gl.AttachShader(programID,vertexShaderID);
        Gl.AttachShader(programID,fragmentShaderID);
        bindAttributes();
        Gl.LinkProgram(programID);
        Gl.ValidateProgram(programID);
        getAllUniformLocations();
    }

    protected abstract void getAllUniformLocations();

    protected int getUniformLocation(string uniformName)
    {
        return Gl.GetUniformLocation(programID, uniformName);
    }

    public void start()
    {
        Gl.UseProgram(programID);
    }

    public void stop()
    {
        Gl.UseProgram(0);
    }

    public void dispose()
    {
        stop();
        Gl.DetachShader(programID,vertexShaderID);
        Gl.DetachShader(programID,fragmentShaderID);
        Gl.DeleteShader(vertexShaderID);
        Gl.DeleteShader(fragmentShaderID);
        Gl.DeleteProgram(programID);
    }

    protected abstract void bindAttributes();

    protected void bindAttribute(uint attribute, string variableName)
    {
        Gl.BindAttribLocation(programID,attribute,variableName);
    }

    protected void loadFloat(int location,float value)
    {
        Gl.Uniform1(location,value);
    }

    protected void loadVector(int location, Vector3 value)
    {
        Gl.Uniform3(location,value);
    }

    protected void loadBool(int location, bool value)
    {
        float toLoad = value ? 1 : 0;
        Gl.Uniform1(location,toLoad);
    }

    protected unsafe void loadMatrix(int location, Matrix4x4 matrix)
    {
        Gl.UniformMatrix4(location,1,false,(float*)&matrix);
    }
    
    private static uint loadShader(GL Gl,string file, GLEnum type)
    {
        string src = File.ReadAllText(file);
        uint shaderID = Gl.CreateShader(type);
        Gl.ShaderSource(shaderID,src);
        Gl.CompileShader(shaderID);

        string infolog = Gl.GetShaderInfoLog(shaderID);
        if (!string.IsNullOrWhiteSpace(infolog))
        {
            Console.WriteLine("Error compiling shader: "+type);
            return 0;
        }

        return shaderID;
    }
}