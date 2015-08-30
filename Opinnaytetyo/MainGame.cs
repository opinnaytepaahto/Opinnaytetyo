﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        private ArrayList entities = new ArrayList();

        //Global static variables
        public static float windowWidth;
        public static float windowHeight;

        //Entities
        private Texture2D playerTexture;
        private Player player;

        private Texture2D backgroundImage;
        private Background background;

        private Texture2D platformImage;
        private Platform platform;

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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

            // Create all entities
            playerTexture = Content.Load<Texture2D>("player.png");
            player = new Player();
            player.init(playerTexture, new Vector2(0.0f, windowHeight - player.getWidth()));

            backgroundImage = Content.Load<Texture2D>("palmuict.png");
            background = new Background();
            background.init(backgroundImage, new Vector2(0.0f, 0.0f));

            platformImage = Content.Load<Texture2D>("platform.png");
            platform = new Platform();
            platform.init(platformImage, new Vector2(70.0f, 450.0f));

            // Add all entities to entity list
            entities.Add(player);
            entities.Add(background);

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
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
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

            // Update all entities
            foreach(Entity e in entities)
            {
                e.update(gameTime);
            }

            player.update();
            platform.update();

            CollisionManager.checkCollision(player, platform);

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

            // Render all entities
            foreach(Entity e in entities)
            {
                e.render(spriteBatch);
            }

            background.render(spriteBatch);
            player.render(spriteBatch);
            platform.render(spriteBatch);

            // End drawing
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
