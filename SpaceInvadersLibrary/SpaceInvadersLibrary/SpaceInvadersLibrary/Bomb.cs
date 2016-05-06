using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
namespace SpaceInvadersLibrary
{
   public class Bomb : IProjectile
    {
        private int velocity;
        private Rectangle boundingBox;
        private bool alive;
        private int screenHeight;
        private Player player;
        private Bunkers bunkers;
        public Bomb(int width, int height, Rectangle firingObject, int velocity, int screenHeight, Player player, Bunkers bunkers) 
        {
            this.velocity = velocity;
            this.screenHeight = screenHeight;
            
            BoundingBox = new Rectangle(firingObject.X + ((firingObject.Width - width) / 2), firingObject.Y, width, height);           
            alive = true;
            this.bunkers = bunkers;
            this.player = player;
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
           
            if (BoundingBox.Y > screenHeight)
                alive = false;

            bunkers.DetectCollision(this);
            player.DetectCollision(this);
          
        }
    }
}
