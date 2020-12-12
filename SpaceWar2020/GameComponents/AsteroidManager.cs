/* 
 * AsteroidManager.cs
 * Final Project: SpaceWar2020
 *                Asteroid Manager
 *                Create Asteroid
 * Revision History: 
 *      Yiphyo Hong, 2020.11.28: Version 1.0
 *      
*/
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWar2020
{
    /// <summary>
    /// Create Asteroid contineously
    ///  (random position and speed, fixed time interval)
    /// </summary>
    class AsteroidManager : GameComponent
    {
        /// <summary>
        /// Duration for creating Asteroids
        /// </summary>
        const double CREATE_DUARTION = 1;

        /// <summary>
        /// Maximum number of Asteroids at the same time in the game
        /// </summary>
        const int MAX_ASTEROID = 5;

        /// <summary>
        /// Random variable for new Asteroid X coordinate
        /// </summary>
        Random random;

        /// <summary>
        /// parent Gamescene
        /// </summary>
        GameScene parent;

        /// <summary>
        /// Timer variable for creating a new Asteroid
        /// </summary>
        double createTimer;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">Game Entity</param>
        /// <param name="parent">parent GameScene</param>
        public AsteroidManager(Game game, GameScene parent) : base(game)
        {
            this.parent = parent;
            random = new Random();
            createTimer = 0;
        }

        public override void Initialize()
        {
            // Set random X coordinate for the first Asteroid
            int x = random.Next(Asteroid.WIDTH,
                    Game.GraphicsDevice.Viewport.Width - Asteroid.WIDTH);
            // Y coordinate: over the top of the game screen 
            int y = -Asteroid.HEIGHT;
            // Add the first Asteroid
            parent.AddComponent(new Asteroid(Game, new Vector2(x, y)));

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            // If current Asteroids is under the game screen,
            //   Remove them
            List<Asteroid> asteroids = (List<Asteroid>)Game.Components.OfType<Asteroid>().ToList();
            for (int i = 0; i < asteroids.Count(); i++)
            {
                if (asteroids[i].CollisionBox.Top > Game.GraphicsDevice.Viewport.Height)
                {
                    Game.Components.Remove(asteroids[i]);
                }
                else if (asteroids[i].CollisionBox.Right < 0)
                {
                    Game.Components.Remove(asteroids[i]);
                }
                else if (asteroids[i].CollisionBox.Left > Game.GraphicsDevice.Viewport.Width)
                {
                    Game.Components.Remove(asteroids[i]);
                }
            }

            // Create a New Asteroid at regular interval
            createTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (createTimer > CREATE_DUARTION)
            {
                // Restrict the maximum number of Asteroids
                if (Game.Components.OfType<Asteroid>().Count() < MAX_ASTEROID)
                {
                    // Create a new Asteroid (random X, top of the screen)
                    int x = random.Next(Asteroid.WIDTH,
                                        Game.GraphicsDevice.Viewport.Width - Asteroid.WIDTH);
                    int y = -Asteroid.HEIGHT;
                    parent.AddComponent(new Asteroid(Game, new Vector2(x, y)));
                }
                createTimer = 0;

            }
            base.Update(gameTime);
        }
    }
}
