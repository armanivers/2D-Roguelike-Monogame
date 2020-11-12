using _2DRoguelike.Content.Core.World.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.World
{
    static class LevelManager
    {
        public static Map maps = new Map();

        public static Tile[,] currentLevel = maps.map;


        public static void Update()
        {

        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            for(int i = 0; i < maps.width; i++)
            {
                for(int j = 0; j < maps.height; j++)
                {
                    spriteBatch.Draw(currentLevel[i, j].texture, new Rectangle(currentLevel[i, j].x, currentLevel[i, j].y, currentLevel[i, j].width, currentLevel[i, j].height),Color.White);
                }
            }
        }
    }
}