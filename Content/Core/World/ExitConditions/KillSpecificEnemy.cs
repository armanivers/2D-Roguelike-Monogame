using System;
using System.Collections.Generic;
using System.Text;
using _2DRoguelike.Content.Core.Entities.Creatures.Enemies;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.World.ExitConditions
{
    class KillSpecificEnemy : ExitCondition
    {
        Enemy enemy;

        public KillSpecificEnemy(Enemy enemy) {
            this.enemy = enemy;
            enemy.initialColor = enemy.currentColor = Color.Gold;
            enemy.HealthPoints = enemy.maxHealthPoints = (int)(enemy.maxHealthPoints * 1.5f);
        }

        protected override bool CheckIfConditionMet()
        {
            return enemy.IsDead();
        }

        public override string PrintCondition()
        {
            return "Kill the Key Guardian!";
        }
    }
}