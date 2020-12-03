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
            // create and add any components that belong to this scene
            AddComponent(new MenuComponent(Game));
            
            base.Initialize();
        }
        
    }
}
