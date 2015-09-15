using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Opinnaytetyo
{
    class Ground : Entity
    {
        public Ground(Rectangle rect)
        {
            this.Hitbox = new Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
        }
    }
}
