using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.World
{
    class Dungeon : Map
    {
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
            for(int i = 0; i < NumRooms; i++)
            {
                Room room = new Room();
                
                do
                {
                    room.setXPos(Map.Random.Next(0, width - room.Width));
                    room.setYPos(Map.Random.Next(0, height - room.Height));
                }while(!avoidRoomCollision(room));
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
                                if (charmap[x, previousRoom.CentreY+1] != RoomObject.Wall)
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
                                if (charmap[room.CentreX+1, y] != RoomObject.Wall)
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
                            
                            if (charmap[x, room.CentreY]!=RoomObject.EmptySpace)
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
            for (int y=room.YPos;y< room.YPos + room.Height;y++)
            {
                for (int x = room.XPos; x < room.XPos + room.Width; x++)
                {
                    if(charmap[x, y] != 0)
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
            for(int i = 0; i < NumRooms; i++)
            {
                if (player.hitbox.Intersects(roomlist[i].roomhitbox))
                {
                    if (roomlist[i].exitroom)
                    {
                        if((int)player.Position.X/32==roomlist[i].XExit && (int)player.Position.Y/32 == roomlist[i].YExit)
                        {
                            LevelManager.NextLevel(player);
                        }
                    }
                }
            }
        }
    }
}
