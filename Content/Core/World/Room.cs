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
        public Rectangle insideroomhitbox;
        public Vector2 doornorth;
        public Vector2 dooreast;
        public Vector2 doorsouth;
        public Vector2 doorwest;
        public Vector2 spawnpoint { get; }
        //Ramdon Generator
        static Random rndState = new Random();
        static int rnd(int maxgenetated) => rndState.Next() % maxgenetated;//Returns integer between 0 and maxgenetated

        public Room()
        {
            width = rnd(10)+5;
            height= rnd(10)+5;
            room = new char[width,height];
            fillRoom();
        }
        public Room(int width,int height)
        {
            this.width = width;
            this.height = height;
            room = new char[width, height];
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
