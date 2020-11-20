using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Actions
{
    public abstract class AbilityAnimationIdentifier : AnimationIdentifier
    {
        public AbilityAnimationIdentifier(string[] animationIdentifiers) : base(animationIdentifiers) { 
        }
    }
}
