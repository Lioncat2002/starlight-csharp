using System.Numerics;
using Silk.NET.Maths;
using Silk.NET.Windowing;
using StarLight.Engine;
using StarLight.Engine.Entities;
using StarLight.Engine.Models;
using StarLight.Engine.Renderer;
using StarLight.Engine.Shaders;
using StarLight.Engine.Textures;

namespace Starlight.Game;

public class Game1:Entry
{
    private static Renderer? _renderer;
    private static RawModel? _model;
    private static ModelTexture? _texture;
    private static TexturedModel? _texturedModel;
    private static Camera? _camera;

    private static List<Entity>? _level;
    
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
        _texturedModel = new TexturedModel(_model, _texture);
        
        //loading the texture 2 and assign it to a model with texture
        var _texture1 = new ModelTexture(Loader.loadTexture("kenney_tinydungeon/Tiles/tile_0040"));
        var _texturedModel1 = new TexturedModel(_model, _texture1);
        
        //quick implementation to loading a level(basically a square arena)
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Entity level_block;
                if (i == 0 || j == 0 || i==9 || j==9)
                {
                    //walls
                    level_block = new Entity(_texturedModel1, new Vector3(i-5, 0, j-5), new Vector3(0, 0, 0), 1);
                }
                else
                {
                    //ground
                    level_block = new Entity(_texturedModel, new Vector3(i-5, -1, j-5), new Vector3(0, 0, 0), 1);
                }
                //push into the level list
                _level.Add(level_block);   
            }    
        }
            
        //initialize new camera
        _camera = new Camera();
    }

    public override void OnUpdate(double deltaTime)
    {
        base.OnUpdate(deltaTime);
        //camera update
        _camera.move(Keyboard);
        
    }

    public override void OnRender(double time)
    {
        base.OnRender(time);
        //start static shader and renderer
        staticShader.start();
        _renderer.start();
        //load the view matrix of camera
        staticShader.loadViewMatrix(_camera);
        //render the entities
        foreach (var obj in _level)
        {
            _renderer.render(obj);
        }
        //stop the static shader
        staticShader.stop();
        
    }

    
}