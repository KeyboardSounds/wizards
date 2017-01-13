using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Wizards
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MainGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        TileMap myMap;
        Player player;
        ProjectileManager projectileManager;

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
            // Initialise ProjectileManager
            projectileManager = ProjectileManager.getInstance();

            // Create player
            player = new Player(new Point(0, 0), "wizzar sprite", 128, 128, 16, projectileManager);
            player.velocity.speed = 5;

            IsMouseVisible = true;

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
            myMap = new TileMap();
            myMap.TileSetTexture = Content.Load<Texture2D>(@"tilesettest");
            player.tex = Content.Load<Texture2D>(player.texFileName);
            player.projectileTex = Content.Load<Texture2D>(player.projectileTextureFilename);
            graphics.PreferredBackBufferHeight = myMap.MapHeight * myMap.TileSize;
            graphics.PreferredBackBufferWidth = myMap.MapWidth * myMap.TileSize;
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

            player.Update(gameTime, GraphicsDevice.Viewport);
            projectileManager.UpdateProjectiles(gameTime, GraphicsDevice.Viewport);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend); //TODO if set to front to back, player disappears upon shooting
            myMap.Draw(spriteBatch);
            player.Draw(spriteBatch);
            projectileManager.DrawProjectiles(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
