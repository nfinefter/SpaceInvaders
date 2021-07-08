using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders
{
    public class Bullet : Sprite
    {
        public Vector2 Speed;

        public Bullet(Vector2 position, Texture2D texture, Vector2 scale, Color tint, float rotation, Vector2 origin, Vector2 speed)
            : base(position, texture, scale, tint, rotation, origin)
        {
            Speed = speed;
        }
        public void Update()
        {
            Position += Speed;
        }
    }
}
