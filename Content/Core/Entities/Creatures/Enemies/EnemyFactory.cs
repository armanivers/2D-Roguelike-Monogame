using _2DRoguelike.Content.Core.Entities.Creatures.Enemies.Bosses;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Creatures.Enemies
{
    static class EnemyFactory
    {
        public static readonly Random Random = new Random();
        public static Enemy CreateRandomEnemy(Vector2 spawnpoint)
        {
            Enemy returnvalue=null;
            int randomvalue = Random.Next(0,101);

            if (randomvalue <= 35)
            {
                returnvalue = new BrownZombie(spawnpoint * new Vector2(32));
            }
            else if (randomvalue <= 70)
            {
                returnvalue = new GreenZombie(spawnpoint * new Vector2(32));
            }
            else if (randomvalue <=90)
            {
                returnvalue = new Skeleton(spawnpoint * new Vector2(32));
            }
            else if (randomvalue <= 100)
            {
                returnvalue = new Wizard(spawnpoint * new Vector2(32));
            }
            return returnvalue;
        }
    }
}
