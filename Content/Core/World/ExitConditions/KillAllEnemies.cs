using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.World.ExitConditions
{
    class KillAllEnemies : ExitCondition
    {
        public bool Exit()
        {
            if (LevelManager.currentmap.EnemiesAlive() == 0) return true;
            else return false;
        }
    }
}
