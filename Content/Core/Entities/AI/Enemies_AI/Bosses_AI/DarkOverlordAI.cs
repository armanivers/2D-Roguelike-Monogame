using System;
using System.Collections.Generic;
using System.Text;
using _2DRoguelike.Content.Core.Entities.Actions;
using _2DRoguelike.Content.Core.Entities.AI.Actions;
using _2DRoguelike.Content.Core.Entities.Creatures.Enemies;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.AI.Enemies_AI.Bosses_AI
{
    public class DarkOverlordAI : BossAI
    {
        public DarkOverlordAI(Enemy agent, int minReactionTime = DEFAULT_REACTION_TIME_MIN + 10, int maxReactionTime = DEFAULT_REACTION_TIME_MAX + 10) : base(agent, minReactionTime, maxReactionTime)
        {
        }

        protected override Entities.Actions.Action GetAIDecision()
        {
            if (agent.IsPlayerInTheSameRoom())
            {
                if (!agent.IsAttacking())
                {
                    int weaponToUse = Game1.rand.Next(0, 2);



                    if (!agent.inventory.WeaponInventory[weaponToUse].InUsage())
                    {
                        // Fireball
                        if (SimulateArrowAttack())
                        {
                            // Versuche, FireBarrage zu erzeugen
                            if (ProjectileBarrage.CanSwitchToState(agent))
                            {
                                if (TryToAttack(agent.inventory.WeaponInventory[weaponToUse]))
                                {
                                    int rand = Game1.rand.Next(0, 2);

                                    if (rand == 1)
                                    {
                                        return new ProjectileBarrage(agent, (float)Gameplay.GameTime.TotalGameTime.TotalSeconds, weaponToUse == 0 ? 4 : 3, 
                                            weaponToUse == 0 ? ProjectileBarrage.DEFAULT_TIME_IN_STATE : ProjectileBarrage.DEFAULT_TIME_IN_STATE + 20);
                                    }
                                    else
                                    {
                                        agent.Mana = 0;
                                    }
                                }
                            }

                            // normaler Fireball-Attack
                            if (TryToAttack(agent.inventory.WeaponInventory[weaponToUse]))
                            {
                                return new RangeAttack(agent);
                            }
                        }
                        // kann nicht treffen: Teleport
                        else if (React())
                        {
                            return new Teleport(agent, (float)Gameplay.GameTime.TotalGameTime.TotalSeconds);
                        }
                    }

                    weaponToUse = 1 - weaponToUse;

                    if (!agent.inventory.WeaponInventory[weaponToUse].InUsage())
                    {
                        if (SimulateArrowAttack())
                        {
                            // Versuche, EnergyBallBarrage zu erzeugen
                            if (ProjectileBarrage.CanSwitchToState(agent))
                            {
                                if (TryToAttack(agent.inventory.WeaponInventory[weaponToUse]))
                                {
                                    int rand = Game1.rand.Next(0, 2);

                                    if (rand == 1)
                                    {
                                        return new ProjectileBarrage(agent, (float)Gameplay.GameTime.TotalGameTime.TotalSeconds, weaponToUse == 0 ? 4 : 3,
                                            weaponToUse == 0 ? ProjectileBarrage.DEFAULT_TIME_IN_STATE : ProjectileBarrage.DEFAULT_TIME_IN_STATE + 20);
                                    }
                                    else
                                    {
                                        agent.Mana = 0;
                                    }
                                }
                            }

                            // normaler Energyball-Attack
                            if (TryToAttack(agent.inventory.WeaponInventory[weaponToUse]))
                                return new RangeAttack(agent);
                        }
                        // kann nicht treffen: Teleport
                        else if (React())
                        {
                            return new Teleport(agent, (float)Gameplay.GameTime.TotalGameTime.TotalSeconds);
                        }
                    }
                    

                    const int VANISHING_RANGE = (int)(2.5f * 32);
                    if (WithinRange(VANISHING_RANGE))
                    {
                        if (React())
                        {
                            return new Teleport(agent, (float)Gameplay.GameTime.TotalGameTime.TotalSeconds);
                        }
                    }

                }
                return new Move(agent);
            }
            else return new Wait(agent);
        }


        public override Vector2 DeterminePath()
        {
            const int FLEEING_RANGE = (int)(2 * 32);
            if (WithinRange(FLEEING_RANGE))
            {
                return Vector2.Negate(Vector2.Normalize(agent.GetAttackDirection() - agent.HitboxCenter));
            }

            return Vector2.Zero;
        }
    }
}
