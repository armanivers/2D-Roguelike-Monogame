using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.Entities.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2DRoguelike.Content.Core.UI
{
    class Skillbar : UIElementBasis
    {
        private Player target;

        private Texture2D skillbarTexture;
        private Vector2 skillbarPosition;

        private Texture2D redCrossSlotTexture;
        private Texture2D selectedItemFrame;

        private float itemFrameWidth;

        public Texture2D usedSlotTexture;
        private Vector2 usedSlotPosition;

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
            
            skillbarTexture = TextureManager.ui.skillbar;
            skillbarPosition = new Vector2(Game1.gameSettings.screenWidth / 2 - skillbarTexture.Width * scalingFactor / 2, Game1.gameSettings.screenHeight-skillbarTexture.Height * scalingFactor - 30);
            
            redCrossSlotTexture = TextureManager.ui.LockedWeapon;
            usedSlotTexture = TextureManager.ui.slotUsed;
            selectedItemFrame = TextureManager.ui.selectedItemFame;
            
            xOffset = 12;
            yOffset = 16;
            itemFrameWidth = 50;
            
            usedSlotPosition = new Vector2(skillbarPosition.X+xOffset,skillbarPosition.Y+yOffset);

            weaponData = new List<WeaponCooldownData>();
        }

        public struct WeaponCooldownData
        {
            public bool unlocked { get; }
            public float maxCooldown { get; }
            public float currentCooldown { get; }
            public float weaponSlotHeight { get; }
            public WeaponCooldownData(bool unlocked, float maxCooldown, float currentCooldown)
            {
                this.unlocked = unlocked;
                this.maxCooldown = maxCooldown;
                this.currentCooldown = currentCooldown;
                if (unlocked)
                {
                    var timerPercentage = 1 - currentCooldown / maxCooldown ; // (1 -) used to revert the red effect, means red highlight only when weapon in cooldown
                    weaponSlotHeight = timerPercentage * 18; //textureheight = 18 stattdessen usedSlotTexture.Height!!!! sollten keine hardgecodede werte sein
                }
                else
                {
                    weaponSlotHeight = 0;
                }

            }
        }

        public override void Update(GameTime gameTime)
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

        public override void Draw(SpriteBatch spriteBatch)
        {
            //weaponsbar
            spriteBatch.Draw(skillbarTexture, skillbarPosition, null, Color.White, 0, Vector2.Zero, scalingFactor, SpriteEffects.None, 0);

            //selected weapon icon (TODO: little bubble on top maybe or frame around weapon)
            spriteBatch.Draw(selectedItemFrame, new Vector2(usedSlotPosition.X-10+(currentWeapon* itemFrameWidth), usedSlotPosition.Y-16), null,Color.White, 0, Vector2.Zero, scalingFactor, SpriteEffects.None, 0);
            
            //cooldowns of weapons

            for(int i = 0; i < weaponData.Count; i++)
            {
                if (weaponData[i].unlocked)
                {
                    spriteBatch.Draw(usedSlotTexture, new Vector2(usedSlotPosition.X + (itemFrameWidth * i), usedSlotPosition.Y), new Rectangle((int)usedSlotPosition.Y, (int)usedSlotPosition.X, usedSlotTexture.Width, (int)weaponData[i].weaponSlotHeight), Color.White * 0.5f, 0, Vector2.Zero, scalingFactor, SpriteEffects.None, 0);
                }
                else
                {
                    spriteBatch.Draw(redCrossSlotTexture, new Vector2(usedSlotPosition.X + (itemFrameWidth * i), usedSlotPosition.Y), null, Color.White*0.8F, 0, Vector2.Zero, scalingFactor, SpriteEffects.None, 0);
                }
            }
        }

        public override void ForceResolutionUpdate()
        {
            skillbarPosition = new Vector2(Game1.gameSettings.screenWidth / 2 - skillbarTexture.Width * scalingFactor / 2, Game1.gameSettings.screenHeight - skillbarTexture.Height * scalingFactor - 30);
            usedSlotPosition = new Vector2(skillbarPosition.X + xOffset, skillbarPosition.Y + yOffset);
        }

    }
}
