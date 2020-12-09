using _2DRoguelike.Content.Core.Entities.Creatures.Projectiles;
using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities
{
    class Explosion : Projectile
    {

        private const float expireTimer = 3;
        private float timer;

        public Explosion(): base(new Vector2(ControllingPlayer.Player.Instance.Hitbox.X-16, ControllingPlayer.Player.Instance.Hitbox.Y-16),16,16, 7f)
        {
            this.Hitbox = new Rectangle((int)Position.X+16, (int)Position.Y+16, 32, 32);
            this.Acceleration = Vector2.Normalize(InputController.MousePosition - new Vector2(hitbox.X,hitbox.Y));

            this.texture = TextureManager.Explosion;
            animations = new Dictionary<string, Animation>()
            {
                {"Explode", new Animation(TextureManager.Explosion,0,6,0.1f)}
            };
            this.animationManager = new AnimationManager(this,animations.First().Value);
            this.timer = 0;
        }

        private void Explode()
        {
            this.isExpired = true;
            SoundManager.Explosion.Play(GameSettings.soundeffectsLevel, 0.2f, 0);
            Camera.ShakeScreen();

        }

        public void checkCollision()
        {
            foreach(var livingEntity in EntityManager.entities)
            {
                if(livingEntity is Creature && livingEntity != ControllingPlayer.Player.Instance) //&& livingEntity != Player.Player.Instance
                {
                    if (this.hitbox.Intersects(livingEntity.Hitbox))
                    {
                        ((Creature)livingEntity).DeductHealthPoints(1);
                        Explode();
                    }
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            checkCollision();

            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            animationManager.Play(animations["Explode"]);
            Position += Acceleration * flyingSpeed*SpeedModifier;

            if (timer > expireTimer)
            {
                this.isExpired = true;
            }

            animationManager.Update(gameTime);
        }
    }
}
