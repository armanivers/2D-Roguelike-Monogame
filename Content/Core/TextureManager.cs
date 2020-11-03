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
        public static Texture2D Player { get; private set; }
        public static Texture2D PlayerWalkUpAxe { get; private set; }
        public static Texture2D PlayerWalkDownAxe { get; private set; }
        public static Texture2D PlayerWalkLeftAxe { get; private set; }
        public static Texture2D PlayerWalkRightAxe { get; private set; }

        // Particle Data

        public static Texture2D Explosion { get; private set; }

        public static void Load(ContentManager content)
        {
            // Player Data
            Player = content.Load<Texture2D>("Assets/Graphics/PlayerElements/playerSheet_default");
            PlayerWalkUpAxe = content.Load<Texture2D>("Assets/Graphics/PlayerElements/playerSheet_walk_axe_back");
            PlayerWalkDownAxe = content.Load<Texture2D>("Assets/Graphics/PlayerElements/playerSheet_walk_axe_front");
            PlayerWalkLeftAxe = content.Load<Texture2D>("Assets/Graphics/PlayerElements/playerSheet_walk_axe_left");
            PlayerWalkRightAxe = content.Load<Texture2D>("Assets/Graphics/PlayerElements/playerSheet_walk_axe_right");

            // Particle Data
            Explosion = content.Load<Texture2D>("Assets/Graphics/Particles/explosion");
        }
    }
}
