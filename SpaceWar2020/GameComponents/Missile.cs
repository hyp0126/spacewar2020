/* 
 * Missile.cs
 * Final Project: SpaceWar2020
 *                Missile (player)
 * Revision History:
 *      Jiyoung Jung, 2020.11.14: Version 1.0
 *      
*/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWar2020
{
    /// <summary>
    /// Missile (from player)
    /// </summary>
    public class Missile : DrawableGameComponent, ICollidable
    {
        /// <summary>
        /// Width of the Missile texture
        /// </summary>
        const int WIDTH = 40;

        /// <summary>
        /// Height of the Missile texture
        /// </summary>
        const int HEIGHT = 40;

        /// <summary>
        /// Left and Right offet of width for checking collision
        /// </summary>
        const int COLLISION_OFFSET_WIDTH = 15;

        /// <summary>
        /// Top and Bottom offet of height for checking collision
        /// </summary>
        const int COLLISION_OFFSET_HEIGHT = 5;

        /// <summary>
        /// Fixed upwared speed
        /// </summary>
        const int SPEED = 5;

        /// <summary>
        /// Volume for Sound Effects 
        /// </summary>
        const float SFX_VOLUME = 0.1f;

        static Texture2D texture;
        Vector2 position;

        /// <summary>
        /// Shooting Sound Effect Instance
        /// </summary>
        static SoundEffect sfxShooting;

        /// <summary>
        /// Missile level
        /// </summary>
        public static int missileLevel = 1;

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
        public Missile(Game game)
            : this(game, Vector2.Zero)
        {
        }

        /// <summary>
        /// Constructor with a initial position
        /// </summary>
        /// <param name="game">Game Entity</param>
        /// <param name="position">Initial Position</param>
        public Missile(Game game, Vector2 position)
            : base(game)
        {
            // Set initial position: a missile is on top of the player
            this.position = new Vector2(position.X - WIDTH/2, position.Y - HEIGHT + 10);
        }

        public override void Update(GameTime gameTime)
        {
            // Move Upward
            position.Y -= SPEED;

            // Check boundary
            // If a missile is over the top of the game screen, remove it
            if(position.Y + HEIGHT == 0)
            {
                Game.Components.Remove(this);
            }

            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            // Single Texture for Missile
            if (texture == null)
            {
                texture = Game.Content.Load<Texture2D>(@"Missile\Missile05");
            }

            // Load Shooting Sound Effect, and Play it
            if (sfxShooting == null)
            {
                sfxShooting = Game.Content.Load<SoundEffect>(@"Sound\ShootingSound");
            }
            sfxShooting.Play(SFX_VOLUME, 0, 0);

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();

            sb.Begin();
            // Display a MIssile
            sb.Draw(texture, 
                    new Rectangle((int)position.X, (int)position.Y, WIDTH, HEIGHT),
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
