using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;


namespace _2DRoguelike.Content.Core.World
{
    class Room
    {
        public char[,] room { get; }
        public int width;
        public int height;
        public Rectangle roomhitbox;
        int doors;
        public Vector2 spawnpoint { get; }
        //Ramdon Generator
        static Random rndState = new Random();
        static int rnd(int maxgenetated) => rndState.Next() % maxgenetated;//Returns integer between 0 and maxgenetated

        public Room()
        {
            doors= rndState.Next(2, 4);
            width = rnd(32)+5;
            height= rnd(32)+5;
            roomhitbox = new Rectangle(31, 31, (height-2)*32,(width-2)*32);
            room = new char[height, width];
            spawnpoint = new Vector2((float)rnd(width - 2)+1,(float)rnd(height-2)+1);
            fillRoom();
        }
        public Room(int width,int height)
        {
            doors = rnd(4);
            this.width = width;
            this.height = height;
            room = new char[height, width];
            spawnpoint = new Vector2((float)rnd(width - 2)+1,(float)rnd(height - 2)+1);
            fillRoom();
        }
        public void fillRoom()
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    bool s = x < 1 || x > width - 2;
                    bool t = y < 1 || y > height-2;
                    if (s && t)
                        room[y, x] = RoomObject.Corner; // avoid generation of doors at corners
                    else if (s ^ t)
                        room[y, x] = RoomObject.Wall;
                    else
                        room[y, x] = RoomObject.EmptySpace;
                    if(x==spawnpoint.X && y == spawnpoint.Y && room[y,x]==RoomObject.EmptySpace)
                        room[y, x] = RoomObject.Spawnpoint;
                }
            }
            setDoors();   
        }
        public void setDoors()
        {
            room[0, width/2] = RoomObject.Door;
            room[height-1,width/2] = RoomObject.Door;
        }
    }
}
