using System;
using System.Collections.Generic;
using System.Text;
using _2DRoguelike.Content.Core.Entities;
using _2DRoguelike.Content.Core.Entities.Projectiles;
using _2DRoguelike.Content.Core.Entities.Weapons;

namespace _2DRoguelike.Content.Core.Items.InventoryItems.Weapons
{
    public class BombWeapon : LongRange
    {
        const float BOMB_COOLDOWN = 3f;
        const int DAMAGE = 100;

        public BombWeapon(Humanoid Owner, float damageMultiplier = 1f, float cooldownMultiplier = 1f) : base(Owner, (int)(DAMAGE * damageMultiplier), BOMB_COOLDOWN * cooldownMultiplier)
        {
            INVENTORY_SLOT = 4;
        }

        public override void CommenceWeaponLogic()
        {
            new BombProjectile(owner, 2.5f);
        }

        public override string GetAnimationType()
        {
            return "Slash";
        }

        public override string ToString()
        {
            return "Bomb";
        }
    }
}
