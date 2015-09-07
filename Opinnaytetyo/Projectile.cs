using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Opinnaytetyo
{
    class Projectile : Entity
    {
        private bool flipped;

        public float timer;

        public Projectile(Texture2D texture, Vector2 position, bool flipped)
        {
            this.texture = texture;
            this.position = position;

            this.flipped = flipped;

            timer = 1.0f;

            this.textureRectangle = texture.Bounds;
        }

        public override void update(GameTime gameTime)
        {
            base.update(gameTime);

            if (flipped)
            {
                 position.X -= 10.0f;
            }
            else
            {
                position.X += 10.0f;
            }

            timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public override void render(SpriteBatch batch)
        {
            base.render(batch);
        }
    }
}
