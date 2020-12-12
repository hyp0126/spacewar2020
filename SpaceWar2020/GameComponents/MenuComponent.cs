/* 
 * MenuComponent.cs
 * Final Project: SpaceWar2020
 *                Main Menu Component
 * Revision History:
 *      Originally from Course Material
 *      Yiphyo Hong, 2020.12.03: Modified, Version 1.1
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
    /// Enum for Menu
    /// </summary>
    public enum MenuSelection
    {
        StartGame,
        HighScore,
        Help,
        About,
        ExitGame
    }

    /// <summary>
    /// Component for manipulating menus in the main menu
    /// </summary>
    public class MenuComponent : DrawableGameComponent
    {
        /// <summary>
        /// Font for unseledted menu items
        /// </summary>
        SpriteFont regularFont;

        /// <summary>
        /// Font for a seledted menu item
        /// </summary>
        SpriteFont highlightFont;

        /// <summary>
        /// Sub-menu text list
        /// </summary>
        private List<string> menuItems;

        /// <summary>
        /// Selected menu index 
        /// </summary>
        private int selectedIndex;

        /// <summary>
        /// Main Menu initial position
        /// </summary>
        private Vector2 startingPosition;

        /// <summary>
        /// Font Color for unseledted menu items
        /// </summary>
        private Color regularColor = Color.Black;

        /// <summary>
        /// Font Color for a seledted menu item
        /// </summary>
        private Color hilightColor = Color.Red;

        /// <summary>
        /// previous keyboard state
        /// </summary>
        private KeyboardState prevKS;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">Game Entity</param>
        public MenuComponent(Game game) : base(game)
        {
            //Set sub-menu texts
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

        public override void Initialize()
        {
            // Assign starting position in the middle of screen
            SpriteFont font = Game.Content.Load<SpriteFont>(@"Fonts\regularFont");
            startingPosition = new Vector2(GraphicsDevice.Viewport.Width / 2 - font.MeasureString(menuItems[0]).X / 2,
                              GraphicsDevice.Viewport.Height / 2 - menuItems.Count * font.MeasureString(menuItems[0]).Y / 2);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Load 2 menu fonts (selected, unselected)
            regularFont = Game.Content.Load<SpriteFont>(@"Fonts\regularFont");
            highlightFont = Game.Content.Load<SpriteFont>(@"Fonts\hilightFont");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            // Down Key: go next menu
            if (ks.IsKeyDown(Keys.Down) && prevKS.IsKeyUp(Keys.Down))
            {
                selectedIndex++;

                // If last sub-menu, go to the first menu
                if (selectedIndex == menuItems.Count)
                {
                    selectedIndex = 0;
                }
            }

            // Up Key: go previous menu
            // Enter Key: enter the selected menu
            if (ks.IsKeyDown(Keys.Up) && prevKS.IsKeyUp(Keys.Up))
            {
                selectedIndex--;

                // If first sub-menu, go to the last menu              
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

        /// <summary>
        /// Change current Menu to a selected Scene
        /// </summary>
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
                    Game.Services.GetService<HighScoreScene>().Show();
                    break;
                case MenuSelection.About:
                    Game.Services.GetService<AboutScene>().Show();
                    break;
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
            // Display All menus
            for (int i = 0; i < menuItems.Count; i++)
            {
                SpriteFont activeFont = regularFont;
                Color activeColor = regularColor;

                // If selected menu, display it with special font and color
                // If not, display it with regualr font and color
                if (selectedIndex == i)
                {
                    activeFont = highlightFont;
                    activeColor = hilightColor;
                }
                sb.DrawString(activeFont, menuItems[i], nextPosition, activeColor);

                // Update the position of next string
                nextPosition.Y += regularFont.LineSpacing;
            }
            sb.End();

            base.Draw(gameTime);
        }
    }
}
