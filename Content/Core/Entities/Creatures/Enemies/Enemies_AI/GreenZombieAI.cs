

using System;
using _2DRoguelike.Content.Core.Entities.Actions;
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
            const int DETECTION_RANGE = 8 * 32;
            if (WithinRange(DETECTION_RANGE))
                return Vector2.Normalize(agent.GetAttackDirection() - agent.Position);
            return Vector2.Zero;
        }

    }
}
