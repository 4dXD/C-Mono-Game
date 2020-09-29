using FinalProjectShell;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectShell
{
    internal class Enemies : DrawableGameComponent
    {
        internal int speed;
        bool enemyActive = true;

        // Type of coin we are
        EnemyType enemyType = EnemyType.fly;

        // Starting position of our coin
        Vector2 position = Vector2.Zero;

        // animation state
        int currentFrame = 0;
        const double FRAME_DURATION = 0.2;
        double frameTimer = 0.0;
        bool forwardFrame = true;

        // Coin assets
        const int MAX_FRAME = 2;
        List<Texture2D> textures;
        SoundEffect bombSound;
        SoundEffect impactSound;
        double ENEMY_INTERVAL = 2.5;

        double timeSinceEnemyAppear = 0.0;

        public override void Initialize()
        {
            textures = new List<Texture2D>();
            base.Initialize();
        }

        public Enemies(Game game, EnemyType enemyType, Vector2 position) : base(game)
        {
            this.position = position;
            this.enemyType = enemyType;
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            sb.Begin();

            if (enemyActive)
            //if (currnetlyUsingBomb < numberOfBomb)
            {
                sb.Draw(textures[currentFrame],
                    AdjustPositionForTexture(),
                    null,
                    Color.White,
                    0f,
                    Vector2.Zero,
                    1f,
                    SpriteEffects.None,
                    0f);
            }


            sb.End();

            CheckForExplode();
            UpdateFrameInfo(gameTime);
            base.Draw(gameTime);
        }

        private void UpdateFrameInfo(GameTime gameTime)
        {
            frameTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (frameTimer >= FRAME_DURATION)
            {
                if (forwardFrame)
                {
                    currentFrame++;
                    if (currentFrame == MAX_FRAME - 1)
                    {
                        forwardFrame = !forwardFrame;
                    }
                }
                else
                {
                    currentFrame--;
                    if (currentFrame <= 0)
                    {
                        forwardFrame = !forwardFrame;
                    }
                }

                frameTimer = 0;
            }
        }

        private void CheckForExplode()
        {
            //if hero reached to enemies
            Hero player = Game.Services.GetService<Hero>();
            Rectangle playerBoundary = player.hero.Bounds;
            playerBoundary.Location = player.position.ToPoint();

            if (this.Enabled&&playerBoundary.Intersects(GetTextureRectangle()))
            {
                CollectScore();
                PlaySoundEffect();
                
                this.Enabled = false;
                Game.Components.Remove(this);
            }

        }

        private void PlaySoundEffect()
        {
            if(this.enemyType == EnemyType.bomb)
            {
                bombSound.Play();
            }
            else
            {
                impactSound.Play();
            }
        }

        private void CollectScore()
        {
            if(enemyType == EnemyType.bomb)
            {
                Game.Services.GetService<Life>().ReduceLife(GetEnemyValue());
            }

            Game.Services.GetService<Score>().AddScore(GetEnemyValue());

        }

        /// <summary>
        /// Figures out the coin value based on the 
        /// coin type that we have
        /// </summary>
        /// <returns></returns>
        private int GetEnemyValue()
        {
            switch (enemyType)
            {
                case EnemyType.slime:
                    return (int)EnemyValue.slime;
                case EnemyType.fish:
                    return (int)EnemyValue.fish;
                case EnemyType.fly:
                    return (int)EnemyValue.fly;
                case EnemyType.snail:
                    return (int)EnemyValue.snail;
                case EnemyType.bomb:
                    return (int)EnemyValue.bomb;
                default:
                    return 0; ;
            }
        }

        private Rectangle GetTextureRectangle()
        {
            Rectangle bounds = textures[currentFrame].Bounds;
            bounds.Location = AdjustPositionForTexture().ToPoint();
            return bounds;
        }

        /// <summary>
        /// Calculates positon based on the actual width of
        /// the texture at current frame
        /// </summary>
        /// <returns></returns>
        private Vector2 AdjustPositionForTexture()
        {
            float adjustedX = position.X - textures[currentFrame].Width / 2;
            float adjustedY = position.Y - textures[currentFrame].Height / 2;

            Vector2 adjustedPos = new Vector2(adjustedX, adjustedY);

            return adjustedPos;
        }

        public override void Update(GameTime gameTime)
        {
            timeSinceEnemyAppear += gameTime.ElapsedGameTime.TotalSeconds;
            if (timeSinceEnemyAppear >= ENEMY_INTERVAL)
            {
                timeSinceEnemyAppear = 0;
                enemyActive = false;
                this.Enabled = false;
            }
            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            if (enemyType.ToString() == "slime" || enemyType.ToString() == "snail")
            {
                for (int i = 0; i < MAX_FRAME; i++)
                {
                    textures.Add(Game.Content.Load<Texture2D>($"Enemy\\{enemyType.ToString()}Walk{i + 1}"));
                }
            }
            else if (enemyType.ToString() == "fly")
            {
                for (int i = 0; i < MAX_FRAME; i++)
                {
                    textures.Add(Game.Content.Load<Texture2D>($"Enemy\\{enemyType.ToString()}Fly{i + 1}"));
                }
            }
            else if (enemyType.ToString() == "fish")
            {
                for (int i = 0; i < MAX_FRAME; i++)
                {
                    textures.Add(Game.Content.Load<Texture2D>($"Enemy\\{enemyType.ToString()}Swim{i + 1}"));
                }
            }
            else
            {
                for (int i = 0; i < MAX_FRAME; i++)
                {
                    textures.Add(Game.Content.Load<Texture2D>($"Enemy\\{enemyType.ToString()}{i + 1}"));
                }
            }

            bombSound = Game.Content.Load<SoundEffect>("8bit_bomb_explosion");
            impactSound = Game.Content.Load<SoundEffect>("sfx_sounds_impact1");
            base.LoadContent();
        }

        public void move()
        {

        }

    }
}
