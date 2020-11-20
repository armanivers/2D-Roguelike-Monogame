using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Actions
{
    public class MoveAnimationIdentifier : AnimationIdentifier
    {
        public MoveAnimationIdentifier(params string[] animationIdentifiers) : base(animationIdentifiers) { }
        public override string ChooseAnimation(Humanoid CallingInstance)
        {
            // TODO: AnimationString bestimmen anhand von LineOfSight

            String ret = PrintLineOfSight(CallingInstance);
            return ret == "" ? "Idle" : "Walk" + ret;
        }
    }
}
