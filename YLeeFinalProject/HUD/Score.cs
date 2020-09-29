using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectShell
{
    class Score : HudString
    {
        public static int score;

        public Score(Game game, string fontName, HudLocation screenLocation)
            : base(game, fontName, screenLocation)
        {
            if (Game.Services.GetService<Score>() == null)
            {
                Game.Services.AddService<Score>(this);
            }
        }

        public override void Update(GameTime gameTime)
        {
            displayString = $"Score: {score}";
            base.Update(gameTime);
        }

        public void AddScore(int value)
        {
            score += value;
        }

        public int GetScore()
        {
            return score;
        }
    }
}
