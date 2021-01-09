/* 
 * Spacecraft.cs
 * Final Project: SpaceWar2020
 *                Spacecraft (player)
 * Revision History:
 *      Jiyoung Jung, 2020.12.03: Version 1.0
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
    /// Enum for displaying animation corresponding to Left/Right Direction of Spacecraft
    /// </summary>
    enum PlayerState
    {
        Idle,
        Right,
        Left
    }

    /// <summary>
    /// Spacecraft (Player)
    /// </summary>
    public class Spacecraft : DrawableGameComponent, ICollidable
    {
        /// <summary>
        /// Width of the Spacecraft texture
        /// </summary>
        const int WIDTH = 50;

        /// <summary>
        /// Height of the Spacecraft texture
        /// </summary>
        const int HEIGHT = 50;

        /// <summary>
        /// Left and Right offet of width for checking collision
        /// </summary>
        const int COLLISION_OFFSET_WIDTH = 5;

        /// <summary>
        /// Top and Bottom offet of height for checking collision
        /// </summary>
        const int COLLISION_OFFSET_HEIGHT = 5;

        /// <summary>
        /// Time interval between frames
        /// </summary>
        const double FRAME_DURATION = 0.2;

        /// <summary>
        /// Spacecraft speed for X, Y coordinate
        /// </summary>
        const int SPEED = 5;

        /// <summary>
        /// Time interval for shooting missile contineouly
        /// </summary>
        const double MISSILE_INTERVAL = 0.1;

        /// <summary>
        /// Texture list for animation corresponding to player horizontal direction  
        /// </summary>
        static Dictionary<PlayerState, List<Texture2D>> textures;

        /// <summary>
        /// parent Gamescene
        /// </summary>
        GameScene parent;

        /// <summary>
        /// enum variable for displaying animation corresponding to Left/Right Direction of Spacecraft
        /// </summary>
        PlayerState state;

        /// <summary>
        /// current Frame number
        /// </summary>
        int currentFrame = 0;

        /// <summary>
        /// Timer variable for checking a next frame time
        /// </summary>
        double frameTimer = 0;

        /// <summary>
        /// Current timer value for checking continuously shooting time interval
        /// </summary>
        double shootingTimer = 0;

        /// <summary>
        /// Current Position of a player
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// Collision Box 
        /// </summary>
        public Rectangle CollisionBox => new Rectangle((int)Position.X + COLLISION_OFFSET_WIDTH, 
                                                       (int)Position.Y + COLLISION_OFFSET_HEIGHT, 
                                                       WIDTH - 2 * COLLISION_OFFSET_WIDTH,
                                                       HEIGHT - 2 * COLLISION_OFFSET_HEIGHT);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">Game Entity</param>
        /// <param name="parent">parent GameScene</param>
        public Spacecraft(Game game, GameScene parent)
            : this(game, parent, Vector2.Zero)
        {
        }

        /// <summary>
        /// Constructor with a initial position
        /// </summary>
        /// <param name="game">Game Entity</param>
        /// <param name="parent">parent GameScene</param>
        /// <param name="position">Initial Position</param>
        public Spacecraft(Game game, GameScene parent, Vector2 position)
            : base(game)
        {
            this.parent = parent;
            this.Position = position;
            state = PlayerState.Idle;
            textures = new Dictionary<PlayerState, List<Texture2D>>();
        }

        public override void Update(GameTime gameTime)
        {
            UpdateUserInput(gameTime);

            UpdateFrames(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// Animation: Check interval and display a next frame 
        /// </summary>
        /// <param name="gameTime">Game Time</param>
        private void UpdateFrames(GameTime gameTime)
        {
            frameTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (frameTimer >= FRAME_DURATION &&
                currentFrame != textures[state].Count - 1)
            {
                frameTimer = 0;
                currentFrame++;
            }
        }

        /// <summary>
        /// Move Spacecraft by user key inputs (Up/Down/Left/Right keys)
        /// Fire missiles (Space key)
        /// </summary>
        /// <param name="gameTime"></param>
        private void UpdateUserInput(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.Right))
            {
                state = PlayerState.Right;
                Position.X += SPEED;
            }
            else if (ks.IsKeyDown(Keys.Left))
            {
                state = PlayerState.Left;
                Position.X -= SPEED;
            }
            else
            {
                state = PlayerState.Idle;
                currentFrame = 0;
            }

            if (ks.IsKeyDown(Keys.Up))
            {
                state = PlayerState.Idle;
                Position.Y -= SPEED;
                currentFrame = 0;
            }
            else if (ks.IsKeyDown(Keys.Down))
            {
                state = PlayerState.Idle;
                Position.Y += SPEED;
                currentFrame = 0;
            }

            // Space Key: Shoot Missile
            if (ks.IsKeyDown(Keys.Space))
            {
                // Check Shooting Time Inverval
                shootingTimer += gameTime.ElapsedGameTime.TotalSeconds;
                if(shootingTimer >= MISSILE_INTERVAL)
                {
                    if(Missile.missileLevel == 1)
                    {
                        // Create a Missile on the top of the player
                        Missile missile = new Missile(Game, new Vector2(Position.X + WIDTH / 2, Position.Y));
                        shootingTimer = 0;
                        parent.AddComponent(missile);
                    }else if(Missile.missileLevel == 2)
                    {
                        // Create a Missile on the top of the player
                        Missile missile1 = new Missile(Game, new Vector2(Position.X, Position.Y));
                        Missile missile2 = new Missile(Game, new Vector2(Position.X + WIDTH, Position.Y));
                        shootingTimer = 0;
                        parent.AddComponent(missile1);
                        parent.AddComponent(missile2);
                    }
                    
                }
            }

            // Check Boundary, a player cannot go out of the screen
            int screenWidth = Game.GraphicsDevice.Viewport.Width;
            int screenHeight = Game.GraphicsDevice.Viewport.Height;
            Position.X = MathHelper.Clamp(Position.X, 0, screenWidth - WIDTH);
            Position.Y = MathHelper.Clamp(Position.Y, 0, screenHeight - HEIGHT);
        }

        protected override void LoadContent()
        {
            if (textures.Count == 0)
            {
                // Load our Idle texture frames
                textures.Add(PlayerState.Idle, new List<Texture2D>());
                textures[PlayerState.Idle].Add(Game.Content.Load<Texture2D>(@"Spacecraft\redfighter0005"));

                // Load our Right textue frames
                textures.Add(PlayerState.Right, new List<Texture2D>());
                textures[PlayerState.Right].Add(Game.Content.Load<Texture2D>(@"Spacecraft\redfighter0006"));
                textures[PlayerState.Right].Add(Game.Content.Load<Texture2D>(@"Spacecraft\redfighter0007"));
                textures[PlayerState.Right].Add(Game.Content.Load<Texture2D>(@"Spacecraft\redfighter0008"));
                textures[PlayerState.Right].Add(Game.Content.Load<Texture2D>(@"Spacecraft\redfighter0009"));

                // Load our Left texture frames
                textures.Add(PlayerState.Left, new List<Texture2D>());
                textures[PlayerState.Left].Add(Game.Content.Load<Texture2D>(@"Spacecraft\redfighter0004"));
                textures[PlayerState.Left].Add(Game.Content.Load<Texture2D>(@"Spacecraft\redfighter0003"));
                textures[PlayerState.Left].Add(Game.Content.Load<Texture2D>(@"Spacecraft\redfighter0002"));
                textures[PlayerState.Left].Add(Game.Content.Load<Texture2D>(@"Spacecraft\redfighter0001"));
            }

            // Set initial position, horizontal center, bottom of the screen
            int screenWidth = Game.GraphicsDevice.Viewport.Width;
            int screenHeight = Game.GraphicsDevice.Viewport.Height;
            Position = new Vector2(screenWidth / 2 - WIDTH / 2,
                                    screenHeight - HEIGHT);

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();

            sb.Begin();
            // Display Spacecraft
            sb.Draw(textures[state][currentFrame], 
                    new Rectangle((int)Position.X, (int)Position.Y, WIDTH, HEIGHT), 
                    null, 
                    Color.White);            
            sb.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Collision: Remove Spacecraft 
        /// and Create a Explosion component (Explosion animation)
        /// </summary>
        public void HandleCollision()
        {
            Game.Components.Remove(this);
            Game.Services.RemoveService(this.GetType());
            parent.AddComponent(new Explosion(Game, new Vector2(Position.X, Position.Y)));
        }
    }
}
