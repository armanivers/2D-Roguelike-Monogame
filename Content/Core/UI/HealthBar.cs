using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
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
        private float scalingFactor;

        private Vector2 position;
        private Vector2 textPosition;

        public HealthBar(Player player)
        {
            target = player;
            scalingFactor = 1.5f;
            healthbarContainer = TextureManager.healthBarEmpty;
            healthBar = TextureManager.healthBarRed;
            position = new Vector2(GameSettings.screenWidth / 2 - healthbarContainer.Width*scalingFactor/2, 30);
            textPosition = new Vector2(GameSettings.screenWidth / 2 - 10, 30);
            fullWidth = healthBar.Width;
            currentWidth = fullWidth;
            currentHealth = player.HealthPoints;
        }

        public void Update(GameTime gameTime)
        {
            if (InputController.keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.B))
            {
                scalingFactor += 0.1f;
                position = new Vector2(GameSettings.screenWidth / 2 - healthbarContainer.Width * scalingFactor / 2, 30);
            }
            if (InputController.keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.C))
            {
                scalingFactor -= 0.1f;
                position = new Vector2(GameSettings.screenWidth / 2 - healthbarContainer.Width * scalingFactor / 2, 30);
            }
            currentHealth = target.HealthPoints;
            currentWidth = (int)(   ( (double)(currentHealth) / 100) * fullWidth );
            //Debug.WriteLine("target.HealthPoints: {3}\ncurrentHealth: {0}\ncurrentWidth: {1}\n fullWidth. {2}\n---------------", currentHealth, currentWidth, fullWidth,target.HealthPoints);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(healthbarContainer, position, null,Color.White,0, Vector2.Zero, scalingFactor, SpriteEffects.None,0);
            spriteBatch.Draw(healthBar, position,new Rectangle(0,0, currentWidth, healthBar.Height), Color.White,0, Vector2.Zero, scalingFactor,SpriteEffects.None,0);
            spriteBatch.DrawString(TextureManager.FontArial, ""+currentHealth, textPosition, Color.White);
        }

    }
}
