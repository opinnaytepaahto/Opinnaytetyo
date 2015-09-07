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

        public static ArrayList collidables;

        public GameStage()
        {
            background = new Background();
            player = new Player();
            ground = new Ground(new Rectangle(0, 345, 1000, 10));

            collidables = new ArrayList();

            collidables.Add(ground);
        }

        public void init()
        {
            background.init(Loading.backgroundImage1, new Vector2(0, 0));
            player.init(Loading.spacemanImage, new Vector2(0, 290));

            initialized = true;
        }

        public void update(GameTime gameTime)
        {
            player.update(gameTime);
        }

        public void render(GameTime gameTime, SpriteBatch batch)
        {
            if (initialized)
            {
                background.render(batch);
                player.render(batch);
            }
        }
    }
}
