using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.World.Tiles
{
    static class TileTextureManager
    {
        //Wood Tiles
        public const int WOODTILES = 1;
        public static Texture2D[] woodtileslist { get; private set; }
        //Stone Tiles
        public const int STONETILES = 1;
        public static Texture2D[] stonetileslist { get; private set; }
        //Grass Tiles
        public const int GRASSTILES = 0;
        public static Texture2D[] grasstileslist { get; private set; }
        //Ladder
        public const int LADDERTILES = 1;
        public static Texture2D[] laddertileslist { get; private set; }

        public static void Load(ContentManager content)
        {
            woodtileslist = new Texture2D[WOODTILES];
            for(int i = 0; i < WOODTILES; i++)
            {
                woodtileslist[i]= content.Load<Texture2D>("Assets/Graphics/WorldElements/Tiles/wood"+i);
            }
            stonetileslist = new Texture2D[STONETILES];
            for (int i = 0; i < STONETILES; i++)
            {
                stonetileslist[i] = content.Load<Texture2D>("Assets/Graphics/WorldElements/Tiles/stone" + i);
            }
            grasstileslist = new Texture2D[GRASSTILES];
            for (int i = 0; i < GRASSTILES; i++)
            {
                grasstileslist[i] = content.Load<Texture2D>("Assets/Graphics/WorldElements/Tiles/grass" + i);
            }
            laddertileslist = new Texture2D[LADDERTILES];
            for (int i = 0; i < LADDERTILES; i++)
            {
                laddertileslist[i] = content.Load<Texture2D>("Assets/Graphics/WorldElements/Tiles/ladder" + i);
            }
        }
    }
}
