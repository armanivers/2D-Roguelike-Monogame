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
        // Sound Effects
        
        // Player Effects
        public static SoundEffect PlayerHurt { get; private set; }
        public static SoundEffect PlayerDie { get; private set; }
        public static SoundEffect ShootArrow { get; private set; }
        public static SoundEffect MeleeWeaponSwing { get; private set; }
        public static SoundEffect EquipWeapon { get; private set; }

        public static SoundEffect Explosion { get; private set; }

        public static void Load(ContentManager content)
        {
            // Background Songs
            MenuMusic = content.Load<Song>("Assets/Sounds/SceenMusics/Menus/MenuMusic");

            // Level Musics
            Level00 = content.Load<Song>("Assets/Sounds/SceenMusics/Gameplay/LevelsMusic/LevelMusic_00");
            Level01 = content.Load<Song>("Assets/Sounds/SceenMusics/Gameplay/LevelsMusic/LevelMusic_01");

            // Boss Musics
            Boss00 = content.Load<Song>("Assets/Sounds/SceenMusics/Gameplay/BossMusic/Bossbattle_00");

            // Sound Effects

            // Player effects
            PlayerHurt = content.Load<SoundEffect>("Assets/Sounds/PlayerSounds/playerHurt");
            PlayerDie = content.Load<SoundEffect>("Assets/Sounds/PlayerSounds/PlayerDie");

            ShootArrow = content.Load<SoundEffect>("Assets/Sounds/PlayerSounds/walking_dirt");

            EquipWeapon = content.Load<SoundEffect>("Assets/Sounds/PlayerSounds/equip");
            
            // Attack sounds

            MeleeWeaponSwing = content.Load<SoundEffect>("Assets/Sounds/PlayerSounds/Whoosh2");


            Explosion = content.Load<SoundEffect>("Assets/Sounds/ParticleSFX/explosion");


        }
    }
}
