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
            //support for multiple keyboards
            for (int i = 0; i < input.Keyboards.Count; i++)
            {
                //keydown events
                input.Keyboards[i].KeyDown += KeyPress;
                //keyup events
                input.Keyboards[i].KeyUp += KeyUp;
            }
            Gl = GL.GetApi(window);
            Keyboard = new Keyboard();
            Loader = new Loader(Gl);
        }

        public virtual void OnRender(double time)
        {
            
            
        }

        
        public virtual void OnUpdate(double deltaTime)
        {
           
        }
        private static void KeyPress(IKeyboard arg1, Key arg2, int arg3)
        {
            
            //Check to close the window on escape.
            if (arg2 == Key.Escape)
            {
                window.Close();
                //dispose the loader and the static shader
                Loader.dispose();
                staticShader.dispose();
            }
            //key press events 
            Keyboard.KeyPress(arg2);
            
        }

        private static void KeyUp(IKeyboard arg1, Key arg2, int arg3)
        {
            //key up events
            Keyboard.KeyUp(arg2);
        }
    }
}