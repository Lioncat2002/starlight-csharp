using Silk.NET.Input;

namespace StarLight.Engine.Input;

public class Keyboard
{
    public Dictionary<Key, bool> keyStates; //contains all the keys that are being pressed and held down
    public Keyboard()
    {
        keyStates = new Dictionary<Key, bool>();
    }

    public void KeyPress(Key key)
    {
        keyStates[key]=true;
    }

    public void KeyUp(Key key)
    {
        keyStates[key]=false;
    }

    public bool KeyDown(Key key)
    {
        bool value;
        if (keyStates.TryGetValue(key, out value))//true if value found else false
        {
            return value;
        }
        return false;
    }
}