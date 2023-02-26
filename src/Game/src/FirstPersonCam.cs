using System.Numerics;
using Silk.NET.Input;
using StarLight.Engine.Camera;
using StarLight.Engine.Input;

namespace Starlight.Game.src;

public class FirstPersonCam: Camera
{
    private Vector3 _forward;
    public void Move(Keyboard keyboard)
    {
        var lookAtMat = GetViewMatrix();
        
        _forward = new Vector3(lookAtMat.M13,lookAtMat.M23,lookAtMat.M33);
        Vector3 rightVector = new Vector3(lookAtMat.M11,lookAtMat.M21,lookAtMat.M31);
        
        _forward=Vector3.Normalize(_forward);
        rightVector = Vector3.Normalize(rightVector);
        
        if (keyboard.KeyDown(Key.W))
        {
            Position -= _forward * Speed;
            
        }
        if (keyboard.KeyDown(Key.A))
        {
            Position -= rightVector * Speed;
           
        }
        if (keyboard.KeyDown(Key.S))
        {
            Position += _forward * Speed;
            
        }
        if (keyboard.KeyDown(Key.D))
        {
            Position += rightVector * Speed;
            
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
}