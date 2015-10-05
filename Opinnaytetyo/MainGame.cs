using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;

namespace Opinnaytetyo
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MainGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Global static variables
        public static float windowWidth;
        public static float windowHeight;

        // State stuff
        public enum state
        {
            LOADING,
            MENU,
            PLAY,
            EXIT
        }

        public enum enemyClass
        {
            NORMAL,
            ROGUE,
            TANK,
            MAGE,
            REAPER
        }

        public static state currentState;

        // State entities
        private Loading loading;
        private MainMenu mainMenu;
        private GameStage gameStage;

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Create objects
            loading = new Loading(Content);
            mainMenu = new MainMenu();
            gameStage = new GameStage();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            // Initialize all global static variables
            windowWidth = graphics.GraphicsDevice.Viewport.Width;
            windowHeight = graphics.GraphicsDevice.Viewport.Height;

            currentState = state.LOADING;
            loading.init();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            switch (currentState)
            {
                case state.LOADING:
                    loading.loadContent();
                    break;
                case state.MENU:
                    break;
                case state.PLAY:
                    break;

            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here

            switch (currentState)
            {
                case state.LOADING:
                    loading.unloadContent();
                    break;
                case state.MENU:
                    break;
                case state.PLAY:
                    break;

            }
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            InputManager.update();

            switch (currentState)
            {
                case state.LOADING:
                    loading.update(gameTime);
                    break;
                case state.MENU:
                    if (!MainMenu.initialized)
                    {
                        mainMenu.init();
                    }
                    mainMenu.update(gameTime);
                    break;
                case state.PLAY:
                    gameStage.update(gameTime);
                    break;
                case state.EXIT:
                    Exit();
                    break;

            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            // Start drawing
            spriteBatch.Begin();

            switch (currentState)
            {
                case state.LOADING:
                    loading.render(gameTime, spriteBatch);
                    break;
                case state.MENU:
                    mainMenu.render(gameTime, spriteBatch);
                    break;
                case state.PLAY:
                    gameStage.render(gameTime, spriteBatch);
                    break;

            }

            // End drawing
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
