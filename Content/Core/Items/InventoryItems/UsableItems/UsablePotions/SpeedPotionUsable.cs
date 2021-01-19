﻿using _2DRoguelike.Content.Core.Entities;
using _2DRoguelike.Content.Core.EntityEffects;
using System;
using System.Collections.Generic;
using System.Text;
using static _2DRoguelike.Content.Core.EntityEffects.EntityEffectsManager;

namespace _2DRoguelike.Content.Core.Items.InventoryItems.UsableItems.UsablePotions
{
    public class SpeedPotionUsable : UsablePotionsBase
    {
        public SpeedPotionUsable(Humanoid owner) : base(owner, EffectType.Speed)
        {
            icon = TextureManager.loot.SpeedPotion;
        }
        public override void ActivateItem()
        {
            base.ActivateItem();
            EntityEffectsManager.ActivateEffect(type, owner);
        }
    }
}