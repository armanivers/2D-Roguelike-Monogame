using System;
using System.Collections.Generic;
using System.Text;
using _2DRoguelike.Content.Core.Entities.Creatures.Projectiles;

namespace _2DRoguelike.Content.Core.Entities.Weapons
{
    public abstract class LongRange : Weapon
    {
        public LongRange(Humanoid Owner, int weaponDamage, float weaponCooldown) : base(Owner, weaponDamage,  weaponCooldown) { }


    }
}
