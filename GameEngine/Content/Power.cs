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

namespace GameEngineDll
{
    class Power
    {
        private int x;//position of alien ship
        private int y;//^
        private int width;//dimensions of alien ship picture
        private int height;//^
        private bool visible;//is it in game

        public Power(int nx, int ny)
        {
            width = 19;
            height = 19;
            visible = true;
            x = nx;
            y = ny;
        }


        public void move()
        {
            x -= 2;//moves from right to left
        }

        public int getX()
        {
            return x;
        }

        public int getY()
        {
            return y;
        }

        public bool isVisible()
        {
            return visible;
        }

        public void setVisible(bool nvisible)
        {
            visible = nvisible;
        }

        public Rectangle getBounds()
        {
            return new Rectangle(x, y, width, height);
        }
        public void reset()
        {
            x += 1000;//puts the ship off the screen on the right no matter where it currently is
        }
    }
}
