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

    public Camera()
    {
        lookAt = new Vector3();
        position = new Vector3();
    }

    public Matrix4x4 GetViewMatrix()
    {
        return Matrix4x4.CreateLookAt(position, position + new Vector3(0, 0, -1), new Vector3(0, 1, 0)) *
                     Matrix4x4.CreateRotationY(MathHelper.DegreesToRadians(yaw));
        
    }

    public void move(Keyboard keyboard)
    {
        var lookAtMat = GetViewMatrix();
        var lookAtInViewSpace = new Vector3(0, 0, -1);
        Matrix4x4.Invert(lookAtMat, out lookAtMat);
        lookAt=Vector3.TransformNormal(lookAtInViewSpace,lookAtMat);
        var upVector = new Vector3(0,1,0);
        Vector3 forwardVector = new Vector3(lookAtMat.M13,lookAtMat.M23,lookAtMat.M33);
        
        forwardVector=Vector3.Normalize(forwardVector);
        if (keyboard.KeyDown(Key.W))
        {
            position += forwardVector * 0.02f;
            
        }
        if (keyboard.KeyDown(Key.A))
        {
            Vector3 leftVector = Vector3.Cross(upVector, forwardVector);
            position += leftVector * 0.02f;
           
        }
        if (keyboard.KeyDown(Key.S))
        {
            position -= forwardVector * 0.02f;
            
        }
        if (keyboard.KeyDown(Key.D))
        {
            Vector3 leftVector = Vector3.Cross(upVector, forwardVector);;
            position -= leftVector * 0.02f;
            
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