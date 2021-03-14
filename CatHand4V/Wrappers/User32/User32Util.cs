using System.Collections.Generic;
using System.Linq;
using CatHand4V.Wrappers.User32.Structs;

namespace CatHand4V.Wrappers.User32
{
    public static class User32Util
    {
        public static IEnumerable<INPUT> CreateKeyboardInputs(IEnumerable<(DwFlags dwflags, VirtualKeyCode virtualKeyCode)> keys)
        {
            return keys.Select(k => CreateKeyboardInput(k.dwflags, k.virtualKeyCode));
        }

        public static INPUT CreateKeyboardInput(DwFlags dwFlags, VirtualKeyCode virtualKeyCode)
        {
            return new INPUT
            {
                type = (int)Type.INPUT_KEYBOARD,
                ui = new INPUT_UNION
                {
                    keyboard = new KEYBDINPUT
                    {
                        wVk = (short)virtualKeyCode,
                        wScan = (short)User32Wrapper.MapVirtualKey((int)virtualKeyCode, 0),
                        dwFlags = (int)dwFlags
                        // time = 0,
                        // dwExtraInfo = IntPtr.Zero
                    }
                }
            };
        }
    }
}
