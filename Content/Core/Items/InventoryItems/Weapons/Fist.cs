using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Weapons
{
    public class Fist : ShortRange
    {
        const float FIST_COOLDOWN = 0.3f;
        const int DAMAGE = 10;
        const float RANGE_MULTIPLIER_X = 0.7f;
        const float RANGE_MULTIPLIER_Y = 0.8f;

        public Fist(Humanoid Owner, float damageMultiplier = 1f, float cooldownMultiplier = 1f, float rangeX = 1f, float rangeY = 1f)
            : base(Owner, rangeX*RANGE_MULTIPLIER_X, rangeY*RANGE_MULTIPLIER_Y,
            (int)(DAMAGE * damageMultiplier), FIST_COOLDOWN * cooldownMultiplier)
        {
            INVENTORY_SLOT = 0;
        }

        public override string GetAnimationType()
        {
            return "Slash";
        }

        public override string ToString()
        {
            return "Fist";
        }

    }
}
