using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DRoguelike.Content.Core
{
    static class SoundManager
    {
        public static Song BackgroundMusic { get; private set; }
        public static SoundEffect PlayerAttack { get; private set; }
        public static SoundEffect Explosion { get; private set; }

        public static void Load(ContentManager content)
        {
            BackgroundMusic = content.Load<Song>("Assets/Sounds/BackgroundMusic");

            PlayerAttack = content.Load<SoundEffect>("Assets/Sounds/PlayerSounds/walking_dirt");

            Explosion = content.Load<SoundEffect>("Assets/Sounds/ParticleSFX/explosion");
        }
    }
}
