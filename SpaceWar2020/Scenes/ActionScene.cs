
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
            Spacecraft spacecraft = new Spacecraft(Game, this);
            this.AddComponent(spacecraft);
            Game.Services.AddService<Spacecraft>(spacecraft);

            this.AddComponent(new AlienSpacecraftManager(Game, this));
            this.AddComponent(new AsteroidManager(Game, this));
            this.AddComponent(new ScoreDisplay(Game, this));
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            if (Game.Services.GetService<Spacecraft>() == null)
            {
                // Stop create enemy
                ClearManagers();

                int score = Game.Services.GetService<ScoreDisplay>().GetScore();
                if (!Game.Components.OfType<HighScoreInput>().Any())
                {
                    if (GameScore.CheckHighScore(score))
                    {
                        this.AddComponent(new HighScoreInput(Game));
                    }

                    if (ks.IsKeyDown(Keys.Enter))
                    {
                        // New Game
                        ClearGame();
                        StartNewGame();
                    }
                }
            }

            // handle the escape key for this scene
            if (ks.IsKeyDown(Keys.Escape))
            {
                // If Game end, delete all
                if (Game.Services.GetService<Spacecraft>() == null)
                {
                    // Game End
                    ClearGame();
                }

                ((Game1)Game).HideAllScenes();
                Game.Services.GetService<StartScene>().Show();
            }

            base.Update(gameTime);
        }

        private void ClearManagers()
        {
            List<AlienSpacecraftManager> alienSpacecraftManagers = (List<AlienSpacecraftManager>)Game.Components.OfType<AlienSpacecraftManager>().ToList();
            for (int i = 0; i < alienSpacecraftManagers.Count(); i++)
            {
                Game.Components.Remove(alienSpacecraftManagers[i]);
            }

            List<AsteroidManager> asteroidManagers = (List<AsteroidManager>)Game.Components.OfType<AsteroidManager>().ToList();
            for (int i = 0; i < asteroidManagers.Count(); i++)
            {
                Game.Components.Remove(asteroidManagers[i]);

            }
        }

        private void ClearGame()
        {
            ClearManagers();

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

            List<ScoreDisplay> scoreDisplays = Game.Components.OfType<ScoreDisplay>().ToList();
            for (int i = 0; i < scoreDisplays.Count(); i++)
            {
                Game.Components.Remove(scoreDisplays[i]);
            }

            List<HighScoreInput> highScoreInput = Game.Components.OfType<HighScoreInput>().ToList();
            for (int i = 0; i < highScoreInput.Count(); i++)
            {
                Game.Components.Remove(highScoreInput[i]);
            }
        }
    }
}
