using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWar2020
{
    public class Bullet : DrawableGameComponent
    {
        Texture2D texture;
        Vector2 position;

        public Bullet(Game game)
            : this(game, Vector2.Zero)
        {
        }

        public Bullet(Game game, Vector2 position)
            : base(game)
        {
            this.position = position;
        }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            texture = Game.Content.Load<Texture2D>("Missile\\new_bullet");
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            sb.Begin();
            //sb.Draw(texture, position, Color.White);
            sb.Draw(texture, new Rectangle((int)position.X, (int)position.Y, 25, 25), null, Color.White);
            sb.End();
            base.Draw(gameTime);
        }
    }
}

