using _2DRoguelike.Content.Core.Entities;
using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.Entities.Creatures.Enemies;
using _2DRoguelike.Content.Core.World.Rooms;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;


namespace _2DRoguelike.Content.Core.World.Maps
{
    class Dungeon : Map
    {
        private static readonly int ROOMTRIES = 2000;
        public const int NumRooms = 10;

        public List<Room> roomlist { get; }
        public Vector2 spawnpoint;
        public Dungeon() : base()
        {
            roomlist = new List<Room>();
            chararray = new char[width, height];
            Generate();
            tilearray = fillTile(chararray);
        }
        public void Generate()
        {
            Room previousRoom = RoomFactory.RandomRoomWithEnemies();
            for (int i = 0; i < NumRooms; i++)
            {
                Room room;
                if (i == 0)
                {
                    room = RoomFactory.StartingRoom();
                }
                else
                {
                    room = RoomFactory.RandomRoomWithEnemies();
                }
                int roomfindingtries = 0;
                do
                {
                    int addedvalue = 3;
                    //Ein Raum wird Relational weit zum Vorgänger in der Welt platziert. Dabei ist der weiteste entfernte Punkt wo ein Raum platziert werden kann die Maximale Größe eines Raum(MAXROOMZISE).
                    room.setXPos(Map.Random.Next(previousRoom.XPos-Room.MAXROOMSIZE- addedvalue < 0?0:previousRoom.XPos - Room.MAXROOMSIZE- addedvalue, previousRoom.XPos + Room.MAXROOMSIZE+ addedvalue > width - room.Width? width - room.Width: previousRoom.XPos + Room.MAXROOMSIZE+ addedvalue));
                    room.setYPos(Map.Random.Next(previousRoom.YPos - Room.MAXROOMSIZE - addedvalue < 0 ? 0 : previousRoom.YPos - Room.MAXROOMSIZE- addedvalue, previousRoom.YPos + Room.MAXROOMSIZE+ addedvalue > height - room.Height? height - room.Height: previousRoom.YPos + Room.MAXROOMSIZE+ addedvalue));
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

                                if (chararray[x, previousRoom.CentreY] != RoomObject.EmptySpace)
                                {
                                    chararray[x, previousRoom.CentreY] = RoomObject.EmptySpace;
                                    if (chararray[x, previousRoom.CentreY + 1] != RoomObject.Wall)
                                    {
                                        chararray[x, previousRoom.CentreY + 1] = RoomObject.EmptySpace;
                                    }
                                }
                            }
                            for (int y = startY; y < endY + 1; y++)
                            {

                                if (chararray[room.CentreX, y] != RoomObject.EmptySpace)
                                {
                                    chararray[room.CentreX, y] = RoomObject.EmptySpace;
                                    if (chararray[room.CentreX + 1, y] != RoomObject.Wall)
                                    {
                                        chararray[room.CentreX + 1, y] = RoomObject.EmptySpace;
                                    }
                                }
                            }
                        }
                        else
                        {
                            for (int y = startY; y < endY + 1; y++)
                            {

                                if (chararray[previousRoom.CentreX, y] != RoomObject.EmptySpace)
                                {
                                    chararray[previousRoom.CentreX, y] = RoomObject.EmptySpace;
                                    if (chararray[previousRoom.CentreX, y] != RoomObject.Wall)
                                    {
                                        chararray[previousRoom.CentreX + 1, y] = RoomObject.EmptySpace;
                                    }
                                }
                            }

                            for (int x = startX; x < endX; x++)
                            {

                                if (chararray[x, room.CentreY] != RoomObject.EmptySpace)
                                {
                                    chararray[x, room.CentreY] = RoomObject.EmptySpace;
                                    if (chararray[x, room.CentreY] != RoomObject.Wall)
                                    {
                                        chararray[x, room.CentreY + 1] = RoomObject.EmptySpace;
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
            SpawnEnemies();
        }
        public void SpawnEnemies()
        {
            //First room should not have enemies
            for(int i=1;i<NumRooms;i++)
            {
                roomlist[i].placeEnemies();
            }
        }
        public void room2Map(Room room)
        {
            for (int y = room.YPos; y < room.YPos + room.Height; y++)
            {
                for (int x = room.XPos; x < room.XPos + room.Width; x++)
                {
                    chararray[x, y] = room.room[x - room.XPos, y - room.YPos];
                }
            }
        }
        public bool avoidRoomCollision(Room room)
        {
            for (int y = room.YPos; y < room.YPos + room.Height; y++)
            {
                for (int x = room.XPos; x < room.XPos + room.Width; x++)
                {
                    if (chararray[x, y] != 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public override Vector2 getSpawnpoint()
        {
            Room room = roomlist[0];
            return new Vector2((float)room.CentreX, (float)room.CentreY);
        }
        public override void Update(Player player)
        {
            //TODO in eigene Methoden auslagern SetCurrentRoom und GoToNextLevel(), da so imperformant (kein Schleifenabbruch)
            currentroom = null;
            for (int i = 0; i < NumRooms; i++)
            {
                if (roomlist[i].roomhitbox.Intersects(player.Hitbox))
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

        public override int CountEnemies()
        {
            int returnvalue = 0;
            foreach(Room r in roomlist)
            {
                returnvalue += r.enemylist.Count;
            }
            return returnvalue;
        }

        public override int EnemiesAlive()
        {
            int returnvalue = 0;
            foreach (Room r in roomlist)
            {
                foreach(Enemy e in r.enemylist)
                {
                    if (!e.dead)
                    {
                        returnvalue++;
                    }
                }
            }
            return returnvalue;
        }
    }
}
