using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Weapons
{
    public class Axe : ShortRange
    {
        const float BOW_COOLDOWN = 0.5f;

        public Axe(float cooldownMultiplier = 1f) : base(BOW_COOLDOWN * cooldownMultiplier) { }

        public override string ToString()
        {
            return "Axe";
        }
    }
}
