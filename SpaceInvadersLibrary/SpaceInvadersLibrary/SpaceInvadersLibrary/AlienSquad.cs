using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvadersLibrary
{
    public delegate void Handler();

    public class AlienSquad : ICollidableCollection
    {
        public event Collision GetShot;
        public event Handler NextWave;
        public event GameOver GameOver;
        public event Handler ReachedBunkers;

        private int screenWidth;
        private int screenHeight;
        private string currentDirection;
        private Alien[,] aliens;
        private int length;
        private Point point;
        private Point point2;
        static int alienW = 35;
        static int alienH = 25;
        static int space = 15;
        
        private bool hitWall;

        public AlienSquad(int screenWidth, int screenHeight, int speed)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            currentDirection = "RIGHT";
            aliens = new Alien[5, 11];
            length = aliens.Length;
            point = new Point((screenWidth - 535) / 2, screenHeight - 700);
            point2 = point;

            for (var i = 0; i < aliens.GetLength(0); i++)
            {
                for (var j = 0; j < aliens.GetLength(1); j++)
                {
                    point2 = new Point((point.X + j * (alienW + space)), point.Y+i*(alienH+space));
                    aliens[i, j] = new Alien(alienW, alienH, screenWidth, screenHeight, point2, speed);
                    aliens[i, j].Points += (i * 5);
                }
            }
        }
        public ICollidable this[int row, int col]
        {
            get { return aliens[row, col]; }
        }

        public Alien[,] Aliens { get { return aliens; } }

        public int Length { get { return length; } }

        public void Move()
        {
            for (var i = 0; i < aliens.GetLength(0); i++)
            {
                for (var j = 0; j < aliens.GetLength(1); j++)
                {
                    if (aliens[i, j].Alive)
                    {
                        aliens[i, j].Move(currentDirection);
                        if (aliens[i, j].BoundingBox.X == 0 || aliens[i, j].BoundingBox.X == (screenWidth - aliens[i, j].BoundingBox.Width))
                            hitWall = true;
                    }
                }
            }
            if (hitWall)
                MoveDown();
        }
        
        public void CreateAliens()
        {
            for (var i = 0; i < aliens.GetLength(0); i++)
                for (var j = 0; j < aliens.GetLength(1); j++)
                    aliens[i, j].Alive = true;
        }

        public void DetectCollision(Laser p)
        {
            for (var i = 0; i < aliens.GetLength(0); i++)
                for (var j = 0; j < aliens.GetLength(1); j++)
                    if(aliens[i,j].Alive)
                        if (aliens[i, j].BoundingBox.Intersects(p.BoundingBox))
                        {
                            p.Alive = false;
                            OnGetShot(aliens[i, j]);
                        }
                            
        }

        protected void OnGetShot(Alien b)
        {
            if (GetShot != null)
                GetShot(b); // executes observers' handlers
        }

        private void MoveDown()
        {
            for (var r = 0; r < aliens.GetLength(0); r++)
                for (var c = 0; c < aliens.GetLength(1); c++)
                    if (aliens[r, c].Alive)
                        aliens[r, c].MoveDown(currentDirection);

            if (currentDirection.Equals("RIGHT"))
                currentDirection = "LEFT";
            else
                currentDirection = "RIGHT";

            hitWall = false; 
        }

    }
}
