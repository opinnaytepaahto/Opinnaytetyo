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

        public MainMenu()
        {
            background = new Background();
            player = new Player();
            playButton = new PlayerButton();
        }

        public void init()
        {
            background.init(Loading.backgroundImage1, new Vector2(0, 0));
            player.init(Loading.playerImage, new Vector2(0, 0));
            playButton.init(Loading.playButtonImage, new Vector2(100, 275));

            initialized = true;
        }

        public void update(GameTime gameTime)
        {
            player.update(gameTime);
            playButton.update(gameTime);

            CollisionManager.checkCollision(player, playButton);

            if (player.colliding)
            {
                if (InputManager.isKeyJustDown(Keys.E))
                {
                    MainGame.currentState = MainGame.state.PLAY;
                }
            }
        }

        public void render(GameTime gameTime, SpriteBatch batch)
        {
            background.render(batch);
            player.render(batch);
            playButton.render(batch);
        }
    }
}
