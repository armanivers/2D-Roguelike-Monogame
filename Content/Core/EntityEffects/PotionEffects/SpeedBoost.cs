using _2DRoguelike.Content.Core.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using static _2DRoguelike.Content.Core.EntityEffects.EntityEffectsManager;

namespace _2DRoguelike.Content.Core.EntityEffects.PotionEffects
{
    public class SpeedBoost : EntityEffectBase
    {
        private float speedAmount = 1.35f;
        // sollte in player klasse wie bei strength stehen ( ein tempSpeedAttribut)
        private float defaultEntitySpeed = 1f;
       
        public SpeedBoost(Humanoid owner) : base(owner)
        {
            effectIcon = TextureManager.loot.SpeedEffectIcon;
            effectDuration = 35;
        }

        public override void UseEffect()
        {
            owner.SpeedModifier = speedAmount;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (isExpired) owner.SpeedModifier = defaultEntitySpeed;
        }
    }
}
