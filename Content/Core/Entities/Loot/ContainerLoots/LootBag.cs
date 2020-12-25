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
                new BowLoot(Position);
            }
            isExpired = true;
        }

        public override void PlaySound()
        {
            SoundManager.LootbagOpen.Play(Game1.gameSettings.soundeffectsLevel, 0, 0);
        }
    }
}