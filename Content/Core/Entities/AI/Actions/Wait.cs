using System;
using System.Collections.Generic;
using System.Text;
using _2DRoguelike.Content.Core.Entities.Actions.AnimationIdentifiers;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.Actions
{
    public class Wait : Action
    {
        public Wait(Humanoid callInst) : base(callInst, new WaitAnimationIdentifier("IdleDown")) { }
        

        public override void ExecuteAction()
        {
            SetLineOfSight();
        }

        public override void SetLineOfSight()
        {
            CallingInstance.SetLineOfSight(Vector2.Zero);
        }
    }
}

