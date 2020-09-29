using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace FinalProjectShell
{
    public class ActionScene : GameScene
    {
        public ActionScene (Game game): base(game)
        {
          
        }

        public override void Initialize()
        {
            
            this.AddComponent(new Score(Game, "fonts\\hilightFont", HudLocation.TopRight));
            this.AddComponent(new Life(Game, "fonts\\hilightFont", HudLocation.TopLeft));
            this.AddComponent(new EnemyManager(Game, this));

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if ( Enabled )
            {
                Hero player = Game.Services.GetService<Hero>();
                player.Visible = true;

                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    player.Visible = false;
                    ((Game1)Game).HideAllScenes();
                    Game.Services.GetService<StartScene>().Show();
                }
                int lifeRemain = this.Game.Services.GetService<Life>().life;
                if(lifeRemain <= 0)
                {
                    player.Visible = false;
                    ((Game1)Game).HideAllScenes();
                    int finalScore = Game.Services.GetService<Score>().GetScore();
                    Game.Services.GetService<EndScene>().SetFinalScore(finalScore);
                    Game.Services.GetService<EndScene>().Show();
                }
            }
            base.Update(gameTime);
        }

       
    }
}
