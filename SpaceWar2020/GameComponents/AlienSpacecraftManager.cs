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
    class AlienSpacecraftManager : GameComponent
    {
        const double CREATE_DUARTION = 1.5;
        const int MAX_ALIEN_SPACECRAFT = 5;

        Random random;
        GameScene parent;

        double creatSpacecrafteTimer;

        public AlienSpacecraftManager(Game game, GameScene parent) 
            : base(game)
        {
            this.parent = parent;
            random = new Random(5678);
            creatSpacecrafteTimer = 0;
        }

        public override void Initialize()
        {
            int x = random.Next(AlienSpacecraft.WIDTH, 
                                Game.GraphicsDevice.Viewport.Width - AlienSpacecraft.WIDTH);
            int y = -AlienSpacecraft.HEIGHT;
            parent.AddComponent(new AlienSpacecraft(Game, parent, new Vector2(x, y)));
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            // check boundary
            List<AlienSpacecraft> alienSpacecrafts = (List<AlienSpacecraft>)Game.Components.OfType<AlienSpacecraft>().ToList();
            for (int i = 0; i < alienSpacecrafts.Count(); i++)
            {
                if (alienSpacecrafts[i].CollisionBox.Top > Game.GraphicsDevice.Viewport.Height)
                {
                    Game.Components.Remove(alienSpacecrafts[i]);
                }
            }

            // Create New AlienSpaceCraft
            creatSpacecrafteTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (creatSpacecrafteTimer > CREATE_DUARTION)
            {
                if (Game.Components.OfType<AlienSpacecraft>().Count() < MAX_ALIEN_SPACECRAFT)
                {
                    int x = random.Next(AlienSpacecraft.WIDTH,
                                        Game.GraphicsDevice.Viewport.Width - AlienSpacecraft.WIDTH);
                    int y = -AlienSpacecraft.HEIGHT;
                    parent.AddComponent(new AlienSpacecraft(Game, parent, new Vector2(x, y)));
                }
                creatSpacecrafteTimer = 0;
            }

            base.Update(gameTime);
        }
    }
}
