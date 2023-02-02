namespace SimpleCityBuilder.Engine.Textures;

public class ModelTexture
{
    private uint textureID;

    public ModelTexture(uint id)
    {
        this.textureID = id;
    }

    public uint getID()
    {
        return this.textureID;
    }
}