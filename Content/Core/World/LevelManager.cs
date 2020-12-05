using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
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
        public const int numLevel = 3;
        public static int level = 0;
        private static List<Map> levelList;


        public static Map maps;
        public static Tile[,] currentLevel ;
        public static void LoadContent()
        {
            levelList = new List<Map>();
            levelList.Add(new TestMap(24, 24));
            levelList.Add(new Dungeon());
            levelList.Add(new Dungeon());
            maps = levelList[level];
            currentLevel = maps.map;
        }
        public static void NextLevel(Player player)
        {
            int x=level;
            level = x + 1;
            player.Position = levelList[level].getSpawnpoint()*new Vector2(32);
            maps = levelList[level];
            currentLevel = maps.map;
        }
        public static void Update(Player player)
        {
            maps.Update(player);
        }
        public static void Draw(SpriteBatch spriteBatch)
        {
            for (int j = 0; j < maps.height; j++)
            {
                for (int i = 0; i < maps.width; i++)
                {
                    spriteBatch.Draw(currentLevel[i, j].texture, new Rectangle(currentLevel[i, j].x, currentLevel[i, j].y, currentLevel[i, j].width, currentLevel[i, j].height), Color.White);
                }
            }
        }
    }
}