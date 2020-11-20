﻿using Microsoft.Xna.Framework.Content;
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
        //TODO: Nur ein Spritesheet verwenden
        public static Texture2D PlayerIdle { get; private set; }
        public static Texture2D PlayerWalk { get; private set; }
        


        public static Texture2D PlayerShoot { get; private set; }
        
        

        // Enemy Data
        // Brown Zombie
        public static Texture2D ZombieBrownWalk { get; private set; }
        public static Texture2D ZombieBrownIdle { get; private set; }
        public static Texture2D ZombieShoot { get; set; }

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
            PlayerIdle = content.Load<Texture2D>("Assets/Graphics/PlayerElements/playerSheet_idle");
            PlayerWalk = content.Load<Texture2D>("Assets/Graphics/PlayerElements/playerSheet_walk_axe");

            PlayerShoot = content.Load<Texture2D>("Assets/Graphics/PlayerElements/playerSheet_shoot");

            // Enemy Data
            // Zombie Brown
            ZombieBrownWalk = content.Load<Texture2D>("Assets/Graphics/EnemyElements/ZombieBrown/brownZombieSheet_walk");
            ZombieBrownIdle = content.Load<Texture2D>("Assets/Graphics/EnemyElements/ZombieBrown/brownZombieSheet_idle");
            ZombieShoot = content.Load<Texture2D>("Assets/Graphics/EnemyElements/ZombieBrown/brownZombieSheet_shoot");
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
