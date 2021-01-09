
using System;
using _2DRoguelike.Content.Core.Entities.Actions;
using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.Entities.Creatures.Projectiles;
using _2DRoguelike.Content.Core.Entities.Weapons;
using _2DRoguelike.Content.Core.GameDebugger;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.Creatures.Enemies.Enemies_AI
{
    public abstract class EnemyAI
    {
        public Enemy agent;
        public const int DEFAULT_REACTION_TIME_MIN = 10;//16;
        public const int DEFAULT_REACTION_TIME_MAX = 30;
        public int[] reactionTimeInterval;
        public int currentReactionTimeGap;

        private int reactionTimer;
        public int ReactionTimer { get => reactionTimer; set => reactionTimer = value; }

        // TODO: Reaktionszeit für Angriff. Random [15-60]
        // Wenn currentRoom != Room des Enemies: Idle

        public EnemyAI(Enemy agent, int minReactionTime = DEFAULT_REACTION_TIME_MIN, int maxReactionTime = DEFAULT_REACTION_TIME_MAX)
        {
            this.agent = agent;
            reactionTimeInterval = new int[2] { minReactionTime, maxReactionTime };
            this.ReactionTimer = currentReactionTimeGap = new Random().Next(reactionTimeInterval[0], reactionTimeInterval[1]);
        }


        public abstract Actions.Action DetermineAction();
        public abstract Vector2 DeterminePath();


        protected bool SimulateArrowAttack()
        {
            SimulatedArrow simulatedArrow = new SimulatedArrow(agent);
            return simulatedArrow.TestForImpact();
        }

        protected bool SimulateMeleeAttack()
        {
            // TODO: Weapon als Parameter übergeben

            Rectangle[] effectiveRange = ((ShortRange)agent.WeaponInventory[0]).GetEffectiveRange();


            // Für Debug
            foreach (Rectangle box in effectiveRange)
            {
                GameDebug.AddToBoxDebugBuffer(box, Color.Violet);
            }

            foreach (Rectangle box in effectiveRange)
                if (box.Intersects(Player.Instance.Hitbox))
                    return true;
            return false;
        }

        protected bool WithinRange(int radius)
        {
            return (Vector2.DistanceSquared(agent.HitboxCenter, Player.Instance.HitboxCenter) <= Math.Pow(radius, 2));

            //Rectangle surroundingRectangle = new Rectangle((int)(agent.HitboxCenter.X - radius), (int)(agent.HitboxCenter.Y - radius), radius*2, radius*2);
            // if (surroundingRectangle.Contains(Player.Instance.Hitbox))
            //     return true;
            // return false;
        }


        public bool React()
        {
            if (reactionTimer > 0)
            {
                reactionTimer--;
                return false;
            }
            else
            {
                reactionTimer = currentReactionTimeGap = new Random().Next(reactionTimeInterval[0], reactionTimeInterval[1]);
                return true;
            }
        }

        public void ResetReactionTimer()
        {
            reactionTimer = currentReactionTimeGap;
        }

        public void SetReactionTimeInterval(int min, int max)
        {
            reactionTimeInterval[0] = min;
            reactionTimeInterval[1] = max;
        }

        public bool TryToAttack(Weapon usedWeapon)
        {
            if (React())
            {
                usedWeapon.CooldownTimer = 0;
                agent.CurrentWeapon = usedWeapon;
                return true;
            }
            return false;

        }
    }
}
