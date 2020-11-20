using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Actions
{
    public abstract class AttackAnimationIdentifier : AnimationIdentifier
    {
        public AttackAnimationIdentifier(string[] animationIdentifiers) : base(animationIdentifiers) { 
        }
    }
}
