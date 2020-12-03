using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Weapons
{
    public abstract class Weapon : InventoryItem
    {
        private float weaponCooldown;
        protected float WeaponCooldown { get => weaponCooldown; set => weaponCooldown = value; }
        
        private float cooldownTimer=0;
        public float CooldownTimer { get => cooldownTimer; set => cooldownTimer = value; }

        
        public Weapon(float weaponCooldown) {
            WeaponCooldown = weaponCooldown;
        }

        public bool InUsage() { 
            return CooldownTimer <= WeaponCooldown;
        }
        public void UpdateCooldownTimer(float elapsedTime) {
            if (CooldownTimer <= WeaponCooldown)
                CooldownTimer += elapsedTime;
        }
       

    }
}
