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
        private int speed;

        public bool hit;
        public bool needsKill;

        public Enemy(Texture2D texture, Vector2 position, MainGame.enemyClass enemyClass)
        {
            this.texture = texture;
            this.position = position;

            this.textureRectangle = texture.Bounds;

            gravity = 0.0f;

            hit = false;
            needsKill = false;

            this.enemyClass = enemyClass;
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

            Console.WriteLine("Enemy health: " + health);
            Console.WriteLine("Enemy needs kill: " + needsKill);
        }

        public override void render(SpriteBatch batch)
        {
            base.render(batch);
        }
    }
}
