using FinalProjectShell;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectShell
{
    class EndScene : GameScene
    {
        int finalScore;
        public EndScene(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            // create and add any components that belong to 
            // this scene to the Scene components list
            //TO-DO
            this.SceneComponents.Add(new EndTextComponent(Game, finalScore));
            this.Hide();
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (Enabled)
            {
                this.SceneComponents.Add(new EndTextComponent(Game, finalScore));

                if (Keyboard.GetState().IsKeyDown(Keys.Enter)|| Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    ((Game1)Game).HideAllScenes();
                    Game.Exit();
                }
            }
            base.Update(gameTime);
        }

        public void SetFinalScore(int score)
        {
            finalScore = score;
        }
    }
}
