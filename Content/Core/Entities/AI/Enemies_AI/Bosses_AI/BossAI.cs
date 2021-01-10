using System;
using System.Collections.Generic;
using System.Text;
using _2DRoguelike.Content.Core.Entities.Creatures.Enemies;
using _2DRoguelike.Content.Core.Entities.Creatures.Enemies.Enemies_AI;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.AI.Enemies_AI.Bosses_AI
{
    public abstract class BossAI : EnemyAI
    {
        public BossAI(Enemy agent, int minReactionTime = DEFAULT_REACTION_TIME_MIN, int maxReactionTime = DEFAULT_REACTION_TIME_MAX) : base(agent, minReactionTime, maxReactionTime)
        {
        }

    }
}
