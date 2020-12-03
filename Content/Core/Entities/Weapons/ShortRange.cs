using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Weapons
{
    public abstract class ShortRange : Weapon
    {
        public ShortRange(float weaponCooldown) : base(weaponCooldown) { }
    }
}
