﻿using _2DRoguelike.Content.Core.Entities.Player;
using _2DRoguelike.Content.Core.World.Tiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2DRoguelike.Content.Core.World
{
    abstract class Map
    {
        public Tile[,] map;
        public char[,] charmap;
        public bool[,] boolmap;
        public int width;
        public int height;

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
            width = 128;
            height = 128;     
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
                    if(level[i, j].Equals(RoomObject.Wall) || level[i, j].Equals(RoomObject.Corner))
                    {
                        result[i, j] = new Tile(true, i, j);
                    }
                    else
                    {
                        result[i, j] = new Tile(false, i, j);
                    } 
                }
            }
            return result;
        }
    }
}
