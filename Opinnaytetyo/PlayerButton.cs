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

        public override void update(GameTime gameTime)
        {
            base.update(gameTime);


        }

        public override void render(SpriteBatch batch)
        {
            base.render(batch);


        }
    }
}
