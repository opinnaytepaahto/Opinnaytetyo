using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Opinnaytetyo
{
    class GameStage
    {
        public bool initialized;

        private Background background;

        public GameStage()
        {
            initialized = false;

            background = new Background();
        }

        public void init()
        {
            background.init(Loading.backgroundImage1, new Vector2(0, 0));

            initialized = true;
        }

        public void update(GameTime gameTime)
        {

        }

        public void render(GameTime gameTime, SpriteBatch batch)
        {
            background.render(batch);
        }
    }
}
