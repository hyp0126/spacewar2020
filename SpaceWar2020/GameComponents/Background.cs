/* 
 * Background.cs
 * Final Project: SpaceWar2020
 *                Game background component
 * Revision History:
 *      Jiyoung Jung, 2020.11.14: Version 1.0
 *      
*/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWar2020
{
    /// <summary>
    /// Game background
    /// </summary>
    public class Background : DrawableGameComponent
    {
        /// <summary>
        /// Volume for Background Music
        /// </summary>
        const float MUSIC_VOLUME = 0.1f;

        Texture2D fullBackTexture;
        Texture2D tileTexture;

        /// <summary>
        /// Music for playing a game
        /// </summary>
        Song backgroundMusic;

        /// <summary>
        /// If true, display Full background
        /// If false, display Tile background
        /// </summary>
        bool drawfullBackground = true;

        /// <summary>
        /// Previous keyboard state for checking a key is pressed
        /// </summary>
        KeyboardState prevKS = Keyboard.GetState();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">Game Entity</param>
        public Background(Game game)
            : base(game)
        {
        }

        protected override void LoadContent()
        {
            fullBackTexture = Game.Content.Load<Texture2D>(@"Background\space-1");
            tileTexture = Game.Content.Load<Texture2D>(@"Background\space-2");
            backgroundMusic = Game.Content.Load<Song>(@"Sound\BackgroundMusic");

            // Set MediaPlayer for playing background music, and play music
            MediaPlayer.Volume = MUSIC_VOLUME;
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(backgroundMusic);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            // Ctrl + B : Change background (Full <-> Tile) 
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.B) && prevKS.IsKeyUp(Keys.B) 
                && (ks.IsKeyDown(Keys.LeftControl) || ks.IsKeyDown(Keys.RightControl)))
            {
                drawfullBackground = !drawfullBackground;
            }
            prevKS = ks;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();

            sb.Begin();
            // Check Full Background or Tile Background
            if (drawfullBackground)
            {
                // Display Full Background
                sb.Draw(fullBackTexture, new Rectangle(0, 0, Game.GraphicsDevice.Viewport.Width,
                    Game.GraphicsDevice.Viewport.Height),
                    Color.White);
            }
            else
            {
                // Display Tile Background
                // Calculate the number of tiles required
                int rows = Game.GraphicsDevice.Viewport.Height / tileTexture.Height + 1;
                int cols = Game.GraphicsDevice.Viewport.Width / tileTexture.Width + 1;

                // Display Tiles
                for (int row = 0; row < rows; row++)
                {
                    for (int col = 0; col < cols; col++)
                    {
                        sb.Draw(tileTexture, new Vector2(tileTexture.Width * col, tileTexture.Height * row), Color.White);
                    }
                }
            }
            sb.End();

            base.Draw(gameTime);
        }
    }
}
