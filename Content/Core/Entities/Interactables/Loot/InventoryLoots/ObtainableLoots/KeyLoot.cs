﻿using _2DRoguelike.Content.Core.Entities.Loot;
using _2DRoguelike.Content.Core.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
namespace _2DRoguelike.Content.Core.Entities.Interactables.Loot.InventoryLoots.ObtainableLoots
{
    // abstrakte klasse ObtainableItemLoot erstellen, falls mehr items
    public class KeyLoot : LootBase
    {
        Texture2D KeyLootIdleAnimation;
        private bool obtained;
        protected float timeToOpen; // 1.2 = 2 Sekunden
        private float fadingSpeed; //0.00833f;
        protected float openingTimer;
        public KeyLoot(Vector2 pos) : base(pos)
        {
            obtained = false;
            KeyLootIdleAnimation = TextureManager.loot.KeyLoot;
            animations = new Dictionary<string, Animation>()
            {
                {"KeyLoot_Idle",new Animation(KeyLootIdleAnimation,0,4,0.2f,true,false,false,32) }
            };
            animationManager = new AnimationManager(this, animations["KeyLoot_Idle"]);
            animationManager.Position = pos;
            floatable = true;
        }
        public override void OnContact()
        {
            ControllingPlayer.Player.Instance.AddKey();
            PlaySound();
            NotifyPlayer();
            obtained = true;
        }
        public override void Update(GameTime gameTime)
        {
            SetAnimation("KeyLoot_Idle");
            animationManager?.Update(gameTime);
            base.Update(gameTime);
            if (obtained)
            {
              isExpired = true;
            }

        }
        private void NotifyPlayer()
        {
            // display message on screen / update ui
            MessageFactory.DisplayMessage("Key Obtained!", Color.Green);
        }
        private void PlaySound()
        {
            SoundManager.KeyCollect.Play(Game1.gameSettings.soundeffectsLevel, 0, 0);
        }

    }
}
