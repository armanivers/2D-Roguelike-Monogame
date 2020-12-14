using System;
using System.Collections.Generic;
using _2DRoguelike.Content.Core.Entities.Loot.WeaponLoots;
using Microsoft.Xna.Framework;


namespace _2DRoguelike.Content.Core.Entities.Loot.Potions
{
    public class Chest : LootContainer
    {
        private List<InventoryItem> dropList;
        public Chest(Vector2 pos, List<InventoryItem> dropList) : base(pos, 2)
        {
            // TODO: Einigung für Origin setzen
            Hitbox = new Rectangle((int)(Position.X  * scaleFactor), (int)(Position.Y * scaleFactor), (int)(32 * scaleFactor), (int)(32 * scaleFactor));
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
        }

        public override void OpenContainer()
        {
            // drop all items from the provided droplist
            new AxeLoot(Position);
            isExpired = true;
        }
    }
}