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
using _2DRoguelike.Content.Core.World.ExitConditions;
using _2DRoguelike.Content.Core.Entities;

namespace _2DRoguelike.Content.Core.World
{
    static class LevelManager
    {
        // TODO: Methode, die ein Tile im Char Array ändert und im Tile-Array aktualisiert

        public const int numLevel = 3;
        public static int level = 0;
        private static List<Level> levelList;
        private static Vector2 playerposition;
        public static Map currentmap;
        public static Tile[,] currenttilemap;

        public static object EnityManager { get; private set; }

        public static void LoadContent()
        {
            levelList = new List<Level>();
            //levelList.Add(RoomFactory.TestMap());
            //levelList.Add(RoomFactory.BossMap());
            levelList.Add(new Level(new Dungeon(), new KillAllEnemies()));
            currentmap = levelList[level].map;
            currenttilemap = currentmap.tilearray;
            playerposition = new Vector2();
        }
        public static void NextLevel(Player player)
        {
            EntityManager.UnloadAllEntities();
            level++;
            switch (level)
            {
                case 0:
                    levelList.Add(new Level(new Dungeon(), new KillAllEnemies()));
                    break;
                case 1:
                    levelList.Add(new Level(new Dungeon(), new KillAllEnemies()));
                    break;
                case 2:
                    levelList.Add(new Level(new BossMap(24, 12), new KillAllEnemies()));
                    break;
            }
            levelList[level - 1].map.clearEnemies();
            player.Position = levelList[level].map.getSpawnpoint() * new Vector2(32);
            currentmap = levelList[level].map;
            currenttilemap = currentmap.tilearray;
            
        }
        public static void Update(Player player)
        {
            levelList[level].map.Update(player);
            playerposition = player.HitboxCenter;
            levelList[level].exitCondition.Exit();
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

            for (int j = y; j < playerposition.Y / 32 + Yvisiblerange && j < levelList[level].map.height; j++)
            {
                for (int i = x; i < playerposition.X / 32 + Xvisiblerange && i < levelList[level].map.width; i++)
                {
                    spriteBatch.Draw(levelList[level].map.tilearray[i, j].texture, new Rectangle(levelList[level].map.tilearray[i, j].x, levelList[level].map.tilearray[i, j].y, levelList[level].map.tilearray[i, j].width, levelList[level].map.tilearray[i, j].height), Color.White * visibility);
                }
            }
        }
    }
}