using System;
using System.Collections.Generic;
using System.Text;
using _2DRoguelike.Content.Core.Entities.Weapons;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.Loot.InventoryLoots.WeaponLoots
{
    public class BombLoot : WeaponLoot
    {
        public BombLoot(Vector2 pos) : base(pos)
        {
            texture = TextureManager.LootBomb;
        }

        public override Weapon GetCorrespondingWeapon()
        {
            return new BombWeapon(ControllingPlayer.Player.Instance);
        }
    }
}
