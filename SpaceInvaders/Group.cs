using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders
{
    class Group
    {
        public Vector2 Position;
        public int WidthCount;
        public int HeightCount;
        public Invaders[,] Invaders;

        public Group (Vector2 position, int width, int height, Texture2D texture)
        {
            Position = position;
            WidthCount = width;
            HeightCount = height;
            Invaders = new Invaders[width, height];

            int x = 50;
            int y = 0;

            for (int i = 0; i < WidthCount; i++)
            {
                x = 0;
                y += 50;
                for (int j = 0; j < HeightCount; j++)
                {
                    Invaders[i, j] = new Invaders(new Vector2(x, y), texture, new Vector2(0.2f, 0.2f), Color.White, 0, new Vector2(0, 0), new Vector2(20, 0));
                    x += 50;
                }
            }
            
        }
        public void RemoveAt(int row, int column)
        {
            Invaders[row, column] = null;


            // Loop through every [i, column] and see if they are all null
            // if so, shrink the bounding box (which needs to be added)
            // only do this if its on the edge (0 or WidthCount - 1)

            // do this opposite for the row as well
        }
    }
}
