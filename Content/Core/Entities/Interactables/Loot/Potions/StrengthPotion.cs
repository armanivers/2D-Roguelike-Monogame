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
        private double strenghtModifier;
        // how long should the potion heal the player (how long effect lasts)
        private const float POTION_DURATION = 15;
        // used to determine end of potion duration
        private float effectTimer;
        // whether potion effect activated
        private bool activated;
        public StrengthPotion(Vector2 pos) : base(pos)
        {
            strenghtModifier = 3;
            texture = TextureManager.loot.StrengthPotion;
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
            // set it every update, incase player levels up, and his damage multiplier increaes as result of level up, potion's multiplier should still be active
            Player.Instance.DamageMultiplier = strenghtModifier;
            //Debug.Print("DamageMul" + Player.Instance.DamageMultiplier);
            if (effectTimer >= POTION_DURATION)
            {
                Player.Instance.DamageMultiplier = Player.Instance.GetUnlockedDamageMultiplier();
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
