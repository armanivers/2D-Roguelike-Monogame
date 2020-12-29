using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.AssetsLoaders
{
    class EnemyAssetsLoader
    {
        /// Enemy Data
        /// 
        // Brown Zombie
        public  Texture2D ZombieBrown_Hurt { get; set; }
        public  Texture2D ZombieBrown_Idle { get; private set; }
        public  Texture2D ZombieBrown_Shoot { get; set; }
        public  Texture2D ZombieBrown_Slash_Fist { get; private set; }
        public  Texture2D ZombieBrown_Walk_Fist { get; private set; }

        // Green Zombie
        public  Texture2D ZombieGreen_Hurt { get; set; }
        public  Texture2D ZombieGreen_Idle { get; private set; }
        public  Texture2D ZombieGreen_Shoot { get; set; }
        public  Texture2D ZombieGreen_Slash_Fist { get; private set; }
        public  Texture2D ZombieGreen_Walk_Fist { get; private set; }


        // Skeleton
        public  Texture2D Skeleton_Hurt { get; set; }
        public  Texture2D Skeleton_Idle { get; private set; }
        public  Texture2D Skeleton_Shoot { get; set; }
        public  Texture2D Skeleton_Slash_Dagger { get; private set; }
        public  Texture2D Skeleton_Slash_Fist { get; private set; }
        public  Texture2D Skeleton_Walk_Fist { get; private set; }
        public  Texture2D Skeleton_Walk_Dagger { get; private set; }


        // Wizard
        public  Texture2D Wizard_Hurt { get; set; }
        public  Texture2D Wizard_Idle { get; private set; }
        public  Texture2D Wizard_Shoot { get; set; }
        public  Texture2D Wizard_Slash_Dagger { get; private set; }
        public  Texture2D Wizard_Slash_Fist { get; private set; }
        public  Texture2D Wizard_Walk_Cane { get; private set; }
        public  Texture2D Wizard_Walk_Dagger { get; private set; }
        public  Texture2D Wizard_Walk_Fist { get; private set; }

        public void Load(ContentManager content)
        {
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
        }
    }
}
