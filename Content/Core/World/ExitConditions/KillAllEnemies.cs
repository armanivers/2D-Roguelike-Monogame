using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.World.ExitConditions
{
    class KillAllEnemies : ExitCondition
    {
        private bool keyplaced;

        public KillAllEnemies()
        {
            keyplaced = false;
        }
        public bool Exit()
        {
            if (LevelManager.currentmap.EnemiesAlive() == 0 && !keyplaced)
            {
                LevelManager.currentmap.AddKeyToRoom(10);
                keyplaced = true;
                return true;
            }
            else return false;
        }
    }
}
