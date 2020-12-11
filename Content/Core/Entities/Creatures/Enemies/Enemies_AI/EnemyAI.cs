
using System;
using _2DRoguelike.Content.Core.Entities.Actions;
using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.Entities.Creatures.Projectiles;
using _2DRoguelike.Content.Core.Entities.Weapons;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.Creatures.Enemies.Enemies_AI
{
    public abstract class EnemyAI
    {
        public Enemy agent;

        public EnemyAI(Enemy agent) {
            this.agent = agent;
        }

        public abstract Actions.Action DetermineAction();
        public abstract Vector2 DeterminePath();


        protected bool SimulateArrowAttack()
        {
            SimulatedArrow simulatedArrow = new SimulatedArrow(agent);
            return simulatedArrow.TestForImpact();
        }

        protected bool SimulateMeleeAttack() {
            Rectangle effectiveRange = ((ShortRange)agent.WeaponInventory[0]).GetEffectiveRange();
            // Für Debug
            agent.AttackRangeHitbox = effectiveRange;

            return (effectiveRange.Intersects(Player.Instance.Hitbox));
        }

        protected bool WithinRange(int radius) {
            return (Vector2.DistanceSquared(agent.HitboxCenter, Player.Instance.HitboxCenter) <= Math.Pow(radius, 2));
           
            //Rectangle surroundingRectangle = new Rectangle((int)(agent.HitboxCenter.X - radius), (int)(agent.HitboxCenter.Y - radius), radius*2, radius*2);
           // if (surroundingRectangle.Contains(Player.Instance.Hitbox))
           //     return true;
           // return false;
        }
    }
}
