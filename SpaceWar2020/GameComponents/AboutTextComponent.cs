/* 
 * AboutTextComponent.cs
 * Final Project: SpaceWar2020
 *                About sub-menu component
 * Revision History:
 *      Jiyoung Jung, 2020.12.09: Version 1.0
 *      
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceWar2020
{
    /// <summary>
    /// About Sub-Menu: Display Developer Names
    /// </summary>
    class AboutTextComponent : DrawableGameComponent
    {
        /// <summary>
        /// Title of menu
        /// </summary>
        const string TITLE = "ABOUT";

        /// <summary>
        /// Developers Name
        /// </summary>
        const string DEVELOPER1 = "Hong, Yi Phyo";
        const string DEVELOPER2 = "Jung, Jiyoung";

        /// <summary>
        /// Font for diaplaying a title and developers' name 
        /// </summary>
        SpriteFont scoreFont;

        /// <summary>
        /// Menu Start Position
        /// </summary>
        private Vector2 startingPosition;

        // Default constructor
        public AboutTextComponent(Game game) : base(game)
        {
        }

        public override void Initialize()
        {

            // Assign starting position in the middle of screen
            SpriteFont font = Game.Content.Load<SpriteFont>(@"Fonts\regularFont");
            startingPosition = new Vector2(GraphicsDevice.Viewport.Width / 2 - font.MeasureString(DEVELOPER1).X / 2,
                              GraphicsDevice.Viewport.Height / 2 - 4 * font.MeasureString(DEVELOPER1).Y / 2);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Load menu font
            scoreFont = Game.Content.Load<SpriteFont>(@"Fonts\regularFont");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            // ESC key: exit menu
            if (ks.IsKeyDown(Keys.Escape))
            {
                ((Game1)Game).HideAllScenes();
                Game.Services.GetService<StartScene>().Show();
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();

            Vector2 nextPosition = startingPosition;

            sb.Begin();
            // First Line: Title
            sb.DrawString(scoreFont, TITLE, nextPosition, Color.Red);
            nextPosition.Y += scoreFont.LineSpacing;
            // Add blank line
            nextPosition.Y += scoreFont.LineSpacing;
            // Display 2 Developer names
            sb.DrawString(scoreFont, DEVELOPER1, nextPosition, Color.Black);
            nextPosition.Y += scoreFont.LineSpacing;
            sb.DrawString(scoreFont, DEVELOPER2, nextPosition, Color.Black);
            sb.End();

            base.Draw(gameTime);
        }
    }
}
