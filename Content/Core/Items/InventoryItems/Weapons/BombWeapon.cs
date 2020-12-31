using System;
using System.Collections.Generic;
using System.Text;
using _2DRoguelike.Content.Core.Entities.Projectiles;

namespace _2DRoguelike.Content.Core.Entities.Weapons
{
    public class BombWeapon : LongRange
    {
        const float BOMB_COOLDOWN = 3f;
        const int DAMAGE = 100;

        public BombWeapon(Humanoid Owner, float damageMultiplier = 1f, float cooldownMultiplier = 1f) : base(Owner, (int)(DAMAGE * damageMultiplier), BOMB_COOLDOWN * cooldownMultiplier)
        {
            INVENTORY_SLOT = 4;
        }

        public override void UseWeapon()
        {
            new BombProjectile(Owner, 2.5f);
        }

        public override string ToString()
        {
            return "Bomb";
        }
    }
}
