using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.Actions
{
     public abstract class AnimationIdentifier
    {
        public string[] PossibleAnimations { get; set; }

        public AnimationIdentifier(string[] animationIdentifiers) {
            PossibleAnimations = animationIdentifiers;
        }


        protected string PrintMouseDirection(Humanoid CallingInstance) {
            var differenz = InputController.MousePosition - CallingInstance.Position;
            var angle = Math.Atan2(differenz.X, differenz.Y);
            if (angle > 1 && angle < 2)
            {
                return "Right";
            }
            else if (angle > 2 && angle < 3)
            {
                return "Up";
            }
            else if (angle > -3 && angle < -2)
            {
                return "Up";
            }
            else if (angle > -1 && angle < 1)
            {
                return "Down";
            }
            else if (angle < -1 && angle > -2)
            {
                return "Left";
            }
            return "";
        }
        
        /// <summary>
        /// Diese Methode dann verwenden, wenn die Blickrichtung des Spielers relevant ist.
        /// NICHT, wenn Richtung von anderen Faktoren abhängt, wie z.B. Mauszeiger
        /// </summary>
        protected string PrintLineOfSight(Humanoid CallingInstance) {
            // returnt "Up", "Down", "Left", "Right" anhand von LineOfSight
            
            if (CallingInstance.LineOfSight.X > 0)
                return "Right";
            else if (CallingInstance.LineOfSight.X < 0)
                return "Left";
            else if (CallingInstance.LineOfSight.Y > 0)
                return "Down";
            else if (CallingInstance.LineOfSight.Y < 0)
                return "Up";
            // Default
            return "Down";
        }


        public abstract string ChooseAnimation(Humanoid CallingInstance);
    }
}
