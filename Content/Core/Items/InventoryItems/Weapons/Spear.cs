using System;
using System.Collections.Generic;
using System.Text;
using _2DRoguelike.Content.Core.Entities;
using _2DRoguelike.Content.Core.Entities.Weapons;

namespace _2DRoguelike.Content.Core.Items.InventoryItems.Weapons
{
    public class Spear : ShortRange
    {
        // TODO: Attribut maximumHitsPerUsage

        const float SPEAR_COOLDOWN = 1f;
        const int DAMAGE = 60;
        const float RANGE_MULTIPLIER_X = 1.3f;
        const float RANGE_MULTIPLIER_Y = 0.55f;

        public Spear(Humanoid Owner, float damageMultiplier = 1f, float cooldownMultiplier = 1f) : base(Owner, RANGE_MULTIPLIER_X, RANGE_MULTIPLIER_Y,
            (int)(DAMAGE * damageMultiplier), SPEAR_COOLDOWN * cooldownMultiplier)
        {
            INVENTORY_SLOT = 5;
        }

        public override string GetAnimationType()
        {
            return "Thrust";
        }

        public override string ToString()
        {
            return "Spear";
        }
    }
}
