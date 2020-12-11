using _2DRoguelike.Content.Core.Entities;
using _2DRoguelike.Content.Core.Entities.Creatures.Enemies;
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
        public int roomsize { get; set; }
        public Rectangle roomhitbox { get; set; }
        public Rectangle exithitbox { get; set; }
        public int XPos;
        public void setXPos(int value)
        {
            XPos = value;
            roomhitbox = new Rectangle(value * 32, roomhitbox.Y, (Width - 1) * 32, (Height - 1) * 32);
        }
        public int getXPos()
        {
            return XPos;
        }
        public int YPos;
        public void setYPos(int value)
        {
            YPos = value;
            roomhitbox = new Rectangle(roomhitbox.X, value * 32, (Width - 1) * 32, (Height - 1) * 32);
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

        //enemies list
        public int enemies;
        public List<Enemy> enemylist;

        //TODO entities
        public List<EntityBasis> entitylist;

        public Room()
        {
            Width = Map.Random.Next(7, 24);
            Height = Map.Random.Next(7, 24);
            roomsize = (Width - 1) * (Height - 1);
            room = new char[Width, Height];
            enemylist = new List<Enemy>();
            fillRoom();
        }
        public Room(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            roomsize = (Width - 1) * (Height - 1);
            room = new char[width, height];
            enemylist = new List<Enemy>();
            fillRoom();
        }
        public void fillRoom()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    bool s = x < 1 || x > Width - 2;
                    bool t = y < 1 || y > Height - 2;
                    if (s && t)
                        room[x, y] = RoomObject.Corner; // avoid generation of doors at corners
                    else if (s ^ t)
                        room[x, y] = RoomObject.Wall;
                    else
                        room[x, y] = RoomObject.EmptySpace;
                }
            }
        }
        public void placeEnemies()
        {
            //Kleinstmöglicher raum 6*6=36
            //Größstmöglicher Raum 24*24=576
            //Kleiner Raum:36-191= 4 Gegner
            //Mittlerer Raum:192-383= 6 Gegner
            //Großer Raum:384-576= 8 Gegner

            if (roomsize < 192)
            {
                enemies = 2;
            }
            else if (roomsize < 384)
            {
                enemies = 4;
            }
            else
            {
                enemies = 6;
            }
            for (int i = 0; i < enemies; i++)
            {
                Vector2 enemyspawnpoint;
                do
                {
                    enemyspawnpoint = new Vector2(Map.Random.Next(2, Width - 2), Map.Random.Next(2, Height - 2));
                } while (room[(int)enemyspawnpoint.X, (int)enemyspawnpoint.Y] != RoomObject.EmptySpace);
                enemyspawnpoint.X += (float)XPos;
                enemyspawnpoint.Y += (float)YPos;
                int enemytype = Map.Random.Next(0, 3);
                enemylist.Add(EnemyFactory.CreateRandomEnemy(enemyspawnpoint));
            }
        }
        public void setExit()
        {
            do
            {
                XExit = (Map.Random.Next(1, Width - 2));
                YExit = (Map.Random.Next(1, Height - 2));
            } while (room[XExit, YExit] != RoomObject.EmptySpace);
            exitroom = true;
            room[XExit, YExit] = RoomObject.Exit;
            XExit = (XExit + XPos);
            YExit = (YExit + YPos);
            exithitbox = new Rectangle(XExit * 32, YExit * 32, 32, 32);
        }
    }
}
