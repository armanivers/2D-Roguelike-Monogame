﻿

using System;
using _2DRoguelike.Content.Core.Entities.Actions;
using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.Entities.Weapons;
using Microsoft.Xna.Framework;
using Action = _2DRoguelike.Content.Core.Entities.Actions.Action;

namespace _2DRoguelike.Content.Core.Entities.Creatures.Enemies.Enemies_AI
{
    public class GreenZombieAI : EnemyAI
    {
        public GreenZombieAI(GreenZombie agent) : base(agent)
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
                        if (SimulateArrowAttack())
                        {
                            agent.WeaponInventory[1].CooldownTimer = 0;
                            agent.CurrentWeapon = agent.WeaponInventory[1];
                            return new RangeAttack(agent);
                        }
                    }
                     */



                    if (!agent.WeaponInventory[0].InUsage())
                    {
                        if (SimulateMeleeAttack())
                        {
                            if (TryToAttack(agent.WeaponInventory[0]))
                                return new Melee(agent);
                        }
                        else
                        {
                            // es wurde "zu spät reagiert" und der Player ist entkommen → Reaktion wieder zurücksetzen
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
            Rectangle[] effectiveMeleeRange = ((ShortRange)agent.WeaponInventory[0]).GetEffectiveRange();
            foreach (Rectangle effective in effectiveMeleeRange)
            {
                if (effective.Intersects(Player.Instance.Hitbox))
                    return Vector2.Zero;
            }

            return Vector2.Normalize(agent.GetAttackDirection() - agent.Position);
        }

    }
}
