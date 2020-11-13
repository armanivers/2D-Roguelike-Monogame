using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace _2DRoguelike.Content.Core.GameDebug
{
    static class GameDebug
    {
        public static HitboxDebug hitboxDebug = new HitboxDebug();
        public static PlayerDebug playerDebug = new PlayerDebug();

        public static void Update()
        {

        }

        public static void Draw(SpriteBatch spriteBatch)
        {

            //hitboxDebug.Draw(spriteBatch);
            //playerDebug.Draw(spriteBatch);

        }
    }
}
