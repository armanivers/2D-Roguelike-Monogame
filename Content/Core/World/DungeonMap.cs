using System;
using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.World;
using Microsoft.Xna.Framework;

namespace _DRoguelike.Content.Core.World
{
    class DungeonMap : Map
    {
        public Vector2 spawnpoint;
        Room room;
        static Random rndState = new Random();
        static int rnd(int x) => rndState.Next() % x;

        public DungeonMap() : base()
        {
            charmap = new char[width, height];
            spawnpoint = new Vector2((float)(height * 32 - 17 - 5), (float)(width * 32 - 14 - 25));
            // init the map
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    charmap[y, x] = ' ';

            // generate
            addRoom(start: true);

            for (int j = 0; j < 5000; j++)
                addRoom(start: false);
            map = fillTile(charmap);
        }

        public override Vector2 getSpawnpoint()
        {
            return spawnpoint;
        }


        public void addRoom(bool start)
        {
            int w = rnd(10) + 5;
            int h = rnd(6) + 3;
            int rx = rnd(width - w - 2) + 1;
            int ry = rnd(height - h - 2) + 1;

            int doorCount = 0, doorX = 0, doorY = 0;

            // generate a door
            if (!start)
            {
                // See if we can process this tile
                for (int y = ry - 1; y < ry + h + 2; y++)
                    for (int x = rx - 1; x < rx + w + 2; x++)
                        if (charmap[y, x] == '.')
                            return;

                // find candidate tiles for the door
                for (int y = ry - 1; y < ry + h + 2; y++)
                    for (int x = rx - 1; x < rx + w + 2; x++)
                    {
                        bool s = x < rx || x > rx + w;
                        bool t = y < ry || y > ry + h;
                        if ((s ^ t) && charmap[y, x] == '#')
                        {
                            ++doorCount;
                            if (rnd(doorCount) == 0)
                            {
                                doorX = x;
                                doorY = y;
                            }
                        }
                    }


                if (doorCount == 0)
                    return;
            }

            // generate a room
            for (int y = ry - 1; y < ry + h + 2; y++)
                for (int x = rx - 1; x < rx + w + 2; x++)
                {
                    bool s = x < rx || x > rx + w;
                    bool t = y < ry || y > ry + h;
                    if (s && t)
                        charmap[y, x] = '!'; // avoid generation of doors at corners
                    else if (s ^ t)
                        charmap[y, x] = '#';
                    else
                        charmap[y, x] = '.';
                }

            // place the door
            if (doorCount > 0)
                charmap[doorY, doorX] = '+';

            if (start)
            {
                charmap[rnd(h) + ry, rnd(w) + rx] = '@';
                spawnpoint = new Vector2((float)(rnd(h) + ry * 32 - 17 - 5), (float)(rnd(w) + rx * 32 - 14 - 25));
            }
            else
            {
                // place other objects
                for (int j = 0; j < (rnd(6) + 1); j++)
                {
                    char thing = rnd(4) == 0 ? '$' : (char)(65 + rnd(62));
                    charmap[rnd(h) + ry, rnd(w) + rx] = thing;
                }
            }
        }

        public override void Update(Player player)
        {
        }

        public override void clearEnemies()
        {
            throw new NotImplementedException();
        }

        public override void clearEnities()
        {
            throw new NotImplementedException();
        }
    }
}
