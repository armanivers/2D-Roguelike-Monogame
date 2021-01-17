using _2DRoguelike.Content.Core.Entities;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace _2DRoguelike.Content.Core.Items.InventoryItems.UsableItems
{
    public abstract class UsableItem : InventoryItem ,IEquatable<UsableItem>
    {
        public Texture2D icon;
        public int quantity = 1;
        public UsableItem(Humanoid owner) : base(owner){}

        public abstract void ActivateItem();

        public bool Equals(UsableItem other)
        {
            if (other == null) return false;

            return (other.GetType() == this.GetType());
        }
    }
}
