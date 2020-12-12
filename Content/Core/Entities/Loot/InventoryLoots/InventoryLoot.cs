using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.Loot
{
    public abstract class InventoryLoot : LootBase
    {
        public InventoryLoot(Vector2 pos) : base(pos) {
            EntityManager.AddLootEntity(this);
            floatingSpeed = 0.05f;
        }
    
    
    }
}
