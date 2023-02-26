using Silk.NET.OpenGL;
using SixLabors.ImageSharp.PixelFormats;
using StarLight.Engine.Models;
using Image = SixLabors.ImageSharp.Image;


namespace StarLight.Engine.Renderer;

public class Loader
{
    private GL Gl;

    private List<uint> vaos;
    private List<uint> vbos;
    private List<uint> textures;

    public Loader(GL Gl)
    {
        this.Gl = Gl;
        vaos = new List<uint>();
        vbos = new List<uint>();
        textures = new List<uint>();
    }
    public RawModel loadToVAO(float[] positions,int[] indices,float[] textureCoords)
    {
        uint vaoID = createVAO();
        bindIndicesBuffer(indices);
        storeDataInAttributeList(0,3,positions);
        storeDataInAttributeList(1,2,textureCoords);
        unbindVAO();
        return new RawModel(vaoID, (uint)indices.Length);
    }

    public unsafe uint loadTexture(string fileName)
    {
        uint textureID = Gl.GenTexture();
        Gl.ActiveTexture(TextureUnit.Texture0);
        Gl.BindTexture(TextureTarget.Texture2D,textureID);
        //Loading an image using imagesharp.
        using (var img = Image.Load<Rgba32>("Content/"+fileName+".png"))
        {
            //Reserve enough memory from the gpu for the whole image
            Gl.TexImage2D(TextureTarget.Texture2D, 0, InternalFormat.Rgba8, (uint)img.Width, (uint)img.Height, 0,
                PixelFormat.Rgba, PixelType.UnsignedByte, null);
            img.ProcessPixelRows(accessor =>
            {
                //ImageSharp 2 does not store images in contiguous memory by default, so we must send the image row by row
                for (int y = 0; y < accessor.Height; y++)
                {
                    fixed (void* data = accessor.GetRowSpan(y))
                    {
                        //Loading the actual image.
                        Gl.TexSubImage2D(TextureTarget.Texture2D, 0, 0, y, (uint) accessor.Width, 1, PixelFormat.Rgba, PixelType.UnsignedByte, data);
                    }
                }
            });
        }
        SetParameters();
        textures.Add(textureID);
        return textureID;
    }

    private void SetParameters()
    {
        //Setting some texture perameters so the texture behaves as expected.
        Gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int) GLEnum.ClampToEdge);
        Gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int) GLEnum.ClampToEdge);
        //needed to set it to NearestMipMap nearest so that the pixel images are shown correctly
        Gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int) GLEnum.NearestMipmapNearest);
        Gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int) GLEnum.Nearest);
        Gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureBaseLevel, 0);
        Gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMaxLevel, 8);
        //Generating mipmaps.
        Gl.GenerateMipmap(TextureTarget.Texture2D);
    }

    private uint createVAO()
    {
        uint vaoID = Gl.GenVertexArray();
        vaos.Add(vaoID);
        Gl.BindVertexArray(vaoID);
        return vaoID;
    }

    private unsafe void storeDataInAttributeList(uint attributeNumber, int coordSize,float[] data)
    {
        uint vboID = Gl.GenBuffer();
        vbos.Add(vboID);
        Gl.BindBuffer(BufferTargetARB.ArrayBuffer,vboID);

        fixed (void* v = &data[0])
        {
            Gl.BufferData(BufferTargetARB.ArrayBuffer,(nuint)(data.Length*sizeof(uint)),v,BufferUsageARB.StaticDraw);
        }
        Gl.VertexAttribPointer(attributeNumber,coordSize,VertexAttribPointerType.Float,false,0,null);
        Gl.BindBuffer(BufferTargetARB.ArrayBuffer,0);
    }

    private unsafe void bindIndicesBuffer(int[] indices)
    {
        uint vboID = Gl.GenBuffer();
        vbos.Add(vboID);
        Gl.BindBuffer(BufferTargetARB.ElementArrayBuffer,vboID);
        fixed (void* i = &indices[0])
        {
            Gl.BufferData(BufferTargetARB.ElementArrayBuffer,(nuint)(indices.Length*sizeof(uint)),i,BufferUsageARB.StaticDraw);
        }
    }

    private void unbindVAO()
    {
        Gl.BindVertexArray(0);
    }

    public void dispose()
    {
        foreach (var vao in vaos)
        {
            Gl.DeleteVertexArray(vao);
        }
        foreach (var vbo in vbos)
        {
            Gl.DeleteBuffer(vbo);
        }

        foreach (var textureID in textures)
        {
            Gl.DeleteTexture(textureID);
        }
    }
}