using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Actions.AnimationIdentifiers
{
    public class WaitAnimationIdentifier : AnimationIdentifier
    {
        public WaitAnimationIdentifier(params string[] animationIdentifiers) : base(animationIdentifiers)
        {
        }

        public override string ChooseAnimation(Humanoid CallingInstance)
        {
            return "Idle" + PrintLineOfSight(CallingInstance);
        }
    }
}
