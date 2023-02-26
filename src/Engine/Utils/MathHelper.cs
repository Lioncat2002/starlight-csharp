using System.Numerics;

namespace StarLight.Engine.Utils;

public class MathHelper
{
    public static float DegreesToRadians(float degrees)
    {
        return MathF.PI / 180f * degrees;
    }
    public static Matrix4x4 CreateTransformationMatrix4X4(Vector3 translation, Vector3 rotation, float scale)
    {
        Matrix4x4 matrix=Matrix4x4.Identity;
        matrix *= Matrix4x4.CreateTranslation(translation);
        matrix *= Matrix4x4.CreateRotationY(DegreesToRadians(rotation.Y));
        matrix *= Matrix4x4.CreateRotationX(DegreesToRadians(rotation.X));
        matrix *= Matrix4x4.CreateRotationZ(DegreesToRadians(rotation.Z));
        matrix *= Matrix4x4.CreateScale(scale);
        return matrix;
    }
}