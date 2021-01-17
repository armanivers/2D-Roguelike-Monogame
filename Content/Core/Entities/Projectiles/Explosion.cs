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

        private Humanoid protectedEntity;
        private const float expireTimer = 1f;
        private float timer;
        private float damageModifier;
        private const int EXPLOSION_DAMAGE = 1;

        public Explosion(Vector2 pos, float explosionDamageModifier = 1f, float size = 1f, Humanoid protectedEntity = null) : this(pos, Vector2.Zero, 0f, explosionDamageModifier, size, protectedEntity)
        {
        }

        public Explosion(Vector2 pos, Vector2 direction, float windSpeed, float explosionDamageModifier = 1f, float size = 1f, Humanoid protectedEntity = null) : base(pos, -TextureManager.projectiles.Explosion.Height / 4, -TextureManager.projectiles.Explosion.Height / 4, windSpeed)
        {
            ScaleFactor = size;
            this.Hitbox = new Rectangle((int)Position.X, (int)Position.Y, (int)(TextureManager.projectiles.Explosion.Height / 2 * ScaleFactor), (int)(TextureManager.projectiles.Explosion.Height / 2 * ScaleFactor));
            this.Acceleration = direction;
            this.damageModifier = explosionDamageModifier;
            this.protectedEntity = protectedEntity;

            this.texture = TextureManager.projectiles.Explosion;
            animations = new Dictionary<string, Animation>()
            {
                {"Explode", new Animation(TextureManager.projectiles.Explosion,0,6,0.1f, false)}
            };
            this.animationManager = new AnimationManager(this, animations.First().Value);
            this.DrawOrigin = new Vector2(texture.Height, texture.Height) / 2;

            this.timer = 0;
            Explode();
        }

        private void Explode()
        {
            SoundManager.Explosion.Play(Game1.gameSettings.soundeffectsLevel, 0.2f, 0);
            Camera.ShakeScreen();
        }

        private float damageCheckDivisor = 4f;
        
        public void checkCollision()
        {
            // TODO: Explosionen können Sachen zerstören, wie Spikes und Projectiles
            if (timer >= expireTimer / damageCheckDivisor)
                if (protectedEntity != Player.Instance && this.Hitbox.Intersects(Player.Instance.Hitbox))
                {
                    var damage = EXPLOSION_DAMAGE * damageModifier;
                    if (protectedEntity != null) damage *= protectedEntity.temporaryDamageMultiplier;
                    Player.Instance.DeductHealthPoints((int)(damage));
                }

            if (timer >= expireTimer / damageCheckDivisor)
                foreach (var livingEntity in EntityManager.creatures)
                {
                    if (livingEntity is Humanoid) //&& livingEntity != Player.Player.Instance
                    {
                        if (protectedEntity != (Humanoid)livingEntity && this.Hitbox.Intersects(livingEntity.Hitbox))
                        {
                            // TODO: je näher man am Explosionsherd steht,desto höher der Schaden
                            var damage = EXPLOSION_DAMAGE * damageModifier;
                            if (protectedEntity != null) damage *= protectedEntity.temporaryDamageMultiplier;
                            ((Humanoid)livingEntity).DeductHealthPoints((int)(damage)) ;
                        }
                    }
                }
        }

        public override void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            checkCollision();

            SetAnimation("Explode");
            Position += Acceleration * flyingSpeed * SpeedModifier;

            if (timer > expireTimer)
            {
                this.isExpired = true;
            }
            if (timer >= expireTimer / damageCheckDivisor)
                damageCheckDivisor--;

            animationManager.Update(gameTime);
        }
    }
}
