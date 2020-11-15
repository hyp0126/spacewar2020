using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWar2020
{
    public class AlienSpacecraft : DrawableGameComponent
    {
        Texture2D texture;
        Vector2 position;

        public AlienSpacecraft(Game game)
            : this(game, Vector2.Zero)
        {
        }

        public AlienSpacecraft(Game game, Vector2 position)
            : base(game)
        {
            DrawOrder = 1;
            this.position = position;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            texture = Game.Content.Load<Texture2D>("AlienSpacecraft\\bluecarrier");
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            sb.Begin();
            //sb.Draw(texture, position, Color.White);
            var origin = new Vector2(texture.Width / 2f, texture.Height / 2f);
            sb.Draw(texture, new Rectangle((int)position.X + 50, (int)position.Y + 50, 100, 100), null, Color.White, (float)Math.PI / 2f, origin, SpriteEffects.None, 0f);
            sb.End();
            base.Draw(gameTime);
        }
    }
}
