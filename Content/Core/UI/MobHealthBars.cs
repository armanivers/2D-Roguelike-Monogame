using _2DRoguelike.Content.Core.Entities;
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

        public List<Vector2> positions;
        public List<int> healths;

        public int fullwidth;

        public MobHealthBars()
        {
            healthbarContainerTexture = TextureManager.EnemyBarContainer;
            healthbarTexture = TextureManager.EnemyBar;
            scalingFactor = 0.2f;
            fullwidth = (int)(healthbarTexture.Width );
            positions = new List<Vector2>();
            healths = new List<int>();

        }

        public void Update(GameTime gameTime)
        {
            positions.Clear();
            healths.Clear();
            foreach(EntityBasis e in EntityManager.entities)
            {
                if(e is Enemy)
                {
                    positions.Add(new Vector2(e.Position.X, e.Position.Y ));
                    var hp = ((Humanoid)e).HealthPoints;
                    var currentWidth = (int)(((double)(hp) / 100) * fullwidth);
                    healths.Add(currentWidth);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for(int i = 0; i < positions.Count; i++)
            {
                spriteBatch.Draw(healthbarTexture, positions[i], null, Color.White, 0, Vector2.Zero, scalingFactor, SpriteEffects.None, 0);
                spriteBatch.Draw(healthbarContainerTexture, positions[i], new Rectangle(0, 0, healths[i], fullwidth), Color.White, 0, Vector2.Zero, scalingFactor, SpriteEffects.None, 0);
            }
        }

    }
}
