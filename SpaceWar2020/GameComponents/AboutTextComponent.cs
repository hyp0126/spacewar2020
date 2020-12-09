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
        Texture2D texture;

        public AboutTextComponent(Game game) : base(game)
        {
        }


        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = Game.Services.GetService<SpriteBatch>();

            spriteBatch.Begin();
            spriteBatch.Draw(texture, Vector2.Zero, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }


        protected override void LoadContent()
        {
            texture = Game.Content.Load<Texture2D>("Images/helpImage");
            base.LoadContent();
        }
    }
}
