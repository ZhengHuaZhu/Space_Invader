using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvadersLibrary
{
    public class Mothership : ICollidable
    {
        public event Collision GetShot;

        private int points;
        Random ran = new Random();
        private Rectangle boundingBox;
        private bool alive;
        private int speed;
        private int screenWidth;
        private string currentDirection;
        private int shipW;
        private int shipH;
        public Mothership(int shipW, int shipH, int speed, int screenW, int screenH)
        {
            points = 50;
            this.speed = speed;
            this.shipW = shipW;
            this.shipH = shipH;
            screenWidth = screenW;
            TrySpawn();

        }

        public Rectangle BoundingBox
        {
            get { return boundingBox; }
            set { boundingBox = value; }
        }

        public int Points { get { return points; } set { points = value; } }

        public bool Alive { get { return alive; } set { alive = value; } }

        public string CurrentDirection { get { return currentDirection; } set { currentDirection = value; } }

        public void Move() // control the ship within the screen width
        {

            if (currentDirection.Equals("LEFT"))
                boundingBox.X = BoundingBox.X - speed;

            else
                boundingBox.X = BoundingBox.X + speed;
            CheckGone();

        }
        public void CheckGone()
        {
            if (BoundingBox.X <= -BoundingBox.Width * 2)
            {
                Alive = false;
            }
            else if (BoundingBox.X >= (screenWidth + BoundingBox.Width * 2))
            {
                Alive = false;
            }
        }
        public void TrySpawn()
        {
            int trying = ran.Next(0, 250);

            if (trying == 1)
                randomSpawn();
        }
        private void randomSpawn()
        {

            int numb = ran.Next(1, 3);

            boundingBox = new Rectangle(0, 70, shipW, shipH);
            if (numb == 1)
            {
                boundingBox.X = screenWidth + boundingBox.Width;
                currentDirection = "LEFT";
            }
            else
                currentDirection = "RIGHT";

            alive = true;

        }

        public void DetectCollision(Laser p)
        {
            if (p.BoundingBox.Intersects(BoundingBox))
            {
                p.Alive = false;
                OnGetShot(this);
                Points *= 2;
            }
        }

        protected void OnGetShot(Mothership ms)
        {
            if (GetShot != null)
                GetShot(ms); // executes observers' handlers
        }
        public void IncreasePoints()
        {
            if (points >= 250)
                points = 50;
            else
                points += 50;
        }

    }
}
