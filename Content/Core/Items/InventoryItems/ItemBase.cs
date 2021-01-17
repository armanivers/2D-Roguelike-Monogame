using _2DRoguelike.Content.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Items.InventoryItems
{
    public abstract class ItemBase
    {
        public Humanoid owner { get; set; }

        public ItemBase(Humanoid owner)
        {
            this.owner = owner;
        }

    }
}
