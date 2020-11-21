using System;
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
            // TODO: Ausführen der Logik für die Bewegung
            Vector2 direction = CallingInstance.GetDirection();
            
            CallingInstance.Acceleration = CallingInstance.movingSpeed * CallingInstance.SpeedModifier * direction;
            if (CallingInstance.MutipleDirections(CallingInstance.GetDirection()))
                CallingInstance.Acceleration /= 1.3f;

            CallingInstance.Acceleration.X = (float)Math.Round((double)CallingInstance.Acceleration.X);
            CallingInstance.Acceleration.Y = (float)Math.Round((double)CallingInstance.Acceleration.Y);

            // von float in int
            CallingInstance.hitbox.X += (int)CallingInstance.Acceleration.X;
            // Wenn Bewegung nicht möglich: Hitbox wieder zurücksetzen
            // CollidesWithFrameBorder() weggemacht
            if (CallingInstance.CollidesWithSolidTile())
            {
                CallingInstance.hitbox.X -= (int)CallingInstance.Acceleration.X;

            }
            else
            {
                CallingInstance.Position += new Vector2(CallingInstance.Acceleration.X, 0);
            }


            CallingInstance.hitbox.Y += (int)CallingInstance.Acceleration.Y;
            if (CallingInstance.CollidesWithSolidTile())
            {
                CallingInstance.hitbox.Y -= (int)CallingInstance.Acceleration.Y;
            }
            else
            {
                CallingInstance.Position += new Vector2(0, CallingInstance.Acceleration.Y);
            }

            
        }

        public override void SetLineOfSight() {
            CallingInstance.SetLineOfSight(CallingInstance.GetDirection());
        }
    }
}
