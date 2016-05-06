using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvadersLibrary
{
    public class BombFactory
    {
        public event Collision GetScoreLife;

        private Player player;
        private Bunkers bunkers;
        private AlienSquad alienSquad;
        private List<Bomb> bombs = new List<Bomb>();
        private int counter = 5;

        static int bombWidth = 3;
        static int bombHeight = 13;
        static int bombSpeed = 7;

        private Random random = new Random();
        private TimeSpan lastShot;
        private TimeSpan betweenShots;
        private DateTime dt;
        private int screenHeight;

        public BombFactory(Player player, ICollidableCollection bunkers,
            ICollidableCollection alienSquad, int screenHeight)
        {
            this.player = player;
            if (bunkers is Bunkers)
                this.bunkers = (Bunkers)bunkers;
            if (alienSquad is AlienSquad)
                this.alienSquad = (AlienSquad)alienSquad;

            this.screenHeight = screenHeight;

            RegisterPlayer();
            RegisterBunkers();
        }

        public List<Bomb> Bombs { get { return bombs; } }

        public Bomb this[int index]
        {
            get { return bombs[index]; }
            set { bombs[index] = value; }
        }

        public void Launch(Rectangle rect, int screenHeight)
        {
            Bomb bp = new Bomb(bombWidth, bombHeight,
                rect, bombSpeed, screenHeight, player, bunkers);
            bombs.Add(bp);
            //bp.Move();
        }

        public void RegisterPlayer()
        {
            player.GetShot += CollideWithBomb;
        }

        public void RegisterBunkers()
        {
            bunkers.GetShot += CollideWithBomb;
        }

        public void CollideWithBomb(ICollidable ic)
        {
            if (ic is Bunker)
            {
                Bunker b = (Bunker)ic;
                b.Alive = false;
                OnGetScoreLife(b);
            }

            if (ic is Player)
            {
                Player player = (Player)ic;
                player.Alive = false;
                OnGetScoreLife(player);
            }
        }

        protected void OnGetScoreLife(ICollidable ic)
        {
            if (GetScoreLife != null)
                GetScoreLife(ic);// executes handler in Score&Life
        }

        public void RandomShoot()
        {
            int randomshoot = random.Next(1, 2);
            lastShot = DateTime.Now - dt;
            int interval = random.Next(0, 15000000);
            betweenShots = new TimeSpan(interval);
            int rc, rr;

            removeDeadProjectiles();
           
            if (counter < bombs.Count)
                randomshoot = 0;

            while (randomshoot > 0)
            {
                rc = random.Next(0, 11); // random column
                rr = random.Next(0, 5); // random row
                  
                if (alienSquad.Aliens[rr, rc].Alive && lastShot > betweenShots)
                {
                    while (rr<(alienSquad.Aliens.GetLength(0)-1))
                    {
                        if (alienSquad.Aliens[rr + 1, rc].Alive)
                            rr++;
                        else
                            break;
                    }
                    Launch(alienSquad.Aliens[rr, rc].BoundingBox, screenHeight);
                    dt = DateTime.Now;
                }                        
                randomshoot--;             
            }
        }
        public void TryRandomShoot()
        {
            int trying = random.Next(0, 40);

            if (trying == 1)
                RandomShoot();
        }

       public void removeDeadProjectiles()
        {
            for (var i = 0; i < bombs.Count; i++)
            {
               /* if (bombs[i].Alive)
                {
                    if (bombs[i].BoundingBox.Y > screenHeight) 
                        bombs[i].Alive = false;
                }*/

                if (!bombs[i].Alive)
                    bombs.Remove(bombs[i]);
            }              
        }
        
    }
}
