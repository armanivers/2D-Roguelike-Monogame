using _2DRoguelike.Content.Core.Entities.Creatures.Enemies.Bosses;
using _2DRoguelike.Content.Core.World;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Creatures.Enemies
{
    static class EnemyFactory
    {
        public static int PixelMultiplier = 32;
        public enum EnemyType
        {
            ZombieBrown,
            ZombieGreen,
            Skeleton,
            Wizard
        }

        // Each level (int -> 0/1/2/3) has a list with key values pairs (int,enemytype) which represent an enemy and his chance to spawn
        // Spawn chances are PER ROOM per level!!
        private static Dictionary<int, List<KeyValuePair<int, EnemyType>>> levelEnemies = new Dictionary<int, List<KeyValuePair<int, EnemyType>>>()
        {
            {0,
                new List<KeyValuePair<int, EnemyType>>()
                {
                    new KeyValuePair<int,EnemyType>(40,EnemyType.ZombieBrown), // 50% chance for a brown zombie
                    new KeyValuePair<int,EnemyType>(40,EnemyType.ZombieGreen), // 50% chance for a green zombie
                    new KeyValuePair<int,EnemyType>(15,EnemyType.Skeleton), // 30% chance for a skeleton
                    new KeyValuePair<int,EnemyType>(5,EnemyType.Wizard), // 10% chance for a wizard
                }
            },

            {1,
                new List<KeyValuePair<int, EnemyType>>()
                {
                    new KeyValuePair<int,EnemyType>(40,EnemyType.ZombieBrown), // 50% chance for a brown zombie
                    new KeyValuePair<int,EnemyType>(30,EnemyType.ZombieGreen), // 50% chance for a green zombie
                    new KeyValuePair<int,EnemyType>(25,EnemyType.Skeleton), // 30% chance for a skeleton
                    new KeyValuePair<int,EnemyType>(5,EnemyType.Wizard), // 10% chance for a wizard
                }
            },
            //boss level, no enemies
            {2,
                new List<KeyValuePair<int, EnemyType>>()
                {
                    new KeyValuePair<int,EnemyType>(100,EnemyType.ZombieBrown), // 50% chance for a brown zombie
                }
            },
            {3,
                new List<KeyValuePair<int, EnemyType>>()
                {
                    new KeyValuePair<int,EnemyType>(40,EnemyType.Skeleton), // 30% chance for a skeleton
                    new KeyValuePair<int,EnemyType>(20,EnemyType.ZombieBrown), // 50% chance for a brown zombie
                    new KeyValuePair<int,EnemyType>(30,EnemyType.ZombieGreen), // 50% chance for a green zombie
                    new KeyValuePair<int,EnemyType>(10,EnemyType.Wizard), // 10% chance for a wizard
                }
            },
             {4,
                new List<KeyValuePair<int, EnemyType>>()
                {
                    new KeyValuePair<int,EnemyType>(50,EnemyType.Skeleton), // 30% chance for a skeleton
                    new KeyValuePair<int,EnemyType>(20,EnemyType.ZombieBrown), // 50% chance for a brown zombie
                    new KeyValuePair<int,EnemyType>(20,EnemyType.ZombieGreen), // 50% chance for a green zombie
                    new KeyValuePair<int,EnemyType>(10,EnemyType.Wizard), // 10% chance for a wizard
                }
            },

             {5,
                new List<KeyValuePair<int, EnemyType>>()
                {
                    new KeyValuePair<int,EnemyType>(100,EnemyType.ZombieBrown), // 50% chance for a brown zombie
                }
            },

            {6,
                new List<KeyValuePair<int, EnemyType>>()
                {
                    new KeyValuePair<int,EnemyType>(30,EnemyType.Skeleton), // 30% chance for a skeleton
                    new KeyValuePair<int,EnemyType>(30,EnemyType.ZombieBrown), // 50% chance for a brown zombie
                    new KeyValuePair<int,EnemyType>(30,EnemyType.ZombieGreen), // 50% chance for a green zombie
                    new KeyValuePair<int,EnemyType>(10,EnemyType.Wizard), // 10% chance for a wizard
                }
            },
             {7,
                new List<KeyValuePair<int, EnemyType>>()
                {
                    new KeyValuePair<int,EnemyType>(20,EnemyType.ZombieBrown), // 50% chance for a brown zombie
                    new KeyValuePair<int,EnemyType>(20,EnemyType.ZombieGreen), // 50% chance for a green zombie
                    new KeyValuePair<int,EnemyType>(50,EnemyType.Skeleton), // 30% chance for a skeleton
                    new KeyValuePair<int,EnemyType>(10,EnemyType.Wizard), // 10% chance for a wizard
                }
            },

             {8,
                new List<KeyValuePair<int, EnemyType>>()
                {
                    new KeyValuePair<int,EnemyType>(100,EnemyType.ZombieBrown), // 50% chance for a brown zombie
                }
            }

        };

        /*
        private static List<KeyValuePair<int, EnemyType>> level0 = new List<KeyValuePair<int, EnemyType>>()
        {
            new KeyValuePair<int,EnemyType>(50,EnemyType.Zombie), // 50% chance for a zombie
            new KeyValuePair<int,EnemyType>(30,EnemyType.Skeleton), // 30% chance for a skeleton
            new KeyValuePair<int,EnemyType>(10,EnemyType.Wizard), // 10% chance for a wizard

        };
        */

        public static Enemy CreateRandomEnemy(Vector2 spawnpoint)
        {
            return DetermineEnemy(ChooseEnemy(), spawnpoint* PixelMultiplier);
        }

        public static Dragon CreateDragonBoss(Vector2 spawnpoint)
        {
            return new Dragon(spawnpoint * new Vector2(32));
        }

        public static Orc CreateOrcBoss(Vector2 spawnpoint)
        {
            return new Orc(spawnpoint * new Vector2(32));
        }

        public static DarkOverlord CreateDarkOverlordBoss(Vector2 spawnpoint) {
            return new DarkOverlord(spawnpoint * new Vector2(32));
        }

        public static EnemyType ChooseEnemy()
        {
            int randomPercentage = Game1.rand.Next(0, 101);
            List<KeyValuePair<int, EnemyType>> enemyList;

            if (LevelManager.level > levelEnemies.Count-1)
                enemyList = levelEnemies[0];
            else
                enemyList = levelEnemies[LevelManager.level];

            // Paramter 0 sollte mit LevelManager.level ersetzt werden! (jetzt 0 damit ich nicht fur jedes Level enemy list erstelle)
            foreach (var enemyPercentage in enemyList)
            {
                if (randomPercentage <= enemyPercentage.Key)
                {
                    return enemyPercentage.Value;
                }
                else
                {
                    randomPercentage -= enemyPercentage.Key;
                }
            }
            
            // default enemy
            return EnemyType.ZombieGreen;
        }

        public static Enemy DetermineEnemy(EnemyType enemy,Vector2 spawnpoint)
        {
            switch(enemy)
            {
                case EnemyType.ZombieBrown:
                    return new BrownZombie(spawnpoint);
                case EnemyType.ZombieGreen:
                    return new GreenZombie(spawnpoint);
                case EnemyType.Skeleton:
                    return new Skeleton(spawnpoint);
                case EnemyType.Wizard:
                    return new Wizard(spawnpoint);
                default:
                    return new BrownZombie(spawnpoint);
            }
        }
    }
}
