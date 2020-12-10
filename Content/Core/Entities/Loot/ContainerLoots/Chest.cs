using System;
using System.Collections.Generic;
using _2DRoguelike.Content.Core.Entities.Loot.WeaponLoots;
using Microsoft.Xna.Framework;


namespace _2DRoguelike.Content.Core.Entities.Loot.Potions
{
    public class Chest : LootContainer
    {
        private List<InventoryItem> dropList;
        public Chest(Vector2 pos, List<InventoryItem> dropList) : base(pos, 2)
        {
            texture = TextureManager.LootChest;
            floatable = false;
            this.dropList = dropList;
        }

        public override void OpenContainer()
        {
            // drop all items from the provided droplist
            new AxeLoot(position);
            isExpired = true;
        }
    }
}