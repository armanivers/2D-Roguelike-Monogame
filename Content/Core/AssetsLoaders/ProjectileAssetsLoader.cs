using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.AssetsLoaders
{
    class ProjectileAssetsLoader
    {
        // Particle Data

        public Texture2D Explosion { get; private set; }

        // Projectile Data
        public Texture2D Arrow { get; private set; }
        public Texture2D Bomb { get; private set; }
        public Texture2D Fireball { get; private set; }

        public void Load(ContentManager content)
        {
            // Particle Data
            Explosion = content.Load<Texture2D>("Assets/Graphics/Particles/explosion");

            // Projectile Data
            Arrow = content.Load<Texture2D>("Assets/Graphics/Projectiles/Arrow");
            Bomb = content.Load<Texture2D>("Assets/Graphics/Projectiles/Bomb");
            Fireball = content.Load<Texture2D>("Assets/Graphics/Projectiles/fireball");
        }
    }
}
