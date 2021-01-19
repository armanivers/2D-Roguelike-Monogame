using _2DRoguelike.Content.Core.Entities.Creatures.Enemies;
using _2DRoguelike.Content.Core.Entities.Creatures.Enemies.Bosses;
using _2DRoguelike.Content.Core.Entities.Interactables.Loot.InventoryLoots.WeaponLoots;
using _2DRoguelike.Content.Core.Entities.Loot.InventoryLoots.WeaponLoots;
using _2DRoguelike.Content.Core.Entities.Loot.Potions;
using _2DRoguelike.Content.Core.Entities.Loot.WeaponLoots;
using _2DRoguelike.Content.Core.World;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Loot
{
    public class RandomLoot
    {
        public enum DropType
        {
            chestNormal,
            chestDiamond,
            lootbagGreenZombie,
            lootbagBrownZombie,
            lootbagSkeleton,
            lootbagWizard,
            lootbagOrc,
            lootbagDragon
        }

        private enum Items
        {
            DAGGER,
            BOW,
            AXE,
            BOMB,
            HEALTH_POTION,
            XP_POTION,
            REGENERATION_POTION,
            SPEAR,
            SPEED_POTION,
            STRENGTH_POTION
        }


        /*
         Drop table has the following Structure: 
         Dictionary containg a droptype(loot source, example: chest/zombie etc) and it's corresponding List with items (its loot table) and then a
         List with keyvaluepairs (so certain items can have same drop percentage) consisting of an int (drop percentage) and an item (dropped item)
        */
        private static Dictionary<DropType, List<KeyValuePair<int, Items>>> dropTables = new Dictionary<DropType, List<KeyValuePair<int, Items>>>()
        {

            {
                DropType.chestNormal, new List<KeyValuePair<int, Items>>()
                {
                    new KeyValuePair<int,Items>(25,Items.DAGGER), // 40% chance to get Spear
                    new KeyValuePair<int,Items>(25,Items.REGENERATION_POTION), // 15% chance to get Healthpotion
                    new KeyValuePair<int,Items>(25,Items.BOW), // 30% chance to get Bow
                    new KeyValuePair<int,Items>(25,Items.BOMB) // 15% chance to get Bomb 
                }
            },
            {
                DropType.chestDiamond,new List<KeyValuePair<int, Items>>()      
                {
                    new KeyValuePair<int,Items>(50,Items.AXE), // 50% chance to get Axe
                    new KeyValuePair<int,Items>(30,Items.SPEAR), // 30% chance to get Bow
                    new KeyValuePair<int,Items>(10,Items.BOW), // 10% chance to get Bomb
                    new KeyValuePair<int,Items>(10,Items.REGENERATION_POTION) // 10% chance to get Healthpotion
                }
            },
            {
                DropType.lootbagGreenZombie,new List<KeyValuePair<int, Items>>()
                {
                    new KeyValuePair<int,Items>(30,Items.DAGGER),
                    new KeyValuePair<int,Items>(30,Items.HEALTH_POTION),
                    new KeyValuePair<int,Items>(30,Items.XP_POTION),
                    new KeyValuePair<int,Items>(10,Items.BOMB)
                }
            },
            {
                DropType.lootbagBrownZombie,new List<KeyValuePair<int, Items>>()
                {
                    new KeyValuePair<int,Items>(30,Items.HEALTH_POTION),
                    new KeyValuePair<int,Items>(30,Items.STRENGTH_POTION),
                    new KeyValuePair<int,Items>(30,Items.SPEAR),
                    new KeyValuePair<int,Items>(10,Items.BOMB)
                }
            },
            {
                DropType.lootbagSkeleton,new List<KeyValuePair<int, Items>>()
                {
                    new KeyValuePair<int,Items>(50,Items.SPEED_POTION),
                    new KeyValuePair<int,Items>(30,Items.REGENERATION_POTION),
                    new KeyValuePair<int,Items>(20,Items.AXE)
                }
            },
            {
                DropType.lootbagWizard,new List<KeyValuePair<int, Items>>()
                {
                    new KeyValuePair<int,Items>(30,Items.REGENERATION_POTION),
                    new KeyValuePair<int,Items>(30,Items.SPEAR),
                    new KeyValuePair<int,Items>(20,Items.SPEED_POTION),
                    new KeyValuePair<int,Items>(20,Items.STRENGTH_POTION)
                }
            },
            {
                DropType.lootbagOrc,new List<KeyValuePair<int, Items>>()
                {
                    new KeyValuePair<int,Items>(100,Items.REGENERATION_POTION)
                }
            },
            {
                DropType.lootbagDragon,new List<KeyValuePair<int, Items>>()
                {
                    new KeyValuePair<int,Items>(100,Items.REGENERATION_POTION)
                }
            }

        };

        // Main method used to spawn the loot!
        public static void SpawnLoot(DropType type,Vector2 pos)
        {
            PlaceLoot(DetermineLoot(dropTables[type]), pos);
        }

        private static Items DetermineLoot(List<KeyValuePair<int, Items>> dropList)
        {
            int stagedependency = LevelManager.level / 3 + 1;
            // % between 0 und 100
            int randomPercentage = Game1.rand.Next(20*(stagedependency-1), 33*stagedependency); // der zweite Wert ist exklusiv (nicht mit einbezogen)
            Debug.Print(" " + (33 * stagedependency)+" Percentage: "+randomPercentage);
            foreach (var itemPercentage in dropList)
            {
                if (randomPercentage <= itemPercentage.Key)
                {
                    //Debug.Print("Item id = " + pos + " random% is "+ randomPercentage +" with itempercentage " +itemPercentage.Key + " itemscount = " +dropList.Count);
                    return itemPercentage.Value;
                }
                else
                {
                    //Debug.Print("Next Weapon");
                    randomPercentage -= itemPercentage.Key;
                }
            }

            // default item
            return Items.DAGGER;
        }

        private static void PlaceLoot(Items item, Vector2 pos)
        {
            switch (item)
            {
                case Items.DAGGER:
                    new DaggerLoot(pos);
                    break;
                case Items.BOW:
                    new BowLoot(pos);
                    break;
                case Items.AXE:
                    new AxeLoot(pos);
                    break;
                case Items.BOMB:
                    new BombLoot(pos);
                    break;
                case Items.SPEAR:
                    new SpearLoot(pos);
                    break;
                case Items.HEALTH_POTION:
                    new HealthPotion(pos);
                    break;
                case Items.XP_POTION:
                    new ExperiencePotion(pos);
                    break;
                case Items.REGENERATION_POTION:
                    new HealthRegenerationPotion(pos);
                    break;
                case Items.SPEED_POTION:
                    new SpeedPotion(pos);
                    break;
                case Items.STRENGTH_POTION:
                    new StrengthPotion(pos);
                    break;
                default:
                    new DaggerLoot(pos);
                    break;
            }
        }

        public static DropType DetermineMonsterLootTable(Humanoid enemy)
        {
            if (enemy == null) return DropType.chestNormal;

            if(enemy is GreenZombie)
            {
                return DropType.lootbagGreenZombie;
            }
            else if(enemy is BrownZombie)
            {
                return DropType.lootbagBrownZombie;
            }
            else if(enemy is Skeleton)
            {
                return DropType.lootbagSkeleton;
            }
            else if(enemy is Wizard)
            {
                return DropType.lootbagWizard;
            }
            else if(enemy is Orc)
            {
                return DropType.lootbagOrc;
            }
            else if (enemy is Dragon)
            {
                return DropType.lootbagDragon;
            }
            // default loottable is normal chest
            else
            {
                return DropType.chestNormal;
            }
        }

        #region OLD LOOTABLE
        /*
        public static void SpawnLoot(DropType type, Vector2 pos)
        {
            /*
            Items chosenItem;
            //type = 1;
            switch (type)
            {
                // Normal Chest Loot
                case DropType.chestNormal:
                    chosenItem = DetermineLoot(chestNormalDroplist);
                    break;
                // Diamond Chest Loot
                case DropType.chestDiamond:
                    chosenItem = DetermineLoot(chestDiamondDroplist);
                    break;
                // Zombies Loot bag 
                case DropType.lootbagZombie:
                    chosenItem = DetermineLoot(lootbagZombieDroplist);
                    break;
                // skeleton loot bag
                case DropType.lootbagSkeleton:
                    chosenItem = DetermineLoot(lootbagSkeletonDroplist);
                    break;
                default:
                    chosenItem = Items.DAGGER;
                    break;
            }
            //Debug.Print("Chosen item to spawn is " + chosenItem
            PlaceLoot(choosenItem, pos);
        }

        
       // List mit keyvaluepairs um items mit gleiche wahrscheinlichkeit zu erlauben
       private static List<KeyValuePair<int, Items>> chestNormal = new List<KeyValuePair<int, Items>>()
       {
           new KeyValuePair<int,Items>(40,Items.DAGGER), // 40% chance to get Dagger (id 0)
           new KeyValuePair<int,Items>(30,Items.BOW), // 30% chance to get Bow (id 1)
           new KeyValuePair<int,Items>(15,Items.BOMB), // 10% chance to get Bomb (id 3)
           new KeyValuePair<int,Items>(10,Items.HEALTH_POTION), // 10% chance to get Healthpotion (id 5)
           new KeyValuePair<int, Items>(5,Items.SPEAR) // 5% chance to get Spear (id 4)
       };

       // List mit keyvaluepairs um items mit gleiche wahrscheinlichkeit zu erlauben
       private static List<KeyValuePair<int, Items>> chestNormalDroplist = new List<KeyValuePair<int, Items>>()
       {
           new KeyValuePair<int,Items>(40,Items.DAGGER), // 40% chance to get Dagger (id 0)
           new KeyValuePair<int,Items>(30,Items.BOW), // 30% chance to get Bow (id 1)
           new KeyValuePair<int,Items>(15,Items.BOMB), // 10% chance to get Bomb (id 3)
           new KeyValuePair<int,Items>(10,Items.HEALTH_POTION), // 10% chance to get Healthpotion (id 5)
           new KeyValuePair<int, Items>(5,Items.SPEAR) // 5% chance to get Spear (id 4)
       };

       private static List<KeyValuePair<int, Items>> chestDiamondDroplist = new List<KeyValuePair<int, Items>>()
       {
           new KeyValuePair<int,Items>(50,Items.AXE),
           new KeyValuePair<int,Items>(30,Items.HEALTH_POTION),
           new KeyValuePair<int, Items>(20,Items.SPEAR)
       };

       private static List<KeyValuePair<int, Items>> lootbagZombieDroplist = new List<KeyValuePair<int, Items>>()
       {
           new KeyValuePair<int,Items>(40,Items.DAGGER),
           new KeyValuePair<int,Items>(30,Items.BOW),
           new KeyValuePair<int,Items>(10,Items.XP_POTION),
           new KeyValuePair<int,Items>(10,Items.BOMB),
           new KeyValuePair<int,Items>(10,Items.HEALTH_POTION)
       };

       private static List<KeyValuePair<int, Items>> lootbagSkeletonDroplist = new List<KeyValuePair<int, Items>>()
       {
           new KeyValuePair<int,Items>(50,Items.DAGGER),
           new KeyValuePair<int,Items>(50,Items.BOW)
       };
       */

        #endregion
    }
}
