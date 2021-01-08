

using System;
using _2DRoguelike.Content.Core.Entities.Actions;
using _2DRoguelike.Content.Core.Entities.Weapons;
using Microsoft.Xna.Framework;
using Action = _2DRoguelike.Content.Core.Entities.Actions.Action;

namespace _2DRoguelike.Content.Core.Entities.Creatures.Enemies.Enemies_AI
{
    public class WizardAI : EnemyAI
    {
        public WizardAI(Wizard agent) : base(agent)
        {
        }



        public override Action DetermineAction()
        {
            // TODO: 
            if (agent.IsPlayerInTheSameRoom())
            {
                /*
                 * if (inDangerZone()) {
                    if (React()) { 
                        return NullReferenceException Teleport(agent);
                    }
                }*/

                if (!agent.IsAttacking())
                {
                    if (!agent.WeaponInventory[1].InUsage())
                    {

                        // Check, ob Pfeil treffen würde
                        if (SimulateArrowAttack())
                        {
                            if (TryToAttack(agent.WeaponInventory[1]))
                                return new RangeAttack(agent);

                        }
                    }
                    else if (!agent.WeaponInventory[2].InUsage())
                    {
                        if (SimulateArrowAttack())
                        {
                            // TODO: SimulatedFireballAttack
                            if (TryToAttack(agent.WeaponInventory[2]))
                                return new RangeAttack(agent);
                        }
                    }
                    // else if (!agent.WeaponInventory[0].InUsage())
                    //{
                    //   if (React) { 
                    //    agent.WeaponInventory[0].CooldownTimer = 0;
                    //    agent.CurrentWeapon = agent.WeaponInventory[0];
                    //    return new Melee(agent);
                    //    }
                    //}
                    else
                    {
                        ResetReactionTimer();
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
