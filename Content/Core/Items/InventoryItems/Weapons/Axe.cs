using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.Weapons
{
    public class Axe : ShortRange
    {
        const float AXE_COOLDOWN = 0.8f;
        const int DAMAGE = 40;
        const float RANGE_MULTIPLIER_X = 0.9f;
        const float RANGE_MULTIPLIER_Y = 0.9f;

        public Axe(Humanoid Owner, float damageMultiplier = 1f, float cooldownMultiplier = 1f) : base(Owner, RANGE_MULTIPLIER_X, RANGE_MULTIPLIER_Y, 
            (int)(DAMAGE*damageMultiplier),AXE_COOLDOWN * cooldownMultiplier) {
            INVENTORY_SLOT = 3;
        }

        public override string GetAnimationType()
        {
            return "Slash";
        }

        public override string ToString()
        {
            return "Axe";
        }
    }
}
