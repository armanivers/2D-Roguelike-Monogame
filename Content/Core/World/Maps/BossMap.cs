using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.Entities.Creatures.Enemies;
using _2DRoguelike.Content.Core.Entities.Creatures.Enemies.Bosses;
using _2DRoguelike.Content.Core.GameDebugger;
using _2DRoguelike.Content.Core.UI;
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
        public Boss bossEntity;
        public bool stageCleared;
        public  BossMap(int width, int height) : base(width, height)
        {
            bossroom = new Room(width,height);
            bossroom.setExit();
            bossEntity = bossroom.placeBoss();
            
            currentroom = bossroom;
            currentroom.enemylist.Add(bossEntity);
            // redundant, aber setzt zumindest die roomhitbox
            currentroom.setXPos(currentroom.XPos);
            currentroom.setYPos(currentroom.YPos);     

            tilearray = fillTile(bossroom.room);
            stageCleared = false;
        }
        public override Vector2 getSpawnpoint()
        {
            return new Vector2((bossroom.Width / 2) , (bossroom.Height / 2) );
        }

        public override void Update(Player player)
        {
            GameDebug.AddToBoxDebugBuffer(LevelManager.currentmap.currentroom.roomhitbox, Color.LightGray);
            // Debug.WriteLine("Enemies= " + bossroom.enemylist.Count);
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

        public void StageCleared()
        {
            if(bossEntity.IsDead() && !stageCleared)
            {
                UIManager.SwitchBossBarState();
                stageCleared = true;
            }
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
