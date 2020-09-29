
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace FinalProjectShell
{
    enum PlayerState
    {
        Idle,
        WalkingRight,
        WalkingLeft,
        Duck,
        Jump
    }

    public class Hero : DrawableGameComponent
    {
        public Texture2D hero;
        public Texture2D bomb;

        Texture2D textureIdle;

        List<Texture2D> textureWalk = new List<Texture2D>();
        List<Texture2D> textureDuck = new List<Texture2D>();
        List<Texture2D> textureJump = new List<Texture2D>();
        const int WALK_FRAME_COUNT = 11;
        const int JUMPDUCK_FRAME_COUNT = 7;
        const double FRAME_RATE = 0.1;

        PlayerState playerState = PlayerState.Idle;

        int currentFrame = 0;
        double timeSinceLastFrame = 0.0;

        public Vector2 position;
        public Vector2 bombPosition;
        private int speed;
        bool bombActive = false;

        double timeSinceBombPlant = 0.0;
        const double BOMB_INTERVAL = 2.5;

        public Hero(Game game): base(game)
        {
            speed = 4;
        }

        public override void Update(GameTime gameTime)
        {
            timeSinceBombPlant += gameTime.ElapsedGameTime.TotalSeconds;
            if (timeSinceBombPlant >= BOMB_INTERVAL)
            {
                bombActive = false;
                timeSinceBombPlant = 0;
            }

            KeyboardState ks = Keyboard.GetState();

            //if (!bombActive)
            /*if (currnetlyUsingBomb < numberOfBomb)
            {
                if (ks.IsKeyDown(Keys.Space))
                {
                    //if()
                    //plant a bomb on the hero's currnet position
                    bombPosition = position;
                    currnetlyUsingBomb++;
                    bombActive = true;
                    //bomb.
                }
            }*/

            if (this.Visible)
            {
                if (ks.IsKeyDown(Keys.Up))
                {
                    playerState = PlayerState.Jump;
                    position.Y -= speed;
                }
                else if (ks.IsKeyDown(Keys.Down))
                {
                    playerState = PlayerState.Duck;
                    position.Y += speed;
                }
                else if (ks.IsKeyDown(Keys.Right))
                {
                    playerState = PlayerState.WalkingRight;
                    position.X += speed;
                }
                else if (ks.IsKeyDown(Keys.Left))
                {
                    playerState = PlayerState.WalkingLeft;
                    position.X -= speed;                
                }
                else
                {
                    playerState = PlayerState.Idle;
                }
            }
            position.X = MathHelper.Clamp(position.X, 0, GraphicsDevice.Viewport.Width - textureIdle.Width);
            int playerHeight = playerState == PlayerState.Duck ? textureDuck[0].Height : textureIdle.Height;

            position.Y = MathHelper.Clamp(position.Y, 0, GraphicsDevice.Viewport.Height - playerHeight);


            // update our frame info
            timeSinceLastFrame += gameTime.ElapsedGameTime.TotalSeconds;
            if (timeSinceLastFrame > FRAME_RATE)
            {
                switch (playerState)
                {
                    case PlayerState.Idle:
                        currentFrame = 0;
                        timeSinceLastFrame = 0;
                        break;
                    case PlayerState.WalkingLeft:
                    case PlayerState.WalkingRight:
                        if (++currentFrame >= WALK_FRAME_COUNT)
                        {
                            currentFrame = 0;
                            timeSinceLastFrame = 0;
                        }
                        break;
                    case PlayerState.Duck:
                        if (++currentFrame >= JUMPDUCK_FRAME_COUNT)
                        {
                            currentFrame = 0;
                            timeSinceLastFrame = 0;
                        }
                        break;
                    case PlayerState.Jump:
                        if (++currentFrame >= JUMPDUCK_FRAME_COUNT)
                        {
                            currentFrame = 0;
                            timeSinceLastFrame = 0;
                        }
                        break;
                }
            }

            // here we make sure that we are not off screen, we
            // clap the value to between 0 and width of screen - texture width
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();

            sb.Begin();
            
            if (playerState == PlayerState.Idle)
            {
                DrawSingleFrame(textureIdle, sb);
            }
            else if (playerState == PlayerState.Duck)
            {
                DrawDuck(sb);

            }
            else if (playerState == PlayerState.Jump)
            {
                DrawJump(sb);
            }

            else if (playerState == PlayerState.WalkingLeft || playerState == PlayerState.WalkingRight)
            {
                DrawWalk(sb);
            }
            

            if (bombActive)
            {
                sb.Draw(bomb, bombPosition, Color.White);
            }
            sb.End();

            base.Draw(gameTime);
        }


        private void DrawWalk(SpriteBatch sb)
        {
            sb.Draw(textureWalk[currentFrame],
                                  position,
                                  null,
                                  Color.White,
                                  0f,
                                  Vector2.Zero,
                                  1.0f,
                                  playerState == PlayerState.WalkingLeft ?
                                            SpriteEffects.FlipHorizontally : SpriteEffects.None,
                                  0f);
        }

        private void DrawDuck(SpriteBatch sb)
        {
            sb.Draw(textureDuck[currentFrame],
                                  position,
                                  null,
                                  Color.White,
                                  0f,
                                  Vector2.Zero,
                                  1.0f,
                                  SpriteEffects.None,
                                  0f);
        }

        private void DrawJump(SpriteBatch sb)
        {
            sb.Draw(textureJump[currentFrame],
                                  position,
                                  null,
                                  Color.White,
                                  0f,
                                  Vector2.Zero,
                                  1.0f,
                                  SpriteEffects.None,
                                  0f);
        }

        private void DrawSingleFrame(Texture2D activeTexture, SpriteBatch sb)
        {
            sb.Draw(activeTexture, position, Color.White);
        }

        protected override void LoadContent()
        {
            hero = Game.Content.Load<Texture2D>("Images/hero1");
            bomb = Game.Content.Load<Texture2D>("Images/bomb1");

            for (int i = 1; i <= WALK_FRAME_COUNT; i++)
            {
                textureWalk.Add(Game.Content.Load<Texture2D>($"player/walk/p1_walk{i:D2}"));
            }

            textureIdle = Game.Content.Load<Texture2D>("player/idle/p1_front");

            textureDuck.Add(Game.Content.Load<Texture2D>($"player/duck/p1_duck"));
            textureDuck.Add(Game.Content.Load<Texture2D>($"player/duck/p1_duck2"));
            textureDuck.Add(Game.Content.Load<Texture2D>($"player/duck/p1_duck3"));
            textureDuck.Add(Game.Content.Load<Texture2D>($"player/duck/p1_duck4"));
            textureDuck.Add(Game.Content.Load<Texture2D>($"player/duck/p1_duck5"));
            textureDuck.Add(Game.Content.Load<Texture2D>($"player/duck/p1_duck6"));
            textureDuck.Add(Game.Content.Load<Texture2D>($"player/duck/p1_duck7"));


            textureJump.Add(Game.Content.Load<Texture2D>($"player/jump/p1_jump"));
            textureJump.Add(Game.Content.Load<Texture2D>($"player/jump/p1_jump2"));
            textureJump.Add(Game.Content.Load<Texture2D>($"player/jump/p1_jump3"));
            textureJump.Add(Game.Content.Load<Texture2D>($"player/jump/p1_jump4"));
            textureJump.Add(Game.Content.Load<Texture2D>($"player/jump/p1_jump5"));
            textureJump.Add(Game.Content.Load<Texture2D>($"player/jump/p1_jump6"));
            textureJump.Add(Game.Content.Load<Texture2D>($"player/jump/p1_jump7"));

            // position our object at bottom center of screen
            position = new Vector2(GraphicsDevice.Viewport.Width / 2 - textureIdle.Width / 2,
                                GraphicsDevice.Viewport.Height/2 - textureIdle.Height/2);
            bombPosition = position;
            base.LoadContent();
        }
    }

}
