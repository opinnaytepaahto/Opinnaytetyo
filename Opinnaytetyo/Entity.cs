﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Opinnaytetyo
{
    abstract class Entity
    {
        public Vector2 position;

        public Texture2D texture;
        public Rectangle textureRectangle;

        protected SpriteBatch batch;
        protected GameTime gameTime;

        public virtual void update(GameTime gameTime)
        {
            this.gameTime = gameTime;

            textureRectangle.X = (int)position.X;
            textureRectangle.Y = (int)position.Y;
            textureRectangle = texture.Bounds;
        }

        public virtual void render(SpriteBatch batch)
        {
            this.batch = batch;

            batch.Draw(texture, position, textureRectangle, Color.White);
        }

        public int getWidth()
        {
            return textureRectangle.Width;
        }

        public int getHeight()
        {
            return textureRectangle.Height;
        }
    }
}
