﻿using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.Entities.Creatures.Enemies.Bosses;
using _2DRoguelike.Content.Core.World;
using _2DRoguelike.Content.Core.World.Maps;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2DRoguelike.Content.Core.UI
{
    class BossBar : UIElementBasis
    {
        private Boss target;
        private int currentHealth;
        private int fullWidth;
        private int currentWidth;


        private Texture2D healthbarContainer;
        private Texture2D healthBar;

        private float scalingFactor = 1.5f;

        private int ySafezone = 50;

        // text position relative to boss health bar position
        private int bossnameTextOffsetY = -30;
        private int bosshealthTextOffsetY = 5;

        private Vector2 position;
        private Vector2 bossNameTextPosition;
        private Vector2 bossHealthTextPosition;

        private string bossName;

        private bool init;

        public BossBar()
        {
            target = null;
            init = false;
            healthbarContainer = TextureManager.ui.BossbarContainer;
            healthBar = TextureManager.ui.BossbarBar;
        }

        public void SwitchState()
        {    
            if(init)
            {
                init = false;
            }
            else { 
                target = ((BossMap)LevelManager.currentmap).bossEntity;       
                bossName = target.bossName;

                position = new Vector2(Game1.gameSettings.screenWidth / 2 - healthbarContainer.Width * scalingFactor / 2, ySafezone);
                bossNameTextPosition = new Vector2(Game1.gameSettings.screenWidth / 2 - TextureManager.FontArial.MeasureString(bossName).X / 2, position.Y + bossnameTextOffsetY);    
                bossHealthTextPosition = new Vector2(Game1.gameSettings.screenWidth / 2 - TextureManager.FontArial.MeasureString("" + target.HealthPoints).X / 2, position.Y + bosshealthTextOffsetY);
                
                fullWidth = healthBar.Width;
                currentWidth = fullWidth;
                currentHealth = target.HealthPoints;
            
                init = true;
            }
        }

        public override void Update(GameTime gameTime)
        {
            if(init)
            {
                currentHealth = target.HealthPoints;
                currentWidth = (int)(((double)(currentHealth) / target.maxHealthPoints) * fullWidth);
                bossNameTextPosition = new Vector2(Game1.gameSettings.screenWidth / 2 - TextureManager.FontArial.MeasureString(bossName).X / 2, position.Y + bossnameTextOffsetY);
                bossHealthTextPosition = new Vector2(Game1.gameSettings.screenWidth / 2 - TextureManager.FontArial.MeasureString("" + target.HealthPoints).X / 2, position.Y + bosshealthTextOffsetY);
            }

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if(init)
            {
                spriteBatch.Draw(healthbarContainer, position, null, Color.White, 0, Vector2.Zero, scalingFactor, SpriteEffects.None, 0);
                spriteBatch.Draw(healthBar, position, new Rectangle(0, 0, currentWidth, healthBar.Height), Color.White, 0, Vector2.Zero, scalingFactor, SpriteEffects.None, 0);
                spriteBatch.DrawString(TextureManager.GameFont, bossName, bossNameTextPosition, Color.Red);
                spriteBatch.DrawString(TextureManager.GameFont, "" + currentHealth, bossHealthTextPosition, Color.Gold);
            }
        }

        public override void ForceResolutionUpdate()
        {
            if(init)
            {
                position = new Vector2(Game1.gameSettings.screenWidth / 2 - healthbarContainer.Width * scalingFactor / 2, 30);
                bossNameTextPosition = new Vector2(Game1.gameSettings.screenWidth / 2 - TextureManager.FontArial.MeasureString(bossName).X / 2, bossnameTextOffsetY);
                bossHealthTextPosition = new Vector2(Game1.gameSettings.screenWidth / 2 - TextureManager.FontArial.MeasureString("" + target.HealthPoints).X / 2, bosshealthTextOffsetY);
            }
        }

    }
}