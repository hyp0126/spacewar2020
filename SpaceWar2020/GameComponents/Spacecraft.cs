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
    enum PlayerState
    {
        Idle,
        Right,
        Left
    }
    public class Spacecraft : DrawableGameComponent, ICollidable
    {
        const int WIDTH = 50;
        const int HEIGHT = 50;
        const int COLLISION_OFFSET_WIDTH = 5;
        const int COLLISION_OFFSET_HEIGHT = 5;
        const double FRAME_DURATION = 0.2;
        const int SPEED = 5;
        const double MISSILE_INTERVAL = 0.1;

        GameScene parent;
        static Dictionary<PlayerState, List<Texture2D>> textures;
        PlayerState state;
        int currentFrame = 0;
        double frameTimer = 0;
        double timer = 0;

        public Vector2 position;

        public int ScreenWidth => Game.GraphicsDevice.Viewport.Width;
        public int ScreenHeight => Game.GraphicsDevice.Viewport.Height;

        public Rectangle CollisionBox => new Rectangle((int)position.X + COLLISION_OFFSET_WIDTH, 
                                                       (int)position.Y + COLLISION_OFFSET_HEIGHT, 
                                                       WIDTH - 2 * COLLISION_OFFSET_WIDTH,
                                                       HEIGHT - 2 * COLLISION_OFFSET_HEIGHT);

        public Spacecraft(Game game, GameScene parent)
            : this(game, parent, Vector2.Zero)
        {
        }

        public Spacecraft(Game game, GameScene parent, Vector2 position)
            : base(game)
        {
            this.parent = parent;
            this.position = position;
            state = PlayerState.Idle;
            textures = new Dictionary<PlayerState, List<Texture2D>>();
        }

        public override void Update(GameTime gameTime)
        {
            UpdateUserInput(gameTime);

            UpdateFrames(gameTime);

            base.Update(gameTime);
        }

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

        private void UpdateUserInput(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Right))
            {
                state = PlayerState.Right;
                position.X += SPEED;
            }
            else if (ks.IsKeyDown(Keys.Left))
            {
                state = PlayerState.Left;
                position.X -= SPEED;
            }
            else
            {

                state = PlayerState.Idle;
                currentFrame = 0;
            }

            if (ks.IsKeyDown(Keys.Up))
            {
                state = PlayerState.Idle;
                position.Y -= SPEED;
                currentFrame = 0;
            }
            else if (ks.IsKeyDown(Keys.Down))
            {
                state = PlayerState.Idle;
                position.Y += SPEED;
                currentFrame = 0;
            }

            if (ks.IsKeyDown(Keys.Space))
            {
                timer += gameTime.ElapsedGameTime.TotalSeconds;
                if(timer >= MISSILE_INTERVAL)
                {
                    Missile missile = new Missile(Game, new Vector2(position.X + WIDTH / 2, position.Y));
                    timer = 0;
                    parent.AddComponent(missile);
                }
            }

            position.X = MathHelper.Clamp(position.X, 0, ScreenWidth - WIDTH);
            position.Y = MathHelper.Clamp(position.Y, 0, ScreenHeight - HEIGHT);
        }

        protected override void LoadContent()
        {
            if (textures.Count == 0)
            {
                // load our Idle texture frames
                textures.Add(PlayerState.Idle, new List<Texture2D>());
                textures[PlayerState.Idle].Add(Game.Content.Load<Texture2D>(@"Spacecraft\redfighter0005"));

                // load our Right textue frames
                textures.Add(PlayerState.Right, new List<Texture2D>());
                textures[PlayerState.Right].Add(Game.Content.Load<Texture2D>(@"Spacecraft\redfighter0006"));
                textures[PlayerState.Right].Add(Game.Content.Load<Texture2D>(@"Spacecraft\redfighter0007"));
                textures[PlayerState.Right].Add(Game.Content.Load<Texture2D>(@"Spacecraft\redfighter0008"));
                textures[PlayerState.Right].Add(Game.Content.Load<Texture2D>(@"Spacecraft\redfighter0009"));

                // load our Left texture frames
                textures.Add(PlayerState.Left, new List<Texture2D>());
                textures[PlayerState.Left].Add(Game.Content.Load<Texture2D>(@"Spacecraft\redfighter0004"));
                textures[PlayerState.Left].Add(Game.Content.Load<Texture2D>(@"Spacecraft\redfighter0003"));
                textures[PlayerState.Left].Add(Game.Content.Load<Texture2D>(@"Spacecraft\redfighter0002"));
                textures[PlayerState.Left].Add(Game.Content.Load<Texture2D>(@"Spacecraft\redfighter0001"));
            }

            position = new Vector2(ScreenWidth / 2 - WIDTH / 2,
                                    ScreenHeight - HEIGHT);


            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();

            sb.Begin();
            sb.Draw(textures[state][currentFrame], 
                    new Rectangle((int)position.X, (int)position.Y, WIDTH, HEIGHT), 
                    null, 
                    Color.White);            
            sb.End();

            base.Draw(gameTime);
        }

        public void HandleCollision()
        {
            Game.Components.Remove(this);
            Game.Services.RemoveService(this.GetType());

            parent.AddComponent(new Explosion(Game, new Vector2(position.X, position.Y)));
        }
    }
}
