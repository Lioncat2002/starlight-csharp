using Silk.NET.Input;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using StarLight.Engine.Input;
using StarLight.Engine.Renderer;
using StarLight.Engine.Shaders;

namespace StarLight.Engine
{
    public class Entry
    {
        public static IWindow window;
        public static GL Gl;
        public static Loader Loader;
        public static Keyboard Keyboard;
        public static StaticShader staticShader;

        public virtual void OnLoad()
        {
            //Set-up input context.
            IInputContext input = window.CreateInput();
            for (int i = 0; i < input.Keyboards.Count; i++)
            {
                input.Keyboards[i].KeyDown += KeyDown;
                input.Keyboards[i].KeyUp += KeyUp;
            }
            Gl = GL.GetApi(window);
            Keyboard = new Keyboard();
            Loader = new Loader(Gl);
        }

        public virtual void OnRender(double time)
        {
            staticShader.start();
        }

        
        public virtual void OnUpdate(double deltaTime)
        {
           
        }
        private static void KeyDown(IKeyboard arg1, Key arg2, int arg3)
        {
            
            //Check to close the window on escape.
            if (arg2 == Key.Escape)
            {
                window.Close();
                Loader.dispose();
                staticShader.dispose();
            }

            Keyboard.KeyPress(arg2);
            
        }

        private static void KeyUp(IKeyboard arg1, Key arg2, int arg3)
        {
            Keyboard.KeyUp(arg2);
        }
    }
}