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
        protected override Action GetAIDecision()
        {
            if (agent.IsPlayerInTheSameRoom())
            {
                if (!agent.IsAttacking())
                {
                    if (!agent.WeaponInventory[0].InUsage())
                    {
                        if (SimulateMeleeAttack())
                        {
                            // Wenn reagiert wird
                            
                            if(TryToAttack(agent.WeaponInventory[0]))
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
            const int DETECTION_RANGE = 8 * 32;
            //if (WithinRange(DETECTION_RANGE))
            //    return Vector2.Normalize(agent.GetAttackDirection() - agent.Position);

            if (WithinRange(DETECTION_RANGE))
            {
                Rectangle[] effectiveMeleeRange = ((ShortRange)agent.WeaponInventory[0]).GetEffectiveRange();
                foreach (Rectangle effective in effectiveMeleeRange)
                {
                    if (effective.Intersects(Player.Instance.Hitbox))
                        return Vector2.Zero;
                }

                return Vector2.Normalize(agent.GetAttackDirection() - agent.Position);
            }
            return Vector2.Zero;
        }

    }
}
