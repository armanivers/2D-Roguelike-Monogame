using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Actions
{
    public class MeleeAnimationIdentifier : AttackAnimationIdentifier
    {
        public MeleeAnimationIdentifier(params string[] animationIdentifiers) : base(animationIdentifiers) { 
        }

        public override string ChooseAnimation(Humanoid CallingInstance)
        {
            // TODO: AnimationString bestimmen anhand von LineOfSight
            throw new NotImplementedException();
        }
    }
}
