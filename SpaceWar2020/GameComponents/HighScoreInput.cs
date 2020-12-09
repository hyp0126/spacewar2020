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
    class HighScoreInput : DrawableGameComponent
    {
        const string TITLE = "Enter Your Name";
        const double STOP_DURATION = 0.2;

        double timer = 0;
        bool timerOn = false;

        SpriteFont font;
        private Vector2 startingPosition;

        string enteredString; 
        KeyboardState prevKS;

        public HighScoreInput(Game game) : base(game)
        {
            enteredString = "";
            prevKS = Keyboard.GetState();
        }

        public override void Initialize()
        {
            SpriteFont font = Game.Content.Load<SpriteFont>(@"Fonts\regularFont");
            startingPosition = new Vector2(GraphicsDevice.Viewport.Width / 2 - font.MeasureString(TITLE).X / 2,
                              GraphicsDevice.Viewport.Height / 2 - 2 * font.MeasureString(TITLE).Y / 2);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            font = Game.Content.Load<SpriteFont>(@"Fonts\regularFont");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();

            // get all keys currently pressed
            Keys[] pressedKeys = keyState.GetPressedKeys();

            if (pressedKeys.Length > 0)
            {

                // iterate through all pressed keys
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
                            enteredString = enteredString.Remove(enteredString.Length - 1);
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
                            int score = Game.Services.GetService<ScoreDisplay>().GetScore();
                            GameScore.WriteScore(new ScoreData(enteredString, score));
                            timerOn = true;
                            break;
                        case Keys.OemMinus:
                            enteredString += "-";
                            break;
                        default:
                            break;
                    }

                }
            }

            prevKS = keyState;

            if (timerOn)
            {
                timer += gameTime.ElapsedGameTime.TotalSeconds;
                if (timer >= STOP_DURATION)
                {
                    Game.Services.GetService<ScoreDisplay>().ResetScore();
                    Game.Components.Remove(this);
                }
            }
            else
            {
                timer = 0;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            Vector2 nextPosition = startingPosition;

            sb.Begin();
            sb.DrawString(font, TITLE, nextPosition, Color.Red);
            nextPosition.Y += font.LineSpacing;

            sb.DrawString(font, enteredString, nextPosition, Color.Red);
            sb.End();
            base.Draw(gameTime);
        }
    }
}