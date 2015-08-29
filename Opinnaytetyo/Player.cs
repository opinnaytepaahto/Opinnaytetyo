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
        private float gravity;

        private Vector2 velocity;

        private SpriteEffects flipEffect;
        private bool flipped;

        public void init(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;

            textureRectangle = texture.Bounds;

            speed = 70.0f;
            friction = 50.0f;
            gravity = 30.0f;

            velocity = new Vector2(0.0f, 0.0f);

            flipEffect = SpriteEffects.FlipHorizontally;
            flipped = false;
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
            if (InputManager.isKeyDown(Keys.W) || InputManager.isKeyDown(Keys.Up))
            {
                velocity.Y -= 100.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (velocity.X > 0)
            {
                flipped = false;
                velocity.X -= friction * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (velocity.X < 0.01f)
                {
                    velocity.X = 0;
                }
            }
            if (velocity.X < 0)
            {
                flipped = true;
                velocity.X += friction * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (velocity.X > -0.01f)
                {
                    velocity.X = 0;
                }
            }

            velocity.Y += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            position += velocity;

            keepOnScreen();
        }

        public override void render(SpriteBatch batch)
        {
            this.batch = batch;

            if (flipped)
            {
                batch.Draw(texture, position, textureRectangle, Color.White, 0.0f, new Vector2(0.0f), 1.0f, flipEffect, 0.0f);
            }
            else
            {
                batch.Draw(texture, position, textureRectangle, Color.White);
            }
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
            if (position.Y > MainGame.windowHeight - textureRectangle.Height)
            {
                velocity.Y = 0;
                position.Y = MainGame.windowHeight - textureRectangle.Height;
            }
        }
    }
}
