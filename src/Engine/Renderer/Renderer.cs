using System.Numerics;
using Silk.NET.OpenGL;
using StarLight.Engine.Camera;
using StarLight.Engine.Entities;
using StarLight.Engine.Models;
using StarLight.Engine.Shaders;
using StarLight.Engine.Utils;

namespace StarLight.Engine.Renderer;

public class Renderer
{
    private GL Gl;
    private StaticShader staticShader;
    
    private Matrix4x4 projectionMatrix;

    private static float FOV = 70;
    private static float NEAR_PLANE = 0.1f;
    private static float FAR_PLANE = 1000f;

    private static float WIDTH = 800;
    private static float HEIGHT = 700;

    public Renderer(GL Gl,StaticShader staticShader)
    {
        this.Gl = Gl;
        this.staticShader = staticShader;
        createProjectionMatrix();
        this.staticShader.start();
        this.staticShader.loadProjectionMatrix(projectionMatrix);
        this.staticShader.stop();
    }

    public void start()
    {
        
        Gl.Enable(EnableCap.DepthTest);
        Gl.Clear((uint) (ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit));
        Gl.ClearColor(0,181,226,1);

    }
    public unsafe void render(Entity entity)
    {
        TexturedModel texturedModel = entity.Model;
        RawModel model = texturedModel.RawModel;
        Gl.BindVertexArray(model.VaoId);
        Gl.EnableVertexAttribArray(0);
        Gl.EnableVertexAttribArray(1);
        Matrix4x4 transformationMatrix =
            MathHelper.CreateTransformationMatrix4X4(entity.Position, entity.Rotation, entity.Scale);
        
        staticShader.loadTranformationMatrix(transformationMatrix);
        //activate one of the textures storage provided by opengl
        Gl.ActiveTexture(TextureUnit.Texture0);
        //bind the texture from the textured model
        Gl.BindTexture(TextureTarget.Texture2D,texturedModel.Texture.TextureId);
        //draw the model using triangle
        Gl.DrawElements(PrimitiveType.Triangles,model.VertexCount,DrawElementsType.UnsignedInt, null);
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