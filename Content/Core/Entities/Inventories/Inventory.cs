using _2DRoguelike.Content.Core.Entities.Creatures.Enemies;
using _2DRoguelike.Content.Core.Entities.Weapons;
using _2DRoguelike.Content.Core.Items.InventoryItems.Weapons;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Inventories
{
    public abstract class Inventory
    {
        public Humanoid owner;

        public const int WEAPON_SLOT_CNT = 6;
        public int WeaponsInPosession;

        public Weapon CurrentWeapon { get; set; }
        public int currentWeaponPos = 0;
        public int CurrentWeaponPos
        {
            get { return currentWeaponPos; }
            set
            {
                currentWeaponPos = value;
            }
        }

        public Weapon[] WeaponInventory;

        public Inventory(Humanoid owner)
        {
            this.owner = owner;
            WeaponInventory = new Weapon[WEAPON_SLOT_CNT];
        }

        public bool ChangeCurrentWeaponSlot(int value)
        {
            if (value != currentWeaponPos)
            {
                SoundManager.EquipWeapon.Play(Game1.gameSettings.soundeffectsLevel, 0.2f, 0);
            }

            if (HasWeaponInSlot(value))
            {
                CurrentWeaponPos = value;
                CurrentWeapon = WeaponInventory[CurrentWeaponPos];
                return true;
            }
            return false;
        }

        public bool HasWeaponInSlot(int pos)
        {
            return WeaponInventory[pos] != null;
        }

        public abstract void AddToWeaponInventory(Weapon weapon);


        
    }
}
