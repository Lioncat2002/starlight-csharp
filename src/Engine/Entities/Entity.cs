using System.Numerics;
using StarLight.Engine.Models;

namespace StarLight.Engine.Entities;

public class Entity
{
    public TexturedModel Model { get; set; }

    public Vector3 Position { get; set; }

    public Vector3 Rotation { get; set; }

    public float Scale { get; set; }


    public Entity(TexturedModel model, Vector3 position, Vector3 rotation, float scale)
    {
        this.Model = model;
        this.Position = position;
        this.Rotation = rotation;
        this.Scale = scale;
    }
}