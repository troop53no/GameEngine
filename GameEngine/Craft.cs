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
using Microsoft.Xna.Framework.Input;
using System.Collections;

namespace GameEngine
{
    class Craft
    {
        private bool fired;//is the craft firing missiles?
        private int dx;//x speed
        private int dy;//y speed
        private int x;//x position
        private int y;//y position
        private int width;//width of the ship picture
        private int height;//height of the ship picture
        private bool visible;//is the ship in game
        private ArrayList missiles;//array of missiles that have been fired that are in game
        private int rateOfFire;//how often you are able to fire a missile
        private int power;//power level of the ship
        //private int health;//not implemented

        public Craft(int pow, int nx, int ny)
        {
            width = 20;
            height = 20;
            visible = true;
            missiles = new ArrayList();//creates missile array
            x = nx;//starting x position
            y = ny;//starting y position
            rateOfFire = 0;//able to fire right away
            power = pow;//sets the power
            fired = false;//not currently firing
        }

        //move the ship, and other calculations
        public void move()
        {
            //rate of fire timer
            if (rateOfFire > 0)
                --rateOfFire;

            //changes the ship position
            x += dx;
            y += dy;

            //prevent the ship from moving off the screen
            if (x < 1)
                x = 1;
            if (y < 1)
                y = 1;
            if (y > 675 - height)
                y = 675 - height;
            if (x > 1000 - width)
                x = 1000 - width;
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

        //get the array of missiles
        public ArrayList getMissiles()
        {
            for (int i = 0; i < missiles.Count; ++i)
            {
                Missile m = (missiles[i] as Missile);
                if (!m.isVisible())
                {
                    missiles.RemoveAt(i);
                    --i;
                }
            }
            return missiles;
        }

        //set the visibility of the ship
        public void setVisible(bool visible)
        {
            this.visible = visible;
        }

        //get the visibility of the ship
        public bool isVisible()
        {
            return visible;
        }

        //get the bounds of the ship for collision detection
        public Rectangle getBounds()
        {
            return new Rectangle(x, y, width, height);
        }

        //keyboard interactions
        public void keyEvent(KeyboardState keyState)
        {
            //fires a missile, updates rate of fire
            if (keyState.IsKeyDown(Keys.Space))
            {
                if (visible)
                {
                    if (rateOfFire == 0)
                    {
                        fire();
                        rateOfFire = 5;//rate of fire.  higher numbers, equals less bullets fired per second (default: 5)
                    }
                }
            }

            //move left
            if (keyState.IsKeyDown(Keys.A))
            {
                if (visible)
                {
                    dx = -6;
                }
            }

            //move right
            if (keyState.IsKeyDown(Keys.D))
            {
                if (visible)
                {
                    dx = 6;
                }
            }


            //move up
            if (keyState.IsKeyDown(Keys.W))
            {
                if (visible)
                {
                    dy = -6;
                }
            }

            //move down
            if (keyState.IsKeyDown(Keys.S))
            {
                if (visible)
                {
                    dy = 6;
                }
            }
            
            //resets dx and dy if keys are released, or if interfering ones are pressed
            if ((keyState.IsKeyUp(Keys.A) && keyState.IsKeyUp(Keys.D)) || (keyState.IsKeyDown(Keys.A) && keyState.IsKeyDown(Keys.D)))
            {
                dx = 0;
            }
            if ((keyState.IsKeyUp(Keys.W) && keyState.IsKeyUp(Keys.S)) || (keyState.IsKeyDown(Keys.W) && keyState.IsKeyDown(Keys.S)))
            {
                dy = 0;
            }
        }

        public void padEvent(GamePadState padState)
        {
            //fires a missile, updates rate of fire
            if (padState.Triggers.Right > 0)
            {
                if (visible)
                {
                    if (rateOfFire == 0)
                    {
                        fire();
                        rateOfFire = 5;//rate of fire.  higher numbers, equals less bullets fired per second (default: 5)
                    }
                }
            }

            //move left/right and up/down
            if (visible)
            {
                dx = (int)(6.0f * padState.ThumbSticks.Left.X);
                dy = (int)(-6.0f * padState.ThumbSticks.Left.Y);
            }
        }

        //fire a missile
        public void fire()
        {
            if (visible)
            {
                if (power < 2)
                {
                    missiles.Add(new Missile(x + width, y + height / 2));
                }
                if (power == 2)
                {
                    missiles.Add(new Missile(x + width, y + height / 2 - 5));
                    missiles.Add(new Missile(x + width, y + height / 2 + 5));
                }
                if (power == 3)
                {
                    missiles.Add(new Missile(x + width, y + height / 2 - 10));
                    missiles.Add(new Missile(x + width, y + height / 2 + 10));
                    missiles.Add(new Missile(x + width, y + height / 2));
                }
                if (power == 4)
                {
                    missiles.Add(new Missile(x + width, y + height / 2 - 10));
                    missiles.Add(new Missile(x + width, y + height / 2 + 10));
                    missiles.Add(new Missile(x + width, y + height / 2 - 20));
                    missiles.Add(new Missile(x + width, y + height / 2 + 20));
                }
                if (power > 4)
                {
                    missiles.Add(new Missile(x + width, y + height / 2 - 10));
                    missiles.Add(new Missile(x + width, y + height / 2 + 10));
                    missiles.Add(new Missile(x + width, y + height / 2));
                    missiles.Add(new Missile(x + width, y + height / 2 + 25));
                    missiles.Add(new Missile(x + width, y + height / 2 - 25));

                    //I added these to test how much my computer could take before it would crash
                    //missiles.Add(new Missile(x + width, y + height/2+40));
                    //missiles.Add(new Missile(x + width, y + height/2-40));
                    //missiles.Add(new Missile(x + width, y + height/2+30));
                    //missiles.Add(new Missile(x + width, y + height/2-30));
                    //missiles.Add(new Missile(x + width, y + height/2+15));
                    //missiles.Add(new Missile(x + width, y + height/2-15));
                }
                fired = true;
            }
        }

        //get the ship's state of fire
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