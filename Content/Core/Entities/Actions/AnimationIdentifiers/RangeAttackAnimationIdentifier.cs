using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Actions
{
    public class RangeAttackAnimationIdentifier : AttackAnimationIdentifier
    {
        public RangeAttackAnimationIdentifier(params string[] animationIdentifiers) : base(animationIdentifiers){
        }

        public override string ChooseAnimation(Humanoid CallingInstance)
        {
            // TODO: AnimationString bestimmen anhand von MousePosition
            string ret = PrintMouseDirection(CallingInstance);
            
            return ret == "" ? "Idle" : "Shoot" + ret;
        }
    }
}
