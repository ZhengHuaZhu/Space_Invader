using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvadersLibrary
{
    public delegate void GameOver();

    public class ScoreLife
    {
        public event GameOver GameOver;
 
        private int score;
        private int wave;
        private int lives = 3;

        public ScoreLife(LaserFactory lf, BombFactory bf, int wave)
        {
            score = 0;

            this.wave = wave;
            lf.GetScoreLife += ChangeScoreLife;
            bf.GetScoreLife += ChangeScoreLife;
        }

        public int Lives { get { return lives; } set { lives = value; } }

        public int Score { get { return score; } set { score = value; } }
        
        public void ChangeScoreLife(ICollidable ic)
        {
            if (ic is Mothership)
                Score += ((Mothership)ic).Points;

            if (ic is Alien)
                Score += ((Alien)ic).Points;

            if (ic is Player)
                Lives--;

            if (Lives < 0)
                OnGameOver();
        }

        protected void OnGameOver()
        {
            if (GameOver != null)
                GameOver();
        }
    }
}
