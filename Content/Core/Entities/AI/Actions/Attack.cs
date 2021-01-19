using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Actions
{
    public abstract class Attack: Action
    {
        public Attack(Humanoid callInst,AttackAnimationIdentifier attackAnimationIdent) : base(callInst, attackAnimationIdent){             
        }

        public override sealed void ExecuteAction()
        {
            CommenceAttack();
            SetLineOfSight();
        }
        public abstract void CommenceAttack();

        public override void SetLineOfSight()
        {
            // Player und Enemies: Schauen in die Richtung, in die sie angreifen
            CallingInstance.SetLineOfSight(CallingInstance.GetAttackLineOfSight());
        }
    }
}
