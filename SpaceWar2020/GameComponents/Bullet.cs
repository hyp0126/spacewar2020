using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWar2020
{
    public class Bullet : DrawableGameComponent, ICollidable
    {
        public const int WIDTH = 10;
        public const int HEIGHT = 10;
        const int COLLISION_OFFSET_WIDTH = 1;
        const int COLLISION_OFFSET_HEIGHT = 1;
        const int DOWN_SPEED = 6;

        static Texture2D texture;
        Vector2 position;

        public Rectangle CollisionBox => new Rectangle((int)position.X + COLLISION_OFFSET_WIDTH,
                       (int)position.Y + COLLISION_OFFSET_HEIGHT,
                       WIDTH - 2 * COLLISION_OFFSET_WIDTH,
                       HEIGHT - 2 * COLLISION_OFFSET_HEIGHT);

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
            position.Y += DOWN_SPEED;

            CheckForCollisionWithPlayer();

            // check boundary
            if (this.CollisionBox.Top > Game.GraphicsDevice.Viewport.Height)
            {
                Game.Components.Remove(this);
            }

            base.Update(gameTime);
        }

        private void CheckForCollisionWithPlayer()
        {
            Spacecraft player = Game.Services.GetService<Spacecraft>();
            if (player != null)
            {
                if (player.CollisionBox.Intersects(this.CollisionBox))
                {
                    player.HandleCollision();
                    this.HandleCollision();
                }
            }
        }

        protected override void LoadContent()
        {
            if (texture == null)
            {
                texture = Game.Content.Load<Texture2D>("Missile\\new_bullet");
            }

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();

            sb.Begin();
            sb.Draw(texture, new Rectangle((int)position.X, 
                                           (int)position.Y,
                                           WIDTH,
                                           HEIGHT),
                                           null,
                                           Color.White);
            sb.End();

            base.Draw(gameTime);
        }

        public void HandleCollision()
        {
            Game.Components.Remove(this);
        }
    }
}

