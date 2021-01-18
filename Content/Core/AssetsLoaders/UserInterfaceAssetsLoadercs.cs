using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.AssetsLoaders
{
    class UserInterfaceAssetsLoadercs
    {
        // UI Data
        // Healths + Boss + Scores
        public Texture2D HealthbarContainer { get; private set; }
        public Texture2D HealthbarBar { get; private set; }
        public Texture2D BossbarContainer { get; private set; }
        public Texture2D BossbarBar { get; private set; }
        public Texture2D HighscoreCoin { get; private set; }
        // score
        public Texture2D XPBarContainer { get; private set; }
        public Texture2D XPBarBar { get; private set; }
        // Manabar
        public Texture2D ManaBarContainer { get; private set; }
        public Texture2D ManaBarBar { get; private set; }
        // skillbar
        public Texture2D skillbar { get; private set; }
        public Texture2D slotUsed { get; private set; }
        public Texture2D selectedItemFame { get; private set; }
        public Texture2D redSlotCross { get; private set; }
        public Texture2D LockedWeapon { get; private set; }
        // Usable Items Bar
        public Texture2D UsableItemsBar { get; private set; }
        // mobhealth
        public Texture2D EnemyBarContainer { get; private set; }
        public Texture2D EnemyBar { get; private set; }
        public Texture2D mouseCursor { get; private set; }

        // Debug Data
        public Texture2D tileHitboxBorder { get; private set; }

        public Texture2D Fog { get; private set; }
        public Texture2D MovingFog { get; private set; }
        public Texture2D FOV { get; private set; }

        public void Load(ContentManager content)
        {
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
            // manabar
            ManaBarContainer = content.Load<Texture2D>("Assets/Graphics/UI/PlayerUI/ManaBar/ManaBarContainer");
            ManaBarBar = content.Load<Texture2D>("Assets/Graphics/UI/PlayerUI/ManaBar/ManaBarBar");
            // skillbar
            skillbar = content.Load<Texture2D>("Assets/Graphics/UI/PlayerUI/Skillbar/skillbar");
            slotUsed = content.Load<Texture2D>("Assets/Graphics/UI/PlayerUI/Skillbar/slotUsed");
            selectedItemFame = content.Load<Texture2D>("Assets/Graphics/UI/PlayerUI/Skillbar/selectedItemFrame");
            redSlotCross = content.Load<Texture2D>("Assets/Graphics/UI/PlayerUI/Skillbar/RedSlotCross");
            LockedWeapon = content.Load<Texture2D>("Assets/Graphics/UI/PlayerUI/Skillbar/lockedWeapon");
            // usable items bar
            UsableItemsBar = content.Load<Texture2D>("Assets/Graphics/UI/PlayerUI/UserItemsBar/UsableItemsBar");
            // mob health
            EnemyBarContainer = content.Load<Texture2D>("Assets/Graphics/UI/EnemeyUI/enemyBarContainer");
            EnemyBar = content.Load<Texture2D>("Assets/Graphics/UI/EnemeyUI/enemyBar");



            // Debug Data
            tileHitboxBorder = content.Load<Texture2D>("Assets/System/Debug/Hitbox/tileHitBox");
        }
    }
}
