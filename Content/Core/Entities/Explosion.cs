using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities
{
    class Explosion : EntityBasis
    {

        private bool shouldExplode;
        private const float expireTimer = 1;
        private float timer;
        
        public Explosion(Vector2 position)
        {
            this.texture = TextureManager.Explosion;
            this.animations = new Dictionary<string, Animation>()
            {
                {"Explode", new Animation(TextureManager.Explosion,6,0.1f)}
            };
            this.animationManager = new AnimationManager(animations.First().Value);
            this.Position = position;
            this.isExpired = false;
            this.shouldExplode = false;
            this.timer = 0;
        }

        public void Explode()
        {
            this.shouldExplode = true;
            SoundManager.Explosion.Play(0.2f, 0.2f, 0);
        }

        public override void Update(GameTime gameTime)
        {
            if (shouldExplode)
            {
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                animationManager.Play(animations["Explode"]);
            }

            if(timer > expireTimer)
            {
                this.isExpired = true;
            }
            animationManager.Update(gameTime);
        }
    }
}
