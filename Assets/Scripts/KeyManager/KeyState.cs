using System;

namespace BreakRules
{
    [Flags]
    internal enum KeyState
    {
        OnPress = 1 << 0,
        OnKeyDown = 1 << 1,
        OnKeyUp = 1 << 2    
    }
}