﻿using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.Entities.Inventories;
using _2DRoguelike.Content.Core.Entities.Loot;
using _2DRoguelike.Content.Core.Items.ObtainableItems;
using _2DRoguelike.Content.Core.UI;
using _2DRoguelike.Content.Core.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using static _2DRoguelike.Content.Core.UI.MessageFactory.Message;

namespace _2DRoguelike.Content.Core.Entities.Interactables.Loot.InventoryLoots.ObtainableLoots
{
    // abstrakte klasse ObtainableItemLoot erstellen, falls mehr items
    public class KeyLoot : LootBase
    {
        
        private bool obtained;



        public KeyLoot(Vector2 pos) : base(pos)
        {
            obtained = false;

            if (LevelManager.level == LevelManager.maxLevel - 1)
            {
                ScaleFactor = 2f; 
                texture = TextureManager.loot.KeyLootSpecial;
            }
            else
            {
                texture = TextureManager.loot.KeyLoot;
            }
            animations = new Dictionary<string, Animation>()
            {
                {"KeyLoot_Idle",new Animation(texture,0,4,0.2f,true,false,false,32) }
            };
            animationManager = new AnimationManager(this, animations["KeyLoot_Idle"]);
            animationManager.Position = pos;
            floatable = true;
        }
        public override void OnContact()
        {
            Player.Instance.Inventory.AddKey(new LevelKey(Player.Instance));
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
            MessageFactory.DisplayMessage("Key Obtained!", Color.Green, AnimationType.UpToDown);
        }
        private void PlaySound()
        {
            SoundManager.KeyPickup.Play(Game1.gameSettings.soundeffectsLevel, 0.3f, 0);
        }

        public bool InWall()
        {
            Rectangle hitbox = Hitbox;
            int levelWidth = LevelManager.currenttilemap.GetLength(0);
            int levelHeight = LevelManager.currenttilemap.GetLength(1);
            // Handling von NullPointer-Exception
            int northWest = hitbox.X < 0 ? 0 : hitbox.X / 32;
            int northEast = (hitbox.X + hitbox.Width) / 32 >= levelWidth ? levelWidth - 1 : (hitbox.X + hitbox.Width) / 32;
            int southWest = hitbox.Y < 0 ? 0 : hitbox.Y / 32;
            int southEast = (hitbox.Y + hitbox.Height) / 32 >= levelHeight ? levelHeight - 1 : (hitbox.Y + hitbox.Height) / 32;

            for (int x = northWest; x <= northEast; x++)
            {
                for (int y = southWest; y <= southEast; y++)
                {
                    if (!LevelManager.currenttilemap[x, y].IsSolid())
                        return false;
                }
            }
            return true;

        }

    }
}
