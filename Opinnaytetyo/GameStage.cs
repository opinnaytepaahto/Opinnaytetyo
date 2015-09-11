using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Opinnaytetyo
{
    class GameStage
    {
        public static bool initialized = false;

        private Background background;
        public static Player player;
        private Ground ground;

        private int score;

        // Temporary stuff
        private Enemy testEnm;

        public static ArrayList collidables;

        public static List<Enemy> enemies;

        public GameStage()
        {
            background = new Background();
            player = new Player();
            ground = new Ground(new Rectangle(0, 400, 1000, 10));

            collidables = new ArrayList();
            enemies = new List<Enemy>();

            score = 0;

            collidables.Add(ground);
        }

        public void init()
        {
            background.init(Loading.backgroundImage2, new Vector2(0, 0));
            player.init(Loading.spacemanImage, new Vector2(0, 340));

            // Temporary stuff
            testEnm = new Enemy(Loading.soldierImage, new Vector2(0, 0), MainGame.enemyClass.NORMAL);
            testEnm.position = new Vector2(200, 399 - testEnm.texture.Height);
            testEnm.init();

            collidables.Add(testEnm);
            enemies.Add(testEnm);

            initialized = true;
        }

        public void update(GameTime gameTime)
        {
            player.update(gameTime);

            if (enemies.Count == 0)
            {
                enemies.Add(new Enemy(Loading.soldierImage, new Vector2(0, 0), MainGame.enemyClass.NORMAL));
                enemies[0].position = new Vector2(200, 399 - testEnm.texture.Height);

                enemies[0].init();
            }

            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].update(gameTime);

                if (enemies[i].needsKill)
                {
                    enemies.RemoveAt(i);
                    score += 10;
                }
            }
        }

        public void render(GameTime gameTime, SpriteBatch batch)
        {
            if (initialized)
            {
                background.render(batch);
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
