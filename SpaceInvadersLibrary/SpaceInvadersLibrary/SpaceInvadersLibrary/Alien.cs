using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvadersLibrary
{
    public class Alien : ICollidable
    {
        public event Handler ReachBunkers;

        //private int alienW;
        //private int alienH;
        private Rectangle boundingBox;
        private int speed;
        private int screenWidth;
        private int screenHeight;
        private int points;
        private bool alive;
        private BombFactory bomb;
        private static int oneDown = 10;
        private static int bunkerHeight = 400;

        public Alien(int alienW, int alienH, int screenWidth,
            int screenHeight, Point point, int speed)
        {
            //this.alienW = alienW;
            //this.alienH = alienH;
            boundingBox = new Rectangle(point.X, point.Y, alienW, alienH);
            this.speed = speed;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            points = 5;
            alive = true;
        }

        public Rectangle BoundingBox
        {
            get { return boundingBox; }
            set { boundingBox = value; }
        }

        public int Points { get { return points; } set { points = value; } }

        public bool Alive { get { return alive; } set { alive = value; } }

        public void Move(string direction) // control the alien within the screen width
        {
            if(direction.Equals("LEFT"))
                boundingBox.X = System.Math.Max(BoundingBox.X - speed, 0);
        
            if(direction.Equals("RIGHT"))
                boundingBox.X = System.Math.Min(BoundingBox.X + speed,
                screenWidth - BoundingBox.Width);
        }

        public void MoveDown(string direction)
        {
            boundingBox.Y += oneDown;
                
            if (BoundingBox.Y > bunkerHeight)
                OnReachBunkers();
        }

        protected void OnReachBunkers()
        {
            if (ReachBunkers != null)
                ReachBunkers();
        }
    }
}
