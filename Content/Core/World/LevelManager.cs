using _2DRoguelike.Content.Core.Entities.Player;
using _2DRoguelike.Content.Core.GameDebug;
using _2DRoguelike.Content.Core.World.Tiles;
using _DRoguelike.Content.Core.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.World
{
    static class LevelManager
    {
        public static Map maps = new RoomDungeon(2,1);
        //public static Map maps = new DungeonMap();
        public static Tile[,] currentLevel = maps.map;

        public static void Update(Player player)
        {
            maps.Update(player);
            currentLevel = maps.map;
        }

        /*public static void Draw(SpriteBatch spriteBatch)
        {
            for(int i = 0; i < maps.roommap[0,0].height; i++)
            {
                for(int j = 0; j < maps.roommap[0,0].width; j++)
                {
                    spriteBatch.Draw(currentLevel[i, j].texture, new Rectangle(currentLevel[i, j].x, currentLevel[i, j].y, currentLevel[i, j].width, currentLevel[i, j].height),Color.White);
                }
            }
        }*/
        public static void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < maps.height; i++)
            {
                for (int j = 0; j < maps.width; j++)
                {
                    spriteBatch.Draw(currentLevel[i, j].texture, new Rectangle(currentLevel[i, j].x, currentLevel[i, j].y, currentLevel[i, j].width, currentLevel[i, j].height), Color.White);
                }
            }
        }
    }
}