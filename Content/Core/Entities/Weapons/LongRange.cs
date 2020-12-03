using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Weapons
{
    public abstract class LongRange : Weapon
    {
        public LongRange(float weaponCooldown) : base(weaponCooldown) { }
    }
}
