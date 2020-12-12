/* 
 * ICollidable.cs
 * Final Project: SpaceWar2020
 *                Interface for manipulating Collisions 
 * Revision History: 
 *      originally from Course Meterial
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
    interface ICollidable
    {
        Rectangle CollisionBox { get; }
        void HandleCollision();
    }
}
