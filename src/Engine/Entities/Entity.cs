using System.Numerics;
using SimpleCityBuilder.Models;

namespace SimpleCityBuilder.Entities;

public class Entity
{
    private TexturedModel model;
    private Vector3 position;
    private Vector3 rotation;
    private float scale;

    public TexturedModel Model
    {
        get => model;
        set => model = value ?? throw new ArgumentNullException(nameof(value));
    }

    public Vector3 Position
    {
        get => position;
        set => position = value;
    }

    public Vector3 Rotation
    {
        get => rotation;
        set => rotation = value;
    }

    public float Scale
    {
        get => scale;
        set => scale = value;
    }

    

    public Entity(TexturedModel model, Vector3 position, Vector3 rotation, float scale)
    {
        this.model = model;
        this.position = position;
        this.rotation = rotation;
        this.scale = scale;
    }

    public void increasePosition(Vector3 dm)
    {
        position+= dm;
    }
    public void increaseRotation(Vector3 dr)
    {
        rotation+= dr;
    }

}