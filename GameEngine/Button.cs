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
    class Button
    {
        private int x;
        private int y;
        private string name;

        public Button(int nx, int ny, string nname)
        {
            x = nx;
            y = ny;
            name = nname;
        }

        public int getX()
        {
            return x;
        }

        public int getY()
        {
            return y;
        }

        public string getName()
        {
            return name;
        }
    }
}
