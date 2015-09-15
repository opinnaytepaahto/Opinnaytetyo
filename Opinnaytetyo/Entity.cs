using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Opinnaytetyo
{
    abstract class Entity
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; protected set; }

        public float gravity;

        public Texture2D Texture { get; protected set; }
        public Rectangle Hitbox { get; protected set; }

        protected SpriteBatch batch;
        protected GameTime gameTime;

        public virtual void update(GameTime gameTime)
        {
            this.gameTime = gameTime;

            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
        }

        public virtual void render(SpriteBatch batch)
        {
            this.batch = batch;

            batch.Draw(Texture, Position, null, Color.White);
        }
    }
}
