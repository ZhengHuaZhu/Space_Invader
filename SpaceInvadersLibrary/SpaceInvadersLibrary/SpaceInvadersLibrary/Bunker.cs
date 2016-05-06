using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvadersLibrary
{
    public class Bunker : ICollidable
    {
        private Rectangle boundingBox;
        private int points;
        private bool alive;

        public Bunker(int w, int h, Point point)
        {
            boundingBox = new Rectangle(point.X, point.Y, w, h);
            points = 0;
            alive = true;
        }

        public Rectangle BoundingBox
        {
            get { return boundingBox; }
            set { boundingBox = value; }
        }

        public int Points { get { return points; } set { points = value; } }

        public bool Alive { get { return alive; } set { alive = value; } }

    }
}
