
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace SpaceWar2020
{
    public class ActionScene : GameScene
    {
        public ActionScene(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            // create and add any components that belong to this scene
            // Add them by calling AddComponent(GameComponent component) 
            // method.  It will take care of adding the component to the 
            // game as well as keeping track of what belongs to it.

            this.AddComponent(new Background(Game));

            StartNewGame();

            base.Initialize();
        }

        private void StartNewGame()
        {
            Spacecraft spacecraft = new Spacecraft(Game, this, new Vector2(200, 300));
            this.AddComponent(spacecraft);
            Game.Services.AddService<Spacecraft>(spacecraft);

            this.AddComponent(new AlienSpacecraftManager(Game, this));
            this.AddComponent(new AsteroidManager(Game, this));
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            // handle the escape key for this scene
            if (ks.IsKeyDown(Keys.Escape))
            {
                // If Game end, delete all
                if (Game.Services.GetService<Spacecraft>() == null)
                {
                    // Game End
                    ClearGame();

                    // Display "New Game : Enter Key"
                }

                ((Game1)Game).HideAllScenes();
                Game.Services.GetService<StartScene>().Show();
            }

            if (ks.IsKeyDown(Keys.Enter))
            {
                if (Game.Services.GetService<Spacecraft>() == null)
                {
                    // New Game
                    ClearGame();
                    StartNewGame();
                }
            }

            base.Update(gameTime);
        }

        private void ClearGame()
        {
            List<AlienSpacecraft> alienSpacecrafts = (List<AlienSpacecraft>)Game.Components.OfType<AlienSpacecraft>().ToList();
            for (int i = 0; i < alienSpacecrafts.Count(); i++)
            {
                Game.Components.Remove(alienSpacecrafts[i]);
            }
            List<Asteroid> asteroids = (List<Asteroid>)Game.Components.OfType<Asteroid>().ToList();
            for (int i = 0; i < asteroids.Count(); i++)
            {
                Game.Components.Remove(asteroids[i]);
            }
            List<Bullet> bullets = (List<Bullet>)Game.Components.OfType<Bullet>().ToList();
            for (int i = 0; i < bullets.Count(); i++)
            {
                Game.Components.Remove(bullets[i]);
            }
            List<Explosion> explosions = (List<Explosion>)Game.Components.OfType<Explosion>().ToList();
            for (int i = 0; i < explosions.Count(); i++)
            {
                Game.Components.Remove(explosions[i]);
            }
            List<Missile> mossiles = (List<Missile>)Game.Components.OfType<Missile>().ToList();
            for (int i = 0; i < mossiles.Count(); i++)
            {
                Game.Components.Remove(mossiles[i]);
            }
        }
    }
}
