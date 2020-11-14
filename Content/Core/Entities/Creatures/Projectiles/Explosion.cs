using _2DRoguelike.Content.Core.Entities.Creatures.Projectiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities
{
    class Explosion : Projectile
    {

        private bool shouldExplode;
        private const float expireTimer = 3;
        private float timer;

        public Explosion(): base(new Vector2(Player.Player.Instance.Hitbox.X-16, Player.Player.Instance.Hitbox.Y-16),16,16)
        {
            this.Hitbox = new Rectangle((int)Position.X+16, (int)Position.Y+16, 32, 32);
            this.Velocity = Vector2.Normalize(InputController.MousePosition - new Vector2(hitbox.X,hitbox.Y));
            this.speed = 7f;

            this.texture = TextureManager.Explosion;
            animations = new Dictionary<string, Animation>()
            {
                {"Explode", new Animation(TextureManager.Explosion,0,6,0.1f)}
            };
            this.animationManager = new AnimationManager(animations.First().Value);

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
                Position += Velocity * speed;
            }

            if (timer > expireTimer)
            {
                this.isExpired = true;
            }

            animationManager.Update(gameTime);
        }
    }
}
