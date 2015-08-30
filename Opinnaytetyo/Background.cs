using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Opinnaytetyo
{
    class Background : Entity
    {
        private Vector2 scale;

        private float backImgWidth;
        private float backImgHeight;

        public void init(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;

            this.textureRectangle = texture.Bounds;

            backImgWidth = 2450.0f;
            backImgHeight = 1440.0f;

            scale = new Vector2(MainGame.windowWidth / backImgWidth, MainGame.windowHeight / backImgHeight);
            Console.WriteLine(MainGame.windowWidth / backImgWidth);
            Console.WriteLine(scale.X + ", " + scale.Y);
        }

        public override void render(SpriteBatch spriteBatch)
        {
            this.batch = spriteBatch;

            batch.Draw(texture, position, textureRectangle, Color.White, 0.0f, new Vector2(0.0f, 0.0f), scale, SpriteEffects.None, 0.0f);
        }
    }
}
