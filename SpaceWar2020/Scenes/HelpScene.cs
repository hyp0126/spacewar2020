/* 
 * HelpScene.cs
 * Final Project: SpaceWar2020
 *                Help sub-menu
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
    /// Sub-Menu: Help
    /// Game Description, Explanation about Game, Keys 
    /// </summary>
    public class HelpScene : GameScene
    {
        // Default constructor
        public HelpScene(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            // Create, Add Help Component
            AddComponent(new HelpTextComponent(Game));
            
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
