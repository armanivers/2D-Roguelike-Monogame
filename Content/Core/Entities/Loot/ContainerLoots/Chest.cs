using System;
using System.Collections.Generic;
using _2DRoguelike.Content.Core.Entities.Loot.WeaponLoots;
using Microsoft.Xna.Framework;


namespace _2DRoguelike.Content.Core.Entities.Loot.Potions
{
    public class Chest : LootContainer
    {
        private List<InventoryItem> dropList;
        private const float TIME_TO_OPEN = 1f;
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
            new AxeLoot(Position);
            isExpired = true;
        }


    }
}