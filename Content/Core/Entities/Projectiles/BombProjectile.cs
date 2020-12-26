using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using _2DRoguelike.Content.Core.Entities.Creatures.Projectiles;
using _2DRoguelike.Content.Core.Entities.Weapons;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.Projectiles
{
    class BombProjectile : Projectile
    {
        private Humanoid shootingEntity;
        private float timer;
        private int DAMAGE;
        private const float EXPIRATION_TIMER = 3;
        private const float SPEED = 5f;
        private bool scalingUp = true;
        private float explosionSize;

        public BombProjectile(Humanoid creat, float explosionSize = 1f) : base(new Vector2(creat.Hitbox.X + 16, creat.Hitbox.Y + 25), -TextureManager.Bomb.Width / 2, -5, SPEED)
        {
            this.texture = TextureManager.Bomb;
            DrawOrigin = TextureSize / 2;
            shootingEntity = creat;
            DAMAGE = ((BombWeapon)shootingEntity.CurrentWeapon).weaponDamage;
            this.Hitbox = new Rectangle((int)Position.X, (int)Position.Y, (int)(TextureManager.Bomb.Width * ScaleFactor), (int)(TextureManager.Bomb.Width * ScaleFactor));
            this.Acceleration = Vector2.Normalize(GetDirection());
            // this.rotation = (float)Math.Atan2(Acceleration.Y, Acceleration.X);
            this.timer = 0;
            this.explosionSize = explosionSize;
        }

        private Vector2 GetDirection()
        {
            return shootingEntity.GetAttackDirection() - Position;
        }

        private bool WithinOwnHitbox()
        {
            return shootingEntity.Hitbox.Intersects(Hitbox);
        }

        public void checkCollision()
        {
            if (!WithinOwnHitbox())
            {
                if (CollidesWithSolidTile())
                {
                    SpeedModifier = 0f;
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            checkCollision();

            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += Acceleration * flyingSpeed * SpeedModifier;

            if (timer > EXPIRATION_TIMER)
            {
                // TODO: Spawn explosion
                new Explosion(Position, explosionSize);
                this.isExpired = true;
            }
            else if (timer > EXPIRATION_TIMER / 2)
            {
                const float growFactor = 0.04f;
                if (scalingUp)
                {
                    colour = Color.Red;
                    if (ScaleFactor >= 1.5f)
                        scalingUp = !scalingUp;
                    else
                        ScaleFactor += growFactor;
                }
                else {
                    colour = Color.White;
                    if (ScaleFactor <= 1f)
                        scalingUp = !scalingUp;
                    else
                        ScaleFactor -= growFactor;
                }
            }

            else if (timer > EXPIRATION_TIMER / 4)
            {
                if (SpeedModifier > 1 / 3)
                    SpeedModifier *= 0.8f;
            }
            else if (timer > EXPIRATION_TIMER / 5)
            {
                if (SpeedModifier > 2 / 3)
                    SpeedModifier *= 0.9f;
            }

        }
    }
}
