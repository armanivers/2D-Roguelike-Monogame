using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.Weapons
{
    public class Axe : ShortRange
    {
        const float AXE_COOLDOWN = 0.7f;
        const int DAMAGE = 50;
        const float RANGE_MULTIPLIER_X = 1f;
        const float RANGE_MULTIPLIER_Y = 1f;

        public Axe(Humanoid Owner, float damageMultiplier = 1f, float cooldownMultiplier = 1f) : base(Owner, RANGE_MULTIPLIER_X, RANGE_MULTIPLIER_Y, 
            (int)(DAMAGE*damageMultiplier),AXE_COOLDOWN * cooldownMultiplier) { }

        public override string ToString()
        {
            return "Axe";
        }
    }
}
