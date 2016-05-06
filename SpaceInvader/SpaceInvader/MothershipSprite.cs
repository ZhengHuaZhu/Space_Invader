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
    class MothershipSprite : DrawableGameComponent
    {
        private Mothership mothership;

        private SpriteBatch spriteBatch;
        private Texture2D imageMothership;
        private Game1 game;
        private Random ran = new Random();


        public MothershipSprite(Game1 game)
            : base(game)
        {
            this.game = game;
        }

        public Mothership MS { get { return mothership; } }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            imageMothership = game.Content.Load<Texture2D>("mothership");

            mothership = new Mothership(imageMothership.Width,
                imageMothership.Height, 5, GraphicsDevice.Viewport.Width, 
                GraphicsDevice.Viewport.Height);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (!mothership.Alive)
                mothership.TrySpawn();
            else
                mothership.Move();
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (mothership.Alive)
            {
                spriteBatch.Begin();

                spriteBatch.Draw(imageMothership, 
                    new Vector2(mothership.BoundingBox.X, mothership.BoundingBox.Y), Color.Red);

                spriteBatch.End();
            }

            base.Draw(gameTime);
        }

    }
}
