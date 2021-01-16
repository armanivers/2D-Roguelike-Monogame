using System;
using System.Collections.Generic;
using System.Text;
using _2DRoguelike.Content.Core.Entities.Actions;

namespace _2DRoguelike.Content.Core.Entities.AI.Actions.AnimationIdentifiers
{
    class TeleportAnimationIdentifier : AbilityAnimationIdentifier
    {
        public TeleportAnimationIdentifier(params string[] animationIdentifiers) : base(animationIdentifiers)
        {
        }
        public override string ChooseAnimation(Humanoid CallingInstance)
        {
            return "Spellcast" + PrintLineOfSight(CallingInstance);
        }
    }
}
