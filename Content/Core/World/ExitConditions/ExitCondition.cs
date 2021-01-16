using System;
using System.Collections.Generic;
using System.Text;
using _2DRoguelike.Content.Core.World.Rooms;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.World.ExitConditions
{
    abstract class ExitCondition
    {

        protected bool keyplaced;


        public static ExitCondition getRandomExitCondition()
        {
            int percentage = Game1.rand.Next(0, 101);

            if (percentage <= 100)
            {
                return new KillRandomEnemy();
            }
            else if (percentage <= 30)
            {
                return new KillAmountOfEnemies();
            }
            else if (percentage <= 5)
            {
                return new KillAllEnemies();
            }

            return new KillAmountOfEnemies();
        }


        public bool CanExit()
        {
            return keyplaced;
        }

        public virtual bool Exit()
        {

            if (CheckIfConditionMet() && !keyplaced)
            {
                keyplaced = true;
                return true;
            }
            return false;
        }

        protected abstract bool CheckIfConditionMet();

        public abstract string PrintCondition();

        public abstract Vector2 GetKeySpawnPosition(Room room);

    }
}