using FinalProjectShell;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectShell
{
    class EnemyManager : GameComponent
    {

        const double CREATION_INTERVAL = 0.8;
        double timer = 0.0;
        Random random = new Random();

        GameScene parent;

        public EnemyManager(Game game, GameScene parent) : base(game)
        {
            this.parent = parent;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            // Count up until it's time to create a new
            // coin
            timer += gameTime.ElapsedGameTime.TotalSeconds;
            if (timer >= CREATION_INTERVAL)
            {
                timer = 0;
                // Create a new coin at some random location
                parent.AddComponent(new Enemies(Game, GetRandomEnemyType(), GetRandomLocation()));
                
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// Given CoinType enum, selects a random
        /// one from it
        /// </summary>
        /// <returns></returns>
        private EnemyType GetRandomEnemyType()
        {
            int enemyMax = Enum.GetNames(typeof(EnemyType)).Length;
            int randomEnemy = random.Next(0, enemyMax);
            return (EnemyType)randomEnemy;
        }

        /// <summary>
        /// Based on current viewport dimensions, generates a 
        /// random Vector2 that fits within the Viewport
        /// </summary>
        /// <returns></returns>
        private Vector2 GetRandomLocation()
        {
            int randomX = random.Next(0, Game.GraphicsDevice.Viewport.Width);
            int randomY = random.Next(0, Game.GraphicsDevice.Viewport.Height);
            
            Hero player = Game.Services.GetService<Hero>();
            Rectangle playerBoundary = player.hero.Bounds;
            playerBoundary.Location = player.position.ToPoint();
            
            if (playerBoundary.Contains(new Vector2(randomX,randomY))
                || playerBoundary.Contains(new Vector2(randomX, randomY+ playerBoundary.Height))
                || playerBoundary.Contains(new Vector2(randomX + playerBoundary.Width, randomY)))
            {
                if(randomX < Game.GraphicsDevice.Viewport.Width / 2)
                {
                    randomX = randomX + playerBoundary.Width*2;
                }
                else
                {
                    randomX = randomX - (playerBoundary.Width*2);
                }

                if (randomY < Game.GraphicsDevice.Viewport.Height / 2)
                {
                    randomY = randomY + playerBoundary.Height*2;
                }
                else
                {
                    randomY = randomX - (playerBoundary.Height*2);
                }
                return new Vector2(randomX, randomY);
            }
            else
            {
                return new Vector2(randomX, randomY);
            }

        }
    }
}
