using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core
{
    static class TextureManager
    {
        public static Texture2D Player { get; private set; }

        public static void Load(ContentManager content)
        {
            Player = content.Load<Texture2D>("Assets/Graphics/PlayerElements/playerSheet");
        }
    }
}
