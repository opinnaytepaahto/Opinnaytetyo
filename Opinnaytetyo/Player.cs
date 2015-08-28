using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Opinnaytetyo
{
    class Player : Entity
    {
        public void init()
        {
            base.init(texture, position);
        }

        public void update()
        {
            base.update(gameTime);

            if (InputManager.isKeyDown(Keys.A) || InputManager.isKeyDown(Keys.Left))
            {
                position.X -= 500.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (InputManager.isKeyDown(Keys.D) || InputManager.isKeyDown(Keys.Right))
            {
                position.X += 500.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        public void render()
        {
            base.render(batch);
        }
    }
}
