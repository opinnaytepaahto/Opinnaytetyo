using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Opinnaytetyo
{
    class Entity
    {
        public Vector2 position;

        public Texture2D texture;
        public Rectangle textureRectangle;

        protected SpriteBatch batch;
        protected GameTime gameTime;

        public void init(Texture2D texture, Vector2 position)
        {
            this.position = position;
            this.texture = texture;

            textureRectangle = texture.Bounds;
        }

        public void update(GameTime gameTime)
        {
            this.gameTime = gameTime;

            textureRectangle.X = (int)position.X;
            textureRectangle.Y = (int)position.Y;
            textureRectangle = texture.Bounds;
        }

        public void render(SpriteBatch batch)
        {
            this.batch = batch;

            batch.Draw(texture, position, textureRectangle, Color.White);
        }
    }
}
