using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opinnaytetyo
{
    class Level2
    {
        public static bool initialized = false;

        private Background background;
        public static Player player;
        private Ground ground;
        private PlayerButton nextButton;
        private Platform platform;
        private Platform platform1;
        private Platform platform2;
        private Platform platform3;
        private Platform platform4;
        private Platform platform5;

        private int score;

        // Temporary stuff
        private Enemy testEnm;
        private Enemy mageEnm;

        public static ArrayList collidables;

        public static List<Enemy> enemies;

        public Level2()
        {
            background = new Background();
            player = new Player();
            ground = new Ground(new Rectangle(0, 400, 1000, 10));
            nextButton = new PlayerButton();
            platform = new Platform();
            platform1 = new Platform();
            platform2 = new Platform();
            platform3 = new Platform();
            platform4 = new Platform();
            platform5 = new Platform();

            collidables = new ArrayList();
            enemies = new List<Enemy>();

            score = 0;
        }

        public void init()
        {
            background.init(Loading.backgroundImage2, new Vector2(0, 0));
            player.init(Loading.spacemanImage, new Vector2(0, 340));
            nextButton.init(Loading.nextImage, new Vector2(100, 200), "NEXT");
            platform.init(Loading.platformImage, new Vector2(525, 300));
            platform1.init(Loading.platformImage, new Vector2(375, 250));
            platform2.init(Loading.platformImage, new Vector2(450, 150));
            platform3.init(Loading.platformImage, new Vector2(150, 100));
            platform4.init(Loading.platformImage, new Vector2(250, 250));
            platform5.init(Loading.platformImage, new Vector2(325, 100));

            // Temporary stuff
            testEnm = new Enemy(Loading.soldierImage, new Vector2(0, 0), MainGame.enemyClass.NORMAL);
            testEnm.Position = new Vector2(200, 399 - testEnm.Texture.Height);
            testEnm.init();

            mageEnm = new Enemy(Loading.wizardImage, new Vector2(0, 0), MainGame.enemyClass.MAGE);
            mageEnm.Position = new Vector2(449, 149 - mageEnm.Texture.Height);
            mageEnm.init();

            enemies.Add(testEnm);
            enemies.Add(mageEnm);

            collidables.Add(testEnm);
            collidables.Add(mageEnm);
            collidables.Add(ground);
            collidables.Add(nextButton);
            collidables.Add(platform);
            collidables.Add(platform1);
            collidables.Add(platform2);
            collidables.Add(platform3);
            collidables.Add(platform4);
            collidables.Add(platform5);

            initialized = true;
        }

        public void update(GameTime gameTime)
        {
            nextButton.update(gameTime, player.Hitbox);
            platform.update(gameTime);
            platform1.update(gameTime);
            platform2.update(gameTime);
            platform3.update(gameTime);
            platform4.update(gameTime);
            platform5.update(gameTime);
            player.update(gameTime);

            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].update(gameTime);

                if (enemies[i].needsKill)
                {
                    enemies.RemoveAt(i);
                    collidables.RemoveAt(i);
                    score += 10;
                }
            }
        }

        public void render(GameTime gameTime, SpriteBatch batch)
        {
            if (initialized)
            {
                background.render(batch);
                nextButton.render(batch);
                platform.render(batch);
                platform1.render(batch);
                platform2.render(batch);
                platform3.render(batch);
                platform4.render(batch);
                platform5.render(batch);
                player.render(batch);

                batch.DrawString(Loading.mainFont, "SCORE: " + score, new Vector2(650, 70), Color.White);
                batch.DrawString(Loading.mainFont, "HEALTH: " + player.health, new Vector2(650, 30), Color.White);

                for (int i = 0; i < enemies.Count; i++)
                {
                    enemies[i].render(batch);
                }
            }
        }
    }
}
