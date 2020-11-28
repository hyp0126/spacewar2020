using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWar2020
{
    class AsteroidManager : GameComponent
    {
        const double CREATE_DUARTION = 1;
        const int MAX_ASTEROID = 5;

        Random random;

        double createTimer;

        public AsteroidManager(Game game) : base(game)
        {
            random = new Random(1234);
            createTimer = 0;
        }
        public override void Initialize()
        {
            int x = random.Next(Asteroid.WIDTH,
                    Game.GraphicsDevice.Viewport.Width - Asteroid.WIDTH);
            int y = -Asteroid.HEIGHT;
            Game.Components.Add(new Asteroid(Game, new Vector2(x, y)));
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            // check boundary
            List<Asteroid> alienSpacecrafts = (List<Asteroid>)Game.Components.OfType<Asteroid>().ToList();
            for (int i = 0; i < alienSpacecrafts.Count(); i++)
            {
                if (alienSpacecrafts[i].CollisionBox.Top > Game.GraphicsDevice.Viewport.Height)
                {
                    Game.Components.Remove(alienSpacecrafts[i]);
                }
                else if (alienSpacecrafts[i].CollisionBox.Right < 0)
                {
                    Game.Components.Remove(alienSpacecrafts[i]);
                }
                else if (alienSpacecrafts[i].CollisionBox.Left > Game.GraphicsDevice.Viewport.Width)
                {
                    Game.Components.Remove(alienSpacecrafts[i]);
                }
            }

            // Create New AlienSpaceCraft
            createTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (createTimer > CREATE_DUARTION)
            {
                if (Game.Components.OfType<Asteroid>().Count() < MAX_ASTEROID)
                {
                    int x = random.Next(Asteroid.WIDTH,
                                        Game.GraphicsDevice.Viewport.Width - Asteroid.WIDTH);
                    int y = -Asteroid.HEIGHT;
                    Game.Components.Add(new Asteroid(Game, new Vector2(x, y)));
                }
                createTimer = 0;

            }
            base.Update(gameTime);
        }
    }
}
