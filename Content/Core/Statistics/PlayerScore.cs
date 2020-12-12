using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Statistics
{
    class PlayerScore
    {
        private int score;

        public int Score { get { return score; } }
        // Score Multipliers
        
        // Item related
        private const int NEW_WEAPON_RECIEVED = 5;
        private const int WEAPON_RECIEVED = 2;

        // Monster related
        private const int MONSTER_KILLED = 7;

        // World and Level related
        private const int LEVEL_UP = 2;
        private const int LOOT_OPEN = 3;
        private const int MAP_LEVEL_REACHED = 10;

        public PlayerScore()
        {
            score = 0;
            // save current date + time?
        }

        public void NewWeaponRecieved()
        {
            score += NEW_WEAPON_RECIEVED;
        }

        public void WeaponRecieved()
        {
            score += WEAPON_RECIEVED;
        }

        public void MonsterKilled()
        {
            score += MONSTER_KILLED;
        }

        public void LevelUp()
        {
            score += LEVEL_UP;
        }

        public void LootOpen()
        {
            score += LOOT_OPEN;
        }

        public void MapLevelReached()
        {
            score += MAP_LEVEL_REACHED;
        }


    }
}
