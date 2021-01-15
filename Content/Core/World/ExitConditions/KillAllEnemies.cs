using System;
using System.Collections.Generic;
using System.Text;

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

        public override void PlaceKeyOnMap()
        {
            LevelManager.currentmap.AddKeyToRoom(10);
        }
    }
}
