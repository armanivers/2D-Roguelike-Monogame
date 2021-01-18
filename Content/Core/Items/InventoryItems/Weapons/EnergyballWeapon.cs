using System;
using System.Collections.Generic;
using System.Text;
using _2DRoguelike.Content.Core.Entities;
using _2DRoguelike.Content.Core.Entities.Projectiles;

namespace _2DRoguelike.Content.Core.Items.InventoryItems.Weapons
{
     class EnergyballWeapon : LongRange
{
    const float ENERGYBALL_COOLDOWN = 3f;
    const int DAMAGE = 20;

    public EnergyballWeapon(Humanoid Owner, float impactDamageMultiplier = 1f, float cooldownMultiplier = 1f) : base(Owner, (int)(DAMAGE * impactDamageMultiplier), ENERGYBALL_COOLDOWN * cooldownMultiplier)
    {
        INVENTORY_SLOT = 5;
    }

    public override void UseWeapon()
    {
        new EnergyballProjectile(owner);
    }

    public override string GetAnimationType()
    {
        return "Spellcast";
    }

    public override string ToString()
    {
        return "Energyball";
    }
}
}
