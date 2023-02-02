using System.Numerics;
using Silk.NET.Input;
using SimpleCityBuilder.Engine.Utils;

namespace SimpleCityBuilder.Engine.Entities;

public class Camera
{
    private Vector3 position=new Vector3(0,0,0);
    private float pitch;
    private float yaw;
    private float roll;
    private bool isPressed=false;

    public Camera()
    {
    }

    public Matrix4x4 GetViewMatrix()
    {
        return Matrix4x4.CreateLookAt(position, position + new Vector3(0, 0, -1),new Vector3(0,1,0))*Matrix4x4.CreateRotationY(MathHelper.DegreesToRadians(yaw));
    }

    public void move(IKeyboard arg1)
    {
        
        if (arg1.IsKeyPressed(Key.W))
        {
            position.Z -= 0.2f;
            isPressed = true;
        }
        if (arg1.IsKeyPressed(Key.A))
        {
            position.X -= 0.2f;
        }
        if (arg1.IsKeyPressed(Key.S))
        {
            position.Z += 0.2f;
        }
        if (arg1.IsKeyPressed(Key.D))
        {
            position.X += 0.2f;
        }
        if (arg1.IsKeyPressed(Key.Q) )
        {
            Yaw -= 2f;
        }
        if (arg1.IsKeyPressed(Key.E) )
        {
            Yaw += 2f;
        }
    }

    public float Roll
    {
        get => roll;
        set => roll = value;
    }

    public float Yaw
    {
        get => yaw;
        set => yaw = value;
    }

    public float Pitch
    {
        get => pitch;
        set => pitch = value;
    }

    public Vector3 Position
    {
        get => position;
        set => position = value;
    }
}