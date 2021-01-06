﻿using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Weapons
{
    public class Dagger : ShortRange
    {
        const float DAGGER_COOLDOWN = 0.4f;
        const int DAMAGE = 34;
        const float RANGE_MULTIPLIER_X = 0.8f;
        const float RANGE_MULTIPLIER_Y = 0.8f;

        public Dagger(Humanoid Owner, float damageMultiplier = 1f, float cooldownMultiplier = 1f) : base(Owner, RANGE_MULTIPLIER_X, RANGE_MULTIPLIER_Y,
            (int)(DAMAGE * damageMultiplier), DAGGER_COOLDOWN * cooldownMultiplier)
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