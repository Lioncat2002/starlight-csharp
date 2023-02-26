namespace StarLight.Engine.Models;

public class RawModel
{
    public uint VaoId { get; set; }

    public uint VertexCount { get; set; }

    public RawModel(uint vaoID, uint vertexCount)
    {
        this.VaoId = vaoID;
        this.VertexCount = vertexCount;
    }
    
}