/* 
 * AboutScene.cs
 * Final Project: SpaceWar2020
 *                About sub-menu
 * Revision History:
 *      Jiyoung Jung, 2020.12.09: Version 1.0
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
    /// Sub-Menu: About (Display Develper names)
    /// </summary>
    class AboutScene : GameScene
    {
        // Default constructor
        public AboutScene(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            // Create, Add About Component
            AddComponent(new AboutTextComponent(Game));

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            // ESC key: exit this menu
            if (ks.IsKeyDown(Keys.Escape))
            {
                ((Game1)Game).HideAllScenes();
                Game.Services.GetService<StartScene>().Show();
            }

            base.Update(gameTime);
        }
    }
}
