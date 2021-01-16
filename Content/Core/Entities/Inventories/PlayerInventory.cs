using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.Entities.Weapons;
using _2DRoguelike.Content.Core.Items.ObtainableItems;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Inventories
{
    public class PlayerInventory : Inventory
    {
        private LevelKey key;
        public bool HasLevelKey
        {
            get { return (key != null); }
        }

        // usable temporary items


        public PlayerInventory(Player player) : base(player)
        {
            
        }

        public override void AddToWeaponInventory(Weapon weapon)
        {
            // sollte nicht vorkommen
            if (WeaponsInPosession >= WEAPON_SLOT_CNT)
            {
                return;
            }

            if (WeaponInventory[weapon.INVENTORY_SLOT] == null)
            {
                WeaponInventory[weapon.INVENTORY_SLOT] = weapon;
                WeaponsInPosession++;
                StatisticsManager.NewWeaponRecieved();
            }
            else
            {
                StatisticsManager.WeaponRecieved();
            }

        }
        public void SetNextWeapon(bool backwards = false)
        {
            // Nächste gültige Position im Array ermitteln
            int currentPos = CurrentWeaponPos;
            do
            {
                currentPos = currentPos + (!backwards ? 1 : -1);
                if (backwards && currentPos < 0) currentPos = WEAPON_SLOT_CNT - 1;
                else if (currentPos >= WEAPON_SLOT_CNT) currentPos = 0;
                // Debug.WriteLine("---Position: " + currentPos);
            } while (!HasWeaponInSlot(currentPos));
            ChangeCurrentWeaponSlot(currentPos);
        }


        // sollte abstrakter sein, z.B. AddItem mit Parameter ObtainableItem item, fuegt es in Liste der schon vorhandene items
        public void AddKey(LevelKey key)
        {
            this.key = key;
        }
        public void ClearKey()
        {
            key = null;
        }
    }
}
