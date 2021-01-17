using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2DRoguelike.Content.Core.UI
{
    class HealthBar : UIElementBasis
    {
        private Player target;

        private int currentHealth;
        private int fullWidth;
        private int currentWidth;

        // space from upper left corner
        private int xSafezone = 30;
        private int ySafezone = 20;

        private Texture2D healthbarContainer;
        private Texture2D healthBar;

        private float scalingFactor = 1.1f;

        // red bar starts at x = 69, before it everything is empty
        private int redbarOffsetStart = 68;
        // redbar ends 3 pixels before the end of iamge, after it everything is empty
        private int redbarOffsetEnd = 4;

        // text position relative to healthbar position
        private int textOffsetY = 20;

        private Vector2 redbarPosition;
        private Vector2 containerPositon;
        private Vector2 textPosition;

        public HealthBar(Player player)
        {
            target = player;

            healthbarContainer = TextureManager.ui.HealthbarContainer;
            healthBar = TextureManager.ui.HealthbarBar;

            containerPositon = new Vector2(xSafezone*scalingFactor, ySafezone*scalingFactor);
            redbarPosition = containerPositon + new Vector2(redbarOffsetStart * scalingFactor, 0);

            fullWidth = healthBar.Width-redbarOffsetStart - redbarOffsetEnd;
            
            textPosition = containerPositon + new Vector2(redbarOffsetStart + fullWidth/2 -TextureManager.FontArial.MeasureString("" + player.HealthPoints).X / 2, ySafezone + textOffsetY);
            
            currentWidth = fullWidth;
            currentHealth = player.HealthPoints;
        }

        public override void Update(GameTime gameTime)
        {
            //if (InputController.keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.B))
            //{
            //    scalingFactor += 0.1f;
            //    position = new Vector2(GameSettings.screenWidth / 2 - healthbarContainer.Width * scalingFactor / 2, 30);
            //}
            //if (InputController.keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.C))
            //{
            //    scalingFactor -= 0.1f;
            //    position = new Vector2(GameSettings.screenWidth / 2 - healthbarContainer.Width * scalingFactor / 2, 30);
            //}
            currentHealth = target.HealthPoints;
            currentWidth = (int)(   ( (double)(currentHealth) / target.maxHealthPoints) * fullWidth );
            textPosition = containerPositon + new Vector2(redbarOffsetStart + fullWidth / 2 - TextureManager.FontArial.MeasureString("" + target.HealthPoints).X / 2, ySafezone + textOffsetY);
            //Debug.WriteLine("target.HealthPoints: {3}\ncurrentHealth: {0}\ncurrentWidth: {1}\n fullWidth. {2}\n---------------", currentHealth, currentWidth, fullWidth,target.HealthPoints);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(healthbarContainer, containerPositon, null,Color.White,0, Vector2.Zero, scalingFactor, SpriteEffects.None,0);
            spriteBatch.Draw(healthBar, redbarPosition, new Rectangle(redbarOffsetStart,0, currentWidth, healthBar.Height), Color.White,0, Vector2.Zero, scalingFactor,SpriteEffects.None,0);
            spriteBatch.DrawString(TextureManager.GameFont, ""+currentHealth, textPosition, Color.White);
        }

    }
}
