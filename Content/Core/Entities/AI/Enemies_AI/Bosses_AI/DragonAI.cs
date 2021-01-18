using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using _2DRoguelike.Content.Core.Entities.Actions;
using _2DRoguelike.Content.Core.Entities.AI.Actions;
using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.Entities.Creatures.Enemies;
using _2DRoguelike.Content.Core.Entities.Weapons;
using _2DRoguelike.Content.Core.Items.InventoryItems.Weapons;
using _2DRoguelike.Content.Core.World;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.AI.Enemies_AI.Bosses_AI
{
    public class DragonAI : BossAI
    {
        public DragonAI(Enemy agent, int minReactionTime = DEFAULT_REACTION_TIME_MIN + 10, int maxReactionTime = DEFAULT_REACTION_TIME_MAX + 10) : base(agent, minReactionTime, maxReactionTime)
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


                    if (!agent.inventory.WeaponInventory[1].InUsage())
                    {
                        if (ProjectileBarrage.CanSwitchToState(agent))
                        {
                            int rand = Game1.rand.Next(0, 2);
                            if (TryToAttack(agent.inventory.WeaponInventory[1]))
                            {

                                if (rand == 1)
                                {
                                    return new ProjectileBarrage(agent, (float)Gameplay.GameTime.TotalGameTime.TotalSeconds, 4);
                                }
                                else
                                {
                                    agent.Mana = 0;
                                }
                            }
                        }

                        if (SimulateArrowAttack())
                        {
                            if (TryToAttack(agent.inventory.WeaponInventory[1]))
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
            Rectangle[] effectiveMeleeRange = ((ShortRange)agent.inventory.WeaponInventory[0]).GetEffectiveRange();
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
