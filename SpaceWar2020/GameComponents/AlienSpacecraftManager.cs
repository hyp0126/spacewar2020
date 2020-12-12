/* 
 * AlienSpacecraft.cs
 * Final Project: SpaceWar2020
 *                Alien Spacecraft Manager
 *                Create Alien Spacecraft
 * Revision History: 
 *      Yiphyo Hong, 2020.11.28: Version 1.0
 *      
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceWar2020
{
    /// <summary>
    /// Create Alien Spacecraft contineously
    ///  (random position and speed, fixed time interval)
    /// </summary>
    class AlienSpacecraftManager : GameComponent
    {
        /// <summary>
        /// Duration for creating Alien Spacecrafts
        /// </summary>
        const double CREATE_DUARTION = 1.5;

        /// <summary>
        /// Maximum number of Alien Spacecrafts at the same time in the game
        /// </summary>
        const int MAX_ALIEN_SPACECRAFT = 5;

        /// <summary>
        /// Random variable for new Alien Spacecraft X coordinate
        /// </summary>
        Random random;

        /// <summary>
        /// parent Gamescene
        /// </summary>
        GameScene parent;

        /// <summary>
        /// Timer variable for creating a new Alien Spacecraft
        /// </summary>
        double createSpacecraftTimer;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">Game Entity</param>
        /// <param name="parent">parent GameScene</param>
        public AlienSpacecraftManager(Game game, GameScene parent) 
            : base(game)
        {
            this.parent = parent;
            random = new Random();
            createSpacecraftTimer = 0;
        }

        public override void Initialize()
        {
            // Set random X coordinate for the first Alien Spacecraft
            int x = random.Next(AlienSpacecraft.WIDTH, 
                                Game.GraphicsDevice.Viewport.Width - AlienSpacecraft.WIDTH);
            // Y coordinate: over the top of the game screen 
            int y = -AlienSpacecraft.HEIGHT;
            // Add the first Alien Spacecraft
            parent.AddComponent(new AlienSpacecraft(Game, parent, new Vector2(x, y)));

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            // If current Alien Spacecrafts is under the game screen,
            //   Remove them
            List<AlienSpacecraft> alienSpacecrafts = (List<AlienSpacecraft>)Game.Components.OfType<AlienSpacecraft>().ToList();
            for (int i = 0; i < alienSpacecrafts.Count(); i++)
            {
                if (alienSpacecrafts[i].CollisionBox.Top > Game.GraphicsDevice.Viewport.Height)
                {
                    Game.Components.Remove(alienSpacecrafts[i]);
                }
            }

            // Create a New AlienSpaceCraft at regular interval
            createSpacecraftTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (createSpacecraftTimer > CREATE_DUARTION)
            {
                // Restrict the maximum number of Alien Spacecrafts
                if (Game.Components.OfType<AlienSpacecraft>().Count() < MAX_ALIEN_SPACECRAFT)
                {
                    // Create a new Alien Spacecraft (random X, top of the screen)
                    int x = random.Next(AlienSpacecraft.WIDTH,
                                        Game.GraphicsDevice.Viewport.Width - AlienSpacecraft.WIDTH);
                    int y = -AlienSpacecraft.HEIGHT;
                    parent.AddComponent(new AlienSpacecraft(Game, parent, new Vector2(x, y)));
                }
                createSpacecraftTimer = 0;
            }

            base.Update(gameTime);
        }
    }
}
