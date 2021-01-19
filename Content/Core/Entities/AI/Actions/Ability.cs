using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Actions
{
    public abstract class Ability: Action
    {
        public Ability(Humanoid callInst,AbilityAnimationIdentifier abilityAnimationIdent) : base(callInst,abilityAnimationIdent) { 
        }

        public override sealed void ExecuteAction()
        {
            UseAbility();
            SetLineOfSight();
        }

        public abstract void UseAbility();
    }
}
