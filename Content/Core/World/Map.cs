using _2DRoguelike.Content.Core.World.Tiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.World
{
    class Map
    {
        public Tile[,] map;
        public int width;
        public int height;

        public Map()
        {
            this.width = 12;
            this.height = 13;
            map = loadLevel();
        }

        public Tile[,] loadLevel()
        {
    
            int[,] level1 = new int[12,13]
            {
                {1,1,1,1,1,1,1,1,1,1,1,1,1},
                {1,0,0,0,0,0,0,0,0,0,0,0,1},
                {1,0,0,1,0,0,0,0,1,0,0,0,1},
                {1,0,0,1,1,1,0,1,1,1,0,0,1},
                {1,0,0,1,0,0,0,0,1,0,0,0,1},
                {1,0,0,0,0,0,0,0,0,0,0,0,1},
                {1,0,0,0,0,0,0,0,0,0,0,0,1},
                {1,0,0,0,0,0,0,0,0,0,0,0,1},
                {1,0,0,0,0,0,0,0,0,0,0,0,1},
                {1,0,0,0,0,0,0,0,0,0,0,0,1},
                {1,0,0,0,0,0,0,0,0,0,0,0,1},
                {1,1,1,1,1,1,1,1,1,1,1,1,1}
            };

            System.Diagnostics.Debug.Print("Rank = " + level1.Rank + " Length = " + level1.Length);

            Tile[,] result = new Tile[12,13];

            for (int i = 0; i < 12; i++)
            {
                for(int j = 0; j < 13; j++)
                {
                    result[i, j] = new Tile(level1[i, j],i,j);
                }
            }
            return result;
        }

    }
}
