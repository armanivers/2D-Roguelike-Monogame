using _2DRoguelike.Content.Core.Entities;
using _2DRoguelike.Content.Core.Items.InventoryItems;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Items.InventoryItems
{
    public abstract class InventoryItem : ItemBase
    {
        public int INVENTORY_SLOT;
        public InventoryItem(Humanoid owner) : base(owner){}
    }
}
