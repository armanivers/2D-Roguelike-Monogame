using System;
using System.Collections.Generic;
using _2DRoguelike.Content.Core.Cutscenes;
using _2DRoguelike.Content.Core.Entities.Creatures.Enemies;
using _2DRoguelike.Content.Core.Entities.Loot.InventoryLoots.WeaponLoots;
using _2DRoguelike.Content.Core.Entities.Loot.WeaponLoots;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2DRoguelike.Content.Core.Entities.Loot.Potions
{
    public class Chest : LootContainer
    {
        private const float TIME_TO_OPEN = 1.2f;
        private const float DIAMOND_CHEST_CHANCE = 30;
        Texture2D chestIdleAnimation;
        Texture2D chestOpenAnimation;
        public Chest(Vector2 pos) : base(pos, TIME_TO_OPEN)
        {
            type = DetermineChestRarity();
            SetTexture();

            animations = new Dictionary<string, Animation>()
            {
                {"Chest_Idle",new Animation(chestIdleAnimation,0,4,0.1f,true,false,false,32) },
                {"Chest_Open",new Animation(chestOpenAnimation,0,4,0.2f,false,false,false,32) }
            };

            animationManager = new AnimationManager(this, animations["Chest_Idle"]);
            animationManager.Position = pos;
            floatable = false;
        }

        public override void OpenContainer()
        {
            RandomLoot.SpawnLoot(type,Position);
            CutsceneManager.PlayCutscene(new FadeInCircle());
            isExpired = true;
        }

        private int DetermineChestRarity()
        {
            int random = Game1.rand.Next(0, 100);

            if(random <= DIAMOND_CHEST_CHANCE)
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }

        public override void PlaySound()
        {

            if (type == 1)
            {
                SoundManager.ChestOpenMagical.Play(Game1.gameSettings.soundeffectsLevel, 0, 0);
            }
            else
            {
                SoundManager.ChestOpenWooden.Play(Game1.gameSettings.soundeffectsLevel, 0, 0);
            }
        }

        private void SetTexture()
        {
            switch(type)
            {
                case 0:
                    chestIdleAnimation = TextureManager.loot.LootChest_Animation_Idle;
                    chestOpenAnimation = TextureManager.loot.Lootchest_Animation_Open;
                    break;
                case 1:
                    chestIdleAnimation = TextureManager.loot.LootChest_Diamond_Animation_Idle;
                    chestOpenAnimation = TextureManager.loot.LootChest_Diamond_Animation_Open;
                    break;
                default:
                    chestIdleAnimation = TextureManager.loot.LootChest_Animation_Idle;
                    chestOpenAnimation = TextureManager.loot.Lootchest_Animation_Open;
                    break;
            }

        }
    }
}