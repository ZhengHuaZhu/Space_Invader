using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SpaceInvadersLibrary;


namespace SpaceInvader
{
    class BunkerSprite : DrawableGameComponent
    {
        private Bunkers bunkers;  //the business logic

        //to render
        private SpriteBatch spriteBatch;
        private Texture2D imageBunker;
        private Game1 game;

        public BunkerSprite(Game1 game):base(game)
        {
            this.game = game;
        }

        public Bunkers BS { get { return bunkers; } }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            imageBunker = game.Content.Load<Texture2D>("bunker");

            bunkers = new Bunkers(imageBunker.Width, imageBunker.Height, 
                GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
                   
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            for (int i = 0; i < bunkers.BunkersArray.GetLength(0); i++)
            {
                for (int j = 0; j < bunkers.BunkersArray.GetLength(1); j++)
                {
                    if(bunkers.BunkersArray[i,j].Alive)
                    spriteBatch.Draw(imageBunker, 
                        new Vector2(bunkers.BunkersArray[i, j].BoundingBox.X, 
                            bunkers.BunkersArray[i, j].BoundingBox.Y), Color.Green);
                }
            }                      
            spriteBatch.End();        
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

    }
}
