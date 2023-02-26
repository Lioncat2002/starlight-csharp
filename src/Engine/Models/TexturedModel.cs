using StarLight.Engine.Textures;

namespace StarLight.Engine.Models;

public class TexturedModel
{
    private RawModel rawModel;

    public RawModel RawModel
    {
        get => rawModel;
        set => rawModel = value ?? throw new ArgumentNullException(nameof(value));
    }

    public ModelTexture Texture
    {
        get => texture;
        set => texture = value ?? throw new ArgumentNullException(nameof(value));
    }

    private ModelTexture texture;

    public TexturedModel(RawModel model, ModelTexture texture)
    {
        rawModel = model;
        this.texture = texture;
    }

}