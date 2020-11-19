using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWar2020
{
    public class Asteroid : DrawableGameComponent
    {
        List<Texture2D> textures;
        int currentFrame = 0;

        double frameTimer = 0;
        const double FRAME_INTERVAL = 0.1;

        Vector2 position;

        public Asteroid(Game game)
            : this(game, Vector2.Zero)
        {
        }

        public Asteroid(Game game, Vector2 position)
            : base(game)
        {
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
            textures.Add(Game.Content.Load<Texture2D>("Asteroid\\a10000"));
            textures.Add(Game.Content.Load<Texture2D>("Asteroid\\a10002"));
            textures.Add(Game.Content.Load<Texture2D>("Asteroid\\a10004"));
            textures.Add(Game.Content.Load<Texture2D>("Asteroid\\a10006"));
            textures.Add(Game.Content.Load<Texture2D>("Asteroid\\a10008"));
            textures.Add(Game.Content.Load<Texture2D>("Asteroid\\a10010"));
            textures.Add(Game.Content.Load<Texture2D>("Asteroid\\a10012"));
            textures.Add(Game.Content.Load<Texture2D>("Asteroid\\a10014"));

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            sb.Begin();
            sb.Draw(textures[currentFrame], position, Color.White);
            sb.End();
            base.Draw(gameTime);
        }
    }
}