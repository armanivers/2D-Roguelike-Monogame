using System;
using System.Collections.Generic;
using System.Text;
using _2DRoguelike.Content.Core.Entities.Weapons;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.Loot.InventoryLoots.WeaponLoots
{
    public abstract class WeaponLoot : InventoryLoot
    {
        public WeaponLoot(Vector2 pos) : base(pos) { }    
        
        public override void OnContact() {
            ControllingPlayer.Player.Instance.AddToWeaponInventory(GetCorrespondingWeapon());
            isExpired = true;
        }

        public abstract Weapon GetCorrespondingWeapon();
        
    }


}
