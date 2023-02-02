using System.Numerics;
using Silk.NET.OpenGL;
using SimpleCityBuilder.Engine.Entities;

namespace SimpleCityBuilder.Engine.Shaders;

public class StaticShader:ShaderProgram
{
    public static string VERTEX_FILE = "shaders/vertex.shader";
    public static string FRAGMENT_FILE = "shaders/fragment.shader";

    private int location_transformation_matrix;
    private int location_projectionMatrix;
    private int location_viewMatrix;
    public StaticShader(GL Gl) : base(Gl, VERTEX_FILE, FRAGMENT_FILE)
    {
        
    }

    protected override void getAllUniformLocations()
    {
        location_transformation_matrix = getUniformLocation("transformationMatrix");
        location_projectionMatrix = getUniformLocation("projectionMatrix");
        location_viewMatrix = getUniformLocation("viewMatrix");
    }

    public void loadTranformationMatrix(Matrix4x4 matrix)
    {
        loadMatrix(location_transformation_matrix,matrix);
    }

    public void loadProjectionMatrix(Matrix4x4 matrix)
    {
        loadMatrix(location_projectionMatrix,matrix);
    }

    public void loadViewMatrix(Camera camera)
    {
        Matrix4x4 matrix = camera.GetViewMatrix();
        loadMatrix(location_viewMatrix,matrix);
    }

    protected override void bindAttributes()
    {
        bindAttribute(0,"position");
        bindAttribute(1,"textureCoords");
    }
}