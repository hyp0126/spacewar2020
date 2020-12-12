/* 
 * AlienSpacecraft.cs
 * Final Project: SpaceWar2020
 *                Alien Spacecraft (enemy)
 * Revision History: 
 *      Yiphyo Hong, 2020.11.14: Version 1.0
 *      
*/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWar2020
{
    /// <summary>
    /// Alien Spacecraft (Enemy)
    /// </summary>
    public class AlienSpacecraft : DrawableGameComponent, ICollidable
    {
        /// <summary>
        /// Width of the Alien Spacecraft texture
        /// </summary>
        public const int WIDTH = 50;

        /// <summary>
        /// Height of the Alien Spacecraft texture
        /// </summary>
        public const int HEIGHT = 50;

        /// <summary>
        /// Left and Right offet of width for checking collision
        /// </summary>
        const int COLLISION_OFFSET_WIDTH = 5;

        /// <summary>
        /// Top and Bottom offet of height for checking collision
        /// </summary>
        const int COLLISION_OFFSET_HEIGHT = 5;

        /// <summary>
        /// Minimum horizontal speed for a random speed
        /// </summary>
        const int MIN_HORIZONTAL_SPEED = 0;

        /// <summary>
        /// Maximum horizontalspeed for a random speed
        /// </summary>
        const int MAX_HORIZONTAL_SPEED = 5;

        /// <summary>
        /// Minimum downward speed for a random speed (Inclusive)
        /// </summary>
        const int MIN_DOWN_SPEED = 1;

        /// <summary>
        /// Maximum downward speed for a random speed (Exclusive)
        /// </summary>
        const int MAX_DOWN_SPEED = 5;

        /// <summary>
        /// The time duration for firing a new bullet
        /// </summary>
        const double BULLET_DUARTION = 0.5;

        /// <summary>
        /// Points which Player get by shooting down an Alein Spacecraft
        /// </summary>
        const int POINTS = 50;

        static Texture2D texture;
        Vector2 position;

        /// <summary>
        /// parent Gamescene
        /// </summary>
        GameScene parent;

        /// <summary>
        /// Current downward Speed
        /// </summary>
        int downSpeed;

        /// <summary>
        /// Current horizontal Speed
        /// </summary>
        int horizontalSpeed;

        /// <summary>
        /// Random variable for new Alien Spacecraft Speeds (X, Y)
        /// </summary>
        static Random random;

        /// <summary>
        /// Timer variable for creating a new Bullet
        /// </summary>
        double createBulletTimer;

        /// <summary>
        /// Collision Box 
        /// </summary>
        public Rectangle CollisionBox => new Rectangle((int)position.X + COLLISION_OFFSET_WIDTH,
                                               (int)position.Y + COLLISION_OFFSET_HEIGHT,
                                               WIDTH - 2 * COLLISION_OFFSET_WIDTH,
                                               HEIGHT - 2 * COLLISION_OFFSET_HEIGHT);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">Game Entity</param>
        /// <param name="parent">parent GameScene</param>
        public AlienSpacecraft(Game game, GameScene parent)
            : this(game, parent, Vector2.Zero)
        {
        }

        /// <summary>
        /// Constructor with a initial position
        /// </summary>
        /// <param name="game">Game Entity</param>
        /// <param name="parent">parent GameScene</param>
        /// <param name="position">Initial Position</param>
        public AlienSpacecraft(Game game, GameScene parent, Vector2 position)
            : base(game)
        {
            this.parent = parent;
            this.position = position;
            createBulletTimer = 0;

            // Single random entity 
            if (random == null)
            {
                random = new Random();
            }

            // Create random X and Y Speeds
            downSpeed = random.Next(MIN_DOWN_SPEED, MAX_DOWN_SPEED);
            horizontalSpeed = random.Next(MIN_HORIZONTAL_SPEED, MAX_HORIZONTAL_SPEED);
        }

        public override void Update(GameTime gameTime)
        {
            // Move downward
            position.Y += downSpeed;

            MoveToPlayer();

            CheckorMissileCollision();

            CheckForCollisionWithPlayer();

            CreateNewBullet(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// Check Collision with Missiles
        /// </summary>
        private void CheckorMissileCollision()
        {
            for (int i = 0; i < Game.Components.OfType<Missile>().Count(); i++)
            {
                Missile missile = Game.Components.OfType<Missile>().ElementAt(i);

                // Check Collision with a missile
                if (this.CollisionBox.Intersects(missile.CollisionBox))
                {
                    // Add points to player score
                    // call missile and alien spacecraft collision handle methods
                    Game.Services.GetService<ScoreDisplay>().AddScore(POINTS);
                    missile.HandleCollision();
                    this.HandleCollision();
                    i--;
                }
            }
        }

        /// <summary>
        /// Create a new bullet at regular time interval
        /// </summary>
        /// <param name="gameTime">Game Time</param>
        private void CreateNewBullet(GameTime gameTime)
        {
            createBulletTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (createBulletTimer > BULLET_DUARTION)
            {
                // Starting X position is a middle of a Alien Spacecraft
                float x = position.X + WIDTH / 2 - Bullet.WIDTH / 2;
                float y = position.Y + HEIGHT;

                parent.AddComponent(new Bullet(Game, new Vector2(x, y)));
                createBulletTimer = 0;
            }
        }

        /// <summary>
        /// Check Collision with Game Player (Spacecraft)
        /// </summary>
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

        /// <summary>
        /// Alien Spacecraft trace a player (Spacecraft)
        /// </summary>
        private void MoveToPlayer()
        {
            var player = Game.Services.GetService<Spacecraft>();
            
            if (player != null)
            {
                // Check the horizontal distance between the Alien Spacecraft and player
                // Trace player at the fixed speed which is created randomly
                float distance = position.X - player.Position.X;
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
            // Single Texture for Alien Spacecraft
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
            // Display Alien Spacecraft faced downward (Rotated 180 deg)
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

        /// <summary>
        /// Collision: Remove Alien Spacecraft 
        /// and Create a Explosion component (Explosion animation)
        /// </summary>
        public void HandleCollision()
        {
            Game.Components.Remove(this);
            Game.Components.Add(new Explosion(Game, new Vector2(position.X, position.Y)));
        }
    }
}
