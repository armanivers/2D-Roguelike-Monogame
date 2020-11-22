using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.World
{
    class RoomDungeon : Map
    {
        public Vector2 spawnpoint;
        static Random rndState = new Random();
        static int rnd(int x) => rndState.Next() % x;

        public override Vector2 getSpawnpoint()
        {
            return spawnpoint;
        }

        public RoomDungeon() : base()
        {
            FillRoomMap();
            map = fillTile(roommap[currentroomy,currentroomx].room);
        }
        public RoomDungeon(int width, int height) : base(width,height)
        {
            roommap = new Room[height, width];
            FillRoomMap();
             map = fillTile(roommap[currentroomy,currentroomx].room);
        }
        public void FillRoomMap()
        {
            for(int y = 0; y < height; y++)
            {
                for(int x = 0; x < width; x++)
                {
                    roommap[y, x] = new Room();
                    if(y==0 &&x==0)
                    {
                        spawnpoint = roommap[y, x].spawnpoint;
                    }
                }

            }

        }
    }
}
