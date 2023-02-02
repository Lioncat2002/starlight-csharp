using System.Drawing;
using System.Numerics;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using SimpleCityBuilder.Entities;
using SimpleCityBuilder.Models;
using SimpleCityBuilder.Renderer;
using SimpleCityBuilder.Shaders;
using SimpleCityBuilder.Textures;

namespace SimpleCityBuilder
{
    public class Program
    {
        private static IWindow? _window;
        private static GL? _Gl;
        private static Loader? _loader;
        private static Renderer.Renderer? _renderer;
        private static RawModel? _model;
        private static StaticShader _staticShader;
        private static ModelTexture _texture;
        private static TexturedModel _texturedModel;
        private static Entity _entity;
        private static Camera _camera;
        
        private static readonly float[] vertices = {			
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
        private static readonly int[] indices = {
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

        private static readonly float[] textureCoords = {
				
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
            options.Title = "Testing";
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
            }
            
            _Gl = GL.GetApi(_window);

            _loader = new Loader(_Gl);
            
            _model = _loader.loadToVAO(vertices,indices,textureCoords);
            _staticShader = new StaticShader(_Gl);
            _renderer = new Renderer.Renderer(_Gl,_staticShader);
            _texture = new ModelTexture(_loader.loadTexture("wall"));
            _texturedModel = new TexturedModel(_model, _texture);
            _entity = new Entity(_texturedModel, new Vector3(0, 0, -2), new Vector3(0, 0, 0), 1);

            _camera = new Camera();
        }

        private static void OnRender(double obj)
        {
            _renderer.start();
            _staticShader.start();
            _staticShader.loadViewMatrix(_camera);
            _renderer.render(_entity,_staticShader);
            _staticShader.stop();
            //Here all rendering should be done.
        }

        private static void OnUpdate(double deltaTime)
        {
            float speed = 0.2f * (float)deltaTime;
            //_entity.increasePosition(new Vector3(0,0,-speed));
            
            Console.WriteLine(_camera.Position);
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

            Vector3 position=new Vector3();
            //Camera movement
            _camera.move(arg1);
        }
    }
}