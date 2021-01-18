using System;
using System.Collections.Generic;
using System.Text;
using _2DRoguelike.Content.Core.Entities.Actions;

namespace _2DRoguelike.Content.Core.Entities.AI.Actions.AnimationIdentifiers.EnemyAnimationIdentifiers
{
    class ProjectileBarrageAnimationIdentifier: AttackAnimationIdentifier
{
        public ProjectileBarrageAnimationIdentifier(params string[] animationIdentifiers) : base(animationIdentifiers)
        {
        }

        public override string ChooseAnimation(Humanoid CallingInstance)
        {


            String ret = CallingInstance.inventory.CurrentWeapon.GetAnimationType() + PrintLineOfSight(CallingInstance);
            if (!CallingInstance.AnimationExists(ret))
                ret += "_" + CallingInstance.defaultAnimationWeapon;
            return ret;
        }
    }
}
