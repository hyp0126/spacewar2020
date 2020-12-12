/* 
 * Bullet.cs
 * Final Project: SpaceWar2020
 *                Bullet (enemy)
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
    /// Bullet (from Enemy)
    /// </summary>
    public class Bullet : DrawableGameComponent, ICollidable
    {
        /// <summary>
        /// Width of the Bullet texture
        /// </summary>
        public const int WIDTH = 10;

        /// <summary>
        /// Height of the Bullet texture
        /// </summary>
        public const int HEIGHT = 10;

        /// <summary>
        /// Left and Right offet of width for checking collision
        /// </summary>
        const int COLLISION_OFFSET_WIDTH = 1;

        /// <summary>
        /// Top and Bottom offet of height for checking collision
        /// </summary>
        const int COLLISION_OFFSET_HEIGHT = 1;

        /// <summary>
        /// Fixed downward speed
        /// </summary>
        const int DOWN_SPEED = 6;

        static Texture2D texture;
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
        public Bullet(Game game)
            : this(game, Vector2.Zero)
        {
        }

        /// <summary>
        /// Constructor with a initial position
        /// </summary>
        /// <param name="game">Game Entity</param>
        /// <param name="position">Initial Position</param>
        public Bullet(Game game, Vector2 position)
            : base(game)
        {
            this.position = position;
        }

        public override void Update(GameTime gameTime)
        {
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
            // Single Texture for Bullet
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
            // Display a Bullet
            sb.Draw(texture, new Rectangle((int)position.X, 
                                           (int)position.Y,
                                           WIDTH,
                                           HEIGHT),
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

