using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWar2020
{
    public class Spacecraft : DrawableGameComponent
    {
        List<Texture2D> textures;
        int currentFrame = 0;

        double frameTimer = 0;
        const double FRAME_INTERVAL = 0.1;

        Vector2 position;

        public Spacecraft(Game game)
            : this(game, Vector2.Zero)
        {
        }

        public Spacecraft(Game game, Vector2 position)
            : base(game)
        {
            DrawOrder = 1;
            this.position = position;
        }

        public override void Update(GameTime gameTime)
        {
            frameTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (frameTimer >= FRAME_INTERVAL)
            {
                frameTimer = 0;

                currentFrame++;
                if (currentFrame >= textures.Count)
                {
                    currentFrame = 0;
                }
            }
            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            textures = new List<Texture2D>();
            textures.Add(Game.Content.Load<Texture2D>("Spacecraft\\redfighter0001"));
            textures.Add(Game.Content.Load<Texture2D>("Spacecraft\\redfighter0002"));
            textures.Add(Game.Content.Load<Texture2D>("Spacecraft\\redfighter0003"));
            textures.Add(Game.Content.Load<Texture2D>("Spacecraft\\redfighter0004"));
            textures.Add(Game.Content.Load<Texture2D>("Spacecraft\\redfighter0005"));
            textures.Add(Game.Content.Load<Texture2D>("Spacecraft\\redfighter0006"));
            textures.Add(Game.Content.Load<Texture2D>("Spacecraft\\redfighter0007"));
            textures.Add(Game.Content.Load<Texture2D>("Spacecraft\\redfighter0008"));
            textures.Add(Game.Content.Load<Texture2D>("Spacecraft\\redfighter0009"));

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            sb.Begin();
            //sb.Draw(textures[currentFrame], position, Color.White);
            sb.Draw(textures[currentFrame], new Rectangle((int)position.X, (int)position.Y, 100, 100), null, Color.White);
            sb.End();
            base.Draw(gameTime);
        }
    }
}
