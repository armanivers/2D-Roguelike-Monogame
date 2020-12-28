using System;
using System.Collections.Generic;
using _2DRoguelike.Content.Core.Entities.Loot.InventoryLoots.WeaponLoots;
using _2DRoguelike.Content.Core.Entities.Loot.WeaponLoots;
using Microsoft.Xna.Framework;


namespace _2DRoguelike.Content.Core.Entities.Loot.Potions
{
    public class Chest : LootContainer
    {
        private const float TIME_TO_OPEN = 1.2f;
        public Chest(Vector2 pos, List<InventoryItem> dropList) : base(pos, TIME_TO_OPEN)
        {

            //texture = TextureManager.LootChest;
            animations = new Dictionary<string, Animation>()
            {
                {"Chest_Idle",new Animation(TextureManager.LootChest_Animation_Idle,0,4,0.1f,true,false,false,32) },
                {"Chest_Open",new Animation(TextureManager.Lootchest_Animation_Open,0,4,0.2f,false,false,false,32) }
            };
            animationManager = new AnimationManager(this, animations["Chest_Idle"]);
            animationManager.Position = pos;
            floatable = false;
            this.dropList = dropList;


            //timeToOpen = AnimationDuration("Chest_Open");
        }

        public override void OpenContainer()
        {
            // drop all items from the provided droplist

            SpawnRandomLoot();
            isExpired = true;
        }

        private void SpawnRandomLoot()
        {

            int WeaponLootNumber = Game1.rand.Next(101);

            if (WeaponLootNumber <= 10)
                new BombLoot(Position);
            else
            if (WeaponLootNumber <= 30)
                new AxeLoot(Position);
            else
            if (WeaponLootNumber <= 60)
                new BowLoot(Position);
            else
            if (WeaponLootNumber <= 1000)
                new DaggerLoot(Position);



        }
        public override void PlaySound()
        {

            // choose one random effect (testing)
            int x = Game1.rand.Next(2);

            if (x == 0)
            {
                SoundManager.ChestOpenMagical.Play(Game1.gameSettings.soundeffectsLevel, 0, 0);
            }
            else
            {
                SoundManager.ChestOpenWooden.Play(Game1.gameSettings.soundeffectsLevel, 0, 0);
            }
        }

    }
}