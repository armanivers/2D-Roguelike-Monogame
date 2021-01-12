using System;
using System.Collections.Generic;
using System.Text;
using _2DRoguelike.Content.Core.Entities;
using _2DRoguelike.Content.Core.Entities.Creatures.Enemies;

namespace _2DRoguelike.Content.Core.World.ExitConditions
{
    class KillRandomEnemy : KillSpecificEnemy
    {
        public KillRandomEnemy() : base(getRandomEnemy()) { 
        
        }

        static Enemy getRandomEnemy() {
            EntityBasis chosenEnemy;
            do
            {
                chosenEnemy = EntityManager.creatures[Game1.rand.Next(0, EntityManager.creatures.Count)];
            } while (!(chosenEnemy is Enemy));
            return (Enemy)chosenEnemy;
        }
    }
}
