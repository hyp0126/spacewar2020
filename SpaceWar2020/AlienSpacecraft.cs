using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWar2020
{
    public class AlienSpacecraft : DrawableGameComponent, ICollidable
    {
        public const int WIDTH = 50;
        public const int HEIGHT = 50;
        const int DOWN_SPEED = 3;
        const int HORIZONTAL_SPEED = 3;
        const double BULLET_DUARTION = 0.5;

        Texture2D texture;
        Vector2 position;

        double createBulletTimer;

        public Rectangle CollisionBox => new Rectangle((int)position.X, (int)position.Y, WIDTH, HEIGHT);


        public AlienSpacecraft(Game game)
            : this(game, Vector2.Zero)
        {
        }

        public AlienSpacecraft(Game game, Vector2 position)
            : base(game)
        {
            this.position = position;
            createBulletTimer = 0;
        }

        public override void Update(GameTime gameTime)
        {
            position.Y += DOWN_SPEED;

            // Move to Player
            var spacecraft = Game.Services.GetService<Spacecraft>();
            if (position.X > spacecraft.position.X)
            {
                position.X -= HORIZONTAL_SPEED;
            }
            else
            {
                position.X += HORIZONTAL_SPEED;
            }

            // Create New Bullet
            createBulletTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (createBulletTimer > BULLET_DUARTION)
            {
                float x = position.X + WIDTH / 2 - Bullet.WIDTH / 2;
                float y = position.Y + HEIGHT;
                Game.Components.Add(new Bullet(Game, new Vector2(x, y)));
                createBulletTimer = 0;
            }
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
            var origin = new Vector2(0, texture.Height);
            sb.Draw(texture, 
                    new Rectangle((int)position.X, (int)position.Y, WIDTH, HEIGHT),
                    null,
                    Color.White,
                    (float)Math.PI / 2f,
                    origin,
                    SpriteEffects.None,
                    0f);
            sb.End();
            base.Draw(gameTime);
        }

        public void HandleCollision()
        {

        }
    }
}
