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
        private static Vector2 playerposition;

        public static Map maps;
        public static Tile[,] currentLevel;
        public static void LoadContent()
        {
            levelList = new List<Map>();
            levelList.Add(new TestMap(24, 24));
            levelList.Add(new Dungeon());
            levelList.Add(new Dungeon());
            maps = levelList[level];
            currentLevel = maps.map;
            playerposition = new Vector2();
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
            playerposition = player.HitboxCenter;
        }

        public static void UnloadContent()
        {
            level = 0;
            levelList.Clear();
        }
        public static void Draw(SpriteBatch spriteBatch)
        {
            float visibility = 1.0f;
            for (int j = 0; j < maps.height; j++)
            {
                for (int i = 0; i < maps.width; i++)
                {
                    
                    if (j*32 >playerposition.Y+5*32 || i * 32 > playerposition.X + 5 * 32 )
                    {
                        visibility = 0.0f;
                    }
                    if(j * 32 < playerposition.Y - 6 * 32 ||i * 32 < playerposition.X - 6 * 32)
                    {
                        visibility = 0.0f;
                    }
                    spriteBatch.Draw(currentLevel[i, j].texture, new Rectangle(currentLevel[i, j].x, currentLevel[i, j].y, currentLevel[i, j].width, currentLevel[i, j].height), Color.White*visibility);
                    visibility = 1.0f;
                }
            }
        }*/
        public static void Draw(SpriteBatch spriteBatch)
        {
            //DrawWithVisibility(spriteBatch, 0.25f, 15);
            //DrawWithVisibility(spriteBatch, 0.5f, 10);
            DrawWithVisibility(spriteBatch, 1.0f, 15,10);
        }
        private static void DrawWithVisibility(SpriteBatch spriteBatch, float visibility, int Xvisiblerange,int Yvisiblerange)
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
            
            for (int j = y; j < playerposition.Y / 32 + Yvisiblerange && j < maps.height; j++)
            {
                for (int i = x; i < playerposition.X / 32 + Xvisiblerange && i < maps.width; i++)
                {
                    spriteBatch.Draw(currentLevel[i, j].texture, new Rectangle(currentLevel[i, j].x, currentLevel[i, j].y, currentLevel[i, j].width, currentLevel[i, j].height), Color.White * visibility);
                }
            }
        }
    }
}