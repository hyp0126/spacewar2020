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
    public class ScoreDisplay : DrawableGameComponent
    {
        string scoreText;
        Vector2 scorePosition;
        SpriteFont scoreFont;
        GameScene parent;
        int totalScore;

        public void AddScore(int score)
        {
            totalScore += score;
        }

        public int GetScore()
        {
            return totalScore;
        }

        public void ResetScore()
        {
            totalScore = 0;
        }

        public ScoreDisplay(Game game, GameScene parent)
                        : base(game)
        {
            if (Game.Services.GetService<ScoreDisplay>() != null)
            {
                Game.Services.RemoveService(typeof(ScoreDisplay));
            }
            Game.Services.AddService<ScoreDisplay>(this);

            this.parent = parent;
            totalScore = 0;
            scoreText = "";
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();

            sb.Begin();
            sb.DrawString(scoreFont, scoreText, scorePosition, Color.Red);
            sb.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            scoreText = $"Score : {GetScore()} points";
            scorePosition = new Vector2(Game.GraphicsDevice.Viewport.Width / 2 - scoreFont.MeasureString(scoreText).X / 2, 10);

            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            scoreFont = Game.Content.Load<SpriteFont>(@"Fonts\scoreDisplayFont");
            base.LoadContent();
        }
    }
}
