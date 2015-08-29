using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Opinnaytetyo
{
    class InputManager
    {
        private static KeyboardState kbstate;

        public static void update()
        {
            kbstate = Keyboard.GetState();
        }

        public static bool isKeyDown(Keys key)
        {
            return kbstate.IsKeyDown(key);
        }

        public static bool isKeyUp(Keys key)
        {
            return kbstate.IsKeyUp(key);
        }
    }
}
