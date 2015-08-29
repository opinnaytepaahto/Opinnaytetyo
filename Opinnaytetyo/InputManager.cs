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
        private static KeyboardState prevKbstate;

        public static void update()
        {
            prevKbstate = kbstate;
            kbstate = Keyboard.GetState();
        }

        public static bool isKeyDown(Keys key)
        {
            return kbstate.IsKeyDown(key);
        }

        public static bool isKeyJustDown(Keys key)
        {
            if (kbstate.IsKeyDown(key) && !prevKbstate.IsKeyDown(key))
            {
                return true;
            }

            return false;
        }

        public static bool isKeyUp(Keys key)
        {
            return kbstate.IsKeyUp(key);
        }
    }
}
