

using System;
using System.Diagnostics;
using _2DRoguelike.Content.Core.Entities.Actions;
using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using Microsoft.Xna.Framework;
using Action = _2DRoguelike.Content.Core.Entities.Actions.Action;

namespace _2DRoguelike.Content.Core.Entities.Creatures.Enemies.Enemies_AI
{
    public class SkeletonAI : EnemyAI
    {
        int ZIELRICHTUNG = 0; // 0 = NE
        public SkeletonAI(Skeleton agent) : base(agent)
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

                }
                return new Move(agent);
            }
            else
            {
                return new Wait(agent);
            }


        }

        public override Vector2 DeterminePath()
        {
            // TODO: Der schmale Grad zwischen Fliehen und Suchen ist buggy
            // Idee: Einen zwischenraum zwischen suchen und fliehen, in dem so gegangen wird, dass der Abstand sich nicht mehr erhöht
            // (Sprich, es wird in einem Kreis um den Player herum gegangen)

            const int FLEEING_RANGE = 4 * 32;
            if (WithinRange(FLEEING_RANGE))
            {

                return Vector2.Negate(Vector2.Normalize(agent.GetAttackDirection() - agent.HitboxCenter));
            }


            else if (//!WithinRange(FLEEING_RANGE + 16) &&  // diese Zeile, wenn der Skeleton warten soll vor der Gefahrenzone
                !SimulateArrowAttack())
            {
                Vector2 ret = agent.GetAttackDirection() - agent.HitboxCenter;

                // Wenn  nicht direkt im Umkreis, aber kurz davor: Um den Player herum im Kreis laufen
                if (WithinRange(FLEEING_RANGE + 32))
                {
                    // TODO: Hier den Vektor so modifizieren, dass er den minimalen Abstand nicht unterschreitet

                    #region Idee mit Quadrat-Bewegung
                    /*
                    var destination = nextPosition(FLEEING_RANGE);

                    return Vector2.Normalize(destination - agent.HitboxCenter);*/
                    #endregion


                }
                return Vector2.Normalize(ret);
            }

            return Vector2.Zero;
        }

        // Alternativ
        public Vector2 nextPosition(int FLEEING_RANGE)
        {
            Vector2[] destinations = new Vector2[]
            {
                new Vector2(Player.Instance.HitboxCenter.X + FLEEING_RANGE, Player.Instance.HitboxCenter.Y - FLEEING_RANGE),
                 new Vector2(Player.Instance.HitboxCenter.X + FLEEING_RANGE, Player.Instance.HitboxCenter.Y + FLEEING_RANGE),
                new Vector2(Player.Instance.HitboxCenter.X - FLEEING_RANGE, Player.Instance.HitboxCenter.Y + FLEEING_RANGE),
                 new Vector2(Player.Instance.HitboxCenter.X - FLEEING_RANGE, Player.Instance.HitboxCenter.Y - FLEEING_RANGE)
            };

            foreach (Vector2 direction in destinations)
            {
                if (Vector2.Distance(agent.HitboxCenter, direction) <= 50)
                {
                    ZIELRICHTUNG++;
                    if (ZIELRICHTUNG > 3)
                        ZIELRICHTUNG = 0;
                    break;
                }
            }
            return destinations[ZIELRICHTUNG];
        }
    }
}
