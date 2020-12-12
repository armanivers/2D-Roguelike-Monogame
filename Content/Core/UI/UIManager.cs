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
        public static ExperienceBar experienceBar;
        public static ToolTip toolTip;
        public static Highscore highscore;
        public static void Update(GameTime gameTime)
        {
            healthBar.Update(gameTime);
            skillBar.Update(gameTime);
            mobHealthBars.Update(gameTime);
            experienceBar.Update(gameTime);
            toolTip.Update(gameTime);
            highscore.Update(gameTime);
        }
        public static void DrawStatic(SpriteBatch spriteBatch)
        {
            healthBar.Draw(spriteBatch);
            skillBar.Draw(spriteBatch);
            experienceBar.Draw(spriteBatch);
            toolTip.Draw(spriteBatch);
            highscore.Draw(spriteBatch);
        }

        public static void DrawDynamic(SpriteBatch spriteBatch)
        {
            mobHealthBars.Draw(spriteBatch);
        }

        public static void ForceResolutionUpdate()
        {
            if(healthBar != null && skillBar != null && experienceBar != null && toolTip != null && highscore != null)
            {
                healthBar.ForceResolutionUpdate();
                skillBar.ForceResolutionUpdate();
                experienceBar.ForceResolutionUpdate();
                toolTip.ForceResolutionUpdate();
                highscore.ForceResolutionUpdate();
            }
        }

    }
}
