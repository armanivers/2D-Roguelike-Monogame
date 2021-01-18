using _2DRoguelike.Content.Core.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using static _2DRoguelike.Content.Core.EntityEffects.EntityEffectsManager;

namespace _2DRoguelike.Content.Core.EntityEffects.PotionEffects
{
    public class StrengthBoost : EntityEffectBase
    {
        private float strengthAmount = 0.6f;

        public StrengthBoost(Humanoid owner) : base(owner)
        {
            effectIcon = TextureManager.loot.StrengthEffectIcon;
            effectDuration = 20;
        }

        public override void UseEffect()
        {
            owner.temporaryDamageMultiplier = owner.DamageMultiplier+strengthAmount ;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (isExpired) owner.temporaryDamageMultiplier = owner.DamageMultiplier;
        }
    }
}
