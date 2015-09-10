using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Opinnaytetyo
{
    class Player : Entity
    {
        private float speed;
        private float friction;
        private float jCooldown;
        private float sCooldown;

        public static Vector2 playerPosStatic;
        public static Rectangle playerRectangleStatic;

        public int health;
        public bool hit;

        private SpriteEffects flipEffect;
        private bool flipped;

        private Vector2 oldPos;

        private List<Projectile> bullets;

        public bool collide = true;
    
        public void init(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;

            textureRectangle = texture.Bounds;

            health = 500;
            speed = 0.5f;
            friction = 0.15f;
            gravity = 1.3f;
            jCooldown = 0.0f;
            sCooldown = 0.0f;

            velocity = new Vector2(0.0f, 0.0f);
            bullets = new List<Projectile>();

            flipEffect = SpriteEffects.FlipHorizontally;
            flipped = false;
        }

        public override void update(GameTime gameTime)
        {
            base.update(gameTime);

            jCooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            sCooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            CollisionManager.bulletCollision(bullets);
            CollisionManager.enemyBulletCollision(Enemy.enemyBullets);

            handleMovement();
            applyFrictionAndGravity();
            keepOnScreen();
            moveIfPossible();
            stopIfBlocked();
            enemygravity();

            Console.WriteLine("Velocity: " + velocity.X + ", " + velocity.Y);

            if (jCooldown <= 0)
            {
                jCooldown = 0;
            }

            if (velocity.X >= 100)
            {
                velocity.X = 100;
            }

            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].update(gameTime);

                if (bullets[i].timer <= 0)
                {
                    bullets.RemoveAt(i);
                }
            }

            if (hit)
            {
                health -= 100;

                hit = false;
            }

            if (health <= 0)
            {
                MainGame.currentState = MainGame.state.EXIT;
            }

            playerPosStatic = position;
            playerRectangleStatic = textureRectangle;
        }

        private void enemygravity()
        {
            //throw new NotImplementedException();
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

            foreach (Projectile p in bullets)
            {
                p.render(batch);
            }
        }

        private void moveIfPossible()
        {
            oldPos = position;
            position += velocity * (float)gameTime.ElapsedGameTime.TotalMilliseconds / 15;

            if (collide)
            {
                position = howLongToMove(oldPos, position, textureRectangle);
            }
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
            if ((InputManager.isKeyJustDown(Keys.W) || InputManager.isKeyJustDown(Keys.Up)) && jCooldown <= 0)
            {
                jCooldown = 0.5f;
                velocity -= Vector2.UnitY * 30.0f;
            }
            if ((InputManager.isKeyDown(Keys.S) || InputManager.isKeyDown(Keys.Down)) && textureRectangle.Bottom <= MainMenu.ground.textureRectangle.Top - 1)
            {
                collide = false;
            }
            else
            {
                collide = true;
            }

            if ((InputManager.isKeyDown(Keys.Space)) && sCooldown <= 0)
            {
                sCooldown = 0.7f;
                if (flipped)
                {
                    bullets.Add(new Projectile(Loading.bulletImage, new Vector2(position.X - 10, position.Y + 20), flipped));
                }
                else
                {
                    bullets.Add(new Projectile(Loading.bulletImage, new Vector2(position.X + 15, position.Y + 20), flipped));
                }
            }
        }

        private void applyFrictionAndGravity()
        {

            // Gravity
            velocity.Y += gravity;

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
