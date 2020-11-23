using _2DRoguelike.Content.Core.Entities.Player;
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
        public Room[,] roommap;
        public int currentroomx;
        public int currentroomy;
        public int nextroomx;
        public int nextroomy;
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
            currentroomx = 0;
            currentroomy = 0;
        }
        public Map()
        {
            width = 64;
            height = 64;        
        }
        public abstract Vector2 getSpawnpoint();
        public void Update(Player player)
        {
            if (!roommap[currentroomy, currentroomx].roomhitbox.Contains(player.GetTileCollisionHitbox()))
            {
                nextroomx = 1;
                nextroomy = 0;
                map=fillTile(CombineRooms(CardinalDirection.East));
            }
            else
            {
                map=fillTile(roommap[currentroomy, currentroomx].room);
                width = roommap[currentroomy, currentroomx].width;
                height = roommap[currentroomy, currentroomx].height;
            }
        }
        public char[,] CombineRooms(CardinalDirection direction)
        {
            Room current = roommap[currentroomy, currentroomx];
            Room next = roommap[nextroomy, nextroomx];
            char[,] returnvalue=null;
            switch (direction)
            {
                case CardinalDirection.East:
                    if (current.height > next.height)
                    {
                        returnvalue = new char[current.height,current.width+next.width];
                    }else returnvalue = new char[next.height, current.width + next.width];
                    for (int y = 0; y < current.height; y++)
                    {
                        for (int x = 0; x <current.width; x++)
                        {
                            returnvalue[y, x] = current.room[y, x];
                        }
                    }
                    for (int y = 0; y < next.height; y++)
                    {
                        for (int x = 0; x < next.width; x++)
                        {
                            returnvalue[y, x+current.width] = next.room[y, x];
                        }
                    }
                    break;
            }
            height = returnvalue.GetLength(0);
            width = returnvalue.GetLength(1);
            return returnvalue;
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
