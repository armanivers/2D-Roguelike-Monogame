using _2DRoguelike.Content.Core.Entities.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2DRoguelike.Content.Core.UI
{
    class HealthBar
    {
        private Player target;
        private int currentHealth;
        private int fullWidth;
        private int currentWidth;


        private Texture2D healthbarContainer;
        private Texture2D healthBar;

        private Vector2 position;
        private Vector2 textPosition;

        public HealthBar(Player player)
        {
            target = player;
            healthbarContainer = TextureManager.healthBarEmpty;
            healthBar = TextureManager.healthBarRed;
            position = new Vector2(GameSettings.screenWidth / 2 - healthbarContainer.Width / 2, 30);
            textPosition = new Vector2(GameSettings.screenWidth / 2 - 10, 30);
            fullWidth = healthBar.Width;
            currentWidth = fullWidth;
            currentHealth = player.HealthPoints;
        }

        public void Update(GameTime gameTime)
        {
            currentHealth = target.HealthPoints;

            // funktioniert nicht??
            currentWidth = (int)(   ( (double)(currentHealth) / 100) * fullWidth );
            Debug.WriteLine("target.HealthPoints: {3}\ncurrentHealth: {0}\ncurrentWidth: {1}\n fullWidth. {2}\n---------------", currentHealth, currentWidth, fullWidth,target.HealthPoints);
            // bei draw von healthbar sollte currentWidth statt currentHealth sein!
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(healthbarContainer, position, Color.White);
            spriteBatch.Draw(healthBar, position,new Rectangle(0,0, currentWidth, healthBar.Height), Color.White);
            spriteBatch.DrawString(TextureManager.FontArial, ""+currentHealth, textPosition, Color.White);
        }

    }
}
