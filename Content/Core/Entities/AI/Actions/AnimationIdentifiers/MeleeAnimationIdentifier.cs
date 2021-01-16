using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Actions
{
    public class MeleeAnimationIdentifier : AttackAnimationIdentifier
    {
        public MeleeAnimationIdentifier(params string[] animationIdentifiers) : base(animationIdentifiers) { 
        }

        public override string ChooseAnimation(Humanoid CallingInstance)
        {
            // Debug.WriteLine("Slash" + PrintLineOfSight(CallingInstance));
            String ret = CallingInstance.CurrentWeapon.GetAnimationType() + PrintLineOfSight(CallingInstance) + "_" + CallingInstance.CurrentWeapon.ToString();
            if (!CallingInstance.AnimationExists(ret))
                ret = ret.Substring(0, ret.Length - CallingInstance.CurrentWeapon.ToString().Length) + CallingInstance.defaultAnimationWeapon;
            return ret;
        }
    }
}
