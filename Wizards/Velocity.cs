using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wizards
{
    struct Velocity
    {
        public int speed;
        public Vector2 direction;

        public Velocity(int spd, Vector2 dir)
        {
            speed = spd;
            direction = dir;
        }
    }
}
