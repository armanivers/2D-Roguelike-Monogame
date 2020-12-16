using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2DRoguelike.Content.Core.UI
{
    class Highscore
    {

        private int currentScore;

        private float scalingFactor;

        private Vector2 textPosition;
        private int textYOffset;

        public Highscore()
        {
            scalingFactor = 1.5f;
            textYOffset = 120;
        }

        public void Update(GameTime gameTime)
        {

            currentScore = StatisticsManager.currentScore.Score;
            textPosition = new Vector2(Game1.gameSettings.screenWidth / 2 - TextureManager.FontArial.MeasureString("Score: " + currentScore).X / 2, textYOffset);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(TextureManager.FontArial, "Score: " + currentScore, textPosition, Color.White);
        }

        public void ForceResolutionUpdate()
        {
            textPosition = new Vector2(Game1.gameSettings.screenWidth / 2 - TextureManager.FontArial.MeasureString("Score: " + currentScore).X / 2, textYOffset);
        }

    }
}
