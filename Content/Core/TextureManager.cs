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
        // Idee: Animationen in ein Array speicher, dann mit [0] auf animation walkup und [1] animation walkdown zugreifen?

        public static Texture2D Player_Hurt { get; private set; }
        public static Texture2D Player_Idle { get; private set; }
        public static Texture2D Player_Shoot { get; private set; }
        public static Texture2D Player_Slash_Axe { get; private set; }
        public static Texture2D Player_Slash_Dagger { get; private set; }
        public static Texture2D Player_Slash_Fist { get; private set; }

        public static Texture2D Player_Walk_Axe { get; private set; }
        public static Texture2D Player_Walk_Dagger { get; private set; }
        public static Texture2D Player_Walk_Fist { get; private set; }
        public static Texture2D Player_Walk_Spear{ get; private set; }

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

        // Font Data
        public static SpriteFont FontArial { get; private set; }

        // Menu Data
        public static Texture2D Background { get; private set; }
        public static Texture2D Blank { get; private set; }
        public static Texture2D Gradient { get; private set; }

        // UI Data
        public static Texture2D healthBarEmpty { get; private set; }
        public static Texture2D healthBarRed { get; private set; }
        public static Texture2D skillbar { get; private set; }
        public static Texture2D slotUsed { get; private set; }
        public static Texture2D EnemyBarContainer { get; private set; }
        public static Texture2D EnemyBar { get; private set; }
        // Tiles Data
        public static Texture2D[] tiles { get; private set; }
        public static int tilesAmount;

        // Debug Data
        public static Texture2D tileHitboxBorder { get; private set; }

        public static void Load(ContentManager content)
        {
            // Player Data
            Player_Hurt = content.Load<Texture2D>("Assets/Graphics/PlayerElements/playerSheet_hurt/playerSheet_hurt");
            Player_Idle = content.Load<Texture2D>("Assets/Graphics/PlayerElements/playerSheet_idle/playerSheet_idle");
            Player_Shoot = content.Load<Texture2D>("Assets/Graphics/PlayerElements/playerSheet_shoot/playerSheet_shoot");
            
            Player_Walk_Axe = content.Load<Texture2D>("Assets/Graphics/PlayerElements/playerSheet_walk/playerWalk_axe/playerSheet_walk_axe");
            Player_Walk_Dagger = content.Load<Texture2D>("Assets/Graphics/PlayerElements/playerSheet_walk/playerWalk_dagger/playerSheet_walk_dagger");
            Player_Walk_Fist = content.Load<Texture2D>("Assets/Graphics/PlayerElements/playerSheet_walk/playerWalk_fist/playerSheet_walk_fist");
            Player_Walk_Spear = content.Load<Texture2D>("Assets/Graphics/PlayerElements/playerSheet_walk/playerWalk_spear/playerSheet_walk_spear");

            Player_Slash_Axe = content.Load<Texture2D>("Assets/Graphics/PlayerElements/playerSheet_slash/playerSlash_axe/playerSheet_slash_axe");
            Player_Slash_Dagger = content.Load<Texture2D>("Assets/Graphics/PlayerElements/playerSheet_slash/playerSlash_dagger/playerSheet_slash_dagger");
            Player_Slash_Fist = content.Load<Texture2D>("Assets/Graphics/PlayerElements/playerSheet_slash/playerSlash_fist/playerSheet_slash_fist");

            // Enemy Data
            // Zombie Brown
            ZombieBrown_Hurt = content.Load<Texture2D>("Assets/Graphics/EnemyElements/ZombieBrown/brownZombie_hurt/brownZombieSheet_hurt");
            ZombieBrown_Idle = content.Load<Texture2D>("Assets/Graphics/EnemyElements/ZombieBrown/brownZombie_idle/brownZombieSheet_idle");
            ZombieBrown_Shoot = content.Load<Texture2D>("Assets/Graphics/EnemyElements/ZombieBrown/brownZombie_shoot/brownZombieSheet_shoot");
            ZombieBrown_Slash_Fist = content.Load<Texture2D>("Assets/Graphics/EnemyElements/ZombieBrown/brownZombie_slash/brownZombieSlash_fist/brownZombieSheet_slash_fist");
            ZombieBrown_Walk_Fist = content.Load<Texture2D>("Assets/Graphics/EnemyElements/ZombieBrown/brownZombie_walk/brownZombieWalk_fist/brownZombieSheet_walk_fist");

            // Zombie Green
            ZombieGreen_Hurt = content.Load<Texture2D>("Assets/Graphics/EnemyElements/ZombieGreen/greenZombie_hurt/greenZombieSheet_hurt");
            ZombieGreen_Idle = content.Load<Texture2D>("Assets/Graphics/EnemyElements/ZombieGreen/greenZombie_idle/greenZombieSheet_idle");
            ZombieGreen_Shoot = content.Load<Texture2D>("Assets/Graphics/EnemyElements/ZombieGreen/greenZombie_shoot/greenZombieSheet_shoot");
            ZombieGreen_Slash_Fist = content.Load<Texture2D>("Assets/Graphics/EnemyElements/ZombieGreen/greenZombie_slash/greenZombieSlash_fist/greenZombieSheet_slash_fist");
            ZombieGreen_Walk_Fist = content.Load<Texture2D>("Assets/Graphics/EnemyElements/ZombieGreen/greenZombie_walk/greenZombieWalk_fist/greenZombieSheet_walk_fist");

            // Skeleton
            Skeleton_Hurt = content.Load<Texture2D>("Assets/Graphics/EnemyElements/Skeleton/skeleton_hurt/skeletonSheet_hurt");
            Skeleton_Idle = content.Load<Texture2D>("Assets/Graphics/EnemyElements/Skeleton/skeleton_idle/skeletonSheet_idle");
            Skeleton_Shoot = content.Load<Texture2D>("Assets/Graphics/EnemyElements/Skeleton/skeleton_shoot/skeletonSheet_shoot");
            Skeleton_Slash_Dagger = content.Load<Texture2D>("Assets/Graphics/EnemyElements/Skeleton/skeleton_slash/skeletonSlash_dagger/skeletonSheet_slash_dagger");
            Skeleton_Slash_Fist = content.Load<Texture2D>("Assets/Graphics/EnemyElements/Skeleton/skeleton_slash/skeletonSlash_fist/skeletonSheet_slash_fist");
            Skeleton_Walk_Dagger = content.Load<Texture2D>("Assets/Graphics/EnemyElements/Skeleton/skeleton_walk/skeletonWalk_dagger/skeletonSheet_walk_dagger");
            Skeleton_Walk_Fist = content.Load<Texture2D>("Assets/Graphics/EnemyElements/Skeleton/skeleton_walk/skeletonWalk_fist/skeletonSheet_walk_fist");

            // Wizard
            Wizard_Hurt = content.Load<Texture2D>("Assets/Graphics/EnemyElements/Wizard/wizard_hurt/wizardSheet_hurt");
            Wizard_Idle = content.Load<Texture2D>("Assets/Graphics/EnemyElements/Wizard/wizard_idle/wizardSheet_idle");
            Wizard_Shoot = content.Load<Texture2D>("Assets/Graphics/EnemyElements/Wizard/wizard_shoot/wizardSheet_shoot");
            Wizard_Slash_Dagger = content.Load<Texture2D>("Assets/Graphics/EnemyElements/Wizard/wizard_slash/wizardSlash_dagger/wizardSheet_slash_dagger");
            Wizard_Slash_Fist = content.Load<Texture2D>("Assets/Graphics/EnemyElements/Wizard/wizard_slash/wizardSlash_fist/wizardSheet_slash_fist");
            Wizard_Walk_Cane = content.Load<Texture2D>("Assets/Graphics/EnemyElements/Wizard/wizard_walk/wizardWalk_cane/wizardSheet_walk_cane");
            Wizard_Walk_Dagger = content.Load<Texture2D>("Assets/Graphics/EnemyElements/Wizard/wizard_walk/wizardWalk_dagger/wizardSheet_walk_dagger");
            Wizard_Walk_Fist = content.Load<Texture2D>("Assets/Graphics/EnemyElements/Wizard/wizard_walk/wizardWalk_fist/wizardSheet_walk_fist");

            // Particle Data
            Explosion = content.Load<Texture2D>("Assets/Graphics/Particles/explosion");

            // Projectile Data
            Arrow = content.Load<Texture2D>("Assets/Graphics/Projectiles/Arrow");

            // Font Data
            FontArial = content.Load<SpriteFont>("Assets/System/Fonts/Arial");

            // Menu Data
            Background = content.Load<Texture2D>("Assets/Graphics/Menus/background");
            Blank = content.Load<Texture2D>("Assets/Graphics/Menus/blank");
            Gradient = content.Load<Texture2D>("Assets/Graphics/Menus/gradient");

            // UI Data
            healthBarEmpty = content.Load<Texture2D>("Assets/Graphics/UI/PlayerUI/EmptyBar");
            healthBarRed = content.Load<Texture2D>("Assets/Graphics/UI/PlayerUI/RedBar");
            skillbar = content.Load<Texture2D>("Assets/Graphics/UI/PlayerUI/skillbar");
            slotUsed = content.Load<Texture2D>("Assets/Graphics/UI/PlayerUI/slotUsed");
            EnemyBarContainer = content.Load<Texture2D>("Assets/Graphics/UI/EnemeyUI/enemyBarContainer");
            EnemyBar = content.Load<Texture2D>("Assets/Graphics/UI/EnemeyUI/enemyBar");

            // Tiles Data
            tilesAmount = 2;
            tiles = new Texture2D[tilesAmount];
            for (int i = 0; i < tilesAmount; i++)
            {
                tiles[i] = content.Load<Texture2D>("Assets/Graphics/WorldElements/Tiles/tile" + i);
            }

            // Debug Data
            tileHitboxBorder = content.Load<Texture2D>("Assets/System/Debug/Hitbox/tileHitBox");
        }
    }
}
