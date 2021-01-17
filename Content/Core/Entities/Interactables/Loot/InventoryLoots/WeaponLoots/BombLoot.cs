using System;
using System.Collections.Generic;
using System.Text;
using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.Entities.Weapons;
using _2DRoguelike.Content.Core.Items.InventoryItems.Weapons;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.Loot.InventoryLoots.WeaponLoots
{
    public class BombLoot : WeaponLoot
    {
        public BombLoot(Vector2 pos) : base(pos)
        {
            texture = TextureManager.loot.LootBomb;
        }

        public override Weapon GetCorrespondingWeapon()
        {
            return new BombWeapon(Player.Instance);
        }
    }
}
