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

        public bool hit;
        public bool needsKill;

        private SpriteEffects flipEffect;
        private bool flipped;

        public Enemy(Texture2D texture, Vector2 position, MainGame.enemyClass enemyClass)
        {
            this.texture = texture;
            this.position = position;

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
            }
        }

        public override void update(GameTime gameTime)
        {
            base.update(gameTime);

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
        }
    }
}
