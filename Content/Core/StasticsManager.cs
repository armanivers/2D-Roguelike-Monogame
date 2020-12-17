using _2DRoguelike.Content.Core.Statistics;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core
{
    static class StatisticsManager
    {
        public static PlayerScore currentScore;

        public static void ClearScore()
        {
            // TODO: also add current score stats to global score stats
            currentScore = null;
        }

        public static void InitializeScore()
        {
            currentScore = new PlayerScore();
        }

        public static void NewWeaponRecieved()
        {
            currentScore.NewWeaponRecieved();
            Game1.gameStats.NewItem();
        }

        public static void WeaponRecieved()
        {
            currentScore.WeaponRecieved();
            Game1.gameStats.NewItem();
        }

        public static void MonsterKilled()
        {
            currentScore.MonsterKilled();
            Game1.gameStats.MonsterKilled();
        }

        public static void LevelUp()
        {
            currentScore.LevelUp();
            Game1.gameStats.LevelReached();
        }

        public static void LootOpen()
        {
            currentScore.LootOpen();
            Game1.gameStats.LootOpened();
        }

        public static void MapLevelReached()
        {
            currentScore.MapLevelReached();
            Game1.gameStats.LevelReached();
        }



    }
}
