using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SpaceInvadersLibrary
{
    public class Laser: IProjectile
    {
        private int velocity;
        private Rectangle boundingBox;
        private bool alive;
        private AlienSquad alienSquad;
        private Bunkers bunkers;
        private Mothership mothership;
        public Laser(int width, int height, Rectangle firingObject, int velocity,AlienSquad alienSquad, Bunkers bunkers, Mothership mothership) 
        {
            this.velocity = velocity;
            this.alienSquad = alienSquad;
            this.bunkers = bunkers;
            this.mothership = mothership;
            BoundingBox = new Rectangle(firingObject.X + ((firingObject.Width - width) / 2), firingObject.Y, width, height);
           
            alive = true;
        }

        public Rectangle BoundingBox
        {
            get { return boundingBox; }
            set { boundingBox = value; }
        }

        public bool Alive { get { return alive; } set { alive = value; } }

        public void Move()
        {
            boundingBox.Y += velocity;

            if (BoundingBox.Y < 0)
                alive = false;

            alienSquad.DetectCollision(this);
            bunkers.DetectCollision(this);
            mothership.DetectCollision(this);
        }
    }
}
