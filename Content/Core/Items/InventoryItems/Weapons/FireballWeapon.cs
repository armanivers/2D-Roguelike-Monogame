using System;
using System.Collections.Generic;
using System.Text;
using _2DRoguelike.Content.Core.Entities;
using _2DRoguelike.Content.Core.Entities.Projectiles;
using _2DRoguelike.Content.Core.Entities.Weapons;

namespace _2DRoguelike.Content.Core.Items.InventoryItems.Weapons
{
    class FireballWeapon: LongRange
    {
        const float FIREBALL_COOLDOWN = 3f;
        const int DAMAGE = 10;
        private float explosionDamageMultiplier;

        public FireballWeapon(Humanoid Owner, float impactDamageMultiplier = 1f, float cooldownMultiplier = 1f, float explosionDamageMultiplier = 1f) : base(Owner, (int)(DAMAGE * impactDamageMultiplier), FIREBALL_COOLDOWN * cooldownMultiplier)
        {
            this.explosionDamageMultiplier = explosionDamageMultiplier;
            INVENTORY_SLOT = 5;
        }

        public override void CommenceWeaponLogic()
        {
            new FireballProjectile(owner, explosionDamageMultiplier);
        }

        public override string GetAnimationType()
        {
            return "Spellcast";
        }

        public override string ToString()
        {
            return "Fireball";
        }
    }
}