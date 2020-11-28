using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWar2020
{
    public class Explosion : DrawableGameComponent
    {
        const int DOWN_SPEED = 3;
        const double FRAME_INTERVAL = 0.1;
        const float SFX_VOLUME = 0.1f;

        static List<Texture2D> textures;
        int currentFrame = 0;

        double frameTimer = 0;

        Vector2 position;

        static SoundEffect sfxExplosion;

        public Explosion(Game game)
            : this(game, Vector2.Zero)
        {
        }

        public Explosion(Game game, Vector2 position)
            : base(game)
        {
            this.position = position;
        }

        public override void Update(GameTime gameTime)
        {
            position.Y += DOWN_SPEED;

            frameTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (frameTimer >= FRAME_INTERVAL)
            {
                frameTimer = 0;

                currentFrame++;
                if (currentFrame >= textures.Count)
                {
                    //currentFrame = 0;
                    Game.Components.Remove(this);
                }
            }
            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            if (textures == null)
            {
                textures = new List<Texture2D>();
                textures.Add(Game.Content.Load<Texture2D>("Explosion\\explosion1_0001"));
                textures.Add(Game.Content.Load<Texture2D>("Explosion\\explosion1_0010"));
                textures.Add(Game.Content.Load<Texture2D>("Explosion\\explosion1_0020"));
                textures.Add(Game.Content.Load<Texture2D>("Explosion\\explosion1_0030"));
                textures.Add(Game.Content.Load<Texture2D>("Explosion\\explosion1_0040"));
                textures.Add(Game.Content.Load<Texture2D>("Explosion\\explosion1_0050"));
                textures.Add(Game.Content.Load<Texture2D>("Explosion\\explosion1_0060"));
                textures.Add(Game.Content.Load<Texture2D>("Explosion\\explosion1_0070"));
                textures.Add(Game.Content.Load<Texture2D>("Explosion\\explosion1_0080"));
                textures.Add(Game.Content.Load<Texture2D>("Explosion\\explosion1_0090"));
            }

            if (sfxExplosion == null)
            {
                sfxExplosion = Game.Content.Load<SoundEffect>(@"Sound\CollisionSound");
            }
            sfxExplosion.Play(SFX_VOLUME, 0, 0);

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();

            sb.Begin();
            //sb.Draw(textures[currentFrame], position, Color.White);
            sb.Draw(textures[currentFrame],
                    new Rectangle((int)position.X, (int)position.Y, 100, 100),
                    null,
                    Color.White);
            sb.End();

            base.Draw(gameTime);
        }
    }
}
