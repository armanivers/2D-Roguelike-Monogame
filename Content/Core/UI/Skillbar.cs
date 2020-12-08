﻿using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.Entities.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2DRoguelike.Content.Core.UI
{
    class Skillbar
    {
        private Player target;

        private Texture2D skillbarTexture;
        private Vector2 skillbarPosition;

        public Texture2D usedSlotTexture;
        private Vector2 usedSlotPosition;
        private float slotFullHeight;
        private float slotCurrentHeight;

        private float scalingFactor;

        private float xOffset;
        private float yOffset;

        // TODO: add item not unlocked Texture(red cross) + item selected texutre (frame/bubble)

        //current selected weapon
        private int currentWeapon;

        public List<WeaponCooldownData> weaponData;

        public Skillbar(Player player)
        {
            target = player;
            scalingFactor = 2.2f;
            skillbarTexture = TextureManager.skillbar;
            skillbarPosition = new Vector2(GameSettings.screenWidth / 2 - skillbarTexture.Width * scalingFactor / 2, GameSettings.screenHeight-skillbarTexture.Height * scalingFactor - 30);
            usedSlotTexture = TextureManager.slotUsed;
            xOffset = 11;
            yOffset = 16;
            usedSlotPosition = new Vector2(skillbarPosition.X+xOffset,skillbarPosition.Y+yOffset);
            slotFullHeight = usedSlotTexture.Height;
            slotCurrentHeight = slotFullHeight;

            weaponData = new List<WeaponCooldownData>();
        }

        public struct WeaponCooldownData
        {
            public Boolean unlocked { get; }
            public float maxCooldown { get; }
            public float currentCooldown { get; }
            public float weaponSlotHeight { get; }
            public WeaponCooldownData(Boolean unlocked, float maxCooldown, float currentCooldown)
            {
                this.unlocked = unlocked;
                this.maxCooldown = maxCooldown;
                this.currentCooldown = currentCooldown;
                if (unlocked)
                {
                    var timerPercentage = currentCooldown / maxCooldown;
                    weaponSlotHeight = timerPercentage * 18; //textureheight = 18 stattdessen usedSlotTexture.Height!!!! sollten keine hardgecodede werte sein
                }
                else
                {
                    weaponSlotHeight = 0;
                }

            }
        }

        public void Update(GameTime gameTime)
        {
            currentWeapon = target.CurrentWeaponPos;
            weaponData.Clear();
            foreach(var weapon in target.WeaponInventory)
            {
                if (weapon != null)
                {
                    weaponData.Add(new WeaponCooldownData(true, weapon.WeaponCooldown, weapon.CooldownTimer));
                }
                else
                {
                    weaponData.Add(new WeaponCooldownData(false, -1, -1));
                }
            }

            // 100% = usedSlotTexture.Height
            // 50% = usedSlotTexture.Height*50/100;
            //maxtimer = 3 sekunden, current timer = 3 = 100%
            //max timer = 3 sekunden, current timer = 1.5 = 50%
            //berechne wie viel % timer ist gelaufen
            //var timerPercentage = weapon1CurrentTimer / weapon1TimerMax;
            //Debug.Print("height = " + usedSlotTexture.Height + " percetage = " + timerPercentage);
            //slotCurrentHeight = timerPercentage * usedSlotTexture.Height; //height = 18
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //weaponsbar
            spriteBatch.Draw(skillbarTexture, skillbarPosition, null, Color.White, 0, Vector2.Zero, scalingFactor, SpriteEffects.None, 0);

            //selected weapon icon (TODO: little bubble on top maybe or frame around weapon)
            spriteBatch.Draw(usedSlotTexture, new Vector2(usedSlotPosition.X+(50*currentWeapon), usedSlotPosition.Y-30), new Rectangle((int)usedSlotPosition.Y, (int)usedSlotPosition.X, usedSlotTexture.Width, (int)10), Color.Blue, 0, Vector2.Zero, scalingFactor, SpriteEffects.None, 0);
            
            //cooldowns of weapons

            for(int i = 0; i < weaponData.Count; i++)
            {
                if (weaponData[i].unlocked)
                {
                    spriteBatch.Draw(usedSlotTexture, new Vector2(usedSlotPosition.X + (50 * i), usedSlotPosition.Y), new Rectangle((int)usedSlotPosition.Y, (int)usedSlotPosition.X, usedSlotTexture.Width, (int)weaponData[i].weaponSlotHeight), Color.White * 0.5f, 0, Vector2.Zero, scalingFactor, SpriteEffects.None, 0);
                }
                //else ein X zeichnen 
            }
        }

    }
}
