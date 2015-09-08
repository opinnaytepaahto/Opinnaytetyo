using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        private Player player;
        private Ground ground;

        // Temporary stuff
        private Enemy testEnm;

        public static ArrayList collidables;

        public static List<Enemy> enemies;

        public GameStage()
        {
            background = new Background();
            player = new Player();
            ground = new Ground(new Rectangle(0, 345, 1000, 10));

            collidables = new ArrayList();
            enemies = new List<Enemy>();

            collidables.Add(ground);
        }

        public void init()
        {
            background.init(Loading.backgroundImage1, new Vector2(0, 0));
            player.init(Loading.spacemanImage, new Vector2(0, 270));

            // Temporary stuff
            testEnm = new Enemy(Loading.soldierImage, new Vector2(200, 200), MainGame.enemyClass.NORMAL);
            testEnm.init();

            collidables.Add(testEnm);
            enemies.Add(testEnm);

            initialized = true;
        }

        public void update(GameTime gameTime)
        {
            player.update(gameTime);

            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].update(gameTime);

                if (enemies[i].needsKill)
                {
                    enemies.RemoveAt(i);
                    collidables.RemoveAt(i + 1);
                }
            }
        }

        public void render(GameTime gameTime, SpriteBatch batch)
        {
            if (initialized)
            {
                background.render(batch);
                player.render(batch);

                for (int i = 0; i < enemies.Count; i++)
                {
                    enemies[i].render(batch);
                }
            }
        }
    }
}
