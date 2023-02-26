using System.Numerics;
using Silk.NET.Maths;
using Silk.NET.Windowing;
using StarLight.Engine;
using StarLight.Engine.Entities;
using StarLight.Engine.Models;
using StarLight.Engine.Renderer;
using StarLight.Engine.Shaders;
using StarLight.Engine.Textures;
using Starlight.Game.src;

namespace Starlight.Game;

public class Game1:Entry
{
    private static Renderer? _renderer;
    private static RawModel? _model;
    private static ModelTexture? _texture;
    private static TexturedModel? _ground;
    private static FirstPersonCam? _firstPersonCam;

    private static List<Entity>? _level;
    private static LevelLoader _levelLoader;
    
    public static void Main(string[] args)
    {
        //Create a window.
        var options = WindowOptions.Default;
        options.Size = new Vector2D<int>(800, 600);
        options.Title = "StarLight";
        options.PreferredDepthBufferBits = 1;

        window = Window.Create(options);
        Game1 instance = new Game1();
            
        //Assign events.
        window.Load += instance.OnLoad;
        window.Update += instance.OnUpdate;
        window.Render += instance.OnRender;
        
        //Run the game.
        window.Run();
    }
    public override void OnLoad()
    {
        base.OnLoad();
        //a list of all the entities in the level so that it is easier to render
        _level = new List<Entity>();
        
        
        staticShader = new StaticShader(Gl);
        _renderer = new Renderer(Gl,staticShader);

        //create a new model
        _model = Loader.loadToVAO(Constants.Vertices,Constants.Indices,Constants.TextureCoords);
        
        //loading the texture 1 and assign it to a model with texture
        _texture = new ModelTexture(Loader.loadTexture("kenney_tinydungeon/Tiles/tile_0049"));
        _ground = new TexturedModel(_model, _texture);
        
        //loading the texture 2 and assign it to a model with texture
        var _texture1 = new ModelTexture(Loader.loadTexture("kenney_tinydungeon/Tiles/tile_0040"));
        var _wall = new TexturedModel(_model, _texture1);
        _levelLoader = new LevelLoader(_wall,_ground);
        //LevelLoader reads from an array of string and loads it into a list
        _level = _levelLoader.Load();
        //initialize new camera
        _firstPersonCam=new FirstPersonCam();
        _firstPersonCam.Position = new Vector3(-1f, 0.1f, 0f);
        _firstPersonCam.Speed = 0.05f;
        
    }

    public bool Intersect(Vector3 a,Vector3 b)
    {
        return a.X <= b.X + 0.6f &&
               a.X + 0.6f >= b.X &&
               a.Y <= b.Y + 0.6f &&
               a.Y+0.6f >= b.Y &&
               a.Z <= b.Z+0.6f &&
               a.Z+0.6f >= b.Z;
    }

    public override void OnUpdate(double deltaTime)
    {
        base.OnUpdate(deltaTime);
        //camera update
        var previousPosition = _firstPersonCam.Position;
        _firstPersonCam.Move(Keyboard);
        foreach (var block in _level)
        {
            if (Intersect(_firstPersonCam.Position, block.Position))
            {
                _firstPersonCam.Position = previousPosition;
            }
          
        }
        
    }

    public override void OnRender(double time)
    {
        base.OnRender(time);
        //start static shader and renderer
        staticShader.start();
        _renderer.start();
        //load the view matrix of camera
        staticShader.loadViewMatrix(_firstPersonCam);
        //render the entities
        foreach (var obj in _level)
        {
            _renderer.render(obj);
        }
        //stop the static shader
        staticShader.stop();
        
    }

    
}