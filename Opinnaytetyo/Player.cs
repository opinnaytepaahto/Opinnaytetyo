﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Opinnaytetyo
{
    class Player : Entity
    {
        private float speed;
        private float friction;

        private Vector2 velocity;

        public void init(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            textureRectangle = texture.Bounds;

            speed = 100.0f;
            friction = 30.0f;
            velocity = new Vector2(0.0f, 0.0f);
        }

        public void update()
        {
            base.update(gameTime);

            if (InputManager.isKeyDown(Keys.A) || InputManager.isKeyDown(Keys.Left))
            {
                velocity.X -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (InputManager.isKeyDown(Keys.D) || InputManager.isKeyDown(Keys.Right))
            {
                velocity.X += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (velocity.X > 0)
            {
                velocity.X -= friction * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (velocity.X < 0.01f)
                {
                    velocity.X = 0;
                }
            }
            if (velocity.X < 0)
            {
                velocity.X += friction * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (velocity.X > -0.01f)
                {
                    velocity.X = 0;
                }
            }

            position += velocity;

            keepOnScreen();
        }

        public void render()
        {
            base.render(batch);
        }

        private void keepOnScreen()
        {
            if (position.X < 0)
            {
                velocity.X = 0;
                position.X = 0;
            }
            if (position.X > MainGame.windowWidth - textureRectangle.Width)
            {
                velocity.X = 0;
                position.X = MainGame.windowWidth - textureRectangle.Width;
            }
        }
    }
}
