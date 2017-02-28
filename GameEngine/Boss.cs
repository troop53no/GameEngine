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
using System.Collections;
using Microsoft.Xna.Framework;

namespace GameEngine
{
    class Boss
    {
        private int x;
        private int y;
        private int dy = -1;
        private int width;
        private int height;
        private bool visible;
        private ArrayList missiles;
        private int rateOfFire;
        private int power;
        private int points;
        private int health;
        private int maxhealth;
        private bool fired = false;

        public Boss(int pow)
        {
            width = 36;
            height = 46;
            missiles = new ArrayList();
            if (pow == 0)
            {
                visible = false;
            }
            else {
                visible = true;
            }

            x = 1000;
            y = (680 - height) / 2;
            power = pow;
            points = 10000 * pow;
            health = 5000 * pow;
            maxhealth = health;
            rateOfFire = 0;
        }


        public void act()
        {
            if (x > 800)
            {
                --x;
            }
            if (x < 800)
            {
                ++x;
            }
            if (x == 800)
            {
                y += dy;
            }
            if (y < 1)
            {
                dy = 1;
            }
            if (y > 680 - height)
            {
                dy = -1;
            }
            if (rateOfFire > 0)
            {
                --rateOfFire;
            }
            else {
                fire();
                rateOfFire = 110 - power * 10;
            }
        }
        public int getX()
        {
            return x;
        }
        public int getY()
        {
            return y;
        }
        public int getHealth()
        {
            return health;
        }
        public int getMaxHealth()
        {
            return maxhealth;
        }
        public void hit()
        {
            health -= 10;
        }
        public int getPoints()
        {
            return points;
        }
        public ArrayList getMissiles()
        {
            return missiles;
        }
        public void setVisible(bool visible)
        {
            this.visible = visible;
        }
        public bool isVisible()
        {
            return visible;
        }
        public Rectangle getBounds()
        {
            return new Rectangle(x, y, width, height);
        }
        public void fire()
        {//amount of shots fired corressponds with the power of the ship
            if (visible)
            {
                if (power < 2)
                {//one missile fired
                    missiles.Add(new BossMissile(x + width, y + height / 2));
                }
                if (power == 2)
                {//two missiles fired
                    missiles.Add(new BossMissile(x + width, y + height / 2 - 5));
                    missiles.Add(new BossMissile(x + width, y + height / 2 + 5));
                }
                if (power == 3)
                {//3 missiles fired
                    missiles.Add(new BossMissile(x + width, y + height / 2 - 10));
                    missiles.Add(new BossMissile(x + width, y + height / 2 + 10));
                    missiles.Add(new BossMissile(x + width, y + height / 2));
                }
                if (power == 4)
                {//4 missiles fired
                    missiles.Add(new BossMissile(x + width, y + height / 2 - 10));
                    missiles.Add(new BossMissile(x + width, y + height / 2 + 10));
                    missiles.Add(new BossMissile(x + width, y + height / 2 - 20));
                    missiles.Add(new BossMissile(x + width, y + height / 2 + 20));
                }
                if (power > 4)
                {//5 missiles fired
                    missiles.Add(new BossMissile(x + width, y + height / 2 - 10));
                    missiles.Add(new BossMissile(x + width, y + height / 2 + 10));
                    missiles.Add(new BossMissile(x + width, y + height / 2));
                    missiles.Add(new BossMissile(x + width, y + height / 2 + 25));
                    missiles.Add(new BossMissile(x + width, y + height / 2 - 25));
                }
                fired = true;
            }
        }
        public void reset()
        {
            missiles = new ArrayList();
        }
        public bool getFire()
        {
            if (fired)
            {
                fired = false;
                return true;
            }
            else {
                return false;
            }
        }
    }
}