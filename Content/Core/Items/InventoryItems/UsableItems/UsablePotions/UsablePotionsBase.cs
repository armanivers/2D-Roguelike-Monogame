using _2DRoguelike.Content.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static _2DRoguelike.Content.Core.EntityEffects.EntityEffectsManager;

namespace _2DRoguelike.Content.Core.Items.InventoryItems.UsableItems.UsablePotions
{
    public abstract class UsablePotionsBase : UsableItem
    {
        protected EffectType type;
        public UsablePotionsBase(Humanoid owner, EffectType type) : base(owner)
        {
            this.type = type;
        }

        public override void ActivateItem()
        {
            base.ActivateItem();
            SoundManager.PotionDrink.Play(Game1.gameSettings.soundeffectsLevel, 0, 0);
        }
    }
}
