using SpaceInvadersLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceInvader
{
    class BombFactorySprite: DrawableGameComponent
    {
        

        private BombFactory bombFactory;

        private SpriteBatch spriteBatch;
        private Texture2D imageBomb;
        private Game1 game;

        private BunkerSprite bunkers;
        private PlayerSprite player;
        private AlienSquadSprite alienSquad;

        public BombFactorySprite(Game1 game, BunkerSprite bunkers, 
            PlayerSprite player, AlienSquadSprite alienSquad):base(game)
        {
            this.game = game;
            this.bunkers = bunkers;
            this.player = player;
            this.alienSquad = alienSquad;
        }
        public BombFactory BombFactory { get { return bombFactory; } }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            imageBomb = game.Content.Load<Texture2D>("laser");

            bombFactory = new BombFactory(player.PL, bunkers.BS, alienSquad.AS, GraphicsDevice.Viewport.Height);
           
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            bombFactory.TryRandomShoot();

            if (bombFactory.Bombs != null)
            {
                foreach (var each in bombFactory.Bombs)
                    if(each.Alive)
                        each.Move();
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            for (var i = 0; i < bombFactory.Bombs.Count; i++)
            {
                if (bombFactory.Bombs[i].Alive)
                {
                    spriteBatch.Draw(imageBomb,
                        new Vector2(bombFactory.Bombs[i].BoundingBox.X,
                            bombFactory.Bombs[i].BoundingBox.Y),
                            Color.White);
                }
            }
                  
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
