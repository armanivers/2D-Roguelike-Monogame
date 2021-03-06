﻿using System;
using System.Collections.Generic;
using System.Text;
using _2DRoguelike.Content.Core.Entities;
using _2DRoguelike.Content.Core.Entities.Creatures.Enemies;
using _2DRoguelike.Content.Core.World.Rooms;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.World.ExitConditions
{
    class KillAmountOfEnemies : ExitCondition
    {
        readonly int killTarget;
        int kills;
        const float KILL_PECENTAGE = 0.4f;
        List<Enemy> allEnemies = new List<Enemy>();
        Vector2 positionOfLastDeadEnemy;

        public KillAmountOfEnemies() : this(KILL_PECENTAGE) { }

        public KillAmountOfEnemies(float percentage)
        {
            foreach (var creature in EntityManager.creatures)
            {
                if (creature is Enemy)
                    allEnemies.Add((Enemy)creature);
            }
            this.killTarget = (int)(allEnemies.Count * percentage);
        }

        protected override bool CheckIfConditionMet()
        {
            if (kills != killTarget)
                for (int i = allEnemies.Count - 1; i >= 0; i--)
                {
                    if (allEnemies[i].IsDead())
                    {
                        positionOfLastDeadEnemy = allEnemies[i].Position;
                        allEnemies.RemoveAt(i);
                        kills++;
                        if (kills == killTarget) 
                            break;
                    }
                }
            return kills == killTarget;
        }

        public override string PrintCondition()
        {
            return "Kill " + killTarget + " Enemies!";
        }

        public string PrintRemainingEnemies()
        {
            return "Enemies Remaining:" + (killTarget - kills);
        }

        public override Vector2 GetKeySpawnPosition(Room room)
        {
            return positionOfLastDeadEnemy;
        }
    }
}
