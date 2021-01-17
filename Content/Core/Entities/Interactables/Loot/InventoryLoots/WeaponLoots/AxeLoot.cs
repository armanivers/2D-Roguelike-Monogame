using System;
using System.Collections.Generic;
using System.Text;
using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.Entities.Loot.InventoryLoots.WeaponLoots;
using _2DRoguelike.Content.Core.Entities.Weapons;
using _2DRoguelike.Content.Core.Items.InventoryItems.Weapons;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.Loot.WeaponLoots
{
    public class AxeLoot : WeaponLoot
    {
        public AxeLoot(Vector2 pos) : base(pos) {
            texture = TextureManager.loot.LootAxe;
        }

        public override Weapon GetCorrespondingWeapon()
        {
            return new Axe(Player.Instance);
        }
    }
}
