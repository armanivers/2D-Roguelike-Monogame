using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Weapons
{
    public class Dagger : ShortRange
    {
        const float DAGGER_COOLDOWN = 0.4f;
        const int DAMAGE = 20;
        const float RANGE_MULTIPLIER_X = 0.8f;
        const float RANGE_MULTIPLIER_Y = 0.8f;

        const byte DEFAULT_MAXIMUM_HITS_PER_ATTACK = 1;

        public Dagger(Humanoid Owner, float damageMultiplier = 1f, float cooldownMultiplier = 1f) : base(Owner, RANGE_MULTIPLIER_X, RANGE_MULTIPLIER_Y,
            (int)(DAMAGE * damageMultiplier), DAGGER_COOLDOWN * cooldownMultiplier, DEFAULT_MAXIMUM_HITS_PER_ATTACK)
        {
            INVENTORY_SLOT = 1;
        }

        public override string GetAnimationType()
        {
            return "Slash";
        }

        public override string ToString()
        {
            return "Dagger";
        }
    }
}
