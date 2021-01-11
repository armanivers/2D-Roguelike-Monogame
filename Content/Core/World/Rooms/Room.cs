using _2DRoguelike.Content.Core.Entities;
using _2DRoguelike.Content.Core.Entities.Creatures.Enemies;
using _2DRoguelike.Content.Core.Entities.Creatures.Enemies.Bosses;
using _2DRoguelike.Content.Core.Entities.Interactables.Loot.InventoryLoots.ObtainableLoots;
using _2DRoguelike.Content.Core.Entities.Interactables.WorldObjects;
using _2DRoguelike.Content.Core.Entities.Loot.InventoryLoots.WeaponLoots;
using _2DRoguelike.Content.Core.Entities.Loot.Potions;
using _2DRoguelike.Content.Core.Entities.Loot.WeaponLoots;
using _2DRoguelike.Content.Core.World.Maps;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;


namespace _2DRoguelike.Content.Core.World.Rooms
{
    class Room
    {
        //Constanten
        public const int MAXROOMSIZE = 24;
        public const int MINROOMSIZE = 7;
        public const int PIXELMULTIPLIER = 32;

        public char[,] room { get; }
        public int Width;
        public int Height;
        public int roomvolume { get; set; }
        public Rectangle roomhitbox { get; set; }
        public Rectangle exithitbox { get; set; }
        public int XPos;
        public void setXPos(int value)
        {
            XPos = value;
            roomhitbox = new Rectangle((value) * PIXELMULTIPLIER, roomhitbox.Y , (Width ) * PIXELMULTIPLIER, (Height ) * PIXELMULTIPLIER);
        }
        public int getXPos()
        {
            return XPos;
        }
        public int YPos;
        public void setYPos(int value)
        {
            YPos = value;
            roomhitbox = new Rectangle(roomhitbox.X, (value )* PIXELMULTIPLIER , (Width ) * PIXELMULTIPLIER, (Height ) * PIXELMULTIPLIER);
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
        // IDEE: bei update: in currentRoom die enemylist durchlaufen, und gucken, ob isExpired = true (um "Leichen" zu entfernen)

        //TODO entities
        public List<EntityBasis> entitylist;

        public Room() : this(Map.Random.Next(MINROOMSIZE, MAXROOMSIZE), Map.Random.Next(MINROOMSIZE, MAXROOMSIZE))
        {
            //XPos = 0;
            //YPos = 0;
            //Width = Map.Random.Next(MINROOMSIZE, MAXROOMSIZE);
            //Height = Map.Random.Next(MINROOMSIZE, MAXROOMSIZE);
            //roomvolume = (Width - 1) * (Height - 1);
            //room = new char[Width, Height];
            //enemylist = new List<Enemy>();
            //entitylist = new List<EntityBasis>();
            //fillRoom();
        }

        public Room(int width, int height)
        {
            XPos = 0;
            YPos = 0;
            this.Width = width;
            this.Height = height;
            roomvolume = (Width - 1) * (Height - 1);
            room = new char[width, height];
            enemylist = new List<Enemy>();
            entitylist = new List<EntityBasis>();
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

            if (roomvolume < 100)
            {
                enemies = 0;
                Vector2 chestspawnpoint;
                do
                {
                    chestspawnpoint = new Vector2(Map.Random.Next(2, Width - 2), Map.Random.Next(2, Height - 2));
                } while (room[(int)chestspawnpoint.X, (int)chestspawnpoint.Y] != RoomObject.EmptySpace);
                chestspawnpoint.X += XPos;
                chestspawnpoint.Y += YPos;
                entitylist.Add(new Chest(chestspawnpoint*new Vector2(PIXELMULTIPLIER)));
            }
            else if (roomvolume < 384)
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
                    enemyspawnpoint = new Vector2(Map.Random.Next(2, Width - 3), Map.Random.Next(2, Height - 3));
                } while (room[(int)enemyspawnpoint.X, (int)enemyspawnpoint.Y] != RoomObject.EmptySpace && IntersectsTileCollisionHitbox((int)enemyspawnpoint.X, (int)enemyspawnpoint.Y));
                // TODO: Schauen, ob beim neuen Spawn der Enemy die TileCollisonHitbox eines anderen Enemy schneidet → neuer Spawn ermitteln
                
                enemyspawnpoint.X += (float)XPos;
                enemyspawnpoint.Y += (float)YPos;
                enemylist.Add(EnemyFactory.CreateRandomEnemy(enemyspawnpoint));
            }
        }
        public Boss placeBoss()
        {
            int XSPAWN = Width-8;
            int YSPAWN = Height / 2;

            switch(LevelManager.bossStage)
            {
                case 0:
                    return EnemyFactory.CreateDragonBoss(new Vector2(XSPAWN, YSPAWN));
                case 1:
                    // return here boss for second boss map
                    return EnemyFactory.CreateDragonBoss(new Vector2(XSPAWN, YSPAWN));
                default:
                    return EnemyFactory.CreateDragonBoss(new Vector2(XSPAWN, YSPAWN));
            }

        }

        private bool IntersectsTileCollisionHitbox(int enemyPosX, int enemyPosY) {
            Rectangle tileCollisionHitbox = new Rectangle(enemyPosX, enemyPosY, 19, 19);
            foreach (Enemy enemy in enemylist)
            {
                if (enemy.GetTileCollisionHitbox().Intersects(tileCollisionHitbox))
                    return true;
            }
            return false;
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
            exithitbox = new Rectangle(XExit * PIXELMULTIPLIER, YExit * PIXELMULTIPLIER, PIXELMULTIPLIER, PIXELMULTIPLIER);
            entitylist.Add(new Ladder(new Vector2(XExit*PIXELMULTIPLIER, YExit*PIXELMULTIPLIER)));
        }
        /// <summary>
        /// Places Key to a Random position in the Room
        /// </summary>
        public void setKey()
        {
            int xpos;
            int ypos;
            xpos = (Map.Random.Next(1, Width - 2))+XPos;
            ypos = (Map.Random.Next(1, Height - 2))+YPos;
            entitylist.Add(new KeyLoot(new Vector2(xpos* PIXELMULTIPLIER, ypos * PIXELMULTIPLIER)));
            Debug.Print("Key placed!");
        }
    }
}
