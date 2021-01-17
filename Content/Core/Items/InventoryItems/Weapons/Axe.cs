using System;
using System.Collections.Generic;
using System.Text;
using _2DRoguelike.Content.Core.Entities;
using _2DRoguelike.Content.Core.Entities.Weapons;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Items.InventoryItems.Weapons
{
    public class Axe : ShortRange
    {
        const float AXE_COOLDOWN = 0.8f;
        const int DAMAGE = 40;
        const float RANGE_MULTIPLIER_X = 0.9f;
        const float RANGE_MULTIPLIER_Y = 0.9f;

        const byte DEFAULT_MAXIMUM_HITS_PER_ATTACK = 2;

        public Axe(Humanoid Owner, float damageMultiplier = 1f, float cooldownMultiplier = 1f, float rangeX = 1f, float rangeY = 1f) : base(Owner, rangeX * RANGE_MULTIPLIER_X, rangeY * RANGE_MULTIPLIER_Y, 
            (int)(DAMAGE*damageMultiplier),AXE_COOLDOWN * cooldownMultiplier, DEFAULT_MAXIMUM_HITS_PER_ATTACK) {
            INVENTORY_SLOT = 3;
        }

        public override string GetAnimationType()
        {
            return "Slash";
        }

        public override string ToString()
        {
            return "Axe";
        }
    }
}
