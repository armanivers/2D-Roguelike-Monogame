using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.UI
{
    class UIManager
    {
        public static HealthBar healthBar;
        public static Skillbar skillBar;
        public static MobHealthBars mobHealthBars;

        public static void Update(GameTime gameTime)
        {
            healthBar.Update(gameTime);
            skillBar.Update(gameTime);
            mobHealthBars.Update(gameTime);
        }
        public static void Draw(SpriteBatch spriteBatch)
        {
            healthBar.Draw(spriteBatch);
            skillBar.Draw(spriteBatch);
        }

        public static void DrawDynamic(SpriteBatch spriteBatch)
        {
            mobHealthBars.Draw(spriteBatch);
        }

        public static void ForceResolutionUpdate()
        {
            if(healthBar != null && skillBar != null)
            {
                healthBar.ForceResolutionUpdate();
                skillBar.ForceResolutionUpdate();
            }
        }

    }
}
