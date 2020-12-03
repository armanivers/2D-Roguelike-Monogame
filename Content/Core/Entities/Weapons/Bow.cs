using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Weapons
{
    public class Bow : LongRange
    {
        const float BOW_COOLDOWN = 1.5f;
        public Bow(float cooldownMultiplier = 1f) : base(BOW_COOLDOWN * cooldownMultiplier) { }

        public override string ToString()
        {
            return "Bow";
        }
    }
}
