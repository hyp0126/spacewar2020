/* 
 * HighScoreInput.cs
 * Final Project: SpaceWar2020
 *                Input component for saving a new high score with a player name
 * Revision History:
 *      originally from Course Material
 *      Jiyoung Jung, 2020.12.09: Modified, Version 1.1
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
    /// Component for inputting a player name who earned a high score
    /// </summary>
    class HighScoreInput : DrawableGameComponent
    {
        /// <summary>
        /// Menu Title
        /// </summary>
        const string TITLE = "Enter Your Name";

        /// <summary>
        /// Maximum player name characters
        /// </summary>
        const int MAX_NAME_CHARS = 12;
        
        /// <summary>
        /// Idel time after player entered user name.
        /// For stable transition from input mode to game mode
        /// </summary>
        const double IDLE_DURATION = 0.2;

        /// <summary>
        /// Font for displaying menu and input
        /// </summary>
        SpriteFont font;

        /// <summary>
        /// Menu start position 
        /// </summary>
        private Vector2 startingPosition;

        /// <summary>
        /// Idle state timer
        /// </summary>
        double idleTimer = 0;

        /// <summary>
        /// Idel timer on flag
        /// </summary>
        bool idleTimerOn = false;

        /// <summary>
        /// Current Text which user inputted
        /// </summary>
        string enteredString; 

        /// <summary>
        /// previous keyboard state
        /// </summary>
        KeyboardState prevKS;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">Game Entity</param>
        public HighScoreInput(Game game) : base(game)
        {
            enteredString = "";
            prevKS = Keyboard.GetState();
        }

        public override void Initialize()
        {
            // Assign starting position in the middle of screen
            SpriteFont font = Game.Content.Load<SpriteFont>(@"Fonts\regularFont");
            startingPosition = new Vector2(GraphicsDevice.Viewport.Width / 2 - font.MeasureString(TITLE).X / 2,
                              GraphicsDevice.Viewport.Height / 2 - 2 * font.MeasureString(TITLE).Y / 2);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Load menu font
            font = Game.Content.Load<SpriteFont>(@"Fonts\regularFont");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();

            // Get all keys currently pressed
            Keys[] pressedKeys = keyState.GetPressedKeys();

            if (pressedKeys.Length > 0)
            {
                // Iterate through all pressed keys
                foreach (Keys key in pressedKeys)
                {
                    if (prevKS.IsKeyDown(key))
                    {
                        continue;
                    }
                    switch (key)
                    {
                        case Keys.Space:
                            enteredString += " ";
                            break;
                        case Keys.Back:
                            if (enteredString.Length > 0)
                            {
                                enteredString = enteredString.Remove(enteredString.Length - 1);
                            }
                            break;
                        case Keys.A:
                        case Keys.B:
                        case Keys.C:
                        case Keys.D:
                        case Keys.E:
                        case Keys.F:
                        case Keys.G:
                        case Keys.H:
                        case Keys.I:
                        case Keys.J:
                        case Keys.K:
                        case Keys.L:
                        case Keys.M:
                        case Keys.N:
                        case Keys.O:
                        case Keys.P:
                        case Keys.Q:
                        case Keys.R:
                        case Keys.S:
                        case Keys.T:
                        case Keys.U:
                        case Keys.V:
                        case Keys.W:
                        case Keys.X:
                        case Keys.Y:
                        case Keys.Z:
                            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift)
                                || Keyboard.GetState().IsKeyDown(Keys.RightShift))
                            {
                                enteredString += key.ToString();
                            }
                            else
                            {
                                enteredString += key.ToString().ToLower();
                            }
                            break;
                        case Keys.OemPeriod:
                            enteredString += ".";
                            break;
                        case Keys.OemComma:
                            enteredString += ",";
                            break;
                        case Keys.OemQuestion:
                            enteredString += "?";
                            break;
                        case Keys.D1:
                            enteredString += "!";
                            break;
                        case Keys.OemQuotes:
                            enteredString += "\"";
                            break;
                        case Keys.Enter:
                            // Add current score to High Score List File
                            int score = Game.Services.GetService<ScoreDisplay>().GetScore();
                            GameScore.WriteScore(new ScoreData(enteredString, score));
                            // Idle timer is on for stable transition from menu to game
                            idleTimerOn = true;
                            break;
                        case Keys.OemMinus:
                            enteredString += "-";
                            break;
                        default:
                            break;
                    }

                }
            }

            // Limit Maximum player name characters
            if (enteredString.Length > MAX_NAME_CHARS)
            {
                enteredString = enteredString.Remove(enteredString.Length - 1);
            }

            prevKS = keyState;

            // Check Idle Timer
            // If Idle Timer is over, reset score and remove this instance
            if (idleTimerOn)
            {
                idleTimer += gameTime.ElapsedGameTime.TotalSeconds;
                if (idleTimer >= IDLE_DURATION)
                {
                    Game.Services.GetService<ScoreDisplay>().ResetScore();
                    Game.Components.Remove(this);
                }
            }
            else
            {
                idleTimer = 0;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            Vector2 nextPosition = startingPosition;

            sb.Begin();
            // Display menu title
            sb.DrawString(font, TITLE, nextPosition, Color.Red);
            nextPosition.Y += font.LineSpacing;
            // Display current inputted name
            sb.DrawString(font, enteredString, nextPosition, Color.Red);
            sb.End();
            base.Draw(gameTime);
        }
    }
}