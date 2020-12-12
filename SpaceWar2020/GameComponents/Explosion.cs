/* 
 * Explosion.cs
 * Final Project: SpaceWar2020
 *                Explosion component
 * Revision History: 
 *      Yiphyo Hong, 2020.11.14: Version 1.0
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
    /// Expolsion Animation
    /// </summary>
    public class Explosion : DrawableGameComponent
    {
        /// <summary>
        /// Fixed downward speed
        /// </summary>
        const int DOWN_SPEED = 3;

        /// <summary>
        /// Time interval between frames
        /// </summary>
        const double FRAME_INTERVAL = 0.1;

        /// <summary>
        /// Volume for Sound Effects 
        /// </summary>
        const float SFX_VOLUME = 0.1f;

        static List<Texture2D> textures;
        Vector2 position;

        /// <summary>
        /// Explosion Sound Effect Instance
        /// </summary>
        static SoundEffect sfxExplosion;

        /// <summary>
        /// current Frame number
        /// </summary>
        int currentFrame = 0;

        /// <summary>
        /// Timer variable for checking a next frame time
        /// </summary>
        double frameTimer = 0;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">Game Entity</param>
        public Explosion(Game game)
            : this(game, Vector2.Zero)
        {
        }

        /// <summary>
        /// Constructor with a initial position
        /// </summary>
        /// <param name="game">Game Entity</param>
        /// <param name="position">Initial Position</param>
        public Explosion(Game game, Vector2 position)
            : base(game)
        {
            this.position = position;
        }

        public override void Update(GameTime gameTime)
        {
            // Move downward
            position.Y += DOWN_SPEED;

            UpdateFrame(gameTime);

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
                    Game.Components.Remove(this);
                }
            }
        }

        protected override void LoadContent()
        {
            // Single Texture List for Explosion
            if (textures == null)
            {
                textures = new List<Texture2D>();
                textures.Add(Game.Content.Load<Texture2D>("Explosion\\explosion1_0001"));
                textures.Add(Game.Content.Load<Texture2D>("Explosion\\explosion1_0010"));
                textures.Add(Game.Content.Load<Texture2D>("Explosion\\explosion1_0020"));
                textures.Add(Game.Content.Load<Texture2D>("Explosion\\explosion1_0030"));
                textures.Add(Game.Content.Load<Texture2D>("Explosion\\explosion1_0040"));
                textures.Add(Game.Content.Load<Texture2D>("Explosion\\explosion1_0050"));
                textures.Add(Game.Content.Load<Texture2D>("Explosion\\explosion1_0060"));
                textures.Add(Game.Content.Load<Texture2D>("Explosion\\explosion1_0070"));
                textures.Add(Game.Content.Load<Texture2D>("Explosion\\explosion1_0080"));
                textures.Add(Game.Content.Load<Texture2D>("Explosion\\explosion1_0090"));
            }

            // Load Sound Effect and Play it
            if (sfxExplosion == null)
            {
                sfxExplosion = Game.Content.Load<SoundEffect>(@"Sound\CollisionSound");
            }
            sfxExplosion.Play(SFX_VOLUME, 0, 0);

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();

            sb.Begin();
            // Display Explosion
            sb.Draw(textures[currentFrame],
                    new Rectangle((int)position.X, (int)position.Y, 100, 100),
                    null,
                    Color.White);
            sb.End();

            base.Draw(gameTime);
        }
    }
}
