using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.Entities.Creatures.Enemies;
using _2DRoguelike.Content.Core.World.Rooms;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2DRoguelike.Content.Core.World.Maps
{
    class BossMap :Map
    {
        public Room bossroom { get; set; }

        public  BossMap(int width, int height) : base(width, height)
        {
            bossroom = new Room(width,height);
            bossroom.setExit();
            bossroom.placeBoss();
            currentroom = bossroom;
            tilearray = fillTile(bossroom.room);
        }
        public override Vector2 getSpawnpoint()
        {
            return new Vector2((bossroom.Width / 2) , (bossroom.Height / 2) );
        }

        public override void Update(Player player)
        {
            string s = "Enemies= "+bossroom.enemylist.Count;
            Debug.Print(s);
        }

        public override void clearEnemies()
        {
            bossroom.enemylist.Clear();
        }

        public override void clearEnities()
        {
            bossroom.entitylist.Clear();
        }

        public override int CountEnemies()
        {
            return bossroom.enemylist.Count;
        }

        public override int EnemiesAlive()
        {
            int returnvalue=0;
            foreach(Enemy e in bossroom.enemylist)
            {
                if (!e.dead)
                {
                    returnvalue++;
                }
            }
            return returnvalue;
        }

        public override bool AddKeyToRoom(int roomnmb)
        {
            if (roomnmb > 1)
            {
                bossroom.setKey();
                return false;
            }
            else
            {
                bossroom.setKey();
                return true;
            }
        }

        public override Room getExitRoom()
        {
            return bossroom;
        }
    }
}
