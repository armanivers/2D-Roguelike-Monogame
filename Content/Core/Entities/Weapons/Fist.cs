﻿using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Weapons
{
    public class Fist : ShortRange
    {
        const float FIST_COOLDOWN = 0.3f;
        const int DAMAGE = 10;
        const float RANGE_MULTIPLIER_X = 0.8f;
        const float RANGE_MULTIPLIER_Y = 0.8f;

        public Fist(Humanoid Owner, float damageMultiplier = 1f, float cooldownMultiplier = 1f) : base(Owner, RANGE_MULTIPLIER_X, RANGE_MULTIPLIER_Y,
            (int)(DAMAGE * damageMultiplier), FIST_COOLDOWN * cooldownMultiplier)
        { }

        public override string ToString()
        {
            return "Fist";
        }

    }
}