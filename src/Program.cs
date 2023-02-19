using System.Numerics;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using SimpleCityBuilder.Engine.Entities;
using SimpleCityBuilder.Engine.Input;
using SimpleCityBuilder.Engine.Models;
using SimpleCityBuilder.Engine.Renderer;
using SimpleCityBuilder.Engine.Shaders;
using SimpleCityBuilder.Engine.Textures;

namespace SimpleCityBuilder
{
    public class Program
    {
        private static IWindow? _window;
        private static GL? _gl;
        private static Loader? _loader;
        private static Renderer? _renderer;
        private static RawModel? _model;
        private static StaticShader? _staticShader;
        private static ModelTexture? _texture;
        private static TexturedModel? _texturedModel;
        private static Keyboard? _keyboard;
        private static Camera? _camera;
        
        private static List<Entity>? _level;
        

        private static readonly float[] Vertices = {			
            -0.5f,0.5f,-0.5f,	
            -0.5f,-0.5f,-0.5f,	
            0.5f,-0.5f,-0.5f,	
            0.5f,0.5f,-0.5f,		
				
            -0.5f,0.5f,0.5f,	
            -0.5f,-0.5f,0.5f,	
            0.5f,-0.5f,0.5f,	
            0.5f,0.5f,0.5f,
				
            0.5f,0.5f,-0.5f,	
            0.5f,-0.5f,-0.5f,	
            0.5f,-0.5f,0.5f,	
            0.5f,0.5f,0.5f,
				
            -0.5f,0.5f,-0.5f,	
            -0.5f,-0.5f,-0.5f,	
            -0.5f,-0.5f,0.5f,	
            -0.5f,0.5f,0.5f,
				
            -0.5f,0.5f,0.5f,
            -0.5f,0.5f,-0.5f,
            0.5f,0.5f,-0.5f,
            0.5f,0.5f,0.5f,
				
            -0.5f,-0.5f,0.5f,
            -0.5f,-0.5f,-0.5f,
            0.5f,-0.5f,-0.5f,
            0.5f,-0.5f,0.5f
				
        };

        //Index data, uploaded to the EBO.
        private static readonly int[] Indices = {
            0,1,3,	
            3,1,2,	
            4,5,7,
            7,5,6,
            8,9,11,
            11,9,10,
            12,13,15,
            15,13,14,	
            16,17,19,
            19,17,18,
            20,21,23,
            23,21,22

        };

        private static readonly float[] TextureCoords = {
				
            0,0,
            0,1,
            1,1,
            1,0,			
            0,0,
            0,1,
            1,1,
            1,0,			
            0,0,
            0,1,
            1,1,
            1,0,
            0,0,
            0,1,
            1,1,
            1,0,
            0,0,
            0,1,
            1,1,
            1,0,
            0,0,
            0,1,
            1,1,
            1,0

				
        };
        private static void Main(string[] args)
        {
            //Create a window.
            var options = WindowOptions.Default;
            options.Size = new Vector2D<int>(800, 600);
            options.Title = "StarLight";
            options.PreferredDepthBufferBits = 1;

            _window = Window.Create(options);
            
            
            //Assign events.
            _window.Load += OnLoad;
            _window.Update += OnUpdate;
            _window.Render += OnRender;

            //Run the window.
            _window.Run();
        }
        
        private static void OnLoad()
        {
            //Set-up input context.
            IInputContext input = _window.CreateInput();
            for (int i = 0; i < input.Keyboards.Count; i++)
            {
                input.Keyboards[i].KeyDown += KeyDown;
                input.Keyboards[i].KeyUp += KeyUp;
            }
            
            _gl = GL.GetApi(_window);

            _loader = new Loader(_gl);
            _level = new List<Entity>();
            _model = _loader.loadToVAO(Vertices,Indices,TextureCoords);
            _staticShader = new StaticShader(_gl);
            _renderer = new Renderer(_gl,_staticShader);
            _texture = new ModelTexture(_loader.loadTexture("kenney_tinydungeon/Tiles/tile_0049"));
            _texturedModel = new TexturedModel(_model, _texture);
            var _texture1 = new ModelTexture(_loader.loadTexture("kenney_tinydungeon/Tiles/tile_0040"));
            var _texturedModel1 = new TexturedModel(_model, _texture1);
            _keyboard = new Keyboard();
            
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

        private static void OnRender(double time)
        {
            _renderer.start();
            _staticShader.start();
            _staticShader.loadViewMatrix(_camera);
            foreach (var obj in _level)
            {
                _renderer.render(obj,_staticShader);
            }
            _staticShader.stop();
            //Here all rendering should be done.
        }

        
        private static void OnUpdate(double deltaTime)
        {
            _camera.move(_keyboard);
        }

        private static void KeyDown(IKeyboard arg1, Key arg2, int arg3)
        {
            
            //Check to close the window on escape.
            if (arg2 == Key.Escape)
            {
                _window.Close();
                _loader.dispose();
                _staticShader.dispose();
            }

            _keyboard.KeyPress(arg2);
            
        }

        private static void KeyUp(IKeyboard arg1, Key arg2, int arg3)
        {
            _keyboard.KeyUp(arg2);
        }
    }
}