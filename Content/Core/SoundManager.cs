using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DRoguelike.Content.Core
{
    static class SoundManager {

        // Background Songs
        public static Song MenuMusic { get; private set; }

        // Level Musics
        public static Song Level00 { get; private set; }
        public static Song Level01 { get; private set; }
        // Boss Musics
        public static Song Boss00 { get; private set; }
        // Menu Item Select
        public static SoundEffect MenuItemSelect { get; private set; }
        // Game Over Menu
        public static SoundEffect GameOver { get; private set; }
        public static SoundEffect GameOverSucc { get; private set; }
        // Sound Effects
        
        // Player Effects
        public static SoundEffect PlayerHurt { get; private set; }
        public static SoundEffect PlayerDie { get; private set; }
        // Attack/Weapon Sounds
        public static SoundEffect ShootArrow { get; private set; }
        public static SoundEffect MeleeWeaponSwing { get; private set; }
        public static SoundEffect EquipWeapon { get; private set; }
        // Particle Sounds
        public static SoundEffect Explosion { get; private set; }
        // Loot sounds
        public static SoundEffect ItemPickup { get; private set; }
        public static SoundEffect ChestOpenWooden { get; private set; }
        public static SoundEffect ChestOpenMagical { get; private set; }
        public static SoundEffect LootbagOpen { get; private set; }

        public static SoundEffect KeyCollect { get; private set; }
        public static SoundEffect KeyPickup { get; private set; } //version 2
        public static SoundEffect KeyMissing { get; private set; }
        // Experience obrs
        public static SoundEffect ExperiencePickup { get; private set; }

        // Potions
        public static SoundEffect PotionDrink { get; private set; }

        // Miscellaneous
        public static SoundEffect ScoreCounterSound00 { get; private set; }
        public static SoundEffect ScoreCounterSound01 { get; private set; }
        public static SoundEffect NextLevel { get; private set; }

        public static void Load(ContentManager content)
        {
            // Background Songs
            MenuMusic = content.Load<Song>("Assets/Sounds/SceenMusics/Menus/MenuMusic");

            // Level Musics
            Level00 = content.Load<Song>("Assets/Sounds/SceenMusics/Gameplay/LevelsMusic/LevelMusic_00");
            Level01 = content.Load<Song>("Assets/Sounds/SceenMusics/Gameplay/LevelsMusic/LevelMusic_01");

            // Boss Musics
            Boss00 = content.Load<Song>("Assets/Sounds/SceenMusics/Gameplay/BossMusic/Bossbattle_00");

            MenuItemSelect = content.Load<SoundEffect>("Assets/Sounds/SceenMusics/MenuItemSelect");
            GameOver = content.Load<SoundEffect>("Assets/Sounds/SceenMusics/GameOver");
            GameOverSucc = content.Load<SoundEffect>("Assets/Sounds/SceenMusics/GameOverSucc");
            // Sound Effects

            // Player effects
            PlayerHurt = content.Load<SoundEffect>("Assets/Sounds/PlayerSounds/playerHurt");
            PlayerDie = content.Load<SoundEffect>("Assets/Sounds/PlayerSounds/PlayerDie");

            // Attack sounds
            ShootArrow = content.Load<SoundEffect>("Assets/Sounds/PlayerSounds/walking_dirt");
            MeleeWeaponSwing = content.Load<SoundEffect>("Assets/Sounds/PlayerSounds/Whoosh2");
            EquipWeapon = content.Load<SoundEffect>("Assets/Sounds/PlayerSounds/equip");

            // Particle Sounds
            Explosion = content.Load<SoundEffect>("Assets/Sounds/ParticleSFX/explosion");

            // Loot sounds
            ItemPickup = content.Load<SoundEffect>("Assets/Sounds/LootSounds/ItemPickup");
            ChestOpenWooden = content.Load<SoundEffect>("Assets/Sounds/LootSounds/ChestOpenWooden");
            ChestOpenMagical = content.Load<SoundEffect>("Assets/Sounds/LootSounds/ChestOpenMagical");
            LootbagOpen = content.Load<SoundEffect>("Assets/Sounds/LootSounds/LootbagOpen");
            KeyCollect = content.Load<SoundEffect>("Assets/Sounds/LootSounds/KeyCollect");
            KeyPickup = content.Load<SoundEffect>("Assets/Sounds/LootSounds/KeySounds/KeyPickup");
            KeyMissing = content.Load<SoundEffect>("Assets/Sounds/LootSounds/KeySounds/KeyMissing ");
            // Experience Orbs
            ExperiencePickup = content.Load<SoundEffect>("Assets/Sounds/Miscellaneous/ExperiencePickup");

            // Potions
            PotionDrink = content.Load<SoundEffect>("Assets/Sounds/PotionSounds/PotionDrink");

            // Miscellaneous
            ScoreCounterSound00 = content.Load<SoundEffect>("Assets/Sounds/Miscellaneous/scoreCounterSound_00");
            ScoreCounterSound01 = content.Load<SoundEffect>("Assets/Sounds/Miscellaneous/scoreCounterSound_01");
            NextLevel = content.Load<SoundEffect>("Assets/Sounds/Miscellaneous/NextLevel");
        }
    }
}
