﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.Actions
{
    public abstract class Action
    {
        public AnimationIdentifier AnimationIdentif { get; set; }
        public Humanoid CallingInstance { get; set; }


        public Action(Humanoid callInst, AnimationIdentifier animIdent) {
            CallingInstance = callInst;
            AnimationIdentif = animIdent;
        }

        public abstract void ExecuteAction();

        public abstract void SetLineOfSight();

        public string ChooseAnimation() {
            String ret = AnimationIdentif.ChooseAnimation(CallingInstance);
            //if(CallingInstance is Player.Player) Debug.WriteLine(ret);
            return ret;
        }

        public virtual bool StateFinished(float gameTime) {
            return true;
        }


    }
}
