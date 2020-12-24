using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2DRoguelike.Content.Core.UI
{
    class BossBar : UIElement
    {
        private Player target;
        private int currentHealth;
        private int fullWidth;
        private int currentWidth;


        private Texture2D healthbarContainer;
        private Texture2D healthBar;

        private float scalingFactor = 1.5f;

        private Vector2 position;
        private Vector2 textPosition;

        public BossBar(Player player)
        {
            target = player;
            healthbarContainer = TextureManager.BossbarContainer;
            healthBar = TextureManager.BossbarBar;
            position = new Vector2(Game1.gameSettings.screenWidth / 2 - healthbarContainer.Width * scalingFactor / 2, 30);
            textPosition = new Vector2(Game1.gameSettings.screenWidth / 2 - TextureManager.FontArial.MeasureString("" + player.HealthPoints).X / 2, 30);
            fullWidth = healthBar.Width;
            currentWidth = fullWidth;
            currentHealth = player.HealthPoints;
        }

        public override void Update(GameTime gameTime)
        {
            currentHealth = target.HealthPoints;
            currentWidth = (int)(((double)(currentHealth) / 100) * fullWidth);
            textPosition = new Vector2(Game1.gameSettings.screenWidth / 2 - TextureManager.FontArial.MeasureString("" + currentHealth).X / 2, 30);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(healthbarContainer, position, null, Color.White, 0, Vector2.Zero, scalingFactor, SpriteEffects.None, 0);
            spriteBatch.Draw(healthBar, position, new Rectangle(0, 0, currentWidth, healthBar.Height), Color.White, 0, Vector2.Zero, scalingFactor, SpriteEffects.None, 0);
            spriteBatch.DrawString(TextureManager.FontArial, "" + currentHealth, textPosition, Color.White);
        }

        public override void ForceResolutionUpdate()
        {
            position = new Vector2(Game1.gameSettings.screenWidth / 2 - healthbarContainer.Width * scalingFactor / 2, 30);
            textPosition = new Vector2(Game1.gameSettings.screenWidth / 2 - 10, 30);
        }

    }
}