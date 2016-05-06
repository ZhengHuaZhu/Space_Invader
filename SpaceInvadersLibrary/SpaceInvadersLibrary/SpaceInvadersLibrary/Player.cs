using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvadersLibrary
{
    public delegate void Collision(ICollidable ic);

    public class Player : ICollidable
    {
        public event Collision GetShot;

        private int playerW;
        private int playerH;
        private Rectangle boundingBox;
        private int speed;
        private int screenWidth;
        private int points;
        private bool alive;

        public Player(int playerW, int playerH, int screenWidth, 
            Point startpoint, int speed) 
        {
            this.playerW = playerW;
            this.playerH = playerH;
            boundingBox = new Rectangle(startpoint.X, startpoint.Y, playerW, playerH);
            this.speed = speed;
            this.screenWidth = screenWidth;
            points = 0;
            alive = true;
        }

        public Rectangle BoundingBox { get { return boundingBox; } 
            set { boundingBox = value; } }

        public int Points { get { return points; } set { points = value; } }

        public bool Alive { get { return alive; } set { alive = value; } }

        public void MoveLeft() // control the player within the screen width
        {
            boundingBox.X = System.Math.Max(BoundingBox.X - speed, 0);
        }

        public void MoveRight()
        {
            boundingBox.X = System.Math.Min(BoundingBox.X + speed, 
                screenWidth - BoundingBox.Width);
        }

        public void DetectCollision(Bomb bombs)
        {
            /*for (var i = 0; i < bombs.Count; i++)
                if (BoundingBox.Intersects(bombs[i].BoundingBox))
                {
                    bombs[i].Alive = false;
                    bombs.Remove(bombs[i]);
                    OnGetShot(this);
                    break;
                }*/
            if (BoundingBox.Intersects(bombs.BoundingBox))
            {
                bombs.Alive = false;
                OnGetShot(this);
            }

        }

        protected void OnGetShot(Player player)
        {
            if (GetShot != null)
                GetShot(player); // executes observers' handlers
        }

    }
}
