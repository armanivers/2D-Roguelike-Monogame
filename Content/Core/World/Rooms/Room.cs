﻿using _2DRoguelike.Content.Core.Entities;
using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.Entities.Creatures.Enemies;
using _2DRoguelike.Content.Core.Entities.Creatures.Enemies.Bosses;
using _2DRoguelike.Content.Core.Entities.Interactables.Loot.InventoryLoots.ObtainableLoots;
using _2DRoguelike.Content.Core.Entities.Interactables.WorldObjects;
using _2DRoguelike.Content.Core.Entities.Loot.InventoryLoots.WeaponLoots;
using _2DRoguelike.Content.Core.Entities.Loot.Potions;
using _2DRoguelike.Content.Core.Entities.Loot.WeaponLoots;
using _2DRoguelike.Content.Core.Entities.Special_Interactables.Negative;
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
        public static Rectangle exithitbox { get; set; }
        public int XPos;
        public static bool placedStartLoot = false;

        public void setXPos(int value)
        {
            XPos = value;
            roomhitbox = new Rectangle((value) * PIXELMULTIPLIER, roomhitbox.Y, (Width) * PIXELMULTIPLIER, (Height) * PIXELMULTIPLIER);
        }
        public int getXPos()
        {
            return XPos;
        }
        public int YPos;
        public void setYPos(int value)
        {
            YPos = value;
            roomhitbox = new Rectangle(roomhitbox.X, (value) * PIXELMULTIPLIER, (Width) * PIXELMULTIPLIER, (Height) * PIXELMULTIPLIER);
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

                entitylist.Add(new Chest(chestspawnpoint * new Vector2(PIXELMULTIPLIER), Entities.Loot.RandomLoot.DropType.chestNormal));

                Vector2 lootSpawn;
                do
                {
                    lootSpawn = new Vector2(Map.Random.Next(2, Width - 2), Map.Random.Next(2, Height - 2));
                } while (room[(int)lootSpawn.X, (int)lootSpawn.Y] != RoomObject.EmptySpace && lootSpawn!=chestspawnpoint);

                lootSpawn.X += XPos;
                lootSpawn.Y += YPos;

                if (LevelManager.level == 0 && !placedStartLoot)
                {
                    entitylist.Add(new DaggerLoot(lootSpawn * new Vector2(PIXELMULTIPLIER)));
                    placedStartLoot = true;
                }
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
                } while (room[(int)enemyspawnpoint.X, (int)enemyspawnpoint.Y] != RoomObject.EmptySpace || IntersectsTileCollisionHitbox((int)enemyspawnpoint.X, (int)enemyspawnpoint.Y));

                enemyspawnpoint.X += (float)XPos;
                enemyspawnpoint.Y += (float)YPos;
                enemylist.Add(EnemyFactory.CreateRandomEnemy(enemyspawnpoint));
            }
        }
        public Boss placeBoss()
        {
            int XSPAWN = Width - 8;
            int YSPAWN = Height / 2;

            switch (LevelManager.bossStage)
            {
                case 0:
                    return EnemyFactory.CreateOrcBoss(new Vector2(XSPAWN, YSPAWN));
                case 1:
                    return EnemyFactory.CreateDragonBoss(new Vector2(XSPAWN, YSPAWN));
                case 2:
                    return EnemyFactory.CreateDarkOverlordBoss(new Vector2(XSPAWN, YSPAWN));
                default:
                    return EnemyFactory.CreateOrcBoss(new Vector2(XSPAWN, YSPAWN));
            }

        }

        private bool IntersectsTileCollisionHitbox(int enemyPosX, int enemyPosY)
        {
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
                XExit = (Map.Random.Next(2, Width - 2));
                YExit = (Map.Random.Next(2, Height - 2));
            } while (room[XExit, YExit] != RoomObject.EmptySpace);
            exitroom = true;
            room[XExit, YExit] = RoomObject.Exit;

            // Debug.WriteLine("Exit is at: {0},{1}" + XExit, YExit);

            XExit = (XExit + XPos);
            YExit = (YExit + YPos);
            exithitbox = new Rectangle(XExit * PIXELMULTIPLIER, YExit * PIXELMULTIPLIER, PIXELMULTIPLIER, PIXELMULTIPLIER);
            entitylist.Add(new Ladder(new Vector2(XExit * PIXELMULTIPLIER, YExit * PIXELMULTIPLIER)));


            if (!(LevelManager.level % 3 == 2))
            {
                Vector2 chestspawnpoint;
                do
                {
                    chestspawnpoint = new Vector2(Map.Random.Next(2, Width - 2), Map.Random.Next(2, Height - 2));
                } while (room[(int)chestspawnpoint.X, (int)chestspawnpoint.Y] != RoomObject.EmptySpace
                || new Rectangle((int)chestspawnpoint.X, (int)chestspawnpoint.Y, 32, 32).Intersects(exithitbox)
                || WithinSecondChest(new Rectangle((int)chestspawnpoint.X, (int)chestspawnpoint.Y, 32, 32)));

                chestspawnpoint.X += XPos;
                chestspawnpoint.Y += YPos;
                entitylist.Add(new Chest(chestspawnpoint * new Vector2(PIXELMULTIPLIER), Entities.Loot.RandomLoot.DropType.chestDiamond));
            }

        }

        private bool WithinSecondChest(Rectangle firstChestHitbox)
        {
            foreach (var secondChest in entitylist)
            {
                if (firstChestHitbox.Intersects(secondChest.Hitbox))
                    return true;
            }
            return false;
        }

        public void SetTrap()
        {
            int spikesAmount = 0;

            if (roomvolume < 100)
            {
                spikesAmount = 2;
            }
            else if (roomvolume < 384)
            {
                spikesAmount = 6;
            }
            else
            {
                spikesAmount = 10;
            }

            for (int i = 0; i < spikesAmount; i++)
            {
                Vector2 trapPos;
                do
                {
                    trapPos = new Vector2(Map.Random.Next(2, Width - 3), Map.Random.Next(2, Height - 3));
                } while (room[(int)trapPos.X, (int)trapPos.Y] != RoomObject.EmptySpace);

                trapPos.X += (float)XPos;
                trapPos.Y += (float)YPos;

                new Spikes(trapPos * PIXELMULTIPLIER);
            }
        }
        /// <summary>
        /// Places Key to a Random position in the Room
        /// </summary>
        public void setKey()
        {
            KeyLoot key = new KeyLoot(LevelManager.levelList[LevelManager.level].exitCondition.GetKeySpawnPosition(this));
            entitylist.Add(key);

            // auskommentieren, wenn Endlosschleife!!!
            //while (key.InWall())
            //{
            //    int xpos;
            //    int ypos;
            //    xpos = (Map.Random.Next(1, Width - 1)) + XPos;
            //    ypos = (Map.Random.Next(1, Height - 1)) + YPos;
            //    key.Position = new Vector2(xpos * Room.PIXELMULTIPLIER, ypos * Room.PIXELMULTIPLIER);
            //}

        }



        public static Vector2 getRandomCoordinateInCurrentRoom(Creature creature)
        {
            Vector2 ret;
            do
            {
                ret = new Vector2(Map.Random.Next(1, LevelManager.currentmap.currentroom.Width), Map.Random.Next(1, LevelManager.currentmap.currentroom.Height));
            } while (WouldBeStuck(creature, ret));
            ret.X += LevelManager.currentmap.currentroom.XPos;
            ret.Y += LevelManager.currentmap.currentroom.YPos;

            return ret * new Vector2(32, 32) - new Vector2(17 * creature.ScaleFactor + 5, 14 * creature.ScaleFactor + 25);
        }



        private static bool WouldBeStuck(Creature creature, Vector2 ret)
        {
            Rectangle newTileCollisionHitbox = new Rectangle((int)(ret.X * 32), (int)(ret.Y * 32), creature.GetTileCollisionHitbox().Width, creature.GetTileCollisionHitbox().Height);

            if (creature is Enemy && newTileCollisionHitbox.Intersects(Player.Instance.GetTileCollisionHitbox()))
                return true;


            foreach (var enemy in LevelManager.currentmap.currentroom.enemylist)
            {
                if (!(creature == enemy) && newTileCollisionHitbox.Intersects(enemy.GetTileCollisionHitbox()))
                {
                    return true;

                }
            }

            int tilesOnCreatureWidth = creature.GetTileCollisionHitbox().Width / 32 + 1;
            int tilesOnCreatureHeight = creature.GetTileCollisionHitbox().Height / 32 + 1;




            for (int x = (int)ret.X; x < (int)ret.X + tilesOnCreatureWidth /*&& (int)ret.X + tilesOnCreatureWidth < LevelManager.currentmap.currentroom.room.GetLength(0)*/; x++)
            {
                for (int y = (int)ret.Y; y < (int)ret.Y + tilesOnCreatureHeight/*&& (int)ret.Y + tilesOnCreatureHeight < LevelManager.currentmap.currentroom.room.GetLength(1)*/; y++)
                {
                    if (LevelManager.currentmap.currentroom.room[x, y] != RoomObject.EmptySpace)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }



}
