using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Opinnaytetyo
{
    class MainMenu
    {
        public static bool initialized = false;

        private Background background;
        private PlayerButton playButton;
        private PlayerButton exitButton;
        private Player player;
        private Ground ground;

        public static ArrayList collidables;

        public MainMenu()
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
            background.init(Loading.backgroundImage1, new Vector2(0, 0));
            playButton.init(Loading.playButtonImage, new Vector2(210, 280), "play");
            exitButton.init(Loading.exitButtonImage, new Vector2(480, 280), "exit");
            player.init(Loading.playerImage, new Vector2(0, 0));

            initialized = true;
        }

        public void update(GameTime gameTime)
        {
            playButton.update(gameTime);
            exitButton.update(gameTime);
            player.update(gameTime);

            // CollisionManager.checkButtonCollision(player, exitButton);
            // CollisionManager.checkButtonCollision(player, playButton);


            if (player.colPlay)
            {
                if (InputManager.isKeyJustDown(Keys.S) || InputManager.isKeyJustDown(Keys.Down))
                {
                    MainGame.currentState = MainGame.state.PLAY;
                }
            }

            if (player.colExit)
            {
                if (InputManager.isKeyJustDown(Keys.S) || InputManager.isKeyJustDown(Keys.Down))
                {
                    MainGame.currentState = MainGame.state.EXIT;
                }
            }
        }

        public void render(GameTime gameTime, SpriteBatch batch)
        {
            background.render(batch);
            playButton.render(batch);
            exitButton.render(batch);
            player.render(batch);
        }
    }
}
