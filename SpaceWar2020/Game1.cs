using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace SpaceWar2020
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 700;
            graphics.PreferredBackBufferHeight = 800;
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
            // Start Menu
            StartScene menuScene = new StartScene(this);
            this.Components.Add(menuScene);
            Services.AddService<StartScene>(menuScene);

            // Sub-Menu action(game), help, high score, about
            ActionScene actionScene = new ActionScene(this);
            this.Components.Add(actionScene);
            Services.AddService<ActionScene>(actionScene);

            HelpScene helpScene = new HelpScene(this);
            this.Components.Add(helpScene);
            Services.AddService<HelpScene>(helpScene);

            HighScoreScene highScoreScene = new HighScoreScene(this);
            this.Components.Add(highScoreScene);
            Services.AddService<HighScoreScene>(highScoreScene);

            AboutScene aboutScene = new AboutScene(this);
            this.Components.Add(aboutScene);
            Services.AddService<AboutScene>(aboutScene);

            base.Initialize(); 

            // Display Start Menu 
            HideAllScenes();
            menuScene.Show();
        }

        /// <summary>
        /// Get all scenes currently in our game, and hide/disable them
        /// </summary>
        public void HideAllScenes()
        {
            foreach (GameScene scene in Components.OfType<GameScene>())
            {
                scene.Hide();
            }
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Add spriteBatch to Service
            Services.AddService<SpriteBatch>(spriteBatch);
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
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }
    }
}
