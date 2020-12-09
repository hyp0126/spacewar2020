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
    class HighScoreComponent : DrawableGameComponent
    {
        const string TITLE = "High Score Record";
        const int MAX_RECORDS = 5;

        SpriteFont scoreFont;

        private Vector2 startingPosition;

        public HighScoreComponent(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            // starting position of the menu items - but you can decise to put it elsewhere
            SpriteFont font = Game.Content.Load<SpriteFont>(@"Fonts\regularFont");
            startingPosition = new Vector2(GraphicsDevice.Viewport.Width / 2 - font.MeasureString(TITLE).X / 2,
                              GraphicsDevice.Viewport.Height / 2 - (MAX_RECORDS + 2) * font.MeasureString(TITLE).Y / 2);

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

            // Read Score File
            List<ScoreData> gameScores = GameScore.ReadScoresFromFile();

            sb.Begin();
            sb.DrawString(scoreFont, TITLE, nextPosition, Color.Red);
            nextPosition.Y += scoreFont.LineSpacing;
            nextPosition.Y += scoreFont.LineSpacing;
            for (int i = 0; i < gameScores.Count; i++)
            {
                sb.DrawString(scoreFont, $"{gameScores[i].Name} : {gameScores[i].Value} points", nextPosition, Color.Black);
                // update the position of next string
                nextPosition.Y += scoreFont.LineSpacing;
            }

            sb.End();

            base.Draw(gameTime);
        }
    }
}
