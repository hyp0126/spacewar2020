using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWar2020
{
    public class Missile : DrawableGameComponent, ICollidable
    {
        const int WIDTH = 40;
        const int HEIGHT = 40;
        const int SPEED = 5;
        const float SFX_VOLUME = 0.1f;
        const double TIME_INTERVAL = 0.1;

        static Texture2D texture;
        Vector2 position;
        double timer = 0;

        static SoundEffect sfxShooting;

        public Rectangle CollisionBox => new Rectangle((int)position.X, (int)position.Y, WIDTH, HEIGHT);

        public Missile(Game game)
            : this(game, Vector2.Zero)
        {
        }

        public Missile(Game game, Vector2 position)
            : base(game)
        {
            this.position = new Vector2(position.X - WIDTH/2, position.Y - HEIGHT + 10);
        }

        public override void Update(GameTime gameTime)
        {

            timer += gameTime.ElapsedGameTime.TotalSeconds;

            if( timer >= TIME_INTERVAL)
            {
                position.Y -= SPEED;
            }

            if(position.Y + HEIGHT == 0)
            {
                Game.Components.Remove(this);
                //Game.Services.RemoveService(this.GetType());
            }

            base.Update(gameTime);
        }

        protected override void LoadContent()
        {

            if (texture == null)
            {
                texture = Game.Content.Load<Texture2D>(@"Missile\Missile05");
            }

            if (sfxShooting == null)
            {
                sfxShooting = Game.Content.Load<SoundEffect>(@"Sound\ShootingSound");
            }

            sfxShooting.Play(SFX_VOLUME, 0, 0);
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();

            sb.Begin();
            //sb.Draw(texture, position, Color.White);
            sb.Draw(texture, 
                    new Rectangle((int)position.X, (int)position.Y, WIDTH, HEIGHT),
                    null,
                    Color.White);
            sb.End();
                        
            base.Draw(gameTime);
        }

        public void HandleCollision()
        {
            Game.Components.Remove(this);
        }
    }
}
