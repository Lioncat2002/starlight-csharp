namespace StarLight.Engine.Models;

public class RawModel
{
    private uint vaoID;
    private uint vertexCount;

    public RawModel(uint vaoID, uint vertexCount)
    {
        this.vaoID = vaoID;
        this.vertexCount = vertexCount;
    }

    public uint getVaoID()
    {
        return vaoID;
    }

    public uint getVertexCount()
    {
        return vertexCount;
    }
}