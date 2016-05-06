using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvadersLibrary;
using System.Threading;


namespace SpaceInvader
{
    class ScoreSprite : DrawableGameComponent
    {
        private SpriteFont fontScore;
        private SpriteFont fontLife;
        private int score;
        private SpriteBatch spriteBatch;
        private Game1 game;
        private ScoreLife scoreLife;
        private int life;
        private LaserFactorySprite laserFactory;
        private BombFactorySprite bombFactory;

        public ScoreSprite(Game1 game, LaserFactorySprite laser,BombFactorySprite bomb)
            : base(game)
        {
            this.game = game;
            this.laserFactory = laser;
            this.bombFactory = bomb;
            score = 0;
            life = 0;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
           // scoreLife
           fontScore = game.Content.Load<SpriteFont>("scoreFont");

           scoreLife = new ScoreLife(laserFactory.LaserFactory, bombFactory.BombFactory, 1);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            updateScore();
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(fontScore, "Score: " + score, new Vector2(20, 15), Color.White);
            spriteBatch.DrawString(fontScore, "Life: " + life, new Vector2(GraphicsDevice.Viewport.Width - 120, 20), Color.White);


            spriteBatch.End();
            base.Draw(gameTime);
        }


        private void updateScore()
        {
            score = scoreLife.Score;
        }
    }
}
