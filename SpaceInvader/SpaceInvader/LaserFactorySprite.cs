using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceInvadersLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvader
{
    class LaserFactorySprite: DrawableGameComponent
    {
        private LaserFactory laserFactory;

        private SpriteBatch spriteBatch;
        private Texture2D imageLaser;
        private Game1 game;

        private KeyboardState oldState;
        private int counter;
        private int threshold;

        private AlienSquadSprite alienSquad;
        private BunkerSprite bunkers;
        private MothershipSprite mothership;
        private PlayerSprite player;

        public LaserFactorySprite(Game1 game, AlienSquadSprite alienSquad, 
            BunkerSprite bunkers, MothershipSprite mothership, 
            PlayerSprite player):base(game)
        {
            this.game = game;
            this.alienSquad = alienSquad;
            this.bunkers = bunkers;
            this.mothership = mothership;
            this.player = player;
            
        }

        public LaserFactory LaserFactory { get { return laserFactory; } }

        public override void Initialize()
        {
            oldState = Keyboard.GetState(); // ?
            threshold = 6; // ?
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            imageLaser = game.Content.Load<Texture2D>("laser");

            laserFactory = new LaserFactory(alienSquad.AS, bunkers.BS, mothership.MS);
            
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            checkInput();
            if (laserFactory.Laser != null && laserFactory.Laser.Alive)
            {
                laserFactory.Laser.Move();
            }

            //move the lasers -> player.PL.laserfact.Move
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (laserFactory.Laser != null && laserFactory.Laser.Alive)
            {
                spriteBatch.Begin();
                //need to draw at player.PL.laserfact.X and Y

                spriteBatch.Draw(imageLaser, new Vector2(laserFactory.Laser.BoundingBox.X,
                    laserFactory.Laser.BoundingBox.Y), Color.White);

                spriteBatch.End();
            }
            base.Draw(gameTime);
        }

        private void checkInput()
        {
            KeyboardState newState = Keyboard.GetState();

            if (newState.IsKeyDown(Keys.Space))
            {
                // If not down last update, key has just been pressed.
                if (!oldState.IsKeyDown(Keys.Space))
                {
                    laserFactory.Launch(player.PL.BoundingBox);
                    counter = 0; //reset counter with every new keystroke
                }
                else
                {
                    counter++;
                    if (counter > threshold) 
                    {
                        laserFactory.Launch(player.PL.BoundingBox);
                    }
                }
            }
            // Once finished checking all keys, update old state.
            oldState = newState;
        }
    }
}
