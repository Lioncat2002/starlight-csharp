using System.Numerics;
using StarLight.Engine.Utils;

namespace StarLight.Engine.Camera;

public class Camera
{
    public float Roll { get; set; }

    public float Yaw { get; set; }

    public float Pitch { get; set; }

    public Vector3 Position { get; set; }

    public float Speed { get; set; }
    public Camera()
    {
        Speed = 0.02f;
        Position = new Vector3();
    }

    public Matrix4x4 GetViewMatrix()
    {
        return Matrix4x4.CreateLookAt(Position, Position + new Vector3(0, 0, -1), new Vector3(0, 1, 0)) *
                     Matrix4x4.CreateRotationY(MathHelper.DegreesToRadians(Yaw));
        
    }
}