using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Actions
{
    public abstract class Attack: Action
    {
        public Attack(Humanoid callInst,AttackAnimationIdentifier attackAnimationIdent) : base(callInst, attackAnimationIdent){             
        }

        public override void ExecuteAction()
        {
            CommenceAttack();
        }
        public abstract void CommenceAttack();
    }
}
