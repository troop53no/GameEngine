/*
*   Half Life 3 Confirmed
*   Created by David Graham from 2013-2016
*   Music by Joseph Gibson
*
*   Contact troop53no@gmail.com with subject
*   "Half Life 3 Confirmed: HELP" for any 
*   information or help related to the code
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameEngine
{
    class Bomb
    {
        private int x, y;//the coordinates of the bomb
        public Bomb(int X, int Y)
        {//creates a new bomb
            x = X - 100;//translates the x coordinate from the center to the corner
            y = Y - 100;//translates the y coordinate from the center to the corner
        }
        public int getX()
        {//returns the bomb's x location
            return x;
        }
        public int getY()
        {//returns the bomb's y location
            return y;
        }
        public Rectangle getBounds()
        {//returns the bomb's 
            return new Rectangle(x, y, 200, 200);
        }
    }
}