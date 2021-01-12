using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2DRoguelike.Content.Core.UI
{
    class KeyStatus : UIElementBasis
    {
        //private float scalingFactor = 1.0f;

        private int xSafezone = 50;
        private int ySafezone = 90;

        private Vector2 keyPosition;
        private Player target;
        private Color color;
        private string displayText;
        public KeyStatus(Player target)
        {
            this.target = target;
            this.color = Color.Red;
            displayText = "";
        }

        public override void Update(GameTime gameTime)
        {
            

            if (target.hasLevelKey)
            {
                displayText = "Key Found!";
                color = Color.Green;
            }
            else
            {
                color = Color.Red;
                displayText = "Find the Key!";
            }

            keyPosition = new Vector2(Game1.gameSettings.screenWidth - xSafezone - TextureManager.FontArial.MeasureString(displayText).X, ySafezone);

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(TextureManager.GameFont, displayText, keyPosition, color);
        }

        public override void ForceResolutionUpdate()
        {
            keyPosition = new Vector2(Game1.gameSettings.screenWidth - xSafezone - TextureManager.FontArial.MeasureString(displayText).X, ySafezone);
        }

    }
}
