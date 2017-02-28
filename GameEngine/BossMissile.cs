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
    class BossMissile
    {
        private int x;
        private int y;
        private bool visible;
        private int width;
        private int height;

        private const int bwidth = 1000;
        private const int speed = 5;

        public BossMissile(int nx, int ny)
        {
            visible = true;
            width = 13;
            height = 5;
            x = nx;
            y = ny;
        }


        public int getX()
        {//returns the missile's x position
            return x;
        }

        public int getY()
        {//returns the missile's y position
            return y;
        }

        public bool isVisible()
        {//returns the missile's visibility
            return visible;
        }

        public void setVisible(bool nvisible)
        {//sets the missile's visibility
            visible = nvisible;
        }

        public Rectangle getBounds()
        {//returns the bounds of the missile for collision detection
            return new Rectangle(x, y, width, height);
        }

        public void move()
        {//moves the missile
            x -= speed;//missile moves from left to right
            if (x < 0)
            {//is the missile off the right of the screen?
                visible = false;//removes missile from the screen
            }
        }
    }
}