using System.Numerics;
using Silk.NET.Input;
using SimpleCityBuilder.Engine.Input;
using SimpleCityBuilder.Engine.Utils;

namespace SimpleCityBuilder.Engine.Entities;

public class Camera
{
    private Vector3 position;
    private Vector3 lookAt;
    private float pitch;
    private float yaw;
    private float roll;
    private bool isPressed=false;

    public Camera()
    {
        position = new Vector3(0, 0, 0);
        lookAt = new Vector3();
    }

    public Matrix4x4 GetViewMatrix()
    {
        return Matrix4x4.CreateLookAt(position, position + new Vector3(0, 0, -1), new Vector3(0, 1, 0)) *
                     Matrix4x4.CreateRotationY(MathHelper.DegreesToRadians(yaw));
        
    }

    public void move(Keyboard keyboard)
    {
        var lookAtMat=GetViewMatrix();
        lookAt.X = lookAtMat.M13;
        lookAt.Y = lookAtMat.M23;
        lookAt.Y = lookAtMat.M33;

        var upVector = new Vector3(0,1,0);
        
        Vector3 forwardVector = lookAt - position;
        forwardVector=Vector3.Normalize(forwardVector);
        Console.WriteLine("Forward vector:="+forwardVector);
        Console.WriteLine("LookAt vector:="+lookAt);

        if (keyboard.KeyDown(Key.W))
        {
            position -= forwardVector * 0.02f;
            lookAt -= forwardVector * 0.02f;
            //position.Z -= 0.2f;
            //isPressed = true;
        }
        if (keyboard.KeyDown(Key.A))
        {
            Vector3 leftVector = Vector3.Cross(upVector, forwardVector);
            position -= leftVector * 0.02f;
            lookAt -= leftVector * 0.02f;
            //position.X -= 0.2f;
        }
        if (keyboard.KeyDown(Key.S))
        {
            position += forwardVector * 0.02f;
            lookAt+= forwardVector * 0.02f;
            //position.Z += 0.2f;
        }
        if (keyboard.KeyDown(Key.D))
        {
            Vector3 rightVector = Vector3.Cross(forwardVector,upVector);
            position += rightVector * 0.02f;
            lookAt += rightVector * 0.02f;
            //position.X += 0.2f;
        }
        if (keyboard.KeyDown(Key.Q) )
        {
            Yaw -= 2f;
        }
        if (keyboard.KeyDown(Key.E) )
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