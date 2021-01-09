/* 
 * Bullet.cs
 * Final Project: SpaceWar2020
 *                Bonus (upgrade missile)
 * Revision History: 
 *      Jiyoung Jung, 2020.12.24: Version 1.0
 *      
*/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWar2020
{
    /// <summary>
    /// Bonus (Gold Coin)
    /// </summary>
    public class Bonus : DrawableGameComponent, ICollidable
    {
        /// <summary>
        /// Width of the Bonus texture
        /// </summary>
        public const int WIDTH = 15;

        /// <summary>
        /// Height of the Bonus texture
        /// </summary>
        public const int HEIGHT = 15;

        /// <summary>
        /// Left and Right offet of width for checking collision
        /// </summary>
        const int COLLISION_OFFSET_WIDTH = 3;

        /// <summary>
        /// Timer variable for checking a next frame time
        /// </summary>
        double frameTimer = 0;

        /// <summary>
        /// Time interval between frames
        /// </summary>
        const double FRAME_INTERVAL = 0.1;

        /// <summary>
        /// current Frame number
        /// </summary>
        int currentFrame = 0;

        /// <summary>
        /// Explosion Sound Effect Instance
        /// </summary>
        static SoundEffect sfxExplosion;

        /// <summary>
        /// Volume for Sound Effects 
        /// </summary>
        const float SFX_VOLUME = 0.2f;

        /// <summary>
        /// Top and Bottom offet of height for checking collision
        /// </summary>
        const int COLLISION_OFFSET_HEIGHT = 1;

        /// <summary>
        /// Fixed downward speed
        /// </summary>
        const int DOWN_SPEED = 3;

        static List<Texture2D> textures;
        Vector2 position;

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
        public Bonus(Game game)
            : this(game, Vector2.Zero)
        {
        }

        /// <summary>
        /// Constructor with a initial position
        /// </summary>
        /// <param name="game">Game Entity</param>
        /// <param name="position">Initial Position</param>
        public Bonus(Game game, Vector2 position)
            : base(game)
        {
            this.position = position;
        }

        public override void Update(GameTime gameTime)
        {
            UpdateFrame(gameTime);

            // Move downward
            position.Y += DOWN_SPEED;

            CheckForCollisionWithPlayer();

            // Check Boundary
            // If bullets is under the bottom of screen, remove it
            if (this.CollisionBox.Top > Game.GraphicsDevice.Viewport.Height)
            {
                Game.Components.Remove(this);
            }

            base.Update(gameTime);
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
                    // Load Sound Effect and Play it
                    if (sfxExplosion == null)
                    {
                        sfxExplosion = Game.Content.Load<SoundEffect>(@"Sound\BonusSound");
                    }
                    sfxExplosion.Play(SFX_VOLUME, 0, 0);
                    //player.HandleCollision();
                    this.HandleCollision();

                    Missile.missileLevel++; 
                }
            }
        }

        protected override void LoadContent()
        {
            // Single Texture List for Explosion
            if (textures == null)
                {
                    textures = new List<Texture2D>();
                    textures.Add(Game.Content.Load<Texture2D>("Bonus\\bonus_1"));
                    textures.Add(Game.Content.Load<Texture2D>("Bonus\\bonus_2"));
                    textures.Add(Game.Content.Load<Texture2D>("Bonus\\bonus_3"));
                    textures.Add(Game.Content.Load<Texture2D>("Bonus\\bonus_4"));
                    textures.Add(Game.Content.Load<Texture2D>("Bonus\\bonus_5"));
                    textures.Add(Game.Content.Load<Texture2D>("Bonus\\bonus_6"));
                }

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            sb.Begin();
            // Display Bonus
             sb.Draw(textures[currentFrame],
                    new Rectangle((int)position.X, (int)position.Y, 50, 50),
                    null,
                    Color.White);
            
            sb.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Collision: Remove this Bullet 
        /// </summary>
        public void HandleCollision()
        {
            Game.Components.Remove(this);
        }
    }
}

