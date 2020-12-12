/* 
 * Asteroid.cs
 * Final Project: SpaceWar2020
 *                Asteroid (enemy)
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
    /// Alien Asteroid (Enemy)
    /// </summary>
    public class Asteroid : DrawableGameComponent, ICollidable
    {
        /// <summary>
        /// Width of the Asteroid texture
        /// </summary>
        public const int WIDTH = 50;

        /// <summary>
        /// Height of the Asteroid texture
        /// </summary>
        public const int HEIGHT = 50;

        /// <summary>
        /// Left and Right offet of width for checking collision
        /// </summary>
        const int COLLISION_OFFSET_WIDTH = 15;

        /// <summary>
        /// Top and Bottom offet of height for checking collision
        /// </summary>
        const int COLLISION_OFFSET_HEIGHT = 15;

        /// <summary>
        /// Fixed downward speed
        /// </summary>
        const int DOWN_SPEED = 3;

        /// <summary>
        /// Maximum horizontal speed for a random speed
        /// -HORIZONTAL_SPEED <= speed < HORIZONTAL_SPEED
        /// </summary>
        const int HORIZONTAL_SPEED = 3;

        /// <summary>
        /// Points which Player get by shooting down an Asteroid
        /// </summary>
        const int POINTS = 20;

        /// <summary>
        /// Time interval between frames
        /// </summary>
        const double FRAME_INTERVAL = 0.1;

        /// <summary>
        /// Random variable for new Asteroid horizontal speed
        /// </summary>
        Random random;

        static List<Texture2D> textures;
        Vector2 position;

        /// <summary>
        /// Current horizontal Speed
        /// </summary>
        int horizontal_speed;

        /// <summary>
        /// current Frame number
        /// </summary>
        int currentFrame = 0;

        /// <summary>
        /// Timer variable for checking a next frame time
        /// </summary>
        double frameTimer = 0;

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
        public Asteroid(Game game)
            : this(game, Vector2.Zero)
        {
        }

        /// <summary>
        /// Constructor with a initial position
        /// </summary>
        /// <param name="game">Game Entity</param>
        /// <param name="position">Initial Position</param>
        public Asteroid(Game game, Vector2 position)
            : base(game)
        {
            random = new Random();
            this.position = position;

            // Create random a X Speed
            horizontal_speed = random.Next(-HORIZONTAL_SPEED, HORIZONTAL_SPEED);
        }

        public override void Update(GameTime gameTime)
        {
            // Move downward
            position.Y += DOWN_SPEED;
            position.X += horizontal_speed;

            UpdateFrame(gameTime);

            CheckorMissileCollision();

            CheckForCollisionWithPlayer();

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
                    // call missile and asteroid collision handle methods
                    Game.Services.GetService<ScoreDisplay>().AddScore(POINTS);
                    missile.HandleCollision();
                    this.HandleCollision();
                    i--;
                }
            }
        }

        /// <summary>
        /// Animation: Check interval and display a next frame 
        /// </summary>
        /// <param name="gameTime">Game Time</param>
        private void UpdateFrame(GameTime gameTime)
        {
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

        protected override void LoadContent()
        {
            // Single Texture List for Asteroid
            if (textures == null)
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
            }

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();

            sb.Begin();
            // Display an Asteroid
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

        /// <summary>
        /// Collision: Remove this Asteroid 
        /// and Create a Explosion component (Explosion animation)
        /// </summary>
        public void HandleCollision()
        {
            Game.Components.Remove(this);
            Game.Components.Add(new Explosion(Game, new Vector2(position.X, position.Y)));
        }
    }
}