using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWar2020
{
    public class Asteroid : DrawableGameComponent, ICollidable
    {
        public const int WIDTH = 50;
        public const int HEIGHT = 50;
        const int DOWN_SPEED = 3;
        const int HORIZONTAL_SPEED = 3;

        Random random;

        List<Texture2D> textures;
        int currentFrame = 0;

        double frameTimer = 0;
        const double FRAME_INTERVAL = 0.1;

        Vector2 position;
        int horizontal_speed;

        public Rectangle CollisionBox => new Rectangle((int)position.X, (int)position.Y, WIDTH, HEIGHT);

        public Asteroid(Game game)
            : this(game, Vector2.Zero)
        {
        }

        public Asteroid(Game game, Vector2 position)
            : base(game)
        {
            random = new Random();
            this.position = position;
            horizontal_speed = random.Next(-HORIZONTAL_SPEED, HORIZONTAL_SPEED);
        }

        public override void Update(GameTime gameTime)
        {
            position.Y += DOWN_SPEED;
            position.X += horizontal_speed;

            frameTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (frameTimer >= FRAME_INTERVAL)
            {
                frameTimer = 0;

                currentFrame++;
                if (currentFrame >= textures.Count)
                {
                    currentFrame = 0;
                }
            }
            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            textures = new List<Texture2D>();
            textures.Add(Game.Content.Load<Texture2D>("Asteroid\\a10000"));
            textures.Add(Game.Content.Load<Texture2D>("Asteroid\\a10002"));
            textures.Add(Game.Content.Load<Texture2D>("Asteroid\\a10004"));
            textures.Add(Game.Content.Load<Texture2D>("Asteroid\\a10006"));
            textures.Add(Game.Content.Load<Texture2D>("Asteroid\\a10008"));
            textures.Add(Game.Content.Load<Texture2D>("Asteroid\\a10010"));
            textures.Add(Game.Content.Load<Texture2D>("Asteroid\\a10012"));
            textures.Add(Game.Content.Load<Texture2D>("Asteroid\\a10014"));

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            sb.Begin();
            sb.Draw(textures[currentFrame],
                    new Rectangle((int)position.X, (int)position.Y, WIDTH, HEIGHT),
                    null,
                    Color.White,
                    0f,
                    Vector2.Zero,
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