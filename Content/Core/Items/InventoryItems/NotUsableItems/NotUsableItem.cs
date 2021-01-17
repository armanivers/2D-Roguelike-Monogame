using _2DRoguelike.Content.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Items.InventoryItems.NotUsableItems
{
    public abstract class NotUsableItem : InventoryItem
    {
        public NotUsableItem(Humanoid owner) : base(owner)
        {

        }
    }
}
