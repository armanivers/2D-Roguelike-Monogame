using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2DRoguelike.Content.Core.UI
{
    class Highscore:UIElement
    {

        private int currentScore;

        private float scalingFactor = 1.0f;

        private int xSafezone = 20;
        private int ySafezone = 30;

        private Vector2 textPosition;
        private Vector2 coinPosition;

        private Texture2D coinTexture;

        public Highscore()
        {
            coinTexture = TextureManager.ui.HighscoreCoin;
        }

        public override void Update(GameTime gameTime)
        {
            currentScore = StatisticsManager.currentScore.UpdateBuffer();
            coinPosition = new Vector2(Game1.gameSettings.screenWidth - xSafezone - coinTexture.Width, ySafezone);
            textPosition = new Vector2(coinPosition.X - TextureManager.FontArial.MeasureString("Score: " + currentScore).X - xSafezone, coinPosition.Y+13);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(coinTexture, coinPosition, null, Color.White, 0, Vector2.Zero, scalingFactor, SpriteEffects.None, 0);
            spriteBatch.DrawString(TextureManager.GameFont, "Score: " + currentScore, textPosition, Color.White);
        }

        public override void ForceResolutionUpdate()
        {
            coinPosition = new Vector2(Game1.gameSettings.screenWidth - xSafezone - coinTexture.Width, ySafezone);
        }

    }
}
