﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalProjectShell
{
    public class HelpScene : GameScene
    {
        public HelpScene(Game game): base(game)
        {
            
        }

        public override void Initialize()
        {
            // create and add any components that belong to 
            // this scene to the Scene components list
            this.SceneComponents.Add(new HelpTextComponent(Game));
            this.Hide();
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if( Enabled)
            {
                
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    ((Game1)Game).HideAllScenes();
                    Game.Services.GetService<StartScene>().Show();
                }
            }
            base.Update(gameTime);
        }

       
    }
}
