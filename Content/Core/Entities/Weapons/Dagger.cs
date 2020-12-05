using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Weapons
{
    public class Dagger : ShortRange
    {
        const float DAGGER_COOLDOWN = 0.4f;
        const int DAMAGE = 33;
        const float RANGE_MULTIPLIER_X = 0.9f;
        const float RANGE_MULTIPLIER_Y = 0.9f;

        public Dagger(Humanoid Owner, float damageMultiplier = 1f, float cooldownMultiplier = 1f) : base(Owner, RANGE_MULTIPLIER_X, RANGE_MULTIPLIER_Y,
            (int)(DAMAGE * damageMultiplier), DAGGER_COOLDOWN * cooldownMultiplier)
        { }

        public override string ToString()
        {
            return "Dagger";
        }
    }
}
