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
            // TODO: AnimationString bestimmen anhand von Weapon Attribut

            return CallingInstance.CurrentWeapon.GetAnimationType() + PrintLineOfSight(CallingInstance) + "_" + CallingInstance.CurrentWeapon.ToString();
        }
    }
}
