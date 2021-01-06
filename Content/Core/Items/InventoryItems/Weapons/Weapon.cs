using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Weapons
{
    public abstract class Weapon : InventoryItem
    {
        public int weaponDamage;
        private float weaponCooldown;
        public float WeaponCooldown { get => weaponCooldown; set => weaponCooldown = value; }
        
        private float cooldownTimer=0;
        public float CooldownTimer { get => cooldownTimer; set => cooldownTimer = value; }

        public int INVENTORY_SLOT;

        public Weapon(Humanoid Owner, int weaponDamage, float weaponCooldown): base(Owner) {
            this.weaponDamage = weaponDamage;
            WeaponCooldown = weaponCooldown;
        }

        public abstract void UseWeapon();

        public abstract string GetAnimationType();
        public bool InUsage() { 
            return CooldownTimer <= WeaponCooldown;
        }
        public void UpdateCooldownTimer(float elapsedTime) {
            if (CooldownTimer <= WeaponCooldown)
                CooldownTimer += elapsedTime;
        }
       

    }
}
