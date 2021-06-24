using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders
{
    class Ship : Sprite
    {
        public Ship(Vector2 position, int width, int height, Vector2 speed)
            : base (position, width, height, speed)
        {

        }
    }
}
