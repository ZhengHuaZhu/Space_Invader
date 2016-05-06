using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvadersLibrary
{
    public class LaserFactory
    {
        public event Collision GetScoreLife;// event declared for Score&Life

        private AlienSquad alienSquad;
        private Bunkers bunkers;
        private Mothership mothership;
        static int laserWidth = 2;
        static int laserHeight = 10;
        static int laserSpeed = -20;
        private Laser laser;

        public LaserFactory(ICollidableCollection alienSquad,
            ICollidableCollection bunkers, ICollidable mothership)
        {
            if (alienSquad is AlienSquad)
                this.alienSquad = (AlienSquad)alienSquad;
            if (bunkers is Bunkers)
                this.bunkers = (Bunkers)bunkers;
            if (mothership is Mothership)
                this.mothership = (Mothership)mothership;

            RegisterAlienSquad();
            RegisterBunkers();
            RegisterMothership();
        }

        public Laser Laser { get { return laser; } }

        public void Launch(Rectangle rect)
        {       
            if( laser == null || !Laser.Alive )
            {        
                //only allow launch if the current laser is off screen or dead
                laser = new Laser(laserWidth, laserHeight,
                rect, laserSpeed, alienSquad, bunkers,mothership);

                 //will be done by move method below in update
               
           
        }
        }

        //add public method that moves the laser if it is alive/on-screen, to be called by LaserFactorySprite update
        //add public properties for the laser's X and Y position, required for LaserFactorySprite draw

     
        public void RegisterAlienSquad()
        {
            alienSquad.GetShot += CollideWithLaser;
        }

        public void RegisterMothership()
        {
            mothership.GetShot += CollideWithLaser;
        }

        public void RegisterBunkers()
        {
            bunkers.GetShot += CollideWithLaser;
        }

        public void CollideWithLaser(ICollidable ic)
        {     
            if (ic is Bunker)
            {
                Bunker b=(Bunker)ic;
                b.Alive = false;
                OnGetScoreLife(b);
            }

            if (ic is Mothership)
            {
                Mothership ms=(Mothership)ic;
                ms.Alive = false;
                OnGetScoreLife(ms);
            }

            if (ic is Alien)
            {
                Alien a=(Alien)ic;
                a.Alive = false;
                OnGetScoreLife(a);
            }
        }

        protected void OnGetScoreLife(ICollidable ic)
        {
            if (GetScoreLife != null)
                GetScoreLife(ic);// executes handler in Score&Life
        }
    }
}
