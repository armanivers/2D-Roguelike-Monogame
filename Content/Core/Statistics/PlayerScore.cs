using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Statistics
{
    public class PlayerScore
    {
        private int score;
        private double scoreBuffer;
        private double incrementSpeed;
        private double incrementTimer;

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
            scoreBuffer = 0;
            incrementTimer = 0;
            incrementSpeed = 0.2;
            // save current date + time?
        }

        public int UpdateBuffer()
        {
            if(scoreBuffer > 0)
            {
                scoreBuffer -= incrementSpeed;
                incrementTimer++;
                if(incrementTimer > 5)
                {
                    incrementTimer = 0;
                    score++;
                }
            }

            return Score;
        }

        public void ForceCounterUpdate()
        {
            score +=(int) scoreBuffer;
        }

        public void NewWeaponRecieved()
        {
            scoreBuffer += NEW_WEAPON_RECIEVED;
        }

        public void WeaponRecieved()
        {
            scoreBuffer += WEAPON_RECIEVED;
        }

        public void MonsterKilled()
        {
            scoreBuffer += MONSTER_KILLED;
        }

        public void LevelUp()
        {
            scoreBuffer += LEVEL_UP;
        }

        public void LootOpen()
        {
            scoreBuffer += LOOT_OPEN;
        }

        public void MapLevelReached()
        {
            scoreBuffer += MAP_LEVEL_REACHED;
        }


    }
}
