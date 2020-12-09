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
    class AboutTextComponent : DrawableGameComponent
    {
        const string TITLE = "ABOUT";
        const string DEVELOPER1 = "Hong, Yi Phyo";
        const string DEVELOPER2 = "Jung, Jiyoung";

        SpriteFont scoreFont;

        private Vector2 startingPosition;

        public AboutTextComponent(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            // starting position of the menu items - but you can decise to put it elsewhere
            SpriteFont font = Game.Content.Load<SpriteFont>(@"Fonts\regularFont");
            startingPosition = new Vector2(GraphicsDevice.Viewport.Width / 2 - font.MeasureString(DEVELOPER1).X / 2,
                              GraphicsDevice.Viewport.Height / 2 - 4 * font.MeasureString(DEVELOPER1).Y / 2);

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

            sb.Begin();
            sb.DrawString(scoreFont, TITLE, nextPosition, Color.Red);
            nextPosition.Y += scoreFont.LineSpacing;
            nextPosition.Y += scoreFont.LineSpacing;
            sb.DrawString(scoreFont, DEVELOPER1, nextPosition, Color.Black);
            nextPosition.Y += scoreFont.LineSpacing;
            sb.DrawString(scoreFont, DEVELOPER2, nextPosition, Color.Black);
            sb.End();

            base.Draw(gameTime);
        }
    }
}
