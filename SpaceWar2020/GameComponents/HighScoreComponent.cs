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
    public struct GameScore
    {
        public string Name;
        public int Value;

        public GameScore(string name, int value)
        {
            Name = name;
            Value = value;
        }
    }

    class HighScoreComponent : DrawableGameComponent
    {
        const int MAX_NUM_OF_SCORE = 5;
        const string SCORE_FILE_NAME = "scores.txt";

        SpriteFont scoreFont;

        private Vector2 startingPosition;

        public HighScoreComponent(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            // starting position of the menu items - but you can decise to put it elsewhere
            startingPosition = new Vector2(GraphicsDevice.Viewport.Width / 2,
                                      GraphicsDevice.Viewport.Height / 2);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // load the fonts we will be using for this menu
            scoreFont = Game.Content.Load<SpriteFont>(@"Fonts\regularFont");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            // handle the escape key for this scene
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

            List<GameScore> gameScores = new List<GameScore>();
            string ErrorMessage;

            // Read Score File
            ReadScoreFile(gameScores);

            sb.Begin();

            for (int i = 0; i < gameScores.Count; i++)
            {

                sb.DrawString(scoreFont, $"{gameScores[i].Name} : {gameScores[i].Value}", nextPosition, Color.Black);

                // update the position of next string
                nextPosition.Y += scoreFont.LineSpacing;
            }

            sb.End();

            base.Draw(gameTime);
        }

        private static void ReadScoreFile(List<GameScore> gameScores)
        {
            string fileName = SCORE_FILE_NAME;
            if (File.Exists(fileName))
            {
                try
                {
                    using (StreamReader reader = new StreamReader(fileName))
                    {
                        //while (reader.EndOfStream == false)
                        //{
                        for (int i = 0; i < MAX_NUM_OF_SCORE; i++)
                        {
                            string line = reader.ReadLine();
                            string[] fields = line.Split(',');
                            int scoreValue = int.Parse(fields[1]);
                            gameScores.Add(new GameScore(fields[0], scoreValue));
                        }
                        //}
                    }
                }
                catch (Exception ex)
                {
                    // Display Error Message
                }

                // Descending Order
                gameScores.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));
            }
            else
            {
                // Create default Score Data
                gameScores.Add(new GameScore("None", 0));
                gameScores.Add(new GameScore("None", 0));
                gameScores.Add(new GameScore("None", 0));
                gameScores.Add(new GameScore("None", 0));
                gameScores.Add(new GameScore("None", 0));

                // Save Score data
                using (StreamWriter writer = new StreamWriter(fileName, false))
                {
                    for (int i = 0; i < MAX_NUM_OF_SCORE; i++)
                    {
                        writer.Write($"{gameScores[i].Name}, {gameScores[i].Value}{Environment.NewLine}");
                    }
                    writer.Flush();
                }
            }
        }
    }
}
