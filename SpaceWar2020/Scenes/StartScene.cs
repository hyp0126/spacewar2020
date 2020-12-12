/* 
 * StartScene.cs
 * Final Project: SpaceWar2020
 *                Main Menu
 * Revision History:
 *      Yiphyo Hong, 2020.12.03: Version 1.0
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

    public class StartScene : GameScene
    {
        public StartScene(Game game): base(game)
        {          
        }

        public override void Initialize()
        {
            // Create, Add Main Menu Component
            AddComponent(new MenuComponent(Game));
            
            base.Initialize();
        }
        
    }
}
