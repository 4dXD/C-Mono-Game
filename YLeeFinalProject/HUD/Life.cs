using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectShell
{
    class Life : HudString
    {
        public int life = 60;

        const double ONE_SECOND_INTERVAL = 1;
        double timeSinceLastCount = 0;

        public Life(Game game, string fontName, HudLocation screenLocation)
            : base(game, fontName, screenLocation)
        {
            if (Game.Services.GetService<Life>() == null)
            {
                Game.Services.AddService<Life>(this);
            }
        }

        public override void Update(GameTime gameTime)
        {
            timeSinceLastCount += gameTime.ElapsedGameTime.TotalSeconds;
            if (timeSinceLastCount >= ONE_SECOND_INTERVAL)
            {
                timeSinceLastCount = 0;
                life--;
            }

            displayString = $"Time Left: {life}";
            base.Update(gameTime);
        }

        public void ReduceLife(int value)
        {
            life--;
        }
    }
}
