using _2DRoguelike.Content.Core.Entities.Creatures.Enemies;
using _2DRoguelike.Content.Core.Entities.Weapons;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Creatures.ControllingPlayer
{
    class EnemyInventory : Inventory
    {

        public EnemyInventory(Enemy enemy) : base(enemy)
        {

        }

        public override void AddToWeaponInventory(Weapon weapon)
        {
            if (weapon is ShortRange)
                WeaponInventory[0] = weapon;
            else  
                WeaponInventory[1] = weapon;
            return;
        }
    }
}
