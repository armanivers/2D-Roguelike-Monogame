using _2DRoguelike.Content.Core.Entities.Loot;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Interactables.Loot.InventoryLoots.ObtainableLoots
{
    // abstrakte klasse ObtainableItemLoot erstellen, falls mehr items
    public class KeyLoot : LootBase
    {
        public KeyLoot(Vector2 pos) : base(pos)
        {
            floatable = true;
        }

        public override void OnContact() 
        {
            ControllingPlayer.Player.Instance.AddKey();
            PlaySound();
            NotifyPlayer();
        }

        private void NotifyPlayer()
        {
            // display message on screen / update ui
        }

        private void PlaySound()
        {

        }

    }
}
