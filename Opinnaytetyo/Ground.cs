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
            this.textureRectangle.X = rect.X;
            this.textureRectangle.Y = rect.Y;

            this.textureRectangle.Width = rect.Width;
            this.textureRectangle.Height = rect.Height;
        }
    }
}
