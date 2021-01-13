using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.World;
using _2DRoguelike.Content.Core.World.ExitConditions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2DRoguelike.Content.Core.UI
{
    // this is objective or exit condition of level
    class KeyStatus : UIElementBasis
    {
        //private float scalingFactor = 1.0f;

        private int xSafezone = 180;
        private int ySafezone = 80;

        private int textVerticalLength = 30;

        private Vector2 keyPosition;
        private Player target;
        private Color color;
        private string displayText;

        private Vector2 objectivePostion;
        private Color objectiveColor = Color.LawnGreen;
        private string objectiveText = "Objective:";
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
                displayText = "Find the Exit!";
                color = Color.MediumSeaGreen;
            }
            else
            {
                displayText = LevelManager.levelList[LevelManager.level].exitCondition.PrintCondition();
                color = Color.Red;
            }

            keyPosition = new Vector2(Game1.gameSettings.screenWidth - xSafezone - TextureManager.GameFont.MeasureString(displayText).X/2, ySafezone);

            objectivePostion = new Vector2(Game1.gameSettings.screenWidth - xSafezone - TextureManager.GameFont.MeasureString(objectiveText).X / 2, ySafezone - textVerticalLength);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(TextureManager.GameFont, displayText, keyPosition, color);
            spriteBatch.DrawString(TextureManager.GameFont, objectiveText, objectivePostion, objectiveColor);

            // exitCondition KillAmountOfEnemies has an extra text to be displayed
            if (LevelManager.levelList[LevelManager.level].exitCondition is KillAmountOfEnemies && !target.hasLevelKey)
            {
                var text = ((KillAmountOfEnemies)LevelManager.levelList[LevelManager.level].exitCondition).PrintRemainingEnemies();
                var pos = new Vector2(Game1.gameSettings.screenWidth - xSafezone - TextureManager.GameFont.MeasureString(text).X/2, keyPosition.Y+ textVerticalLength);
                spriteBatch.DrawString(TextureManager.GameFont, text, pos, Color.Gold);
            }
        }

        public override void ForceResolutionUpdate()
        {
            keyPosition = new Vector2(Game1.gameSettings.screenWidth - xSafezone - TextureManager.FontArial.MeasureString(displayText).X, ySafezone);
            objectivePostion = new Vector2(Game1.gameSettings.screenWidth - xSafezone - TextureManager.GameFont.MeasureString(objectiveText).X / 2, ySafezone - textVerticalLength);
        }

    }
}
