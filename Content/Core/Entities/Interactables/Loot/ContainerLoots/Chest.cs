using System;
using System.Collections.Generic;
using _2DRoguelike.Content.Core.Cutscenes;
using _2DRoguelike.Content.Core.Entities.Creatures.Enemies;
using _2DRoguelike.Content.Core.Entities.Loot.InventoryLoots.WeaponLoots;
using _2DRoguelike.Content.Core.Entities.Loot.WeaponLoots;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static _2DRoguelike.Content.Core.Entities.Loot.RandomLoot;

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
            // static bool Variable für einmaliges Ausführen der Cutscene?
            // CutsceneManager.PlayCutscene(new FadeInCircle());
            isExpired = true;
        }

        private DropType DetermineChestRarity()
        {
            int random = Game1.rand.Next(0, 100);

            if(random <= DIAMOND_CHEST_CHANCE)
            {
                return DropType.chestDiamond;
            }
            else
            {
                return DropType.chestNormal;
            }

        }

        public override void PlaySound()
        {
            if (type == DropType.chestDiamond)
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
                case DropType.chestNormal:
                    chestIdleAnimation = TextureManager.loot.LootChest_Animation_Idle;
                    chestOpenAnimation = TextureManager.loot.Lootchest_Animation_Open;
                    break;
                case DropType.chestDiamond:
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