using _2DRoguelike.Content.Core.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using static _2DRoguelike.Content.Core.EntityEffects.EntityEffectsManager;

namespace _2DRoguelike.Content.Core.EntityEffects.PotionEffects
{
    public class HealthRegeneration : EntityEffectBase
    {
        private int regenerationAmount = 5;

        // how many seconds between each heal (duration between each heal)
        private const float SINGLE_REGENERATION_DURATION = 2;
        private float regenerationTimer = 0;

        public HealthRegeneration(Humanoid owner) : base(owner)
        {
            effectDuration = 8;
        }

        public override void UseEffect()
        {
            if (regenerationTimer <= SINGLE_REGENERATION_DURATION)   
            {
                regenerationTimer += 0.1f;
            }
            else   
            {
                owner.AddHealthPoints(regenerationAmount);
                regenerationTimer = 0;
            }       
        }
    }
}
