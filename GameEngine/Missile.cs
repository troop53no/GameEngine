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
    class Missile
    {
        private int x;//x position
        private int y;//y position
        bool visible;//is it on screen
        private int width;//width of the missile picture
        private int height;//height of the missile picture

        private const int bwidth = 1000;//width of the screen
        private const int speed = 15;//speed of the missile

        public Missile(int nx, int ny)
        {
            visible = true;//puts the missile into the game
            width = 8;
            height = 5;
            x = nx;//starting x position
            y = ny;//starting y position
        }

        //get the x position
        public int getX()
        {
            return x;
        }

        //get the y position
        public int getY()
        {
            return y;
        }

        //get the missile's visibility
        public bool isVisible()
        {
            return visible;
        }

        //set the missile's visibility
        public void setVisible(bool nvisible)
        {
            visible = nvisible;
        }

        //get the bounds of the missile for collision detection
        public Rectangle getBounds()
        {
            return new Rectangle(x, y, width, height);
        }

        //moves the missile
        public void move()
        {
            //move the missile
            x += speed;

            //remove the missile if it goes off screen
            if (x > bwidth)
            {
                visible = false;
            }
        }
    }
}


