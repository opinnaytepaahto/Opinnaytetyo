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

        public enum HitType
        {
            SOLDIER,
            MAGIC,
            REAPER
        }

        public HitType currentHit;

        private SpriteEffects flipEffect;
        private bool flipped;

        private Vector2 oldPos;

        private List<Projectile> bullets;

        public bool collide = true;
    
        public void init(Texture2D texture, Vector2 position)
        {
            this.Texture = texture;
            this.Position = position;

            Hitbox = texture.Bounds;

            health = 500;
            speed = 0.5f;
            friction = 0.15f;
            gravity = 1.3f;
            jCooldown = 0.0f;
            sCooldown = 0.0f;

            Velocity = new Vector2(0.0f, 0.0f);
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

            if (jCooldown <= 0)
            {
                jCooldown = 0;
            }

            if (Velocity.X >= 100)
            {
                Velocity = new Vector2(100, 0);
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
               switch(currentHit)
                {
                    case HitType.SOLDIER:
                        health -= 100;
                        break;
                    case HitType.MAGIC:
                        health -= 250;
                        break;
                    case HitType.REAPER:
                        health -= 500;
                        break;
                }

                hit = false;
            }

            if (health <= 0)
            {
                MainGame.currentState = MainGame.state.EXIT;
            }

            playerPosStatic = Position;
            playerRectangleStatic = Hitbox;
        }

        public override void render(SpriteBatch batch)
        {
            this.batch = batch;

            if (flipped)
            {
                batch.Draw(Texture, Position, null, Color.White, 0.0f, new Vector2(0.0f, 0.0f), 1.0f, flipEffect, 0.0f);
            }
            else
            {
                batch.Draw(Texture, Position, null, Color.White);
            }

            foreach (Projectile p in bullets)
            {
                p.render(batch);
            }
        }

        private void moveIfPossible()
        {
            oldPos = Position;
            Position += Velocity * (float)gameTime.ElapsedGameTime.TotalMilliseconds / 15;

            if (collide)
            {
                Position = howLongToMove(oldPos, Position, Hitbox);
            }
        }

        private void handleMovement()
        {
            if (InputManager.isKeyDown(Keys.A) || InputManager.isKeyDown(Keys.Left))
            {
                Velocity -= Vector2.UnitX * speed;
                flipped = true;
            }
            if (InputManager.isKeyDown(Keys.D) || InputManager.isKeyDown(Keys.Right))
            {
                Velocity += Vector2.UnitX * speed;
                flipped = false;
            }
            if ((InputManager.isKeyJustDown(Keys.W) || InputManager.isKeyJustDown(Keys.Up)) && jCooldown <= 0)
            {
                jCooldown = 0.5f;
                Loading.jumpSound.Play();
                Velocity -= Vector2.UnitY * 30.0f;
            }
            if ((InputManager.isKeyDown(Keys.S) || InputManager.isKeyDown(Keys.Down)) && Hitbox.Bottom <= MainMenu.ground.Hitbox.Top - 1 )
            {
                collide = false;
            }
            else
            {
                collide = true;
            }

            if ((InputManager.isKeyDown(Keys.Space)) && sCooldown <= 0)
            {
                Loading.shootSound.Play();
                sCooldown = 0.3f;
                if (flipped)
                {
                    bullets.Add(new Projectile(Loading.bulletImage, new Vector2(Position.X - 23, Position.Y + 20), flipped, "kuti", 10.0f, 1.5f));
                }
                else
                {
                    bullets.Add(new Projectile(Loading.bulletImage, new Vector2(Position.X + 23, Position.Y + 20), flipped, "kuti", 10.0f, 1.5f));
                }
            }

            if (InputManager.isKeyDown(Keys.Escape))
            {
                Loading.levelChangeSound.Play();
            }
        }

        private void applyFrictionAndGravity()
        {

            // Gravity
            Velocity += new Vector2(0, gravity);

            // Friciton
            Velocity -= Velocity * Vector2.One * friction;
        }

        private void keepOnScreen()
        {
            if (Position.X < 0)
            {
                Velocity =  new Vector2(0, 0);
                Position = new Vector2(0, Position.Y);
            }
            if (Position.X > MainGame.windowWidth - Hitbox.Width)
            {
                Velocity = new Vector2(0, Position.Y);
                Position = new Vector2(MainGame.windowWidth - Hitbox.Width, 0);
            }
        }

        void stopIfBlocked()
        {
            Vector2 lastVelocity = Position - oldPos;

            if (lastVelocity.X == 0)
            {
                Velocity *= Vector2.UnitY;
            }
            if (lastVelocity.Y == 0)
            {
                Velocity *= Vector2.UnitX;
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
