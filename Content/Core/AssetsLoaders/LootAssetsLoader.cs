using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.AssetsLoaders
{
    class LootAssetsLoader
    {

        // Loot Data

        // Potion Data
        public Texture2D HealthPotion { get; private set; }
        public Texture2D ExperiencePotion { get; private set; }

        // Weapon Loot Data
        public Texture2D LootAxe { get; private set; }
        public Texture2D LootBow { get; private set; }
        public Texture2D LootDagger { get; private set; }
        public Texture2D LootBomb { get; private set; }
        public Texture2D LootSpear { get; private set; }

        // Loot Container Data
        public Texture2D LootBag { get; private set; }
        public Texture2D LootChest { get; private set; }
        public Texture2D LootChest_Animation_Idle { get; private set; }
        public Texture2D Lootchest_Animation_Open { get; private set; }
        public Texture2D LootChest_Diamond_Animation_Idle { get; private set; }
        public Texture2D LootChest_Diamond_Animation_Open { get; private set; }
        public Texture2D Shadow { get; private set; }

        // Key 
        public Texture2D KeyLoot { get; private set; }

        public void Load(ContentManager content)
        {
            // Loot Data

            // Loot Weapon Data

            LootAxe = content.Load<Texture2D>("Assets/Graphics/LootElements/Weapons/LootAxe");
            LootBow = content.Load<Texture2D>("Assets/Graphics/LootElements/Weapons/LootBow");
            LootDagger = content.Load<Texture2D>("Assets/Graphics/LootElements/Weapons/LootDagger");
            LootBomb = content.Load<Texture2D>("Assets/Graphics/LootElements/Weapons/LootBomb");
            LootSpear = content.Load<Texture2D>("Assets/Graphics/LootElements/Weapons/LootSpear");


            // Potion Data
            HealthPotion = content.Load<Texture2D>("Assets/Graphics/LootElements/Potions/HealthPotion");
            ExperiencePotion = content.Load<Texture2D>("Assets/Graphics/LootElements/Potions/ExperiencePotion");

            // Loot Container Data
            LootBag = content.Load<Texture2D>("Assets/Graphics/LootElements/Containers/LootBag");
            LootChest = content.Load<Texture2D>("Assets/Graphics/LootElements/Containers/LootChest");
            LootChest_Animation_Idle = content.Load<Texture2D>("Assets/Graphics/LootElements/Containers/chest_animation");
            Lootchest_Animation_Open = content.Load<Texture2D>("Assets/Graphics/LootElements/Containers/chest_open_animation");
            LootChest_Diamond_Animation_Idle = content.Load<Texture2D>("Assets/Graphics/LootElements/Containers/chest_diamond_idle_animation");
            LootChest_Diamond_Animation_Open = content.Load<Texture2D>("Assets/Graphics/LootElements/Containers/chest_diamond_open_animation");

            Shadow = content.Load<Texture2D>("Assets/Graphics/WorldElements/Shadow");

            //Key
            KeyLoot = content.Load<Texture2D>("Assets/Graphics/LootElements/Obtainable/key_animation");
        }
    }
}
