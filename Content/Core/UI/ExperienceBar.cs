using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2DRoguelike.Content.Core.UI
{
    class ExperienceBar
    {
        private Player target;
        private double currentXP;
        private int currentXPLevel;
        private int fullWidth;
        private int currentWidth;


        private Texture2D xpContainer;
        private Texture2D xpBar;
        private float scalingFactor;

        private Vector2 position;
        private Vector2 textPosition;

        public ExperienceBar(Player player)
        {
            target = player;
            scalingFactor = 1.5f;
            //healthbarContainer = TextureManager.healthBarEmpty;
            //healthBar = TextureManager.healthBarRed;
            //position = new Vector2(GameSettings.screenWidth / 2 - healthbarContainer.Width * scalingFactor / 2, 30);
            
            //fullWidth = healthBar.Width;
            currentWidth = fullWidth;
            //currentHealth = player.HealthPoints;
        }

        public void Update(GameTime gameTime)
        {
            
            currentXP = target.CurrentXP;
            currentXPLevel = target.currentXPLevel;
            textPosition = new Vector2(Game1.gameSettings.screenWidth / 2 - TextureManager.FontArial.MeasureString("Level: " + currentXPLevel + " XP: " + currentXP).X/2, 80);
            //currentWidth = (int)(((double)(currentHealth) / 100) * fullWidth);
            //Debug.WriteLine("target.HealthPoints: {3}\ncurrentHealth: {0}\ncurrentWidth: {1}\n fullWidth. {2}\n---------------", currentHealth, currentWidth, fullWidth,target.HealthPoints);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(healthbarContainer, position, null, Color.White, 0, Vector2.Zero, scalingFactor, SpriteEffects.None, 0);
            //spriteBatch.Draw(healthBar, position, new Rectangle(0, 0, currentWidth, healthBar.Height), Color.White, 0, Vector2.Zero, scalingFactor, SpriteEffects.None, 0);
            spriteBatch.DrawString(TextureManager.FontArial, "Level: " + currentXPLevel + " XP: "+currentXP , textPosition, Color.White);
        }

        public void ForceResolutionUpdate()
        {
            textPosition = new Vector2(Game1.gameSettings.screenWidth / 2 - TextureManager.FontArial.MeasureString("Level: " + currentXPLevel + " XP: " + currentXP).X / 2, 80);
        }

    }
}
