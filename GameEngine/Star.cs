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
    class Star
    {
        private int x;//x position of star
        private int y;//y position of the star
        private int speed;//speed of the star
        private Color color;

        public Star(int nx, int ny, Color nc, int ns)
        {
            x = nx;
            y = ny;
            color = nc;
            speed = ns;

        }

        //moves the star
        public void move()
        {
            x -= speed;
            //resets the star if it goes offscreen
            if (x < 0)
            {
                x = 1000;
            }
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

        //get the color
        public Color getColor()
        {
            return color;
        }
    }
}
