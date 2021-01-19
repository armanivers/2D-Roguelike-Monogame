using _2DRoguelike.Content.Core.Entities;
using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.Entities.Creatures.Enemies;
using _2DRoguelike.Content.Core.GameDebugger;
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
        public const int NumRooms = 5;

        public List<Room> roomlist { get; }
        public Vector2 spawnpoint;
        Room exitroom;
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
                }else if(i==1 && LevelManager.level==0){
                    room = RoomFactory.RoomWithChest();
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
                    room.setXPos(Map.Random.Next(previousRoom.XPos - Room.MAXROOMSIZE - addedvalue < 0 ? 0 : previousRoom.XPos - Room.MAXROOMSIZE - addedvalue, previousRoom.XPos + Room.MAXROOMSIZE + addedvalue > width - room.Width ? width - room.Width : previousRoom.XPos + Room.MAXROOMSIZE + addedvalue));
                    room.setYPos(Map.Random.Next(previousRoom.YPos - Room.MAXROOMSIZE - addedvalue < 0 ? 0 : previousRoom.YPos - Room.MAXROOMSIZE - addedvalue, previousRoom.YPos + Room.MAXROOMSIZE + addedvalue > height - room.Height ? height - room.Height : previousRoom.YPos + Room.MAXROOMSIZE + addedvalue));
                    roomfindingtries++;
                } while (!avoidRoomCollision(room) && roomfindingtries <= ROOMTRIES);
                if (roomfindingtries != ROOMTRIES)
                {
                    if (i == NumRooms - 1)
                    {
                        room.setExit();
                        exitroom = room;
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
                                if (chararray[x, previousRoom.CentreY] != RoomObject.EmptySpace && chararray[x, previousRoom.CentreY] != RoomObject.Exit)
                                {
                                    chararray[x, previousRoom.CentreY] = RoomObject.EmptySpace;
                                    chararray[x, previousRoom.CentreY + 1] = RoomObject.EmptySpace;
                                }
                            }
                            for (int y = startY; y < endY + 1; y++)
                            {
                                if (chararray[room.CentreX, y] != RoomObject.EmptySpace && chararray[room.CentreX, y] != RoomObject.Exit)
                                {
                                    chararray[room.CentreX, y] = RoomObject.EmptySpace;
                                    chararray[room.CentreX + 1, y] = RoomObject.EmptySpace;

                                }
                            }
                        }
                        else
                        {
                            for (int y = startY; y < endY + 1; y++)
                            {
                                if (chararray[previousRoom.CentreX, y] != RoomObject.EmptySpace && chararray[previousRoom.CentreX, y] != RoomObject.Exit)
                                {
                                    chararray[previousRoom.CentreX, y] = RoomObject.EmptySpace;
                                    chararray[previousRoom.CentreX + 1, y] = RoomObject.EmptySpace;
                                }
                            }

                            for (int x = startX; x < endX; x++)
                            {
                                if (chararray[x, room.CentreY] != RoomObject.EmptySpace && chararray[x, room.CentreY] != RoomObject.Exit)
                                {
                                    chararray[x, room.CentreY] = RoomObject.EmptySpace;
                                    chararray[x, room.CentreY + 1] = RoomObject.EmptySpace;
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
                    exitroom = previousRoom;
                }
            }

            SpawnEnemies();
            PlaceTraps();

            // DEBUG-Test:
            foreach (Room room in roomlist)
            {
                GameDebug.AddToBoxDebugBuffer(room.roomhitbox, Color.LightGray, true);
            }
        }
        public void SpawnEnemies()
        {
            //First room should not have enemies
            for (int i = 1; i < NumRooms; i++)
            {
                roomlist[i].placeEnemies();
            }
        }
        public void PlaceTraps()
        {
            for (int i = 1; i < NumRooms; i++)
            {
                roomlist[i].SetTrap();
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
            currentroom = null;
            for (int i = 0; i < NumRooms; i++)
            {
                if (roomlist[i].roomhitbox.Intersects(player.GetTileCollisionHitbox()))
                {
                    currentroom = roomlist[i];
                    break;
                }
            }
            // Check nach Überresten der Toten
            if (currentroom != null)
                for (int i = currentroom.enemylist.Count - 1; i >= 0; i--)
                {
                    if (currentroom.enemylist[i].isExpired)
                        currentroom.enemylist.RemoveAt(i);
                }
        }

        public override void clearEnemies()
        {
            foreach (Room r in roomlist)
            {
                foreach (Enemy e in r.enemylist)
                {
                    e.isExpired = true;
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
            foreach (Room r in roomlist)
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
                foreach (Enemy e in r.enemylist)
                {
                    if (!e.dead)
                    {
                        returnvalue++;
                    }
                }
            }
            return returnvalue;
        }
        /// <summary>
        /// Places a Key to a Room
        /// When roomnmb out of range. Last Room gets the Key.
        /// Returns true when roomnmb is in range
        /// </summary>
        public override bool AddKeyToRoom(int roomnmb)
        {
            if (roomnmb - 1 <= NumRooms)
            {
                roomlist[roomnmb - 1].setKey();
                return true;
            }
            else
            {
                roomlist[NumRooms - 1].setKey();
                return false;
            }
        }

        public override Room getExitRoom()
        {
            return exitroom;
        }
    }
}
