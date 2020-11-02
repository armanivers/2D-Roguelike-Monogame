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
        //public static Song Music { get; private set; }
        public static SoundEffect PlayerWalk { get; private set; }

        private static readonly Random rand = new Random();

        // return a random explosion sound
        //private static SoundEffect[] playerWalk
        //public static SoundEffect PlayerWalk { get { return playerWalk[rand.Next(playerWalk.Length)]; } }

        public static void Load(ContentManager content)
        {
            //Music = content.Load<Song>("Sound/Music");
           
            PlayerWalk = content.Load<SoundEffect>("Assets/Sounds/PlayerSounds/walking_dirt");

            // These linq expressions are just a fancy way loading all sounds of each category into an array.
            //playerWalk = Enumerable.Range(1, 8).Select(x => content.Load<SoundEffect>("Sound/explosion-0" + x)).ToArray();
        }
    }
}
