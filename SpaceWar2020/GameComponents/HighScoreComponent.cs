/* 
 * HighScoreComponent.cs
 * Final Project: SpaceWar2020
 *                High Score sub-menu component
 * Revision History:
 *      Jiyoung Jung, 2020.12.03: Version 1.0
 *      
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceWar2020
{
    /// <summary>
    /// Hgh Score Sub-Menu: Display High Score List
    /// </summary>
    class HighScoreComponent : DrawableGameComponent
    {
        /// <summary>
        /// Menu Title
        /// </summary>
        const string TITLE = "High Score Record";
        
        /// <summary>
        /// Maximum number of score saved in a file
        /// </summary>
        const int MAX_RECORDS = 5;

        /// <summary>
        /// Font for High Score Menu Title
        /// </summary>
        SpriteFont scoreTitleFont;

        /// <summary>
        /// Font for High Score List
        /// </summary>
        SpriteFont scoreFont;

        /// <summary>
        /// Menu Start Position
        /// </summary>
        private Vector2 startingPosition;

        // Default Constructor
        public HighScoreComponent(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            // Assign starting position in the middle of screen
            SpriteFont font = Game.Content.Load<SpriteFont>(@"Fonts\regularFont");
            startingPosition = new Vector2(GraphicsDevice.Viewport.Width / 2 - font.MeasureString(TITLE).X / 2,
                              GraphicsDevice.Viewport.Height / 2 - (MAX_RECORDS + 2) * font.MeasureString(TITLE).Y / 2);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Load menu title font
            scoreTitleFont = Game.Content.Load<SpriteFont>(@"Fonts\regularFont");
            // Load score list font which is Monospace font for alienment (Name : score)
            scoreFont = Game.Content.Load<SpriteFont>(@"Fonts\scoreMenuFont");
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

            // Read Scores form a File
            List<ScoreData> gameScores = GameScore.ReadScoresFromFile();

            sb.Begin();
            // Display Menu Title
            sb.DrawString(scoreTitleFont, TITLE, nextPosition, Color.Red);
            nextPosition.Y += scoreFont.LineSpacing;
            // Display a Blank Line
            nextPosition.Y += scoreFont.LineSpacing;
            // Diplay score list
            for (int i = 0; i < gameScores.Count; i++)
            {
                // Name: Left Alignment
                string name = String.Format("{0, -12}", gameScores[i].Name);
                // Score: Right Alignment
                string point = String.Format("{0, 6}", gameScores[i].Value.ToString());
                sb.DrawString(scoreFont, name + " : " + point, nextPosition, Color.Black);
                // Update the position of next score
                nextPosition.Y += scoreFont.LineSpacing;
            }
            sb.End();

            base.Draw(gameTime);
        }
    }
}
