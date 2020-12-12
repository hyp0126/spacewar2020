/* 
 * HelpTextComponent.cs
 * Final Project: SpaceWar2020
 *                Help sub-menu component
 * Revision History:
 *      Jiyoung Jung, 2020.12.09: Version 1.0
 *      
*/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceWar2020
{
    /// <summary>
    /// Help Sub-Menu: Display Game Desciption, Rules, and Keys for playing game
    /// </summary>
    class HelpTextComponent : DrawableGameComponent
    {
        Texture2D texture;

        // Default constructor
        public HelpTextComponent(Game game) : base(game)
        {
        }


        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = Game.Services.GetService<SpriteBatch>();

            spriteBatch.Begin();
            // Display Game Help bitmap
            spriteBatch.Draw(texture, Vector2.Zero, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
        

        protected override void LoadContent()
        {
            // Load Game Help bitmap
            texture = Game.Content.Load<Texture2D>("Images/helpImage");
            base.LoadContent();
        }
    }
}
