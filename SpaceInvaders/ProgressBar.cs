﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders
{
    public class ProgressBar
    {
        public Vector2 Position;
        public int Value;
        public Vector2 Scale;
        Sprite background;
        Sprite foreground;


        public ProgressBar (Vector2 position, int value, Vector2 scale, Sprite background, Sprite foreground)
        {
            Position = position;
            Value = value;
            Scale = scale;
            this.background = background;
            this.foreground = foreground;
        }

        public void Draw(SpriteBatch sb)
        {
            background.Draw(sb);
            foreground.Draw(sb);

        }
        public void ChangePosition(Vector2 Position)
        {
            this.Position.X = Position.X;
            this.Position.Y = Position.Y;

            background.Position = Position;
            foreground.Position = Position;

        }

        //function to change progress amount

        public void ChangeProgressAmount(double product)
        {
            foreground.Scale = new Vector2((float)(product * Scale.X), Scale.Y );
        }

        //When adjusting progressbar values change foreground sprites x scale
    }
}
