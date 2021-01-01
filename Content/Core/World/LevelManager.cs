using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.GameDebug;
using _2DRoguelike.Content.Core.World.Rooms;
using _2DRoguelike.Content.Core.World.Tiles;
using _2DRoguelike.Content.Core.World.Maps;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.World
{
    static class LevelManager
    {
        // TODO: Methode, die ein Tile im Char Array ändert und im Tile-Array aktualisiert

        public const int numLevel = 3;
        public static int level = 0;
        private static List<Map> levelList;
        private static Vector2 playerposition;
        public static Map currentmap;
        public static Tile[,] currentLevel;
        public static void LoadContent()
        {
            levelList = new List<Map>();
            //levelList.Add(RoomFactory.TestMap());
            //levelList.Add(RoomFactory.BossMap());
            levelList.Add(new Dungeon());
            currentmap = levelList[level];
            currentLevel = currentmap.map;
            playerposition = new Vector2();
        }
        public static void NextLevel(Player player)
        {
            level++;
            levelList.Add(new Dungeon());
            levelList[level-1].clearEnemies();
            player.Position = levelList[level].getSpawnpoint() * new Vector2(32);
            currentmap = levelList[level];
            currentLevel = currentmap.map;
        }
        public static void Update(Player player)
        {
            currentmap.Update(player);
            playerposition = player.HitboxCenter;
        }

        public static void UnloadContent()
        {
            level = 0;
            levelList.Clear();
        }
        public static void Draw(SpriteBatch spriteBatch)
        {
            DrawWithVisibility(spriteBatch, 1.0f, 12, 9);
        }
        private static void DrawWithVisibility(SpriteBatch spriteBatch, float visibility, int Xvisiblerange, int Yvisiblerange)
        {
            int y = (int)playerposition.Y / 32;
            int x = (int)playerposition.X / 32;

            for (int i = 0; i < Xvisiblerange; i++)
            {
                if (x > 0) x--;
            }
            for (int i = 0; i < Yvisiblerange; i++)
            {
                if (y > 0) y--;
            }

            for (int j = y; j < playerposition.Y / 32 + Yvisiblerange && j < currentmap.height; j++)
            {
                for (int i = x; i < playerposition.X / 32 + Xvisiblerange && i < currentmap.width; i++)
                {
                    spriteBatch.Draw(currentLevel[i, j].texture, new Rectangle(currentLevel[i, j].x, currentLevel[i, j].y, currentLevel[i, j].width, currentLevel[i, j].height), Color.White * visibility);
                }
            }
        }
    }
}