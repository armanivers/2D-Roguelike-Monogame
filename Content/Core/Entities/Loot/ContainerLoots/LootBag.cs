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
            new BowLoot(Position);
            isExpired = true;
        }
    }
}