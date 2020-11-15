using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWar2020
{
    public class Background : DrawableGameComponent
    {
        Texture2D fullBackTexture;
        Texture2D tileTexture;

        bool drawfullBackground = true;
        KeyboardState prevKS = Keyboard.GetState();

        public Background(Game game)
            : base(game)
        {
            DrawOrder = 0;
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            sb.Begin();

            if (drawfullBackground)
            {
                sb.Draw(fullBackTexture, new Rectangle(0, 0, Game.GraphicsDevice.Viewport.Width,
                    Game.GraphicsDevice.Viewport.Height),
                    Color.White);
            }
            else
            {
                int rows = Game.GraphicsDevice.Viewport.Height / tileTexture.Height + 1;
                int cols = Game.GraphicsDevice.Viewport.Width / tileTexture.Width + 1;
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

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.B) && prevKS.IsKeyUp(Keys.B))
            {
                drawfullBackground = !drawfullBackground;
            }
            prevKS = ks;

            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            fullBackTexture = Game.Content.Load<Texture2D>("Background\\space-1");
            tileTexture = Game.Content.Load<Texture2D>("Background\\space-2");
            base.LoadContent();
        }
    }
}
