using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.Entities.Weapons;
using _2DRoguelike.Content.Core.Items.InventoryItems.UsableItems;
using _2DRoguelike.Content.Core.Items.InventoryItems.Weapons;
using _2DRoguelike.Content.Core.Items.ObtainableItems;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public const int USABLE_ITEMS_SIZE = 3;
        private int currentUsablesCount = 0;

        // usable temporary items
        public UsableItem[] usableItems;

        public PlayerInventory(Player player) : base(player)
        {
            usableItems = new UsableItem[USABLE_ITEMS_SIZE];
        }

        #region UsableItems
        public void AddUsableItemToInventory(UsableItem item)
        {
            // inventory isnt empty, so first ry if we can stack the new item
            if (currentUsablesCount > 0 && StackItem(item)) return;

            // if no space left
            if (currentUsablesCount >= USABLE_ITEMS_SIZE) return;

            // otherwise add the item to a free spot in inventory
            usableItems[FindNextFreeSpot()] = item;
        }

        public int FindNextFreeSpot()
        {
            for(int i = 0; i < USABLE_ITEMS_SIZE; i++)
            {
                if (usableItems[i] == null)
                {
                    currentUsablesCount++;
                    return i;
                }
            }
            return -1;
        }

        public bool StackItem(UsableItem stackableItem)
        {
            // go throught the items and see if this items is already there, if yes stack it
            for(int i = 0; i < USABLE_ITEMS_SIZE; i++)
            { 
                if(usableItems[i]!= null && usableItems[i].Equals(stackableItem))
                {
                    usableItems[i].quantity++;
                    return true;
                }
            }
            return false;
        }

        public void UseItem(int inventorySlot)
        {
            // check if there is an item
            if (usableItems[inventorySlot] == null)
            {
                return;
            }

            // activate the item
            usableItems[inventorySlot].ActivateItem();

            // if quantity == 0, then remove it after use
            if (usableItems[inventorySlot].quantity < 1)
            {
                usableItems[inventorySlot] = null;
                currentUsablesCount--;
            }
        }
        #endregion

        #region Weapons
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

        #endregion

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
