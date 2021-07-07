﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asteroid
{
    class Asteroid : Sprite
    {
        public Asteroid(Vector2 position, Texture2D texture, Vector2 scale, Color tint, float rotation, Vector2 origin)
            : base(position, texture, scale, tint, rotation, origin)
        {

        }
    }
}
