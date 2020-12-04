using System;
using System.Collections.Generic;
using System.Text;
using _2DRoguelike.Content.Core.Entities.Loot.InventoryLoots.WeaponLoots;
using _2DRoguelike.Content.Core.Entities.Weapons;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.Loot.WeaponLoots
{
    public class BowLoot : WeaponLoot
    {
        public BowLoot(Vector2 pos) : base(pos) { }

        public override Weapon GetCorrespondingWeapon()
        {
            return new Bow(ControllingPlayer.Player.Instance);
        }
    }
}
