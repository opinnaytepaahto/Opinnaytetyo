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
            this.Texture = texture;
            this.Position = position;
            this.id = id;
        }

        public void update(GameTime gameTime, Rectangle bounds)
        {
            base.update(gameTime);

            if (Hitbox.Intersects(bounds))
            {
                switch(id)
                {
                    case "play":
                        MainGame.currentState = MainGame.state.PLAY;
                        break;
                    case "exit":
                        MainGame.currentState = MainGame.state.EXIT;
                        break;
                    case "NEXT":
                        GameStage.currentLevel = GameStage.Level.LEVEL2;
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
