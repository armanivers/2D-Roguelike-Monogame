using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.Actions
{
    // TODO: Funktion zum Schützen einbinden
    public class Protect : Ability
    {
        const float DEFAULT_TIME_IN_STATE = 3f;
        public float expiredTimeInState;

        private const float PROTECT_COOLDOWN = 3f;

        private static float timeOfLastUsage = 0f;


        public Protect(Humanoid callInst, float startingTime) : base(callInst, new ProtectAnimationIdentifier("SpellcastRight", "SpellcastLeft", "SpellcastDown", "SpellcastUp"))
        {
            CallingInstance.Mana = 0;
            timeOfLastUsage = startingTime;
        }


        public override void UseAbility()
        {
            // TODO: Hier die Logik für den Ablauf einfügen
            CallingInstance.Invincible = true;
            CallingInstance.currentColor = Color.GhostWhite;
            if (CallingInstance.transparency >= 0.5f)
                CallingInstance.transparency -= 0.02f;
        }

        public override void SetLineOfSight()
        {
        }

        public override bool StateFinished(float currentGameTime)
        {
            if (!InputController.IsRightMouseButtonHeld() || (currentGameTime - timeOfLastUsage) > DEFAULT_TIME_IN_STATE)
            {
                CallingInstance.transparency = 1f;
                CallingInstance.currentColor = CallingInstance.initialColor;
                CallingInstance.Invincible = false;
                timeOfLastUsage = currentGameTime;
                
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CanSwitchToState(float currentGameTime) {
            return timeOfLastUsage == 0f || currentGameTime - timeOfLastUsage >= PROTECT_COOLDOWN;
        }
    }
}
