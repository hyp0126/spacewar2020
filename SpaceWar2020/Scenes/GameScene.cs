/* 
 * GameScene.cs
 * Final Project: SpaceWar2020
 *                GameScene abstact class
 * Revision History: 
 *      Originally from Course Material
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
    public abstract class GameScene : GameComponent
    {
        /// <summary>
        /// Will clean up our scene components collection every 
        /// 1 second to delete any items no longer in the game
        /// </summary>
        const int CLEANUP_INTERVAL = 1;

        double cleanupTimer = 0.0;

        /// <summary>
        /// Used to hold a reference to the components that belong to 
        /// this GameScene instance.  Used to quickly iterate through components 
        /// that belong to the scence to enable and make visible where applicable, 
        /// or to hide.  If not in this collection, the items will not be disabled and 
        /// hidden with the scene when switching screens
        /// </summary>
        List<GameComponent> sceneComponents;

        public GameScene(Game game) : base(game)
        {
            sceneComponents = new List<GameComponent>();           
        }

        public override void Update(GameTime gameTime)
        {
            // Used to clean up our game components list
            // Iterate through what we have, make sure these are still 
            // in the game.  If not, remove them from our scene list
            cleanupTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if( cleanupTimer >= CLEANUP_INTERVAL)
            {
                cleanupTimer = 0.0;
                for (int i = 0; i < sceneComponents.Count; i++)
                {
                    if (Game.Components.Contains(sceneComponents[i]) == false)
                    {
                        sceneComponents.Remove(sceneComponents[i]);
                        i--;
                    }
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Shows this instance of game scene and all of its
        /// components.  Sets Enabled and Visible to true
        /// for all of the components that belong to this 
        /// scene
        /// </summary>
        public virtual void Show()
        {
            // Enabled -> if true, will call Update on the component
            Enabled = true;

            // iterate though all components held by this scene 
            // and set Enabled to true and if it's also a DrawableGameComponent
            // set Visible to true
            foreach( GameComponent component in sceneComponents)
            {
                component.Enabled = true;
                if( component is DrawableGameComponent drawable)
                {
                    // if true, will call Draw on the component
                    drawable.Visible = true;
                }
            }  
        }

        /// <summary>
        /// Hides this instance of game scene and all of its
        /// components.  Sets Enabled and Visible to false
        /// for all of the components that belong to this 
        /// scene
        /// </summary>
        public virtual void Hide()
        {
            // Enabled -> when false, will not call update on the component
            Enabled = false;

            // iterate though all components held by this scene 
            // and set Enabled to false and if it's also a DrawableGameComponent
            // set Visible to false
            foreach (GameComponent component in sceneComponents)
            {
                component.Enabled = false;
                if (component is DrawableGameComponent drawable)
                {
                    // when false, will not call Draw on the component
                    drawable.Visible = false;
                }
            }
        }
                

        /// <summary>
        /// Use only this method to add components 
        /// to the scene.  This method will add the component both
        /// to the game components and to internal collection
        /// of components this scene is responsible for
        /// </summary>
        /// <param name="component"></param>
        public void AddComponent(GameComponent component)
        {
            sceneComponents.Add(component);
            Game.Components.Add(component);
        }


        /// <summary>
        /// Counts how many components of a given type
        /// we are holding in this scene
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public int GetComponentCount(Type type)
        {
            int count = 0;
            foreach (GameComponent component in sceneComponents)
            {
                if (component.GetType() == type)
                {
                    count++;
                }
            }
            return count;
        }
    }
}
