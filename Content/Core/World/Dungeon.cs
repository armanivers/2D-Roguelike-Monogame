using _2DRoguelike.Content.Core.Entities;
using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.Entities.Creatures.Enemies;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;


namespace _2DRoguelike.Content.Core.World
{
    class Dungeon : Map
    {
        private static readonly int ROOMTRIES = 2000;
        List<Room> roomlist;
        public const int NumRooms = 10;
        public Vector2 spawnpoint;
        public Dungeon() : base()
        {
            roomlist = new List<Room>();
            charmap = new char[width, height];
            Generate();
            map = fillTile(charmap);
        }
        public void Generate()
        {
            Room previousRoom = new Room();
            for (int i = 0; i < NumRooms; i++)
            {
                Room room = new Room();
                int roomfindingtries = 0;
                do
                {
                    room.setXPos(Map.Random.Next(previousRoom.XPos-24<0?0:previousRoom.XPos - 24, previousRoom.XPos + 24>width - room.Width? width - room.Width: previousRoom.XPos + 24));

                    room.setYPos(Map.Random.Next(previousRoom.YPos - 24 < 0 ? 0 : previousRoom.YPos - 24, previousRoom.YPos + 24>height - room.Height? height - room.Height: previousRoom.YPos + 24));


                    //room.setXPos(Map.Random.Next(0, width - room.Width));
                    //room.setYPos(Map.Random.Next(0, height - room.Height));
                    roomfindingtries++;
                } while (!avoidRoomCollision(room) && roomfindingtries <= ROOMTRIES);
                if (roomfindingtries != ROOMTRIES)
                {
                    if (i == NumRooms - 1)
                    {
                        room.setExit();
                    }
                    room2Map(room);
                    if (i > 0)
                    {
                        int startX = Math.Min(room.CentreX, previousRoom.CentreX);
                        int startY = Math.Min(room.CentreY, previousRoom.CentreY);
                        int endX = Math.Max(room.CentreX, previousRoom.CentreX);
                        int endY = Math.Max(room.CentreY, previousRoom.CentreY);

                        if (Map.Random.Next(1) == 0)
                        {
                            for (int x = startX; x < endX; x++)
                            {

                                if (charmap[x, previousRoom.CentreY] != RoomObject.EmptySpace)
                                {
                                    charmap[x, previousRoom.CentreY] = RoomObject.EmptySpace;
                                    if (charmap[x, previousRoom.CentreY + 1] != RoomObject.Wall)
                                    {
                                        charmap[x, previousRoom.CentreY + 1] = RoomObject.EmptySpace;
                                    }
                                }
                            }
                            for (int y = startY; y < endY + 1; y++)
                            {

                                if (charmap[room.CentreX, y] != RoomObject.EmptySpace)
                                {
                                    charmap[room.CentreX, y] = RoomObject.EmptySpace;
                                    if (charmap[room.CentreX + 1, y] != RoomObject.Wall)
                                    {
                                        charmap[room.CentreX + 1, y] = RoomObject.EmptySpace;
                                    }
                                }
                            }
                        }
                        else
                        {
                            for (int y = startY; y < endY + 1; y++)
                            {

                                if (charmap[previousRoom.CentreX, y] != RoomObject.EmptySpace)
                                {
                                    charmap[previousRoom.CentreX, y] = RoomObject.EmptySpace;
                                    if (charmap[previousRoom.CentreX, y] != RoomObject.Wall)
                                    {
                                        charmap[previousRoom.CentreX + 1, y] = RoomObject.EmptySpace;
                                    }
                                }
                            }

                            for (int x = startX; x < endX; x++)
                            {

                                if (charmap[x, room.CentreY] != RoomObject.EmptySpace)
                                {
                                    charmap[x, room.CentreY] = RoomObject.EmptySpace;
                                    if (charmap[x, room.CentreY] != RoomObject.Wall)
                                    {
                                        charmap[x, room.CentreY + 1] = RoomObject.EmptySpace;
                                    }
                                }
                            }

                        }
                    }
                    roomlist.Add(room);
                    previousRoom = room;
                }
                else
                {
                    previousRoom.setExit();
                    i = NumRooms;
                }
            }
            foreach(Room room in roomlist)
            {
                room.placeEnemies();
            }
        }
        public void room2Map(Room room)
        {
            for (int y = room.YPos; y < room.YPos + room.Height; y++)
            {
                for (int x = room.XPos; x < room.XPos + room.Width; x++)
                {
                    charmap[x, y] = room.room[x - room.XPos, y - room.YPos];
                }
            }
        }
        public bool avoidRoomCollision(Room room)
        {
            for (int y = room.YPos; y < room.YPos + room.Height; y++)
            {
                for (int x = room.XPos; x < room.XPos + room.Width; x++)
                {
                    if (charmap[x, y] != 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public override Vector2 getSpawnpoint()
        {
            Room room = roomlist[1];
            return new Vector2((float)room.CentreX, (float)room.CentreY);
        }
        public override void Update(Player player)
        {
            for (int i = 0; i < NumRooms; i++)
            {
                if (player.Hitbox.Intersects(roomlist[i].roomhitbox))
                {
                    currentroom = roomlist[i];
                    if (roomlist[i].exitroom)
                    {
                        if (player.GetTileCollisionHitbox().Intersects(roomlist[i].exithitbox))
                        {
                            LevelManager.NextLevel(player);
                        }
                    }
                }
                else
                {
                    currentroom = null;
                }
            }
        }

        public override void clearEnemies()
        {
            foreach(Room r in roomlist)
            {
                foreach(Enemy e in r.enemylist)
                {
                    e.isExpired=true;
                }
                r.enemylist = null;
            }
        }

        public override void clearEnities()
        {
            foreach (Room r in roomlist)
            {
                foreach (EntityBasis e in r.entitylist)
                {
                    e.isExpired = true;
                }
                r.entitylist = null;
            }
        }
    }
}
