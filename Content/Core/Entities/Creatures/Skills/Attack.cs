using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities
{
    public abstract class Attack: Skills
    {
        public enum AttackMethod { 
            MELEE,
            RANGE_ATTACK
        }
        public abstract void StartAttack();
    }
}
