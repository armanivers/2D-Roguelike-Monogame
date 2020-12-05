using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities
{
    public abstract class InventoryItem
    {
        public Humanoid Owner { get; set; }

        public InventoryItem(Humanoid Owner) {
            this.Owner = Owner;
        }
    }
}
