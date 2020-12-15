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
                        agent.WeaponInventory[0].CooldownTimer = 0;
                        agent.CurrentWeapon = agent.WeaponInventory[0];
                        return new Melee(agent);
                    }
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