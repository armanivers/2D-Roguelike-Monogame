
using _2DRoguelike.Content.Core.Entities.Actions;
using _2DRoguelike.Content.Core.Entities.Creatures.Projectiles;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.Creatures.Enemies.Enemies_AI
{
    public abstract class EnemyAI
    {
        public Enemy agent;

        public EnemyAI(Enemy agent) {
            this.agent = agent;
        }

        public abstract Action DetermineAction();
        public abstract Vector2 DeterminePath();


        protected bool SimulateArrowAttack()
        {
            SimulatedArrow simulatedArrow = new SimulatedArrow(agent);
            return simulatedArrow.TestForImpact();
        }
    }
}
