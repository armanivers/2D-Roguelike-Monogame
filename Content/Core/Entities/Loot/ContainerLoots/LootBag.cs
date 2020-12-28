using _2DRoguelike.Content.Core.Entities.Loot.InventoryLoots.WeaponLoots;
using _2DRoguelike.Content.Core.Entities.Loot.WeaponLoots;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Loot.Potions
{
    public class LootBag : LootContainer
    {
        private const float TIME_TO_OPEN = 0.3f;

        public LootBag(Vector2 pos, List<InventoryItem> dropList) : base(pos, TIME_TO_OPEN)

        {
            texture = TextureManager.LootBag;
            this.dropList = dropList;
        }

        public override void OpenContainer()
        {
            // drop all items from the provided droplist

            if(dropList != null)
            {
                foreach(var item in dropList)
                {

                }
            }
            else
            {
                // default drop item if no droplist given

                SpawnRandomLoot();
                //new BowLoot(Position);
            }
            isExpired = true;
        }

        private void SpawnRandomLoot()
        {
            int LootTypeProbability = Game1.rand.Next(101);
            
            if (LootTypeProbability <= 20)
            {
                int WeaponLootProbability = Game1.rand.Next(101);

                if (WeaponLootProbability <= 10)
                    new BombLoot(Position);
                else
                if (WeaponLootProbability <= 30)
                    new AxeLoot(Position);
                else
                if (WeaponLootProbability <= 60)
                    new BowLoot(Position);
                else
                if (WeaponLootProbability <= 100)
                    new DaggerLoot(Position);
            }
            else if (LootTypeProbability <= 100)
            {
                int PotionLootProbability = Game1.rand.Next(101);

                if (PotionLootProbability <= 40)
                    new ExperiencePotion(Position);
                else
                if (PotionLootProbability <= 100)
                    new HealthPotion(Position);
            }
        }
        public override void PlaySound()
        {
            SoundManager.LootbagOpen.Play(Game1.gameSettings.soundeffectsLevel, 0, 0);
        }
    }
}