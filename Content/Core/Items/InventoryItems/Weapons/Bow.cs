﻿using System;
using System.Collections.Generic;
using System.Text;
using _2DRoguelike.Content.Core.Entities;
using _2DRoguelike.Content.Core.Entities.Creatures.Projectiles;
using _2DRoguelike.Content.Core.Entities.Weapons;

namespace _2DRoguelike.Content.Core.Items.InventoryItems.Weapons
{
    public class Bow : LongRange
    {
        const float BOW_COOLDOWN = 1.5f;
        const int DAMAGE = 35;

        public Bow(Humanoid Owner,float damageMultiplier = 1f, float cooldownMultiplier = 1f) : base(Owner,(int)(DAMAGE * damageMultiplier), BOW_COOLDOWN * cooldownMultiplier) {
            INVENTORY_SLOT = 2;
        }

        public override void CommenceWeaponLogic()
        {
           new Arrow(owner);
        }

        public override string GetAnimationType()
        {
            return "Shoot";
        }

        public override string ToString()
        {
            return "Bow";
        }
    }
}
