using _2DRoguelike.Content.Core.Entities;
using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.World.Rooms;
using _2DRoguelike.Content.Core.World.Tiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2DRoguelike.Content.Core.World.Maps
{
    abstract class Map
    {



        public Tile[,] tilearray;
        public char[,] chararray;
        public int width;
        public int height;
        public Room currentroom{ get; set; }
        public static readonly Random Random = new Random();
        public static int rnd(int x) => Random.Next() % x;
        #region TestMap
        /*
        public Map()
        {
            this.width = 12;
            this.height = 38;
            map = loadLevel();
        }

        public Tile[,] loadLevel()
        {
    
            int[,] level1 = new int[12,38]
            {
                {0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                {0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {0,0,0,1,0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {1,0,0,1,1,1,0,1,1,1,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {1,0,0,1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {1,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {1,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {1,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {1,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {1,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {1,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
            };

            Tile[,] result = new Tile[12, 38];

            for (int i = 0; i < level1.GetLength(0); i++)
            {
                for(int j = 0; j < level1.GetLength(1); j++)
                {
                    result[i, j] = new Tile(level1[i, j],i,j);
                }
            }
            return result;
        }
        */
        #endregion TestMap

        public Map(int width, int height)
        {
            this.width = width;
            this.height = height;
        }
        public Map()
        {
            width = 96;
            height = 96;
        }

        public abstract Vector2 getSpawnpoint();
        public abstract void Update(Player player);

        public Tile[,] fillTile(bool[,] level)
        {
            Tile[,] result = new Tile[level.GetLength(0), level.GetLength(1)];
            for (int i = 0; i < level.GetLength(0); i++)
            {
                for (int j = 0; j < level.GetLength(1); j++)
                {
                    result[i, j] = new Tile(level[i, j], i, j);
                }
            }
            return result;
        }
        public Tile[,] fillTile(char[,] level)
        {
            Tile[,] result = new Tile[level.GetLength(0), level.GetLength(1)];
            for (int i = 0; i < level.GetLength(0); i++)
            {
                for (int j = 0; j < level.GetLength(1); j++)
                {
                    if (level[i, j].Equals(RoomObject.Wall) || level[i, j].Equals(RoomObject.Corner) || level[i, j] == 0)
                    {
                        result[i, j] = new Tile(RoomObject.Wall, i, j);
                    }
                    else if (level[i, j].Equals(RoomObject.Exit))
                    {
                        result[i, j] = new Tile(RoomObject.Exit, i, j);
                    }
                    else
                    {
                        result[i, j] = new Tile(RoomObject.EmptySpace, i, j);
                    }
                }
            }
            return result;
        }
        public abstract void clearEnemies();
        public abstract void clearEnities();
        public abstract int CountEnemies();
        public abstract int EnemiesAlive();
        public abstract bool AddKeyToRoom(int roomnmb);
        public abstract Room getExitRoom();
    }
}
