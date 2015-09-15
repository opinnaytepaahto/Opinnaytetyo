using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Opinnaytetyo
{
    class Platform : Entity
    {
        public void init(Texture2D texture, Vector2 position)
        {
            this.Texture = texture;
            this.Position = position;

            this.Hitbox = texture.Bounds;
        }

        public void update()
        {
            base.update(gameTime);
        }

        public void render()
        {
            base.render(batch);
        }
    }
}
