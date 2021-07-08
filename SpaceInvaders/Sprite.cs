using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders
{
    public class Sprite
    {
        public Vector2 Position { get; set; }
        Texture2D Texture { get; set; }
        Vector2 Scale { get; set; }
        Color Tint { get; set; }
        public float Rotation { get; set; }
        Vector2 Origin { get; set; }



        public float ScaledWidth
        {
            get
            {
                return Texture.Width * Scale.X;
            }
        }

        public float ScaledHeight
        {
            get
            {
                return Texture.Height * Scale.Y;
            }
        }

        public Rectangle HitBox
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, (int)ScaledWidth, (int)ScaledHeight);
            }
        }

        public Sprite(Vector2 position, Texture2D texture, Vector2 scale, Color tint, float rotation, Vector2 origin)
        {
            Position = position;
            Texture = texture;
            Scale = scale;
            Tint = tint;
            Rotation = rotation;
            Origin = origin;

        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(Texture, Position, null, Tint, Rotation, Origin, Scale, SpriteEffects.None, 0f);
        }
    }
}
