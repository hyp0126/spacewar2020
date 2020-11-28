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
        const int MIN_HORIZONTAL_SPEED = 0;
        const int MAX_HORIZONTAL_SPEED = 5;
        const int MIN_DOWN_SPEED = 1;
        const int MAX_DOWN_SPEED = 5;
        const double BULLET_DUARTION = 0.5;

        int downSpeed;
        int horizontalSpeed;

        static Texture2D texture;
        Vector2 position;

        static Random random;

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
            if (random == null)
            {
                random = new Random();
            }
            downSpeed = random.Next(MIN_DOWN_SPEED, MAX_DOWN_SPEED);
            horizontalSpeed = random.Next(MIN_HORIZONTAL_SPEED, MAX_HORIZONTAL_SPEED);
        }

        public override void Update(GameTime gameTime)
        {
            position.Y += downSpeed;

            MoveToPlayer();

            CheckorMissileCollision();

            CheckForCollisionWithPlayer();

            CreateNewBullet(gameTime);

            base.Update(gameTime);
        }

        private void CheckorMissileCollision()
        {
            for (int i = 0; i < Game.Components.OfType<Missile>().Count(); i++)
            {
                Missile missile = Game.Components.OfType<Missile>().ElementAt(i);
                if (this.CollisionBox.Intersects(missile.CollisionBox))
                {
                    missile.HandleCollision();
                    this.HandleCollision();
                    i--;
                }
            }
        }

        private void CreateNewBullet(GameTime gameTime)
        {
            createBulletTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (createBulletTimer > BULLET_DUARTION)
            {
                float x = position.X + WIDTH / 2 - Bullet.WIDTH / 2;
                float y = position.Y + HEIGHT;
                Game.Components.Add(new Bullet(Game, new Vector2(x, y)));
                createBulletTimer = 0;
            }
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

        private void MoveToPlayer()
        {
            var player = Game.Services.GetService<Spacecraft>();
            if (player != null)
            {
                float distance = position.X - player.position.X;
                if (distance > 0 && distance >= horizontalSpeed)
                {
                    position.X -= horizontalSpeed;
                }
                else if (distance <= -horizontalSpeed)
                {
                    position.X += horizontalSpeed;
                }
            }
        }

        protected override void LoadContent()
        {
            if (texture == null)
            {
                texture = Game.Content.Load<Texture2D>("AlienSpacecraft\\bluecarrier");
            }

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
            Game.Components.Remove(this);
            Game.Components.Add(new Explosion(Game, new Vector2(position.X, position.Y)));
        }
    }
}
