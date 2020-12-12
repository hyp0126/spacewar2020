/* 
 * HighScoreScene.cs
 * Final Project: SpaceWar2020
 *                High Score sub-menu
 * Revision History:
 *      Yiphyo Hong, 2020.12.03: Version 1.0
 *      
*/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWar2020
{
    class HighScoreScene : GameScene
    {
        public HighScoreScene(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            // Create, Add Main Menu Component
            AddComponent(new HighScoreComponent(Game));

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
