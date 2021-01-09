using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace _2DRoguelike.Content.Core.GameDebugger
{
    static class GameDebug
    {
        public static HitboxDebug hitboxDebug = new HitboxDebug();
        public static PlayerDebug playerDebug = new PlayerDebug();

        public static void DrawStatic(SpriteBatch spritebatch)
        {
            playerDebug.Draw(spritebatch);
        }

        public static void DrawDynamic(SpriteBatch spriteBatch)
        {
            hitboxDebug.Draw(spriteBatch);
        }

        public static void AddToBoxDebugBuffer(Rectangle box, Color color, int timeToDraw = 0)
        {
            hitboxDebug.AddToBoxDebugBuffer(box, color, timeToDraw);
        }

        public static void AddToBoxDebugBuffer(Rectangle box, Color color, bool always)
        {
            hitboxDebug.AddToBoxDebugBuffer(box, color, always);
        }

        public static void UnloadHitboxBuffer() {
            hitboxDebug.boxDebugBuffer.Clear();
        }

    }
}
