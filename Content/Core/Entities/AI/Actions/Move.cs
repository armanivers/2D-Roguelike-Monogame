﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.Actions
{
    public class Move : Action
    {
        public Move(Humanoid callInst) : base(callInst, new MoveAnimationIdentifier("WalkRight","WalkLeft","WalkDown","WalkUp")){             
        }

        public override void ExecuteAction()
        {
            Walk();
            SetLineOfSight();
        }

        public void Walk() {
            Vector2 direction = CallingInstance.GetDirection();
            
            CallingInstance.Acceleration = CallingInstance.movingSpeed * CallingInstance.SpeedModifier * direction;
            if (CallingInstance.MutipleDirections(CallingInstance.GetDirection()))
                CallingInstance.Acceleration /= 1.3f;

            CallingInstance.Acceleration.X = (float)Math.Round((double)CallingInstance.Acceleration.X);
            CallingInstance.Acceleration.Y = (float)Math.Round((double)CallingInstance.Acceleration.Y);

            // von float in int
            CallingInstance.Position += new Vector2((int)CallingInstance.Acceleration.X,0);
            // Wenn Bewegung nicht möglich: Hitbox wieder zurücksetzen
            // CollidesWithFrameBorder() weggemacht
            if (CallingInstance.CannotWalkHere())
            {
                CallingInstance.Position -= new Vector2((int)CallingInstance.Acceleration.X, 0);
            }



            CallingInstance.Position += new Vector2(0,(int)CallingInstance.Acceleration.Y);
            if (CallingInstance.CannotWalkHere())
            {
                CallingInstance.Position -= new Vector2(0, (int)CallingInstance.Acceleration.Y);
            }
            

            
        }

        public override void SetLineOfSight() {
            CallingInstance.SetLineOfSight(CallingInstance.GetDirection());
        }
    }
}
