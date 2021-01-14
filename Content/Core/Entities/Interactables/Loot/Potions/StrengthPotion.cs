using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.Entities.Loot;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Loot.Potions
{
    public class StrengthPotion : Potion
    {
        private float strenghtModifier;
        // how long should the potion heal the player (how long effect lasts)
        private const float POTION_DURATION = 8;
        // used to determine end of potion duration
        private float effectTimer;
        // whether potion effect activated
        private bool activated;
        public StrengthPotion(Vector2 pos) : base(pos)
        {
            strenghtModifier = 1.5f;
            texture = TextureManager.loot.HealthPotion;
            effectTimer = 0;
            activated = false;
        }

        public override void ActivateEffect()
        {
            PlaySound();
            transparency = 0;
            activated = true;
        }

        public void BoostPlayerSpeed()
        {
            // kein damage modifier?
            if (effectTimer == 0) //Player.Instance. = strenghtModifier;
            if (effectTimer >= POTION_DURATION)
            {
                Player.Instance.SpeedModifier = 1.0f;
                isExpired = true;
            }
            effectTimer += 0.1f;
        }

        public override void OnContact()
        {
            if (!activated) ActivateEffect();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (activated) BoostPlayerSpeed();
        }

        public override void PlaySound()
        {
            SoundManager.PotionDrink.Play(Game1.gameSettings.soundeffectsLevel, 0, 0);
        }
    }
}
