using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvadersLibrary
{
    public class Bunkers : ICollidableCollection
    {
        public event Collision GetShot;// declare related event

        private Bunker[,] bunkers;
        private int length;
        //static int bunkerW = 20;
        //static int bunkerH = 15;
        private Point point1;
        private Point point2;

        public Bunkers(int bunkerW, int bunkerH, int screenWidth, int screenHeight)
        {
            bunkers = new Bunker[4, 20];
            length = bunkers.Length;
            point1 = new Point((screenWidth - 760) / 2, screenHeight - 200);
            point2 = point1;


            for (var i = 0; i < bunkers.GetLength(0); i++)
                for (var j = 0; j < bunkers.GetLength(1); j++)
                {
                    point2 = new Point(point1.X + j * bunkerW + (j / 5) * 120, point1.Y + i * bunkerH);

                    bunkers[i, j] = new Bunker(bunkerW, bunkerH, point2);
                }
        }
        public Bunker[,] BunkersArray { get { return bunkers; } }

        public ICollidable this[int row, int col]
        {
            get { return bunkers[row, col]; }
        }

        public int Length { get { return length; } }

        public void DetectCollision(Laser p)
        {
            for (var i = 0; i < bunkers.GetLength(0); i++)
                for (var j = 0; j < bunkers.GetLength(1); j++)
                    if (p.BoundingBox.Intersects(bunkers[i, j].BoundingBox) && bunkers[i, j].Alive)
                    {
                        p.Alive = false;
                        OnGetShot(bunkers[i, j]);// fires event
                        break;
                    }
        }

        public void DetectCollision(Bomb bomb)
        {
            /*
            for (var k = 0; k < bombs.Count; k++)
                for (var i = 0; i < bunkers.GetLength(0); i++)
                    for (var j = 0; j < bunkers.GetLength(1); j++)
                        if (bombs[k].BoundingBox.Intersects(bunkers[i, j].BoundingBox))
                        {
                            */
                for (var i = 0; i < bunkers.GetLength(0); i++)
                    for (var j = 0; j < bunkers.GetLength(1); j++)
                        if (bomb.BoundingBox.Intersects(bunkers[i, j].BoundingBox) && bunkers[i, j].Alive)
                        {
                            bomb.Alive = false;
                            OnGetShot(bunkers[i, j]);// fires events
                        }
        }

        protected void OnGetShot(Bunker b)
        {
            if (GetShot != null)
                GetShot(b); // executes observers' handlers
        }

    }
}
