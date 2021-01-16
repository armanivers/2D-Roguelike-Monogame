

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
        protected override Action GetAIDecision()
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
                    return Vector2.Zero;
            }

            return Vector2.Normalize(agent.GetAttackDirection() - agent.Position);
        }

    }
}
