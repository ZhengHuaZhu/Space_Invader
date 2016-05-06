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
    public class PlayerSprite : DrawableGameComponent
    {
        private Player player;

        private SpriteBatch spriteBatch;
        private Texture2D imagePlayer;
        private Game1 game;

        private KeyboardState oldState;
        private int counter;
        private int threshold;

        public PlayerSprite(Game1 game) 
            : base(game)
        {
            this.game = game;
        }

        public Player PL { get { return player; } }

        public override void Initialize()
        {
            oldState = Keyboard.GetState(); // ?
            threshold = 6; // ?
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            imagePlayer = game.Content.Load<Texture2D>("player");

            player = new Player(imagePlayer.Width, imagePlayer.Height,
                GraphicsDevice.Viewport.Width, 
                new Point(0, GraphicsDevice.Viewport.Height-60), 5);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            checkInput(); // detect key stroke
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(imagePlayer, 
                new Vector2(player.BoundingBox.X, player.BoundingBox.Y), 
                Color.Green);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void checkInput()
        {
            KeyboardState newState = Keyboard.GetState();

            if (newState.IsKeyDown(Keys.Right))
            {
                // If not down last update, key has just been pressed.
                if (!oldState.IsKeyDown(Keys.Right))
                {
                    player.MoveRight();
                    counter = 0; //reset counter with every new keystroke
                }
                else
                {
                    counter++;
                    if (counter > threshold)
                        player.MoveRight();
                }
            }

            // Improve/change the code above to also check for Keys.Left
            if (newState.IsKeyDown(Keys.Left))
            {
                // If not down last update, key has just been pressed.
                if (!oldState.IsKeyDown(Keys.Left))
                {
                    player.MoveLeft();
                    counter = 0; //reset counter with every new keystroke
                }
                else
                {
                    counter++;
                    if (counter > threshold)
                        player.MoveLeft();
                }
            }

            // Once finished checking all keys, update old state.
            oldState = newState;
        }
    }
}
