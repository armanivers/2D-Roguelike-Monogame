﻿using _2DRoguelike.Content.Core.World.Tiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.World
{
    class Map
    {
        bool[,] boolmap;
        public Tile[,] map;
        public int width;
        public int height;

        //Variablen zum einstellen der generierten Map
        int deathLimit = 4;
        int birthLimit = 5;
        float chanceToStartAlive = 0.45f;
        Random random = new Random();
        int simulationsteps = 2;
        
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
        
        #region RandomMapGenerator
        public Map(int width, int height)
        {
            this.width = width;
            this.height = height;
            boolmap = initialiseMap(new bool[width, height]);
            for (int i = 0; i < simulationsteps; i++)
            {
                boolmap = doSimulationStep(boolmap);
            }
            map = fillTile(boolmap);
        }
        public Map()
        {
            width = 128;
            height = 128;
            boolmap = initialiseMap(new bool[width, height]);
            for (int i = 0; i < simulationsteps; i++)
            {
                boolmap = doSimulationStep(boolmap);
            }
            map = fillTile(boolmap);
        }
        public  Vector2 getSpawnpoint()
        {
            Vector2 alternativespawnpoint=new Vector2();
            for (int x = 1; x < boolmap.GetLength(0)-1; x++)
            {
                for (int y = 1; y < boolmap.GetLength(1)-1; y++)
                {
                    int nbs = countAliveNeighbours(boolmap, x, y);
                    if (nbs == 0)
                    {
                        return new Vector2((float)x, (float)y);
                    }
                    else if (nbs < 6)
                    {
                        alternativespawnpoint.X = (float)x;
                        alternativespawnpoint.Y = (float)y;
                    }
                }
            }
            return alternativespawnpoint;
        }
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
        public bool[,] initialiseMap(bool[,] map)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if ((float)random.NextDouble()< chanceToStartAlive)
                    {
                        map[x,y] = true;
                    }
                }
            }
            return map;
        }

        //Returns the number of cells in a ring around (x,y) that are alive.
        public int countAliveNeighbours(bool[,] map, int x, int y)
        {
            int count = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    int neighbour_x = x + i;
                    int neighbour_y = y + j;
                    //If we're looking at the middle point
                    if (i == 0 && j == 0)
                    {
                        //Do nothing, we don't want to add ourselves in!
                    }
                    //In case the index we're looking at it off the edge of the map
                    else if (neighbour_x < 0 || neighbour_y < 0 || neighbour_x >= map.GetLength(0) || neighbour_y >= map.GetLength(1))
                    {
                        count = count + 1;
                    }
                    //Otherwise, a normal check of the neighbour
                    else if (map[neighbour_x,neighbour_y])
                    {
                        count = count + 1;
                    }
                }
            }
            return count;
        }
        public bool[,] doSimulationStep(bool[,] oldMap)
        {
            bool[,] newMap = new bool[width,height];
            //Loop over each row and column of the map
            for (int x = 0; x < oldMap.GetLength(0); x++)
            {
                for (int y = 0; y < oldMap.GetLength(1); y++)
                {
                    int nbs = countAliveNeighbours(oldMap, x, y);
                    //The new value is based on our simulation rules
                    //First, if a cell is alive but has too few neighbours, kill it.
                    if (oldMap[x,y])
                    {
                        if (nbs < deathLimit)
                        {
                            newMap[x,y] = false;
                        }
                        else
                        {
                            newMap[x,y] = true;
                        }
                    } //Otherwise, if the cell is dead now, check if it has the right number of neighbours to be 'born'
                    else
                    {
                        if (nbs > birthLimit)
                        {
                            newMap[x,y] = true;
                        }
                        else
                        {
                            newMap[x,y] = false;
                        }
                    }
                }
            }
            return newMap;
        }
        #endregion RandomMapGenerator
        
    }
}