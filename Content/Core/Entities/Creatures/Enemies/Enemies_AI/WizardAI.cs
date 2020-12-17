﻿

using System;
using _2DRoguelike.Content.Core.Entities.Actions;
using Microsoft.Xna.Framework;
using Action = _2DRoguelike.Content.Core.Entities.Actions.Action;

namespace _2DRoguelike.Content.Core.Entities.Creatures.Enemies.Enemies_AI
{
    public class WizardAI : EnemyAI
    {
        public WizardAI(Wizard agent) : base(agent)
        {
        }
        
        public override Action DetermineAction()
        {


            if (!agent.IsAttacking())
            {
                if (!agent.WeaponInventory[1].InUsage())
                {

                    // Check, ob Pfeil treffen würde
                    if (SimulateArrowAttack())
                    {
                        if (React())
                        {
                            agent.WeaponInventory[1].CooldownTimer = 0;
                            agent.CurrentWeapon = agent.WeaponInventory[1];
                            return new RangeAttack(agent);
                        }

                    }
                }
                // else if (!agent.WeaponInventory[0].InUsage())
                //{
                //   if (React) { 
                //    agent.WeaponInventory[0].CooldownTimer = 0;
                //    agent.CurrentWeapon = agent.WeaponInventory[0];
                //    return new Melee(agent);
                //    }
                //}
                else {
                    ResetReactionTimer();
                }
            }
            return new Move(agent);
        }

        public override Vector2 DeterminePath()
        {
            throw new NotImplementedException();
        }

    }
}
