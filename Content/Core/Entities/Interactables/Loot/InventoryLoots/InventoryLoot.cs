using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.Loot
{
    public abstract class InventoryLoot : LootBase
    {
        public InventoryLoot(Vector2 pos) : base(pos) {
            floatingSpeed = 0.05f;
        }

        public override void OnContact()
        {
            SoundManager.ItemPickup.Play(Game1.gameSettings.soundeffectsLevel, 0, 0);
        }
    }
}
