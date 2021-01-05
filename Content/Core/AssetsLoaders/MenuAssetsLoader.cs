using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.AssetsLoaders
{
    class MenuAssetsLoader
    {
        // Menu Data
        public Texture2D DungeonBackground { get; private set; }
        public Texture2D FirepitBackground { get; private set; }
        public Texture2D CaveDoorBackground { get; private set; }
        public Texture2D Blank { get; private set; }
        public Texture2D Gradient { get; private set; }

        // Cutscenes
        public Texture2D NPCTalk00 { get; private set; }
        
        public void Load(ContentManager content)
        {
            // Menu Data
            DungeonBackground = content.Load<Texture2D>("Assets/Graphics/Menus/DungeonBackground");
            FirepitBackground = content.Load<Texture2D>("Assets/Graphics/Menus/FirepitBackground");
            CaveDoorBackground = content.Load<Texture2D>("Assets/Graphics/Menus/CaveBackground");
            Blank = content.Load<Texture2D>("Assets/Graphics/Menus/blank");
            Gradient = content.Load<Texture2D>("Assets/Graphics/Menus/gradient");

            // Cutscenes
            NPCTalk00 = content.Load<Texture2D>("Assets/Graphics/Cutscenes/NPCTalk00");
        }
    }
}
