﻿using _2DRoguelike.Content.Core.Entities;
using _2DRoguelike.Content.Core.Entities.Creatures.Enemies;
using _2DRoguelike.Content.Core.Entities.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2DRoguelike.Content.Core.UI
{
    class MobHealthBars
    {

        public Texture2D healthbarContainerTexture;
        public Texture2D healthbarTexture;

        public float scalingFactor;

        public int fullwidth;

        public struct MobData
        {
            public MobData(Vector2 position, int maxHealth, int currentWidth)
            {
                this.position = position;
                this.maxHealth = maxHealth;
                this.currentWidth = currentWidth;
            }
            public Vector2 position { get; }
            public int maxHealth { get; }
            public int currentWidth { get; }
        }

        public List<MobData> mobData;

        public MobHealthBars()
        {
            healthbarContainerTexture = TextureManager.EnemyBarContainer;
            healthbarTexture = TextureManager.EnemyBar;
            scalingFactor = 0.2f;
            fullwidth = (int)(healthbarTexture.Width );
            mobData = new List<MobData>();
        }

        public void Update(GameTime gameTime)
        {
            mobData.Clear();
            foreach(EntityBasis e in EntityManager.entities)
            {
                if(e is Enemy)
                {

                    var maxHealth = ((Humanoid)e).maxHealthPoints;
                    var hp = ((Humanoid)e).HealthPoints;
                    var currentWidth = (int)(((double)(hp) / maxHealth) * fullwidth);
                    mobData.Add(new MobData(e.Position, maxHealth, currentWidth));
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for(int i = 0; i < mobData.Count; i++)
            {
                spriteBatch.Draw(healthbarTexture, mobData[i].position, null, Color.White, 0, Vector2.Zero, scalingFactor, SpriteEffects.None, 0);
                spriteBatch.Draw(healthbarContainerTexture, mobData[i].position, new Rectangle(0, 0, mobData[i].currentWidth, fullwidth), Color.White, 0, Vector2.Zero, scalingFactor, SpriteEffects.None, 0);
            }
        }

    }
}