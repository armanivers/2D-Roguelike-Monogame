using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core
{
    static class TextureManager
    {
        // Player Data

        public static Texture2D Player_Hurt { get; private set; }
        public static Texture2D Player_Idle { get; private set; }
        public static Texture2D Player_Shoot { get; private set; }
        public static Texture2D Player_Slash_Axe { get; private set; }
        public static Texture2D Player_Slash_Dagger { get; private set; }
        public static Texture2D Player_Slash_Fist { get; private set; }

        public static Texture2D Player_Walk_Axe { get; private set; }
        public static Texture2D Player_Walk_Dagger { get; private set; }
        public static Texture2D Player_Walk_Fist { get; private set; }
        public static Texture2D Player_Walk_Spear { get; private set; }

        /// Enemy Data
        // Brown Zombie
        public static Texture2D ZombieBrown_Hurt { get; set; }
        public static Texture2D ZombieBrown_Idle { get; private set; }
        public static Texture2D ZombieBrown_Shoot { get; set; }
        public static Texture2D ZombieBrown_Slash_Fist { get; private set; }
        public static Texture2D ZombieBrown_Walk_Fist { get; private set; }

        // Green Zombie
        public static Texture2D ZombieGreen_Hurt { get; set; }
        public static Texture2D ZombieGreen_Idle { get; private set; }
        public static Texture2D ZombieGreen_Shoot { get; set; }
        public static Texture2D ZombieGreen_Slash_Fist { get; private set; }
        public static Texture2D ZombieGreen_Walk_Fist { get; private set; }


        // Skeleton
        public static Texture2D Skeleton_Hurt { get; set; }
        public static Texture2D Skeleton_Idle { get; private set; }
        public static Texture2D Skeleton_Shoot { get; set; }
        public static Texture2D Skeleton_Slash_Dagger { get; private set; }
        public static Texture2D Skeleton_Slash_Fist { get; private set; }
        public static Texture2D Skeleton_Walk_Fist { get; private set; }
        public static Texture2D Skeleton_Walk_Dagger { get; private set; }


        // Wizard
        public static Texture2D Wizard_Hurt { get; set; }
        public static Texture2D Wizard_Idle { get; private set; }
        public static Texture2D Wizard_Shoot { get; set; }
        public static Texture2D Wizard_Slash_Dagger { get; private set; }
        public static Texture2D Wizard_Slash_Fist { get; private set; }
        public static Texture2D Wizard_Walk_Cane { get; private set; }
        public static Texture2D Wizard_Walk_Dagger { get; private set; }
        public static Texture2D Wizard_Walk_Fist { get; private set; }

        // Particle Data

        public static Texture2D Explosion { get; private set; }

        // Projectile Data
        public static Texture2D Arrow { get; private set; }
        public static Texture2D Bomb { get; private set; }

        // Loot Data

        // Potion Data
        public static Texture2D HealthPotion { get; private set; }
        public static Texture2D ExperiencePotion { get; private set; }

        // Weapon Loot Data
        public static Texture2D LootAxe { get; private set; }
        public static Texture2D LootBow { get; private set; }
        public static Texture2D LootDagger { get; private set; }
        public static Texture2D LootBomb { get; private set; }

        // Loot Container Data
        public static Texture2D LootBag { get; private set; }
        public static Texture2D LootChest { get; private set; }
        public static Texture2D LootChest_Animation_Idle { get; private set; }
        public static Texture2D Lootchest_Animation_Open { get; private set; }
        public static Texture2D LootChest_Diamond_Animation_Idle { get; private set; }
        public static Texture2D LootChest_Diamond_Animation_Open { get; private set; }
        public static Texture2D Shadow { get; private set; }
        // Font Data
        public static SpriteFont FontArial { get; private set; }
        public static SpriteFont GameFont { get; private set; }

        // Menu Data
        public static Texture2D DungeonBackground { get; private set; }
        public static Texture2D FirepitBackground { get; private set; }
        public static Texture2D CaveDoorBackground { get; private set; }
        public static Texture2D Blank { get; private set; }
        public static Texture2D Gradient { get; private set; }

        public static Texture2D Fog { get; private set; }
        public static Texture2D MovingFog { get; private set; }
        public static Texture2D FOV { get; private set; }

        // UI Data
        // Healths + Boss + Scores
        public static Texture2D HealthbarContainer { get; private set; }
        public static Texture2D HealthbarBar { get; private set; }
        public static Texture2D BossbarContainer { get; private set; }
        public static Texture2D BossbarBar { get; private set; }
        public static Texture2D HighscoreCoin { get; private set; }
        // score
        public static Texture2D XPBarContainer { get; private set; }
        public static Texture2D XPBarBar { get; private set; }
        // skillbar
        public static Texture2D skillbar { get; private set; }
        public static Texture2D slotUsed { get; private set; }
        public static Texture2D selectedItemFame { get; private set; }
        public static Texture2D redSlotCross { get; private set; }
        public static Texture2D LockedWeapon { get; private set; }
        // mobhealth
        public static Texture2D EnemyBarContainer { get; private set; }
        public static Texture2D EnemyBar { get; private set; }
        public static Texture2D mouseCursor { get; private set; }
        
        // Debug Data
        public static Texture2D tileHitboxBorder { get; private set; }

        public static void Load(ContentManager content)
        {
            // Player Data
            Player_Hurt = content.Load<Texture2D>("Assets/Graphics/PlayerElements/playerSheet_hurt");
            Player_Idle = content.Load<Texture2D>("Assets/Graphics/PlayerElements/playerSheet_idle");
            Player_Shoot = content.Load<Texture2D>("Assets/Graphics/PlayerElements/playerSheet_shoot");
            
            Player_Walk_Axe = content.Load<Texture2D>("Assets/Graphics/PlayerElements/playerSheet_walk_axe");
            Player_Walk_Dagger = content.Load<Texture2D>("Assets/Graphics/PlayerElements/playerSheet_walk_dagger");
            Player_Walk_Fist = content.Load<Texture2D>("Assets/Graphics/PlayerElements/playerSheet_walk_fist");
            Player_Walk_Spear = content.Load<Texture2D>("Assets/Graphics/PlayerElements/playerSheet_walk_spear");

            Player_Slash_Axe = content.Load<Texture2D>("Assets/Graphics/PlayerElements/playerSheet_slash_axe");
            Player_Slash_Dagger = content.Load<Texture2D>("Assets/Graphics/PlayerElements/playerSheet_slash_dagger");
            Player_Slash_Fist = content.Load<Texture2D>("Assets/Graphics/PlayerElements/playerSheet_slash_fist");

            // Enemy Data
            // Zombie Brown
            ZombieBrown_Hurt = content.Load<Texture2D>("Assets/Graphics/EnemyElements/ZombieBrown/brownZombieSheet_hurt");
            ZombieBrown_Idle = content.Load<Texture2D>("Assets/Graphics/EnemyElements/ZombieBrown/brownZombieSheet_idle");
            ZombieBrown_Shoot = content.Load<Texture2D>("Assets/Graphics/EnemyElements/ZombieBrown/brownZombieSheet_shoot");
            ZombieBrown_Slash_Fist = content.Load<Texture2D>("Assets/Graphics/EnemyElements/ZombieBrown/brownZombieSheet_slash_fist");
            ZombieBrown_Walk_Fist = content.Load<Texture2D>("Assets/Graphics/EnemyElements/ZombieBrown/brownZombieSheet_walk_fist");

            // Zombie Green
            ZombieGreen_Hurt = content.Load<Texture2D>("Assets/Graphics/EnemyElements/ZombieGreen/greenZombieSheet_hurt");
            ZombieGreen_Idle = content.Load<Texture2D>("Assets/Graphics/EnemyElements/ZombieGreen/greenZombieSheet_idle");
            ZombieGreen_Shoot = content.Load<Texture2D>("Assets/Graphics/EnemyElements/ZombieGreen/greenZombieSheet_shoot");
            ZombieGreen_Slash_Fist = content.Load<Texture2D>("Assets/Graphics/EnemyElements/ZombieGreen/greenZombieSheet_slash_fist");
            ZombieGreen_Walk_Fist = content.Load<Texture2D>("Assets/Graphics/EnemyElements/ZombieGreen/greenZombieSheet_walk_fist");

            // Skeleton
            Skeleton_Hurt = content.Load<Texture2D>("Assets/Graphics/EnemyElements/Skeleton/skeletonSheet_hurt");
            Skeleton_Idle = content.Load<Texture2D>("Assets/Graphics/EnemyElements/Skeleton/skeletonSheet_idle");
            Skeleton_Shoot = content.Load<Texture2D>("Assets/Graphics/EnemyElements/Skeleton/skeletonSheet_shoot");
            Skeleton_Slash_Dagger = content.Load<Texture2D>("Assets/Graphics/EnemyElements/Skeleton/skeletonSheet_slash_dagger");
            Skeleton_Slash_Fist = content.Load<Texture2D>("Assets/Graphics/EnemyElements/Skeleton/skeletonSheet_slash_fist");
            Skeleton_Walk_Dagger = content.Load<Texture2D>("Assets/Graphics/EnemyElements/Skeleton/skeletonSheet_walk_dagger");
            Skeleton_Walk_Fist = content.Load<Texture2D>("Assets/Graphics/EnemyElements/Skeleton/skeletonSheet_walk_fist");

            // Wizard
            Wizard_Hurt = content.Load<Texture2D>("Assets/Graphics/EnemyElements/Wizard/wizardSheet_hurt");
            Wizard_Idle = content.Load<Texture2D>("Assets/Graphics/EnemyElements/Wizard/wizardSheet_idle");
            Wizard_Shoot = content.Load<Texture2D>("Assets/Graphics/EnemyElements/Wizard/wizardSheet_shoot");
            Wizard_Slash_Dagger = content.Load<Texture2D>("Assets/Graphics/EnemyElements/Wizard/wizardSheet_slash_dagger");
            Wizard_Slash_Fist = content.Load<Texture2D>("Assets/Graphics/EnemyElements/Wizard/wizardSheet_slash_fist");
            Wizard_Walk_Cane = content.Load<Texture2D>("Assets/Graphics/EnemyElements/Wizard/wizardSheet_walk_cane");
            Wizard_Walk_Dagger = content.Load<Texture2D>("Assets/Graphics/EnemyElements/Wizard/wizardSheet_walk_dagger");
            Wizard_Walk_Fist = content.Load<Texture2D>("Assets/Graphics/EnemyElements/Wizard/wizardSheet_walk_fist");

            // Particle Data
            Explosion = content.Load<Texture2D>("Assets/Graphics/Particles/explosion");

            // Loot Data

            // Loot Weapon Data

            LootAxe = content.Load<Texture2D>("Assets/Graphics/LootElements/Weapons/LootAxe");
            LootBow = content.Load<Texture2D>("Assets/Graphics/LootElements/Weapons/LootBow");
            LootDagger = content.Load<Texture2D>("Assets/Graphics/LootElements/Weapons/LootDagger");
            LootBomb = content.Load<Texture2D>("Assets/Graphics/LootElements/Weapons/LootBomb");

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

            // Projectile Data
            Arrow = content.Load<Texture2D>("Assets/Graphics/Projectiles/Arrow");
            Bomb = content.Load<Texture2D>("Assets/Graphics/Projectiles/Bomb");

            // Font Data
            FontArial = content.Load<SpriteFont>("Assets/System/Fonts/Arial");
            GameFont = content.Load<SpriteFont>("Assets/System/Fonts/File");

            // Menu Data
            DungeonBackground = content.Load<Texture2D>("Assets/Graphics/Menus/DungeonBackground");
            FirepitBackground = content.Load<Texture2D>("Assets/Graphics/Menus/FirepitBackground");
            CaveDoorBackground = content.Load<Texture2D>("Assets/Graphics/Menus/CaveBackground");
            Blank = content.Load<Texture2D>("Assets/Graphics/Menus/blank");
            Gradient = content.Load<Texture2D>("Assets/Graphics/Menus/gradient");
            Fog = content.Load<Texture2D>("Assets/Graphics/WorldElements/Fog");
            MovingFog = content.Load<Texture2D>("Assets/Graphics/WorldElements/movingFog");
            FOV = content.Load<Texture2D>("Assets/Graphics/WorldElements/FOV");
            mouseCursor = content.Load<Texture2D>("Assets/System/mouseCursor");
            // UI Data
            // health + scores
            HealthbarContainer = content.Load<Texture2D>("Assets/Graphics/UI/PlayerUI/Healthbar/HealthbarContainer");
            HealthbarBar = content.Load<Texture2D>("Assets/Graphics/UI/PlayerUI/Healthbar/HealthbarBar");
            BossbarContainer = content.Load<Texture2D>("Assets/Graphics/UI/Bossbar/BossbarContainer");
            BossbarBar = content.Load<Texture2D>("Assets/Graphics/UI/Bossbar/BossbarBar");
            HighscoreCoin = content.Load<Texture2D>("Assets/Graphics/UI/PlayerUI/Highscore/HighscoreCoin");
            // xpbar
            XPBarContainer = content.Load<Texture2D>("Assets/Graphics/UI/PlayerUI/Experiencebar/XPBarContainer");
            XPBarBar = content.Load<Texture2D>("Assets/Graphics/UI/PlayerUI/Experiencebar/XPBarBar");
            // skillbar
            skillbar = content.Load<Texture2D>("Assets/Graphics/UI/PlayerUI/Skillbar/skillbar");
            slotUsed = content.Load<Texture2D>("Assets/Graphics/UI/PlayerUI/Skillbar/slotUsed");
            selectedItemFame = content.Load<Texture2D>("Assets/Graphics/UI/PlayerUI/Skillbar/selectedItemFrame");
            redSlotCross = content.Load<Texture2D>("Assets/Graphics/UI/PlayerUI/Skillbar/RedSlotCross");
            LockedWeapon = content.Load<Texture2D>("Assets/Graphics/UI/PlayerUI/Skillbar/lockedWeapon");
            // mob health
            EnemyBarContainer = content.Load<Texture2D>("Assets/Graphics/UI/EnemeyUI/enemyBarContainer");
            EnemyBar = content.Load<Texture2D>("Assets/Graphics/UI/EnemeyUI/enemyBar");

            

            // Debug Data
            tileHitboxBorder = content.Load<Texture2D>("Assets/System/Debug/Hitbox/tileHitBox");
        }
    }
}
