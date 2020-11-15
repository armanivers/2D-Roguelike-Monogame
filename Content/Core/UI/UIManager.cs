using _2DRoguelike.Content.Core.Entities.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.UI
{
    static class UIManager
    {
        private static HealthBar healthBar = new HealthBar(Player.Instance);

        public static void Update(GameTime gameTime)
        {
            healthBar.Update(gameTime) ;
        }
        public static void Draw(SpriteBatch spriteBatch)
        {
            healthBar.Draw(spriteBatch);
        }


    }
}
