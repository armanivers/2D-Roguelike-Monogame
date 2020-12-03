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
    
        public static Song BackgroundMusic { get; private set; }
        public static SoundEffect ShootArrow { get; private set; }
        public static SoundEffect MeleeWeaponSwing { get; private set; }
        public static SoundEffect EquipWeapon { get; private set; }

        public static SoundEffect Explosion { get; private set; }

        public static void Load(ContentManager content)
        {
            BackgroundMusic = content.Load<Song>("Assets/Sounds/BackgroundMusic");

            ShootArrow = content.Load<SoundEffect>("Assets/Sounds/PlayerSounds/walking_dirt");

            MeleeWeaponSwing = content.Load<SoundEffect>("Assets/Sounds/PlayerSounds/Whoosh2");

            EquipWeapon = content.Load<SoundEffect>("Assets/Sounds/PlayerSounds/equip");

            Explosion = content.Load<SoundEffect>("Assets/Sounds/ParticleSFX/explosion");


        }
    }
}
