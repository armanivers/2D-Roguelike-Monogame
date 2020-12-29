using _2DRoguelike.Content.Core.Entities.Creatures.Enemies;
using _2DRoguelike.Content.Core.Entities.Loot.InventoryLoots.WeaponLoots;
using _2DRoguelike.Content.Core.Entities.Loot.WeaponLoots;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Loot.Potions
{
    public class LootBag : LootContainer
    {
        private const float TIME_TO_OPEN = 0.3f;

        public LootBag(Vector2 pos, Enemy enemy) : base(pos, TIME_TO_OPEN)
        {
            texture = TextureManager.loot.LootBag;
            type = RandomLoot.DetermineMonsterLootTable(enemy);
        }

        public override void OpenContainer()
        {
            RandomLoot.SpawnLoot(type, Position);
            isExpired = true;
        }

        public override void PlaySound()
        {
            SoundManager.LootbagOpen.Play(Game1.gameSettings.soundeffectsLevel, 0, 0);
        }
    }
}