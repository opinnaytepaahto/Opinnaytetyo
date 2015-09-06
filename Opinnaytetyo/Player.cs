using Microsoft.Xna.Framework;
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
        private float cooldown;

        private SpriteEffects flipEffect;
        private bool flipped;

        private Vector2 oldPos;
        private bool grounded = false;

        public bool colliding = false;
        public bool colPlay;
        public bool colExit;

        public bool collide = true;
    
        public void init(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;

            textureRectangle = texture.Bounds;

            speed = 0.5f;
            friction = 0.15f;
            gravity = 0.8f;
            cooldown = 0.0f;

            velocity = new Vector2(0.0f, 0.0f);

            flipEffect = SpriteEffects.FlipHorizontally;
            flipped = false;

            colPlay = false;
            colExit = false;
        }

        public override void update(GameTime gameTime)
        {
            base.update(gameTime);

            cooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            handleMovement();
            applyFrictionAndGravity();
            keepOnScreen();
            moveIfPossible();
            stopIfBlocked();

            Console.WriteLine("Velocity: " + velocity.X + ", " + velocity.Y);

            if (cooldown <= 0)
            {
                cooldown = 0;
            }

            if (velocity.X >= 100)
            {
                velocity.X = 100;
            }

            if (!colliding)
            {
                colPlay = false;
                colExit = false;
            }
        }

        public override void render(SpriteBatch batch)
        {
            this.batch = batch;

            if (flipped)
            {
                batch.Draw(texture, position, null, Color.White, 0.0f, new Vector2(0.0f, 0.0f), 1.0f, flipEffect, 0.0f);
            }
            else
            {
                batch.Draw(texture, position, null, Color.White);
            }
        }

        private void moveIfPossible()
        {
            oldPos = position;
            position += velocity * (float)gameTime.ElapsedGameTime.TotalMilliseconds / 15;
            position = howLongToMove(oldPos, position, textureRectangle);
        }

        private void handleMovement()
        {
            if (InputManager.isKeyDown(Keys.A) || InputManager.isKeyDown(Keys.Left))
            {
                velocity -= Vector2.UnitX * speed;
                flipped = true;
            }
            if (InputManager.isKeyDown(Keys.D) || InputManager.isKeyDown(Keys.Right))
            {
                velocity += Vector2.UnitX * speed;
                flipped = false;
            }
            if ((InputManager.isKeyJustDown(Keys.W) || InputManager.isKeyJustDown(Keys.Up)) && grounded)
            {
                cooldown = 0.5f;
                velocity = -Vector2.UnitY * 20;
            }
            if (InputManager.isKeyDown(Keys.S) || InputManager.isKeyDown(Keys.Down))
            {
                collide = false;
            }
            else
            {
                collide = true;
            }
        }

        private void applyFrictionAndGravity()
        {
            //if (velocity.X > 0)
            //{
            //    flipped = false;
            //    velocity.X -= friction * (float)gameTime.ElapsedGameTime.TotalSeconds;

            //    if (velocity.X < 0.01f)
            //    {
            //        velocity.X = 0;
            //    }
            //}
            //if (velocity.X < 0)
            //{
            //    flipped = true;
            //    velocity.X += friction * (float)gameTime.ElapsedGameTime.TotalSeconds;

            //    if (velocity.X > -0.01f)
            //    {
            //        velocity.X = 0;
            //    }
            //}

            // Gravity
            velocity += Vector2.UnitY * gravity;

            // Friciton
            velocity -= velocity * Vector2.One * friction;
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
            if (position.Y > MainGame.windowHeight - textureRectangle.Height - 133)
            {
                velocity.Y = 0;
                position.Y = MainGame.windowHeight - textureRectangle.Height - 133;
                grounded = true;
            }
            else
            {
                grounded = false;
            }
        }

        void stopIfBlocked()
        {
            Vector2 lastVelocity = position - oldPos;

            if (lastVelocity.X == 0)
            {
                velocity *= Vector2.UnitY;
            }
            if (lastVelocity.Y == 0)
            {
                velocity *= Vector2.UnitX;
            }
        }

        private void checkGround()
        {
            Rectangle pixelLower = textureRectangle;
            pixelLower.Offset(0, 1);

            if (CollisionManager.checkCollision(pixelLower))
            {
                grounded = true;
            }
            else
            {
                grounded = false;
            }
        }

        private Rectangle createRectAtPos(Vector2 pos, int width, int height)
        {
            return new Rectangle((int)pos.X, (int)pos.Y, width, height);
        }

        private Vector2 howLongToMove(Vector2 originalPos, Vector2 destination, Rectangle bounds)
        {
            Vector2 movementToTry = destination - originalPos;
            Vector2 furthestAvailableLocationSoFar = originalPos;

            int numberOfStepsToBreakMovementInto = (int)(movementToTry.Length() * 2) + 1;
            Vector2 oneStep = movementToTry / numberOfStepsToBreakMovementInto;

            for (int i = 1; i <= numberOfStepsToBreakMovementInto; i++)
            {
                Vector2 positionToTry = originalPos + oneStep * i;
                Rectangle newBoundary = createRectAtPos(positionToTry, bounds.Width, bounds.Height);

                if (CollisionManager.checkCollision(newBoundary))
                {
                    furthestAvailableLocationSoFar = positionToTry;
                }
                else
                {
                    bool isDiagonalMove = movementToTry.X != 0 && movementToTry.Y != 0;
                    if (isDiagonalMove)
                    {
                        int stepsLeft = numberOfStepsToBreakMovementInto - (i - 1);

                        Vector2 remainingHorizontalMovement = oneStep.X * Vector2.UnitX * stepsLeft;
                        Vector2 finalPositionIfMovingHorizontally = furthestAvailableLocationSoFar + remainingHorizontalMovement;
                        furthestAvailableLocationSoFar =
                            howLongToMove(furthestAvailableLocationSoFar, finalPositionIfMovingHorizontally, bounds);

                        Vector2 remainingVerticalMovement = oneStep.Y * Vector2.UnitY * stepsLeft;
                        Vector2 finalPositionIfMovingVertically = furthestAvailableLocationSoFar + remainingVerticalMovement;
                        furthestAvailableLocationSoFar =
                            howLongToMove(furthestAvailableLocationSoFar, finalPositionIfMovingVertically, bounds);
                    }

                    break;
                }
            }

            return furthestAvailableLocationSoFar;
        }
    }
}
