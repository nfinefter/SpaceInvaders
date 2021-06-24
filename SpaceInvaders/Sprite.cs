using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders
{
    class Sprite
    {
        public Vector2 Speed;
        public Vector2 Position;
        public int Width;
        public int Height;

        public Sprite (Vector2 position, int width, int height, Vector2 speed)
        {
            Position = position;
            Width = width;
            Height = height;
            Speed = speed;
        }
    }
}
