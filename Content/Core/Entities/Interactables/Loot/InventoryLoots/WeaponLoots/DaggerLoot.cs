using System;
using System.Collections.Generic;
using System.Text;
using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.Entities.Weapons;
using _2DRoguelike.Content.Core.Items.InventoryItems.Weapons;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.Loot.InventoryLoots.WeaponLoots
{
    class DaggerLoot: WeaponLoot
    {
        public DaggerLoot(Vector2 pos) : base(pos) {
            texture = TextureManager.loot.LootDagger;
        }

        public override Weapon GetCorrespondingWeapon()
        {
            return new Dagger(Player.Instance);
        }
    }
}
