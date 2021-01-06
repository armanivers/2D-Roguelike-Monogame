using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.Actions
{
    public class RangeAttackAnimationIdentifier : AttackAnimationIdentifier
    {
        public RangeAttackAnimationIdentifier(params string[] animationIdentifiers) : base(animationIdentifiers){
        }

        public override string ChooseAnimation(Humanoid CallingInstance)
        {


            String ret = CallingInstance.CurrentWeapon.GetAnimationType() + PrintLineOfSight(CallingInstance);
            if (!CallingInstance.AnimationExists(ret))
                ret += "_" + CallingInstance.defaultAnimationWeapon;
            return ret;
        }
    }
}
