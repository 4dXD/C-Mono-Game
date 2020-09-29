using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalProjectShell
{
    public class EndTextComponent : DrawableGameComponent
    {
        Texture2D texture;

        SpriteFont regularFont;
        SpriteFont highlightFont;

        private string score;
        private int SelectedIndex { get; set; }
        private Vector2 position;

        private Color regularColor = Color.Black;
        private Color hilightColor = Color.Red;


        public EndTextComponent(Game game, int score) : base(game)
        {
            this.score = score.ToString();
        }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = Game.Services.GetService<SpriteBatch>();

            spriteBatch.Begin();

            Vector2 tempPos = position;

            SpriteFont scoreFont = highlightFont;
            //spriteBatch.DrawString(scoreFont, score, tempPos, Color.White);
            spriteBatch.Draw(texture, Vector2.Zero, Color.White);
            spriteBatch.End();


            base.Draw(gameTime);
        }

        public override void Initialize()
        {
            // starting position of the menu items
            position = new Vector2((GraphicsDevice.Viewport.Width / 2)-100,
                                      GraphicsDevice.Viewport.Height / 2);

            score = $"Your Score: {Game.Services.GetService<Score>().GetScore().ToString()}";

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // load the fonts we will be using for this menu
            texture = Game.Content.Load<Texture2D>("Images/gameEnd2");
            regularFont = Game.Content.Load<SpriteFont>("Fonts/regularFont");
            highlightFont = Game.Content.Load<SpriteFont>("Fonts/hilightFont");
            base.LoadContent();
        }
    }
}
