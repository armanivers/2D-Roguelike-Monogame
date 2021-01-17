using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using _2DRoguelike.Content.Core.Entities.Actions;
using _2DRoguelike.Content.Core.Entities.AI.Actions.AnimationIdentifiers;
using _2DRoguelike.Content.Core.World;
using _2DRoguelike.Content.Core.World.Rooms;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.AI.Actions
{
    public class Teleport : Ability
    {

        Vector2? newPosition;

        const float DEFAULT_TIME_IN_STATE = 30f;
        public float expiredTimeInState;

        private float startingGameTimee = 0f;


        public Teleport(Humanoid callInst, float startingTime) : base(callInst, new TeleportAnimationIdentifier("SpellcastRight", "SpellcastLeft", "SpellcastDown", "SpellcastUp"))
        {
            callInst.currentColor = Color.LightGreen;
            CallingInstance.Invincible = true;
            startingGameTimee = startingTime;
            newPosition = null;
        }


        public override void UseAbility()
        {
            CallingInstance.Mana = 0;
            if (expiredTimeInState < DEFAULT_TIME_IN_STATE / 2)
            {
                if (CallingInstance.transparency >= 0f)
                    CallingInstance.transparency -= 0.05f;
            }
            else if (newPosition == null)
            {
                if (LevelManager.currentmap.currentroom != null)
                {
                    newPosition = CallingInstance.Position = Room.getRandomCoordinateInCurrentRoom(CallingInstance);
                }
                else newPosition = CallingInstance.Position;
            }
            else
            {
                if (CallingInstance.transparency < 1f)
                    CallingInstance.transparency += 0.05f;
            }
        }

        public override void SetLineOfSight()
        {
        }

        public override bool StateFinished(float currentGameTime)
        {
            expiredTimeInState += (currentGameTime - startingGameTimee);

            if (expiredTimeInState >= DEFAULT_TIME_IN_STATE) /*(currentGameTime - timeOfLastUsage) > DEFAULT_TIME_IN_STATE)*/
            {
                CallingInstance.transparency = 1f;
                CallingInstance.currentColor = CallingInstance.initialColor;
                CallingInstance.Invincible = false;
                // timeOfLastUsage = currentGameTime;

                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CanSwitchToState(Creature creat)
        {
            return creat.Mana == Creature.MAX_MANA && LevelManager.currentmap.currentroom != null;
            //return timeOfLastUsage == 0f || currentGameTime - timeOfLastUsage >= PROTECT_COOLDOWN;
        }
    }
}
