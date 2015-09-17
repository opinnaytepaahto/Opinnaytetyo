using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Opinnaytetyo
{
    class Enemy : Entity
    {
        MainGame.enemyClass enemyClass;

        public int health;

        private float speed;
        private float friction;

        private float shootTimer = 1.5f;

        public bool hit;
        public bool needsKill;

        private SpriteEffects flipEffect;
        private bool flipped;

        private Vector2 oldPos;

        public static List<Projectile> enemyBullets;

        public Enemy(Texture2D texture, Vector2 position, MainGame.enemyClass enemyClass)
        {
            this.Texture = texture;
            this.Position = position;

            enemyBullets = new List<Projectile>();

            this.Hitbox = texture.Bounds;

            gravity = 1.3f;
            speed = 0.25f;
            friction = 0.2f;

            hit = false;
            needsKill = false;

            this.enemyClass = enemyClass;

            flipEffect = SpriteEffects.FlipHorizontally;
            flipped = false;
        }

        public void init()
        {
            switch (enemyClass)
            {
                case MainGame.enemyClass.NORMAL:
                    health = 100;
                    break;

                case MainGame.enemyClass.MAGE:
                    health = 50;
                    break;
            }
        }

        public override void update(GameTime gameTime)
        {
            base.update(gameTime);

            switch (enemyClass)
            {
                case MainGame.enemyClass.NORMAL:
                    shootSoldier();
                    break;
                case MainGame.enemyClass.MAGE:
                    shootMagic();
                    break;
            }

            shootTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (hit)
            {
                health -= 50;

                if (health <= 0)
                {
                    needsKill = true;
                }

                hit = false;
            }

            if (Player.playerPosStatic.X < Position.X)
            {
                flipped = true;
                Velocity -= Vector2.UnitX * speed;
            }

            else
            {
                flipped = false;
                Velocity += Vector2.UnitX * speed;
            }

            for (int i = 0; i < enemyBullets.Count; i++)
            {
                enemyBullets[i].update(gameTime);
            }

            applyFrictionAndGravity();
            moveIfPossible();
            stopIfBlocked();
        }

        public override void render(SpriteBatch batch)
        {
            if (flipped)
            {
                batch.Draw(Texture, Position, null, Color.White, 0.0f, new Vector2(0.0f, 0.0f), 1.0f, flipEffect, 0.0f);
            }
            else
            {
                batch.Draw(Texture, Position, null, Color.White);
            }

            for (int i = 0; i < enemyBullets.Count; i++)
            {
                enemyBullets[i].render(batch);
            }
        }

        private void shootSoldier()
        {
            if (shootTimer <= 0 && Player.playerRectangleStatic.Bottom > Hitbox.Top && Player.playerRectangleStatic.Top < Hitbox.Bottom)
            {
                shootTimer = 1.5f;
                if (flipped)
                {
                    enemyBullets.Add(new Projectile(Loading.soldierBulletImage, new Vector2(Position.X - 10, Position.Y + 20), flipped, "soldier", 10.0f));
                }
                else
                {
                    enemyBullets.Add(new Projectile(Loading.soldierBulletImage, new Vector2(Position.X + 15, Position.Y + 20), flipped, "soldier", 10.0f));
                }
            }
        }

        private void shootMagic()
        {
            if (shootTimer <= 0 && Player.playerRectangleStatic.Bottom > Hitbox.Top && Player.playerRectangleStatic.Top < Hitbox.Bottom)
            {
                shootTimer = 2.0f;

                if (flipped)
                {
                    enemyBullets.Add(new Projectile(Loading.fireballImage, new Vector2(Position.X - 1, Position.Y + 10), flipped, "magic", 4.0f));
                }
                else
                {
                    enemyBullets.Add(new Projectile(Loading.fireballImage, new Vector2(Position.X + 15, Position.Y + 10), flipped, "magic", 4.0f));
                }
            }
        }


        private void applyFrictionAndGravity()
        {
            // Gravity
            Velocity += new Vector2(0, gravity);

            // Friciton
            Velocity -= Velocity * Vector2.One * friction;
        }

        private void moveIfPossible()
        {
            oldPos = Position;
            Position += Velocity * (float)gameTime.ElapsedGameTime.TotalMilliseconds / 15;

            Position = howLongToMove(oldPos, Position, Hitbox);
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
