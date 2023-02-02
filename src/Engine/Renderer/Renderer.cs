using System.Diagnostics;
using System.Numerics;
using Silk.NET.OpenGL;
using SimpleCityBuilder.Entities;
using SimpleCityBuilder.Models;
using SimpleCityBuilder.Shaders;
using SimpleCityBuilder.Utils;

namespace SimpleCityBuilder.Renderer;

public class Renderer
{
    private GL Gl;
    private Matrix4x4 projectionMatrix;

    private static float FOV = 70;
    private static float NEAR_PLANE = 0.1f;
    private static float FAR_PLANE = 1000f;

    private static float WIDTH = 800;
    private static float HEIGHT = 700;

    public Renderer(GL Gl,StaticShader staticShader)
    {
        this.Gl = Gl;
        createProjectionMatrix();
        staticShader.start();
        staticShader.loadProjectionMatrix(projectionMatrix);
        staticShader.stop();
    }

    public void start()
    {
        
        Gl.Enable(EnableCap.DepthTest);
        Gl.Clear((uint) (ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit));
        Gl.ClearColor(1,0,0,1);

    }
    public unsafe void render(Entity entity,StaticShader shader)
    {
        TexturedModel texturedModel = entity.Model;
        RawModel model = texturedModel.getRawModel();
        Gl.BindVertexArray(model.getVaoID());
        Gl.EnableVertexAttribArray(0);
        Gl.EnableVertexAttribArray(1);
        Matrix4x4 transformationMatrix =
            MathHelper.CreateTransformationMatrix4X4(entity.Position, entity.Rotation, entity.Scale);
        
        shader.loadTranformationMatrix(transformationMatrix);
        Gl.ActiveTexture(TextureUnit.Texture0);
        Gl.BindTexture(TextureTarget.Texture2D,texturedModel.getTexture().getID());
        Gl.DrawElements(PrimitiveType.Triangles,model.getVertexCount(),DrawElementsType.UnsignedInt, null);
        Gl.DisableVertexAttribArray(0);
        Gl.DisableVertexAttribArray(1);
        Gl.BindVertexArray(0);
    }

    private void createProjectionMatrix()
    {
        projectionMatrix = 
            Matrix4x4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(FOV), 
            WIDTH / HEIGHT, 
            NEAR_PLANE, FAR_PLANE);
    }
}