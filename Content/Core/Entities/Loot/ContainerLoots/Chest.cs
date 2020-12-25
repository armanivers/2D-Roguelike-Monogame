using System;
using System.Collections.Generic;
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
            
            new AxeLoot(Position);
            isExpired = true;
        }
        public override void PlaySound()
        {
            // choose one random effect (testing)
            int x = Game1.rand.Next();

            if (x % 2 == 0)
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