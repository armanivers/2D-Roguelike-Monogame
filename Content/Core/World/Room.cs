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
        public Rectangle roomhitbox { get; set; }
        public int XPos;
        public void setXPos(int value)
        {
            XPos = value;
            roomhitbox = new Rectangle(value*32, roomhitbox.Y, (Width-1)*32, (Height-1)*32);
        }
        public int getXPos()
        {
            return XPos;
        }
        public int YPos;
        public void setYPos(int value)
        {
            YPos = value;
            roomhitbox = new Rectangle(roomhitbox.X,value*32, (Width-1)*32, (Height-1)*32);
        }
        public int getYPos()
        {
            return YPos;
        }
        public int CentreX { get { return XPos + (Width / 2); } }
        public int CentreY { get { return YPos + (Height / 2); } }
        public bool exitroom { get; set; }
        public int XExit { get; set; }
        public int YExit { get; set; }

        //TODO enemies list
        //TODO spawnpoints
        //TODO entities

        
        public Room()
        {
            Width = Map.Random.Next(7, 24);
            Height= Map.Random.Next(7, 24);
            room = new char[Width,Height];
            fillRoom();
        }
        public Room(int width,int height)
        {
            this.Width = width;
            this.Height = height;
            roomhitbox = new Rectangle(0, 0, Width, Height);
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
        public void setExit()
        {
            XExit = (Map.Random.Next(1,Width-2));
            YExit = (Map.Random.Next(1, Height - 2));
            exitroom = true;
            room[XExit, YExit] = RoomObject.Exit;
            XExit = (XExit + XPos);
            YExit = (YExit + YPos);
        }
    }
}
