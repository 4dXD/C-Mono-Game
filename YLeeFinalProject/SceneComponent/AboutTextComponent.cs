﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectShell
{
    class AboutTextComponent : DrawableGameComponent
    {
        Texture2D texture;

        public AboutTextComponent(Game game) : base(game)
        {

        }


        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = Game.Services.GetService<SpriteBatch>();

            spriteBatch.Begin();
            spriteBatch.Draw(texture, Vector2.Zero, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }


        protected override void LoadContent()
        {
            texture = Game.Content.Load<Texture2D>("Images/About3");
            base.LoadContent();
        }
    }
}
