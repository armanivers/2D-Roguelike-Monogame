using _2DRoguelike.Content.Core.World.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.World
{
    class LevelManager
    {
        public static Tile[,] currentLevel;

        Map map1;

        public LevelManager()
        {
            map1 = new Map(7, 13);
            currentLevel = map1.map;
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for(int i = 0; i < map1.width; i++)
            {
                for(int j = 0; j < map1.height; j++)
                {
                    spriteBatch.Draw(currentLevel[i, j].texture, new Rectangle(currentLevel[i, j].x, currentLevel[i, j].y, currentLevel[i, j].width, currentLevel[i, j].height),Color.White);
                }
            }
        }
    }
}
