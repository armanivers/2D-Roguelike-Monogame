using System;
using System.Collections.Generic;
using System.Text;
using _2DRoguelike.Content.Core.Entities.Weapons;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.Loot.InventoryLoots.WeaponLoot
{
    public abstract class WeaponLoot : InventoryLoot
    {
        public WeaponLoot(Vector2 pos) : base(pos) { }    
        
        public override void OnContact() {
            //Player.Player.Instance.addToWeaponInventory(GetCorrespondingWeapon());
        }

        public abstract Weapon GetCorrespondingWeapon();
        
    }


}
