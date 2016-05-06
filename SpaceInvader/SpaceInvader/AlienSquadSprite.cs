using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceInvadersLibrary;

namespace SpaceInvader
{
    class AlienSquadSprite: DrawableGameComponent
    {
        private AlienSquad alienSquad;
        private MothershipSprite ms;

        private SpriteBatch spriteBatch;
        private Texture2D imageAlien;
        private Game1 game;

        public AlienSquadSprite(Game1 game, MothershipSprite ms):base(game)
        {
            this.game = game;
            this.ms = ms;
        }

        public AlienSquad AS { get { return alienSquad; } }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            imageAlien = game.Content.Load<Texture2D>("squad");

            alienSquad = new AlienSquad(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, 3);
                   
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            for (int i = 0; i < alienSquad.Aliens.GetLength(0); i++)
            {
                for (int j = 0; j < alienSquad.Aliens.GetLength(1); j++)
                {
                    if (alienSquad.Aliens[i, j].Alive)
                    spriteBatch.Draw(imageAlien, alienSquad.Aliens[i,j].BoundingBox, Color.White);
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            alienSquad.Move();
            base.Update(gameTime);
        }

    }
}
