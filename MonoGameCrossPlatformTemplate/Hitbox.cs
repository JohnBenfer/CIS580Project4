using System;
using Microsoft.Xna.Framework;

namespace CIS580Project4
{
    public class Hitbox
    {
        public Rectangle box;
        public Hitbox(int height, int width, int x, int y)
        {
            box = new Rectangle((int)(x - (0.5 * width)), (int)(y - (0.5 * height)), width, height);
        }

        public void Move(int x, int y)
        {
            box.X = (int)(x - (0.5 * box.Width));
            box.Y = (int)(y - (0.5 * box.Height));
        }
    }
}
