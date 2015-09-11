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
        private Level1 level1;
        private Level2 level2;

        public enum Level
        {
            LEVEL1,
            LEVEL2
        }

        public static Level currentLevel;

        public GameStage()
        {
            switch(currentLevel)
            {
                case Level.LEVEL1:
                    level1 = new Level1();
                    break;
                case Level.LEVEL2:
                    level2 = new Level2();
                    break;
            }
        }

        public void init()
        {
            switch (currentLevel)
            {
                case Level.LEVEL1:
                    level1.init();
                    break;
                case Level.LEVEL2:
                    level2.init();
                    break;
            }
        }
       
        public void update(GameTime gameTime)
        {
            switch (currentLevel)
            {
                case Level.LEVEL1:
                    level1.update(gameTime);
                    break;
                case Level.LEVEL2:
                    level2.update(gameTime);
                    break;
            }
        }

        public void render(GameTime gameTime, SpriteBatch batch)
        {
            switch (currentLevel)
            {
                case Level.LEVEL1:
                    level1.render(gameTime, batch);
                    break;
                case Level.LEVEL2:
                    level2.render(gameTime, batch);
                    break;
            }
        }
    }
}
