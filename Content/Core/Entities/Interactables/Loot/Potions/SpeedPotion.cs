using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.Entities.Loot;
using _2DRoguelike.Content.Core.Items.InventoryItems.UsableItems.UsablePotions;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Loot.Potions
{
    public class SpeedPotion : Potion
    {
        public SpeedPotion(Vector2 pos) : base(pos)
        {
            texture = TextureManager.loot.SpeedPotion;
        }

        public override void ActivateEffect()
        {
            PlaySound();
            Player.Instance.Inventory.AddUsableItemToInventory(new SpeedPotionUsable(Player.Instance));
        }

        public override void OnContact()
        {
            ActivateEffect();
            isExpired = true;
        }

        public override void PlaySound()
        {

        }
    }
}
