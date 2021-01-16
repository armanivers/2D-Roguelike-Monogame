using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Creatures.ControllingPlayer
{
    class PlayerInventory : Inventory
    {
        public PlayerInventory(Player player) : base(player)
        {
            
        }

        public override void SetNextWeapon(bool backwards = false)
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

        /*
        public LevelKey key;

        public LevelKey Key
        {
            get { return key; }
            private set
            {
                key = value;
            }
        }
        */

        // sollte abstrakter sein, z.B. AddItem mit Parameter ObtainableItem item, fuegt es in Liste der schon vorhandene items
        public override void AddKey()
        {
            hasLevelKey = true;
        }
        public override void ClearKey()
        {
            hasLevelKey = false;
        }
    }
}
