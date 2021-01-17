using _2DRoguelike.Content.Core.Entities;
using _2DRoguelike.Content.Core.EntityEffects;
using System;
using System.Collections.Generic;
using System.Text;
using static _2DRoguelike.Content.Core.EntityEffects.EntityEffectsManager;

namespace _2DRoguelike.Content.Core.Items.InventoryItems.UsableItems.UsablePotions
{
    public class RegenerationPotionUsable : UsablePotionsBase
    {
        public RegenerationPotionUsable(Humanoid owner) : base(owner,EffectType.HealthRegeneration)
        {
            icon = TextureManager.loot.HealthPotion;
        }
        public override void ActivateItem()
        {
            EntityEffectsManager.ActivateEffect(type,owner);
        }
    }
}
