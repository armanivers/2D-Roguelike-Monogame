﻿using System;
using System.Collections.Generic;
using System.Text;
using _2DRoguelike.Content.Core.World.Maps;
using _2DRoguelike.Content.Core.World.Rooms;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.World.ExitConditions
{
    class KillAllEnemies : ExitCondition
    {


        public KillAllEnemies()
        {
            keyplaced = false;
        }
        protected override bool CheckIfConditionMet()
        {
            return LevelManager.currentmap.EnemiesAlive() == 0;
        }

        public override string PrintCondition()
        {
            return "Kill everyone!";
        }

        public override Vector2 GetKeySpawnPosition(Room room)
        {
            int xpos;
            int ypos;
            // im letzten Raum ist KeyLoot doppelt so groß
            xpos = (Map.Random.Next(1, room.Width - (LevelManager.level == LevelManager.maxLevel - 1 ? 2 : 1))) + room.XPos;
            ypos = (Map.Random.Next(1, room.Height - (LevelManager.level == LevelManager.maxLevel - 1 ? 2 : 1))) + room.YPos;
            return new Vector2(xpos * Room.PIXELMULTIPLIER, ypos * Room.PIXELMULTIPLIER);
        }
    }
}
