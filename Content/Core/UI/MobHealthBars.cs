using _2DRoguelike.Content.Core.Entities;
using _2DRoguelike.Content.Core.Entities.Creatures.Enemies;
using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
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
            public MobData(Vector2 position, int maxHealth, int currentWidth, int currentHealth,float transparency)
            {
                Transparency = transparency;
                this.currentHealth = currentHealth;
                this.position = position;
                this.maxHealth = maxHealth;
                this.currentWidth = currentWidth;
            }
            public float Transparency { get; }
            public int currentHealth { get; }
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
            fullwidth = (int)(healthbarTexture.Width);
            mobData = new List<MobData>();
        }

        public void Update(GameTime gameTime)
        {
            mobData.Clear();
            foreach (EntityBasis e in EntityManager.creatures)
            {
                if (e is Enemy)
                {

                    var maxHealth = ((Humanoid)e).maxHealthPoints;
                    var hp = ((Humanoid)e).HealthPoints;
                    var currentWidth = (int)(((double)(hp) / maxHealth) * fullwidth);
                    mobData.Add(new MobData(e.Position, maxHealth, currentWidth, hp,e.transparency));
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < mobData.Count; i++)
            {
                if (mobData[i].Transparency > 0)
                {
                    spriteBatch.Draw(healthbarTexture, mobData[i].position, null, Color.White * mobData[i].Transparency, 0, Vector2.Zero, scalingFactor, SpriteEffects.None, 0);
                    spriteBatch.Draw(healthbarContainerTexture, mobData[i].position, new Rectangle(0, 0, mobData[i].currentWidth, fullwidth), Color.White * mobData[i].Transparency, 0, Vector2.Zero, scalingFactor, SpriteEffects.None, 0);
                    spriteBatch.DrawString(TextureManager.FontArial,mobData[i].currentHealth.ToString(), 
                        new Vector2(mobData[i].position.X +25 ,
                        mobData[i].position.Y), Color.White, 0, Vector2.Zero, 0.35f, SpriteEffects.None, 0);
                }
            }
        }

    }
}
