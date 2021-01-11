using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using _2DRoguelike.Content.Core.Entities.Actions;
using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.Entities.Creatures.Enemies;
using _2DRoguelike.Content.Core.Entities.Weapons;
using _2DRoguelike.Content.Core.World;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.AI.Enemies_AI.Bosses_AI
{
    public class DragonAI : BossAI
    {
        public DragonAI(Enemy agent, int minReactionTime = DEFAULT_REACTION_TIME_MIN + 10, int maxReactionTime = DEFAULT_REACTION_TIME_MAX + 10) : base(agent, minReactionTime, maxReactionTime)
        {
        }

        protected override Actions.Action GetAIDecision()
        {
            if (agent.IsPlayerInTheSameRoom())
            {
                if (!agent.IsAttacking())
                {
                    if (!agent.WeaponInventory[0].InUsage())
                    {
                        if (SimulateMeleeAttack())
                        {
                            if (TryToAttack(agent.WeaponInventory[0]))
                                return new Melee(agent);
                        }
                    }

                    if (!agent.WeaponInventory[1].InUsage())
                    {
                        if (SimulateArrowAttack())
                        {
                            if (TryToAttack(agent.WeaponInventory[1]))
                                return new RangeAttack(agent);
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
                {
                    return Vector2.Zero;
                }
            }
            return Vector2.Normalize(agent.GetAttackDirection() - agent.Position);
        }
    }
}
