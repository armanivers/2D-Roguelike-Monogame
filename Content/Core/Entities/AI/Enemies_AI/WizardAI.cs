

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



        protected override Action GetAIDecision()
        {

            if (agent.IsPlayerInTheSameRoom())
            {


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
                    if (!agent.WeaponInventory[2].InUsage())
                    {
                        if (SimulateArrowAttack())
                        {
                            if (TryToAttack(agent.WeaponInventory[2]))
                                return new RangeAttack(agent);
                        }
                    }


                    /*
                    if (inDangerZone()) {
                        if (React()) { 
                            return NullReferenceException Teleport(agent);
                        }
                    }
                    */
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
