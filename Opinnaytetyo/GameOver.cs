using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opinnaytetyo
{
    class GameOver
    {
        public static bool initialized = false;

        private Background background;
        private PlayerButton playButton;
        private PlayerButton exitButton;
        private Player player;

        public static Ground ground;
        public static ArrayList collidables;

        public GameOver()
        {
            collidables = new ArrayList();

            background = new Background();
            playButton = new PlayerButton();
            exitButton = new PlayerButton();
            player = new Player();
            ground = new Ground(new Rectangle(0, 345, 1000, 10));

            collidables.Add(playButton);
            collidables.Add(exitButton);
            collidables.Add(ground);
        }

        public void init()
        {
            player.init(Loading.spacemanImage, new Vector2(0, 290));
            background.init(Loading.hellBground, new Vector2(0, 0));
            playButton.init(Loading.playButtonImage, new Vector2(210, 275), "play");
            exitButton.init(Loading.exitButtonImage, new Vector2(0, 400), "exit");
            
            initialized = true;
        }

        public void update(GameTime gameTime)
        {
            player.update(gameTime);
            playButton.update(gameTime, player.Hitbox);
            exitButton.update(gameTime, player.Hitbox);
        }

        public void render(GameTime gameTime, SpriteBatch batch)
        {
            background.render(batch);
            player.render(batch);
            playButton.render(batch);
            exitButton.render(batch);
        }
    }
}
