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
        private SpriteEffects flipEffect;

        public String id;

        public float Speed { get; set; }

        public float timer;

        public Projectile(Texture2D texture, Vector2 position, bool flipped, String id, float speed)
        {
            this.Texture = texture;
            this.Position = position;

            this.id = id;

            this.Speed = speed;

            this.flipped = flipped;
            flipEffect = SpriteEffects.FlipHorizontally;

            timer = 1.5f;

            this.Hitbox = texture.Bounds;
        }

        public override void update(GameTime gameTime)
        {
            base.update(gameTime);

            if (flipped)
            {
                 Position -= new Vector2(Speed, 0);
            }
            else
            {
                Position += new Vector2(Speed, 0);
            }

            timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public override void render(SpriteBatch batch)
        {
            base.render(batch);

            if (flipped)
            {
                batch.Draw(Texture, Position, null, Color.White, 0.0f, new Vector2(0.0f, 0.0f), 1.0f, flipEffect, 0.0f);
            }
            else
            {
                batch.Draw(Texture, Position);
            }
        }
    }
}
