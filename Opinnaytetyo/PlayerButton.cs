using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

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
                        Loading.levelChangeSound.Play();
                        Thread.Sleep(1500);
                        MainGame.currentState = MainGame.state.PLAY;
                        Loading.backgroundMusic1.Play();
                        break;
                    case "exit":
                        Loading.levelChangeSound.Play();
                        Thread.Sleep(1500);
                        MainGame.currentState = MainGame.state.EXIT;
                        break;
                    case "NEXT":
                        Loading.levelChangeSound.Play();
                        Thread.Sleep(1500);
                        GameStage.CurrentLevel = GameStage.Level.LEVEL2;
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
