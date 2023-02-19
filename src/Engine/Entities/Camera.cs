using System.Numerics;
using Silk.NET.Input;
using StarLight.Engine.Input;
using StarLight.Engine.Utils;

namespace StarLight.Engine.Entities;

public class Camera
{
    private Vector3 _position;
    private Vector3 _forward;
    
    private float _pitch;
    private float _yaw;
    private float _roll;

    private float _speed;
    

    public Camera()
    {
        _speed = 0.02f;
        _position = new Vector3();
    }

    public Matrix4x4 GetViewMatrix()
    {
        return Matrix4x4.CreateLookAt(_position, _position + new Vector3(0, 0, -1), new Vector3(0, 1, 0)) *
                     Matrix4x4.CreateRotationY(MathHelper.DegreesToRadians(_yaw));
        
    }

    public void move(Keyboard keyboard)
    {
        var lookAtMat = GetViewMatrix();
        
        _forward = new Vector3(lookAtMat.M13,lookAtMat.M23,lookAtMat.M33);
        Vector3 rightVector = new Vector3(lookAtMat.M11,lookAtMat.M21,lookAtMat.M31);
        
        _forward=Vector3.Normalize(_forward);
        rightVector = Vector3.Normalize(rightVector);
        
        if (keyboard.KeyDown(Key.W))
        {
            _position -= _forward * _speed;
            
        }
        if (keyboard.KeyDown(Key.A))
        {
            _position -= rightVector * _speed;
           
        }
        if (keyboard.KeyDown(Key.S))
        {
            _position += _forward * _speed;
            
        }
        if (keyboard.KeyDown(Key.D))
        {
            _position += rightVector * _speed;
            
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
        get => _roll;
        set => _roll = value;
    }

    public float Yaw
    {
        get => _yaw;
        set => _yaw = value;
    }

    public float Pitch
    {
        get => _pitch;
        set => _pitch = value;
    }

    public Vector3 Position
    {
        get => _position;
        set => _position = value;
    }
    public float Speed
    {
        get => _speed;
        set => _speed = value;
    }
}