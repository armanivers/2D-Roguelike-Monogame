using _2DRoguelike.Content.Core.Entities.Player;
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

        public static void Update(GameTime gameTime)
        {
            healthBar.Update(gameTime);
            skillBar.Update(gameTime);
        }
        public static void Draw(SpriteBatch spriteBatch)
        {
            healthBar.Draw(spriteBatch);
            skillBar.Draw(spriteBatch);
        }


    }
}
