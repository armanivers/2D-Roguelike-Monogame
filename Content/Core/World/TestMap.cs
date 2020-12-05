using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.World
{
    class TestMap : Map
    {
        Room room;
        public TestMap(int width, int height):base(width,height)
        {
            room = new Room(width, height);
            room.setExit();
            dottedArea(width/2,height/2);
            placeTables(6, 17);
            placelabyrinth();
            placeTables(3, 15);
            map = fillTile(room.room);
        }
        public void placelabyrinth()
        {
            for (int y = 1; y < height/2; y++)
            {
                for (int x = width/2; x < width - 1; x++)
                {
                    if (y % 2 == 0)
                    {
                        room.room[x, y] = RoomObject.Wall;
                    }
                    else if (x == width / 2 && y!=1 &&y!= height / 2-3 )
                    {
                        room.room[x, y] = RoomObject.Wall;
                    }
                }
            }
            room.room[22, 2] = RoomObject.EmptySpace;
            room.room[13, 4] = RoomObject.EmptySpace;
            room.room[22, 6] = RoomObject.EmptySpace;
            room.room[13, 8] = RoomObject.EmptySpace;
        }
        public void dottedArea(int x_start,int y_start)
        {
            for (int y=y_start;y<height-1;y++)
            {
                for (int x=x_start;x<width-1;x++)
                {
                    if (y % 2 == 0)
                    {
                        if ((y + x) % 2 == 0)
                        {
                            room.room[x, y] = RoomObject.Wall;
                        }
                    }
                }
            }
        }
        public void placeTables(int x_start, int y_start)
        {
            for (int y = y_start; y < y_start+3; y++)
            {
                for (int x = x_start; x < x_start +2; x++)
                {
                    room.room[x, y] = RoomObject.Wall;
                }
            }
        }
        public override Vector2 getSpawnpoint()
        {
            return new Vector2(1, 1);
        }

        public override void Update(Player player)
        {
          if (player.hitbox.Intersects(room.exithitbox))
          {
               LevelManager.NextLevel(player);
          }
        }
    }
}
