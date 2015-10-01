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
        private Level2 level1;
        private Level2 level2;

        public static int score;

        public enum Level
        {
            LEVEL1,
            LEVEL2
        }

        public static Level CurrentLevel { get; set; }

        public GameStage()
        {
            switch(CurrentLevel)
            {
                case Level.LEVEL1:
                    level1 = new Level2();
                    break;
                case Level.LEVEL2:
                    level2 = new Level2();
                    break;
            }
        }

        public void init()
        {
            switch (CurrentLevel)
            {
                case Level.LEVEL1:
                    level1.init();
                    break;
                case Level.LEVEL2:
                    level2 = new Level2();
                    level2.init();
                    break;
            }
        }
       
        public void update(GameTime gameTime)
        {
            switch (CurrentLevel)
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
            switch (CurrentLevel)
            {
                case Level.LEVEL1:
                    level1.render(gameTime, batch);
                    break;
                case Level.LEVEL2:
                    if (level2 == null)
                    {
                        init();
                    }
                    level2.render(gameTime, batch);
                    break;
            }
        }
    }
}
