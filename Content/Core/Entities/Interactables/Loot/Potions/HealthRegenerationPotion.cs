using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.Entities.Loot;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Loot.Potions
{
    public class HealthRegenerationPotion : Potion
    {
        private int healthModifier;
        // how long should the potion heal the player (how long effect lasts)
        private const float POTION_DURATION = 8;
        // how many seconds between each heal (duration between each heal)
        private const float SINGLE_REGENERATION_DURATION = 2;
        private float regenerationTimer;
        // used to determine end of potion duration
        private float effectTimer;
        // whether potion effect activated
        private bool activated;

        public HealthRegenerationPotion(Vector2 pos) : base(pos)
        {
            healthModifier = 5;
            texture = TextureManager.loot.HealthPotion;
            effectTimer = 0;
            regenerationTimer = 0;
            activated = false;
        }

        public override void ActivateEffect()
        {
            PlaySound();
            transparency = 0;
            activated = true;
        }

        public void RegenerateHealth()
        {
            if(effectTimer <= POTION_DURATION)
            {
                if (regenerationTimer <= SINGLE_REGENERATION_DURATION)
                {
                    regenerationTimer += 0.1f;
                }
                else
                {
                    Player.Instance.AddHealthPoints(healthModifier);
                    regenerationTimer = 0;
                }
                effectTimer += 0.1f;
            }
            else
            {
                isExpired = true;
            }
        }

        public override void OnContact()
        {
            if(!activated) ActivateEffect();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (activated) RegenerateHealth();
        }

        public override void PlaySound()
        {
            SoundManager.PotionDrink.Play(Game1.gameSettings.soundeffectsLevel, 0, 0);
        }
    }
}
