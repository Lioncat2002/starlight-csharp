using System.Numerics;
using Silk.NET.Maths;
using Silk.NET.Windowing;
using StarLight.Engine;
using StarLight.Engine.Entities;
using StarLight.Engine.Input;
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
            
           

        //Run the window.
        window.Run();
    }
    public override void OnLoad()
    {
        base.OnLoad();
        _level = new List<Entity>();
        _model = Loader.loadToVAO(Constants.Vertices,Constants.Indices,Constants.TextureCoords);
        staticShader = new StaticShader(Gl);
        _renderer = new Renderer(Gl,staticShader);
        
        _texture = new ModelTexture(Loader.loadTexture("kenney_tinydungeon/Tiles/tile_0049"));
        _texturedModel = new TexturedModel(_model, _texture);
        
        var _texture1 = new ModelTexture(Loader.loadTexture("kenney_tinydungeon/Tiles/tile_0040"));
        var _texturedModel1 = new TexturedModel(_model, _texture1);
        Keyboard = new Keyboard();
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Entity level_block;
                if (i == 0 || j == 0 || i==9 || j==9)
                {
                       
                    level_block = new Entity(_texturedModel1, new Vector3(i-5, 0, j-5), new Vector3(0, 0, 0), 1);
                }
                else
                {
                      
                    level_block = new Entity(_texturedModel, new Vector3(i-5, -1, j-5), new Vector3(0, 0, 0), 1);
                }
                _level.Add(level_block);   
            }    
        }
            
            
        _camera = new Camera();
    }

    public override void OnUpdate(double deltaTime)
    {
        base.OnUpdate(deltaTime);
        _camera.move(Keyboard);
        
    }

    public override void OnRender(double time)
    {
        base.OnRender(time);
        staticShader.loadViewMatrix(_camera);
        _renderer.start();
        
        foreach (var obj in _level)
        {
            _renderer.render(obj);
        }
        staticShader.stop();
        
    }

    
}