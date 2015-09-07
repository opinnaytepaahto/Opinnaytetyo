using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Opinnaytetyo
{
    class Loading
    {
        // Private variables
        private ContentManager content;
        private volatile bool loading;
        private volatile bool loadingComplete;

        // Public variables
        public static Texture2D playerImage;
        public static Texture2D spacemanImage;
        public static Texture2D armyImage;
        public static Texture2D backgroundImage1;
        public static Texture2D fireballImage;
        public static Texture2D banditImage;
        public static Texture2D playButtonImage;
        public static Texture2D exitButtonImage;

        public static Texture2D loading1Tex;
        public static Background loading1;

        public static Texture2D loading2Tex;
        public static Background loading2;

        public static Texture2D loading3Tex;
        public static Background loading3;

        public static Texture2D loading4Tex;
        public static Background loading4;

        public Loading(ContentManager content)
        {
            this.content = content;

            // Create objects
            loading1 = new Background();
            loading2 = new Background();
            loading3 = new Background();
            loading4 = new Background();
        }

        public void init()
        {
            loading = true;
            loadingComplete = false;
        }

        public void loadContent()
        {
            Task.Factory.StartNew(() => { loadContentThread(); });
        }

        void loadContentThread()
        {
            loading1Tex = content.Load<Texture2D>("LOADING0.png");
            loading1.init(loading1Tex, new Vector2(0, 0));

            loading2Tex = content.Load<Texture2D>("LOADING1.png");
            loading2.init(loading1Tex, new Vector2(0, 0));

            loading3Tex = content.Load<Texture2D>("LOADING2.png");
            loading3.init(loading1Tex, new Vector2(0, 0));

            loading4Tex = content.Load<Texture2D>("LOADING3.png");
            loading4.init(loading1Tex, new Vector2(0, 0));;

            loading = false;

            playerImage = content.Load<Texture2D>("player.png");
            spacemanImage = content.Load<Texture2D>("Spaceman.png");
            armyImage = content.Load<Texture2D>("armyman.png");
            backgroundImage1 = content.Load<Texture2D>("palmuict.png");
            fireballImage = content.Load<Texture2D>("fireball.png");
            banditImage = content.Load<Texture2D>("bandit.png");
            playButtonImage = content.Load<Texture2D>("Play.png");
            exitButtonImage = content.Load<Texture2D>("Exit.png");

            // Thread.Sleep(2500);

            loadingComplete = true;
        }

        public void unloadContent()
        {
            loading1Tex.Dispose();
            loading2Tex.Dispose();
            loading3Tex.Dispose();
            loading4Tex.Dispose();

            playerImage.Dispose();
            backgroundImage1.Dispose();
            loading1Tex.Dispose();
            fireballImage.Dispose();
            banditImage.Dispose();
        }

        public void update(GameTime gameTime)
        {

        }

        public void render(GameTime gameTime, SpriteBatch batch)
        {
            if (!loading)
            {
                loading1.render(batch);
            }
            
            if (loadingComplete)
            {
                MainGame.currentState = MainGame.state.MENU;
            }
        }
    }
}
