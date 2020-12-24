using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2DRoguelike.Content.Core.UI
{
    class ExperienceBar: UIElement
    {
        private Player target;

        private int currentXP;
        private int currentXPLevel;
        private int fullWidth;
        private int currentWidth;

        // space from upper left corner
        private int xSafezone = 25;
        private int ySafezone = 80;

        private Texture2D xpbarContainer;
        private Texture2D xpbarBar;

        private float scalingFactor = 1.5f;

        // red bar starts at x = 69, before it everything is empty
        private int xpbarOffsetStart = 3;
        // redbar ends 3 pixels before the end of iamge, after it everything is empty
        private int xpbarOffsetEnd = 5;

        private Vector2 xpbarContainerPosition;
        private Vector2 xpbarBarPosition;
        private Vector2 textPosition;

        public ExperienceBar(Player player)
        {
            target = player;

            xpbarContainer = TextureManager.XPBarContainer;
            xpbarBar = TextureManager.XPBarBar;

            xpbarContainerPosition = new Vector2(xSafezone * scalingFactor, ySafezone * scalingFactor);
            xpbarBarPosition = xpbarContainerPosition + new Vector2(xpbarOffsetStart * scalingFactor, 0);

            fullWidth = xpbarBar.Width - xpbarOffsetStart - xpbarOffsetEnd;

            textPosition = xpbarContainerPosition + new Vector2(69 + fullWidth / 2 - TextureManager.FontArial.MeasureString("" + player.HealthPoints).X / 2, xSafezone);

            currentWidth = fullWidth;
            currentXP = player.HealthPoints;
            currentXPLevel = target.currentXPLevel;
        }

        public override void Update(GameTime gameTime)
        {
            
            currentXP = target.CurrentXP;
            currentXPLevel = target.currentXPLevel;

            textPosition = new Vector2(Game1.gameSettings.screenWidth / 2 - TextureManager.FontArial.MeasureString("Level: " + currentXPLevel + " XP: " + currentXP).X/2, 80);


            currentWidth = (int)((target.LevelupPercentage) * fullWidth);
            //Debug.Print("Percentage {0} width = {1}", target.LevelupPercentage, currentWidth);
            //textPosition = containerPositon + new Vector2(redbarOffsetStart + fullWidth / 2 - TextureManager.FontArial.MeasureString("" + target.HealthPoints).X / 2, xSafezone);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(xpbarContainer, xpbarContainerPosition, null, Color.White, 0, Vector2.Zero, scalingFactor, SpriteEffects.None, 0);
            spriteBatch.Draw(xpbarBar, xpbarBarPosition, new Rectangle(xpbarOffsetStart, 0, currentWidth, xpbarBar.Height), Color.White, 0, Vector2.Zero, scalingFactor, SpriteEffects.None, 0);

            spriteBatch.DrawString(TextureManager.FontArial, "Level: " + currentXPLevel + " XP: "+currentXP , textPosition, Color.White);
        }

        public override void ForceResolutionUpdate()
        {
            textPosition = new Vector2(Game1.gameSettings.screenWidth / 2 - TextureManager.FontArial.MeasureString("Level: " + currentXPLevel + " XP: " + currentXP).X / 2, 80);
        }

    }
}
