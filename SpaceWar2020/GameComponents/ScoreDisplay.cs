/* 
 * ScoreDisplay.cs
 * Final Project: SpaceWar2020
 *                Calcualte, Display Score
 * Revision History:
 *      Jiyoung Jung, 2020.12.08: Version 1.0
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
    /// Component for manipulating and displaying the player score 
    /// </summary>
    public class ScoreDisplay : DrawableGameComponent
    {
        /// <summary>
        /// Font for displaying a score
        /// </summary>
        SpriteFont scoreFont;

        /// <summary>
        /// parent Gamescene
        /// </summary>
        GameScene parent;

        /// <summary>
        /// Position for displaying score value
        /// </summary>
        Vector2 scorePosition;

        /// <summary>
        /// Current Score value (String)
        /// </summary>
        string scoreText;

        /// <summary>
        /// Current Score
        /// </summary>
        int totalScore;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">Game Entity</param>
        /// <param name="parent">parent GameScene</param>
        public ScoreDisplay(Game game, GameScene parent)
                        : base(game)
        {
            // If previous ScoreDisplay component exists,
            //  remove it and add a new component
            if (Game.Services.GetService<ScoreDisplay>() != null)
            {
                Game.Services.RemoveService(typeof(ScoreDisplay));
            }
            Game.Services.AddService<ScoreDisplay>(this);

            this.parent = parent;
            totalScore = 0;
            scoreText = "";
            scorePosition = new Vector2();
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();

            sb.Begin();
            // Display current score
            sb.DrawString(scoreFont, scoreText, scorePosition, Color.Red);
            sb.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            // Set current score text (string) to display it
            scoreText = $"Score : {GetScore()} points";

            // Set position for display score (top, center horizontally)
            scorePosition.X = Game.GraphicsDevice.Viewport.Width / 2 - scoreFont.MeasureString(scoreText).X / 2;
            scorePosition.Y = scoreFont.MeasureString(scoreText).Y;

            if (((GetScore() >= 100 && GetScore() < 150) ||
                (GetScore() >= 1000 && GetScore() < 1050)
                    )&& !Game.Components.OfType<Bonus>().Any())
            {
                // Create a Bonus on the top
                Bonus bonus = new Bonus(Game, new Vector2(50, 10));
                
                parent.AddComponent(bonus);
            }
            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            scoreFont = Game.Content.Load<SpriteFont>(@"Fonts\scoreDisplayFont");
            base.LoadContent();
        }

        /// <summary>
        /// Add score to current total score
        /// </summary>
        /// <param name="score">score</param>
        public void AddScore(int score)
        {
            totalScore += score;
        }

        /// <summary>
        /// Get current score
        /// </summary>
        /// <returns>current score</returns>
        public int GetScore()
        {
            return totalScore;
        }

        /// <summary>
        /// Reset current score
        /// </summary>
        public void ResetScore()
        {
            totalScore = 0;
        }
    }
}
