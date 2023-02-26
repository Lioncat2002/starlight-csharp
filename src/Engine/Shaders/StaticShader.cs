using System.Numerics;
using Silk.NET.OpenGL;
using StarLight.Engine.Camera;

namespace StarLight.Engine.Shaders;

public class StaticShader:ShaderProgram
{
    public static string VERTEX_FILE = "src/Engine/Shaders/vertex.shader";
    public static string FRAGMENT_FILE = "src/Engine/Shaders/fragment.shader";

    private int location_transformation_matrix;
    private int location_projection_matrix;
    private int location_view_matrix;
    public StaticShader(GL Gl) : base(Gl, VERTEX_FILE, FRAGMENT_FILE) //calling base class constructor
    {
        
    }

    protected override void getAllUniformLocations()
    {
        location_transformation_matrix = getUniformLocation("transformationMatrix");
        location_projection_matrix = getUniformLocation("projectionMatrix");
        location_view_matrix = getUniformLocation("viewMatrix");
    }

    public void loadTranformationMatrix(Matrix4x4 matrix)
    {
        loadMatrix(location_transformation_matrix,matrix);
    }

    public void loadProjectionMatrix(Matrix4x4 matrix)
    {
        loadMatrix(location_projection_matrix,matrix);
    }

    public void loadViewMatrix(Camera.Camera camera)
    {
        Matrix4x4 matrix = camera.GetViewMatrix();
        loadMatrix(location_view_matrix,matrix);
    }

    protected override void bindAttributes()
    {
        bindAttribute(0,"position");
        bindAttribute(1,"textureCoords");
    }
}