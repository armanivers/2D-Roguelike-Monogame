using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;


namespace _2DRoguelike.Content.Core.World
{
    class Room
    {
        public char[,] room { get; }
        public int Width;
        public int Height;
        public int XPos { get; set; }
        public int YPos { get; set; }
        public int CentreX { get { return XPos + (Width / 2); } }
        public int CentreY { get { return YPos + (Height / 2); } }

        public Rectangle roomhitbox;
        public Room()
        {
            Width = Map.Random.Next(7, 32);
            Height= Map.Random.Next(7, 32);
            room = new char[Width,Height];
            fillRoom();
        }
        public Room(int width,int height)
        {
            this.Width = width;
            this.Height = height;
            room = new char[width, height];
            fillRoom();
        }
        public void fillRoom()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    bool s = x < 1 || x > Width - 2;
                    bool t = y < 1 || y > Height-2;
                    if (s && t)
                        room[x, y] = RoomObject.Corner; // avoid generation of doors at corners
                    else if (s ^ t)
                        room[x, y] = RoomObject.Wall;
                    else
                        room[x, y] = RoomObject.EmptySpace;
                }
            }
        }
    }
}
