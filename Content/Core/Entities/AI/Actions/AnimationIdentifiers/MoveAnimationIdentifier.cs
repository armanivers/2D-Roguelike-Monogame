using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.Actions
{
    public class MoveAnimationIdentifier : AnimationIdentifier
    {
        public MoveAnimationIdentifier(params string[] animationIdentifiers) : base(animationIdentifiers) { }
        public override string ChooseAnimation(Humanoid CallingInstance)
        {
            // TODO: AnimationString bestimmen anhand von LineOfSight
            if(CallingInstance.GetDirection() == Vector2.Zero)
                return "Idle" + PrintLineOfSight(CallingInstance);
            String ret = "Walk" + PrintLineOfSight(CallingInstance) + "_" + CallingInstance.CurrentWeapon.ToString();
            if (!CallingInstance.AnimationExists(ret))
                ret = ret.Substring(0, ret.Length - CallingInstance.CurrentWeapon.ToString().Length) + "Fist";
            return ret;
        }
    }
}
