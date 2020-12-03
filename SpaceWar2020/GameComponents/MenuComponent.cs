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
    public enum MenuSelection
    {
        StartGame,
        HighScore,
        Help,
        About,
        ExitGame
    }

    public class MenuComponent : DrawableGameComponent
    {

        SpriteFont regularFont;
        SpriteFont highlightFont;

        private List<string> menuItems;
        private int selectedIndex;
        private Vector2 startingPosition;

        private Color regularColor = Color.Black;
        private Color hilightColor = Color.Red;

        private KeyboardState prevKS;

        public MenuComponent(Game game) : base(game)
        {
            menuItems = new List<string>
            {
                "Start Game",
                "High Score",
                "Help",
                "About",
                "Exit Game"
            };
            prevKS = Keyboard.GetState();
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.Down) && prevKS.IsKeyUp(Keys.Down))
            {
                selectedIndex++;

                // if we're now out of bounds in our menu
                // items, reset to first item at index 0
                if (selectedIndex == menuItems.Count)
                {
                    selectedIndex = 0;
                }
            }

            if (ks.IsKeyDown(Keys.Up) && prevKS.IsKeyUp(Keys.Up))
            {
                selectedIndex--;

                // if we're now out of bounds at -1, 
                // move us to the last menu item index                
                if (selectedIndex == -1)
                {
                    selectedIndex = menuItems.Count - 1;
                }
            }
            else if (ks.IsKeyDown(Keys.Enter))
            {
                SwitchScenes();
            }
            prevKS = ks;


            base.Update(gameTime);
        }

        private void SwitchScenes()
        {
            ((Game1)Game).HideAllScenes();

            switch ((MenuSelection)selectedIndex)
            {
                case MenuSelection.StartGame:
                    Game.Services.GetService<ActionScene>().Show();
                    break;
                case MenuSelection.Help:
                    Game.Services.GetService<HelpScene>().Show();
                    break;
                case MenuSelection.ExitGame:
                    Game.Exit();
                    break;
                case MenuSelection.HighScore:
                    //To Do
                case MenuSelection.About:
                    //To Do
                default:
                    // for now there is nothing handling the other options
                    // we will simply show this screen again
                    Game.Services.GetService<StartScene>().Show();
                    break;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();

            Vector2 nextPosition = startingPosition;

            sb.Begin();

            for (int i = 0; i < menuItems.Count; i++)
            {
                SpriteFont activeFont = regularFont;
                Color activeColor = regularColor;

                // if the selection is the item we are drawing
                // made it a the special font and colour
                if (selectedIndex == i)
                {
                    activeFont = highlightFont;
                    activeColor = hilightColor;
                }

                sb.DrawString(activeFont, menuItems[i], nextPosition, activeColor);

                // update the position of next string
                nextPosition.Y += regularFont.LineSpacing;
            }

            sb.End();

            base.Draw(gameTime);
        }

        public override void Initialize()
        {
            // starting position of the menu items - but you can decise to put it elsewhere
            startingPosition = new Vector2(GraphicsDevice.Viewport.Width / 2,
                                      GraphicsDevice.Viewport.Height / 2);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // load the fonts we will be using for this menu
            regularFont = Game.Content.Load<SpriteFont>(@"Fonts\regularFont");
            highlightFont = Game.Content.Load<SpriteFont>(@"Fonts\hilightFont");
            base.LoadContent();
        }
    }
}
