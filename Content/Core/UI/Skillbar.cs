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

        private float scalingFactor = 2.7f;

        // transparent/unused white space in the texutre itself
        private float skillbarWhitespaceHeight;
        private float skillbarWhitespaceWidth;

        // distance between skillbar and lower screen end
        private float skillbarYOffset = 100;

        //current selected weapon
        private int currentWeapon;

        public List<WeaponCooldownData> weaponData;

        public Skillbar(Player player)
        {
            target = player;

            skillbarWhitespaceWidth = 29 * scalingFactor;
            skillbarWhitespaceHeight = 23 * scalingFactor;
            itemFrameWidth = 24 * scalingFactor;

            skillbarTexture = TextureManager.ui.skillbar;
            redCrossSlotTexture = TextureManager.ui.LockedWeapon;
            usedSlotTexture = TextureManager.ui.slotUsed;
            selectedItemFrame = TextureManager.ui.selectedItemFame;

            skillbarPosition = new Vector2(Game1.gameSettings.screenWidth / 2 - (skillbarTexture.Width* scalingFactor - skillbarWhitespaceWidth) / 2, Game1.gameSettings.screenHeight-(skillbarTexture.Height*scalingFactor - skillbarWhitespaceHeight)-skillbarYOffset);

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
                    weaponSlotHeight = timerPercentage*16; //textureheight = 18 stattdessen usedSlotTexture.Height!!!! sollten keine hardgecodede werte sein
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
            spriteBatch.Draw(selectedItemFrame, new Vector2(skillbarPosition.X+1+(currentWeapon * itemFrameWidth), skillbarPosition.Y + skillbarWhitespaceHeight), null,Color.White, 0, Vector2.Zero, scalingFactor, SpriteEffects.None, 0);
            
            //cooldowns of weapons

            for(int i = 0; i < weaponData.Count; i++) 
            {
                if (weaponData[i].unlocked)
                {
                    spriteBatch.Draw(usedSlotTexture, new Vector2(skillbarPosition.X+1 + (itemFrameWidth * i), skillbarPosition.Y + skillbarWhitespaceHeight+1),
                        new Rectangle(0, 0, usedSlotTexture.Width, (int)weaponData[i].weaponSlotHeight+3),
                        Color.White * 0.5f, 0, Vector2.Zero, scalingFactor, SpriteEffects.None, 0);
                }
                else
                {
                    spriteBatch.Draw(redCrossSlotTexture, new Vector2(skillbarPosition.X + (itemFrameWidth * i), skillbarPosition.Y+ skillbarWhitespaceHeight ), null, Color.White*0.8F, 0, Vector2.Zero, scalingFactor, SpriteEffects.None, 0);
                }
            }
        }

        public override void ForceResolutionUpdate()
        {
            skillbarPosition = new Vector2(Game1.gameSettings.screenWidth / 2 - (skillbarTexture.Width * scalingFactor - skillbarWhitespaceWidth) / 2, Game1.gameSettings.screenHeight - (skillbarTexture.Height * scalingFactor - skillbarWhitespaceHeight) - skillbarYOffset);
        }

    }
}
