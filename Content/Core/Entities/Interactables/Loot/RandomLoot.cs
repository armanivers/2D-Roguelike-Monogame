using _2DRoguelike.Content.Core.Entities.Creatures.Enemies;
using _2DRoguelike.Content.Core.Entities.Loot.InventoryLoots.WeaponLoots;
using _2DRoguelike.Content.Core.Entities.Loot.Potions;
using _2DRoguelike.Content.Core.Entities.Loot.WeaponLoots;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Loot
{
    class RandomLoot
    {
        // Item IDS 

        // drop types
        private static int chestNormal = 0;
        private static int chestDiamond = 1;
        private static int lootbagZombie = 2;
        private static int lootbagSkeleton = 3;
        private static int lootbagWizard = 4;

        // dagger = 0, bow = 1, axe = 2, bomb = 3, healthpotion = 4, xppotion = 5

        // List mit keyvaluepairs um items mit gleiche wahrscheinlichkeit zu erlauben
        private static List<KeyValuePair<int, int>> chestNormalDroplist = new List<KeyValuePair<int, int>>()
        {
            new KeyValuePair<int,int>(40,0), // 40% chance to get Dagger (id 0)
            new KeyValuePair<int,int>(40,1), // 30% chance to get Bow (id 1)
            new KeyValuePair<int,int>(10,3), // 10% chance to get Bomb (id 3)
            new KeyValuePair<int,int>(10,4) // 10% chance to get Healthpotion (id 4)
        };

        private static List<KeyValuePair<int, int>> chestDiamondDroplist = new List<KeyValuePair<int, int>>()
        {
            new KeyValuePair<int,int>(50,2), // 10% chance to get Axe (id 2)
            new KeyValuePair<int,int>(50,4) // 10% chance to get Healthpotion (id 4)
        };

        private static List<KeyValuePair<int, int>> lootbagZombieDroplist = new List<KeyValuePair<int, int>>()
        {
            new KeyValuePair<int,int>(40,0), // 40% chance to get Dagger (id 0)
            new KeyValuePair<int,int>(30,1), // 30% chance to get Bow (id 1)
            new KeyValuePair<int,int>(10,5), // 10% chance to get XPPOTION (id 5)
            new KeyValuePair<int,int>(10,3), // 10% chance to get Bomb (id 3)
            new KeyValuePair<int,int>(10,4) // 10% chance to get Healthpotion (id 4)
        };

        private static List<KeyValuePair<int, int>> lootbagSkeletonDroplist = new List<KeyValuePair<int, int>>()
        {
            new KeyValuePair<int,int>(50,1), // 40% chance to get Dagger (id 0)
            new KeyValuePair<int,int>(50,0), // 30% chance to get Bow (id 1)
        };



        // Main method used to spawn the loot!
        public static void SpawnLoot(int type,Vector2 pos)
        {
            int chosenItem;

            switch(type)
            {
                // Normal Chest Loot
                case 0:
                    chosenItem = DetermineLoot(chestNormalDroplist);
                    break;
                // Diamond Chest Loot
                case 1:
                    chosenItem = DetermineLoot(chestDiamondDroplist);
                    break;
                // Zombies Loot bag 
                case 2:
                    chosenItem = DetermineLoot(lootbagZombieDroplist);
                    break;
                // skeleton loot bag
                case 3:
                    chosenItem = DetermineLoot(lootbagSkeletonDroplist);
                    break;
                default:
                    chosenItem = 0;
                    break;
            }
            //Debug.Print("Chosen item to spawn is " + chosenItem);
            PlaceLoot(chosenItem, pos);
        }

        private static int DetermineLoot(List<KeyValuePair<int, int>> dropList)
        {
            // % between 0 und 100
            int randomPercentage = Game1.rand.Next(0, 100);
            int pos = 0;

            foreach(var itemPercentage in dropList)
            {
                if (randomPercentage <= itemPercentage.Key)
                {
                    //Debug.Print("Item id = " + pos + " random% is "+ randomPercentage +" with itempercentage " +itemPercentage.Key + " itemscount = " +dropList.Count);
                    return itemPercentage.Value;
                }
                else
                {
                    pos++;
                    //Debug.Print("Next Weapon");
                    randomPercentage -= itemPercentage.Key;
                }
            }

            // default item
            return 0;
        }

        private static void PlaceLoot(int itemId, Vector2 pos)
        {
            switch (itemId)
            {
                case 0:
                    new DaggerLoot(pos);
                    break;
                case 1:
                    new BowLoot(pos);
                    break;
                case 2:
                    new AxeLoot(pos);
                    break;
                case 3:
                    new BombLoot(pos);
                    break;
                case 4:
                    new HealthPotion(pos);
                    break;
                case 5:
                    new ExperiencePotion(pos);
                    break;
                default:
                    new HealthPotion(pos);
                    break;
            }
        }

        public static int DetermineMonsterLootTable(Enemy enemy)
        {
            if (enemy == null) return chestNormal;

            if(enemy is BrownZombie || enemy is GreenZombie)
            {
                return lootbagZombie;
            }
            else if(enemy is Skeleton)
            {
                return lootbagSkeleton;
            }
            else if(enemy is Wizard)
            {
                return lootbagWizard;
            }
            // default loottable is normal chest
            else
            {
                return chestNormal;
            }
        }
    }
}
