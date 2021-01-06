﻿using System;
using System.Collections.Generic;
using System.Text;
using _2DRoguelike.Content.Core.Entities.Actions;
using _2DRoguelike.Content.Core.Entities.Creatures.Projectiles;
using Microsoft.Xna.Framework;
using Action = _2DRoguelike.Content.Core.Entities.Actions.Action;

namespace _2DRoguelike.Content.Core.Entities.Creatures.Enemies.Enemies_AI
{
    public class BrownZombieAI : EnemyAI
    {
        public BrownZombieAI(BrownZombie agent) : base(agent)
        {
        }
        public override Action DetermineAction()
        {
            if (agent.IsPlayerInTheSameRoom())
            {
                if (!agent.IsAttacking())
                {
                    /*if (!agent.WeaponInventory[1].InUsage())
                    {
                        // Check, ob Pfeil treffen würde
                        if (SimulateArrowAttack()) { 
                            agent.WeaponInventory[1].CooldownTimer = 0;
                            agent.CurrentWeapon = agent.WeaponInventory[1];
                            return new RangeAttack(agent);
                        }
                    }
                    else */
                    if (!agent.WeaponInventory[0].InUsage())
                    {
                        if (SimulateMeleeAttack())
                        {
                            // Wenn reagiert wird
                            
                            if(TryToAttack(agent.WeaponInventory[0]))
                                return new Melee(agent);
                            
                        }
                        // TODO: Verschieben
                        else
                        {
                            // Es wurde "zu spät reagiert" und der Player ist entkommen
                            ResetReactionTimer();
                        }
                    }
                }
                return new Move(agent);
            }
            else return new Wait(agent);

        }

        public override Vector2 DeterminePath()
        {
            throw new NotImplementedException();
        }

    }
}