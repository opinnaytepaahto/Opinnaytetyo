using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Opinnaytetyo
{
    class PlayerButton : Entity
    {
        public String id;

        public void init(Texture2D texture, Vector2 position, String id)
        {
            this.texture = texture;
            this.position = position;
            this.id = id;
        }

        public void update(GameTime gameTime, Rectangle bounds)
        {
            base.update(gameTime);

            if (textureRectangle.Intersects(bounds))
            {
                switch(id)
                {
                    case "play":
                        MainGame.currentState = MainGame.state.PLAY;
                        break;
                    case "exit":
                        MainGame.currentState = MainGame.state.EXIT;
                        break;
                }
            }
        }

        public override void render(SpriteBatch batch)
        {
            base.render(batch);
        }
    }
}
