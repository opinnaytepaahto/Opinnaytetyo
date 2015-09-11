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

        private float shootTimer = 1.5f;

        public bool hit;
        public bool needsKill;

        private SpriteEffects flipEffect;
        private bool flipped;

        public static List<Projectile> enemyBullets;

        public Enemy(Texture2D texture, Vector2 position, MainGame.enemyClass enemyClass)
        {
            this.texture = texture;
            this.position = position;

            enemyBullets = new List<Projectile>();

            this.textureRectangle = texture.Bounds;

            gravity = 0.0f;

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
                    speed = 100;
                    gravity = 1.3f;
                    break;

                case MainGame.enemyClass.MAGE:
                    health = 50;
                    speed = 100;
                    gravity = 1.3f;
                    break;
            }
        }

        public override void update(GameTime gameTime)
        {
            base.update(gameTime);

            switch(enemyClass)
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

            if (Player.playerPosStatic.X < position.X)
            {
                flipped = true;
            }
            else
            {
                flipped = false;
            }

            for (int i = 0; i < enemyBullets.Count; i++)
            {
                enemyBullets[i].update(gameTime);
            }

            Console.WriteLine("Enemy health: " + health);
            Console.WriteLine("Enemy needs kill: " + needsKill);
        }

        public override void render(SpriteBatch batch)
        {
            if (flipped)
            {
                batch.Draw(texture, position, null, Color.White, 0.0f, new Vector2(0.0f, 0.0f), 1.0f, flipEffect, 0.0f);
            }
            else
            {
                batch.Draw(texture, position, null, Color.White);
            }

            for (int i = 0; i < enemyBullets.Count; i++)
            {
                enemyBullets[i].render(batch);
            }
        }

        private void shootSoldier()
        {
            if (shootTimer <= 0 && Player.playerRectangleStatic.Bottom > textureRectangle.Top)
            {
                shootTimer = 1.5f;
                if (flipped)
                {
                    enemyBullets.Add(new Projectile(Loading.soldierBulletImage, new Vector2(position.X - 10, position.Y + 20), flipped, "soldier"));
                }
                else
                {
                    enemyBullets.Add(new Projectile(Loading.soldierBulletImage, new Vector2(position.X + 15, position.Y + 20), flipped, "soldier"));
                }
            }
        }

        private void shootMagic()
        {
            if (shootTimer <= 0 && Player.playerRectangleStatic.Bottom > textureRectangle.Top)
            {
                shootTimer = 2.0f;
                if (flipped)
                {
                    enemyBullets.Add(new Projectile(Loading.fireballImage, new Vector2(position.X - 10, position.Y + 20), flipped, "magic"));
                }
                else
                {
                    enemyBullets.Add(new Projectile(Loading.fireballImage, new Vector2(position.X + 15, position.Y + 20), flipped, "magic"));
                }
            }
        }
    }
}
