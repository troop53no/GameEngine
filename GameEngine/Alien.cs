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
    class Alien
    {
        private int points;//how many points the ship is worth if it's destroyed
        private int x;//position of alien ship
        private int y;//^
        private int width;//dimensions of alien ship picture
        private int height;//^
        private bool visible;//is it in game

        public Alien(int nx, int ny)
        {
            width = 10;
            height = 9;
            visible = true;
            x = nx;
            y = ny;
            points = 100;
        }


        public void move()
        {
            if (x < 0)
            {
                reset();
                if (points > 0)
                {
                    points -= 5;
                }
            }
            x -= 2;
        }

        public int getPoints()
        {
            return points;
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
        {//returns the bounds of the ship for collision detection
            return new Rectangle(x, y, width, height);
        }

        public void reset()
        {//puts the ship off the screen on the right no matter where it currently is
            x += 1000;
        }
    }
}