using _2DRoguelike.Content.Core.AssetsLoaders;
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

        // Genral Data

        // Font Data
        public static SpriteFont FontArial { get; private set; }
        public static SpriteFont GameFont { get; private set; }

        // Textures
        public static Texture2D placeholderImage { get; private set; }

        // Other Data

        public static EnemyAssetsLoader enemy = new EnemyAssetsLoader();
        public static UserInterfaceAssetsLoadercs ui = new UserInterfaceAssetsLoadercs();
        public static LootAssetsLoader loot = new LootAssetsLoader();
        public static MenuAssetsLoader menu = new MenuAssetsLoader();
        public static ProjectileAssetsLoader projectiles = new ProjectileAssetsLoader();

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

            enemy.Load(content);
            ui.Load(content);
            loot.Load(content);
            menu.Load(content);
            projectiles.Load(content);

            // Font Data
            FontArial = content.Load<SpriteFont>("Assets/System/Fonts/Arial");
            GameFont = content.Load<SpriteFont>("Assets/System/Fonts/File");
            placeholderImage = content.Load<Texture2D>("Assets/Graphics/placeholderImage");
        }
    }
}
