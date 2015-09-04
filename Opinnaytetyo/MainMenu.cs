using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Opinnaytetyo
{
    class MainMenu
    {
        public static bool initialized = false;

        private Background background;
        private Player player;
        private PlayerButton playButton;
        private PlayerButton exitButton;

        public MainMenu()
        {
            background = new Background();
            player = new Player();
            playButton = new PlayerButton();
            exitButton = new PlayerButton();
        }

        public void init()
        {
            background.init(Loading.backgroundImage1, new Vector2(0, 0));
            player.init(Loading.playerImage, new Vector2(0, 0));
            playButton.init(Loading.playButtonImage, new Vector2(210, 280), "play");
            exitButton.init(Loading.exitButtonImage, new Vector2(480, 280), "exit");

            initialized = true;
        }

        public void update(GameTime gameTime)
        {
            player.update(gameTime);
            playButton.update(gameTime);
            exitButton.update(gameTime);

            CollisionManager.checkButtonCollision(player, exitButton);
            CollisionManager.checkButtonCollision(player, playButton);


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
            player.render(batch);
            playButton.render(batch);
            exitButton.render(batch);
        }
    }
}
