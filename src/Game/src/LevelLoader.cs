using System.Numerics;
using StarLight.Engine.Entities;
using StarLight.Engine.Models;

namespace Starlight.Game.src;

public class LevelLoader
{
    private TexturedModel Wall;
    private TexturedModel Ground;
    
    private List<Entity> Level;
    public string[] data={
        "1111111111111111                1111111111111111111111",
        "100000000000000111111111111111111000000000000000000001",
        "100000000000000000000000000000000000000001000000000001",
        "100000000100000000000000000000000000000001000000000001",
        "100000000100000111111111111111111000000001000000000001",
        "1000000000000001                1000000000000000000001",
        "1111111111111111                1111111111111111111111",
    };
    public LevelLoader(TexturedModel Wall,TexturedModel Ground)
    {
        this.Wall = Wall;
        this.Ground = Ground;
        Level = new List<Entity>();
    }

    public List<Entity> Load()
    {
        Entity entity;
        for (int i = 0; i < data.Length; i++)
        {
            for (int j = 0; j < data[i].Length; j++)
            {
                switch (data[i][j])
                {
                    case '0':
                        entity = new Entity(Ground,new Vector3(i-5,-1,j-5),new Vector3(),1);
                        Level.Add(entity);
                        break;
                    case '1':
                        entity = new Entity(Wall,new Vector3(i-5,0,j-5),new Vector3(),1);
                        Level.Add(entity);
                        break;
                    default:
                        //Console.WriteLine("Error: Unknown values");
                        break;
                }
                
            }
        }
        return Level;
    }
    
}