using System;
using System.Collections.Generic;
using System.Text;
using _2DRoguelike.Content.Core.Entities.Actions;
using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.Entities.Creatures.Projectiles;
using _2DRoguelike.Content.Core.Entities.Weapons;
using Microsoft.Xna.Framework;
using Action = _2DRoguelike.Content.Core.Entities.Actions.Action;

namespace _2DRoguelike.Content.Core.Entities.Creatures.Enemies.Enemies_AI
{
    public class BrownZombieAI : EnemyAI
    {
        public BrownZombieAI(BrownZombie agent) : base(agent, (int)(DEFAULT_REACTION_TIME_MIN*1.5), (int)(DEFAULT_REACTION_TIME_MAX * 1.5))
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
            const int DETECTION_RANGE = 8 * 32;
            //if (WithinRange(DETECTION_RANGE))
            //    return Vector2.Normalize(agent.GetAttackDirection() - agent.Position);

            if (WithinRange(DETECTION_RANGE))
            {
                Rectangle effectiveMeleeRange = ((ShortRange)agent.WeaponInventory[0]).GetEffectiveRange();
                if (!effectiveMeleeRange.Intersects(Player.Instance.Hitbox))
                    return Vector2.Normalize(agent.GetAttackDirection() - agent.Position);
            }
            return Vector2.Zero;
        }

    }
}
