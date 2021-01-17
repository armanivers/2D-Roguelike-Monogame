using System;
using System.Collections.Generic;
using System.Text;
using _2DRoguelike.Content.Core.Entities.Actions;
using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.Entities.Creatures.Enemies;
using _2DRoguelike.Content.Core.Entities.Weapons;
using _2DRoguelike.Content.Core.Items.InventoryItems.Weapons;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.AI.Enemies_AI.Bosses_AI
{
    public class OrcAI : BossAI
    {
        public OrcAI(Enemy agent, int minReactionTime = DEFAULT_REACTION_TIME_MIN + 0, int maxReactionTime = DEFAULT_REACTION_TIME_MAX + 10) : base(agent, minReactionTime, maxReactionTime)
        {
        }

        protected override Entities.Actions.Action GetAIDecision()
        {
            if (agent.IsPlayerInTheSameRoom())
            {
                if (!agent.IsAttacking())
                {
                    if (!agent.inventory.WeaponInventory[0].InUsage())
                    {
                        if (SimulateMeleeAttack())
                        {
                            if (TryToAttack(agent.inventory.WeaponInventory[0]))
                                return new Melee(agent);
                        }
                    }


                }
                return new Move(agent);
            }
            else return new Wait(agent);
        }

        public override Vector2 DeterminePath()
        {
            Rectangle[] effectiveMeleeRange = ((ShortRange)agent.inventory.WeaponInventory[0]).GetEffectiveRange();
            foreach (Rectangle effective in effectiveMeleeRange)
            {
                if (effective.Intersects(Player.Instance.Hitbox))
                {
                    return Vector2.Zero;
                }
            }
            return Vector2.Normalize(agent.GetAttackDirection() - agent.HitboxCenter);
        }
    }
}