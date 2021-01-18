using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using _2DRoguelike.Content.Core.Entities.Creatures.Projectiles;
using _2DRoguelike.Content.Core.Entities.Weapons;
using _2DRoguelike.Content.Core.Items.InventoryItems.Weapons;
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


        private Vector2 aimedTarget;
        private float distanceToAimedTarget;

        public BombProjectile(Humanoid creat, float explosionSize = 1f) : base(new Vector2(creat.Hitbox.X + 16, creat.Hitbox.Y + 25), -TextureManager.projectiles.Bomb.Width / 2, -5, SPEED)
        {
            this.texture = TextureManager.projectiles.Bomb;
            DrawOrigin = TextureSize / 2;
            shootingEntity = creat;
            DAMAGE = ((BombWeapon)shootingEntity.inventory.CurrentWeapon).weaponDamage;
            this.Hitbox = new Rectangle((int)Position.X, (int)Position.Y, (int)(TextureManager.projectiles.Bomb.Width * ScaleFactor), (int)(TextureManager.projectiles.Bomb.Width * ScaleFactor));
            this.Acceleration = Vector2.Normalize(GetDirection());
            // this.rotation = (float)Math.Atan2(Acceleration.Y, Acceleration.X);
            this.timer = 0;
            this.explosionSize = explosionSize;

            aimedTarget = shootingEntity.GetAttackDirection();

            distanceToAimedTarget = Vector2.Distance(Position, aimedTarget);
        }

        private Vector2 GetDirection()
        {
            return shootingEntity.GetAttackDirection() - Position;
        }

        private bool WithinOwnHitbox()
        {
            return shootingEntity.Hitbox.Intersects(Hitbox);
        }

        private bool WithinOwnTileCollisionHitbox()
        {
            return shootingEntity.GetTileCollisionHitbox().Intersects(Hitbox);
        }

        public bool checkCollision()
        {
            if (!WithinOwnTileCollisionHitbox())
                if (CollidesWithSolidTile())
                {
                    return true;
                }
            return false;
        }

        public void Explode() {
            new Explosion(Position, 25, explosionSize);
            this.isExpired = true;
        }

        public override void Update(GameTime gameTime)
        {
            if (SpeedModifier != 0 && checkCollision())
            {
                SpeedModifier = 0f;
                timer = EXPIRATION_TIMER / 2f;
            }
            else
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            Position += Acceleration * flyingSpeed * SpeedModifier;

            if (timer > EXPIRATION_TIMER)
            {
                Explode();
            }
            else
            {

                if (timer >= EXPIRATION_TIMER / 2f)
                {
                    const float growFactor = 0.04f;
                    if (scalingUp)
                    {
                        currentColor = Color.Red;
                        if (ScaleFactor >= 1.5f)
                            scalingUp = !scalingUp;
                        else
                            ScaleFactor += growFactor;
                    }
                    else
                    {
                        currentColor = Color.White;
                        if (ScaleFactor <= 1f)
                            scalingUp = !scalingUp;
                        else
                            ScaleFactor -= growFactor;
                    }
                }
                else
                {
                    if (timer >= EXPIRATION_TIMER / 2.5f || (distanceToAimedTarget - Vector2.Distance(Position, aimedTarget)) > distanceToAimedTarget * 0.9f)
                    {
                        SpeedModifier = 0;
                    }
                    else if (timer > EXPIRATION_TIMER / 4f || (distanceToAimedTarget - Vector2.Distance(Position, aimedTarget)) > distanceToAimedTarget * 4f / 5f)
                    {
                        if (SpeedModifier > (1f / 3))
                            SpeedModifier *= 0.8f;
                    }
                    else if (timer > EXPIRATION_TIMER / 5f || (distanceToAimedTarget - Vector2.Distance(Position, aimedTarget)) > distanceToAimedTarget * 3f / 5)
                    {
                        if (SpeedModifier > (2f / 3))
                            SpeedModifier *= 0.9f;
                    }
                }

            }
            //Debug.WriteLine("Position der Bombe: {0}\nZiel: {1}\nGesamtdistanz: {2}\nZurückgelegte Distanz: {3}\nNoch zurückzulegende Distanz: {4}\n---",
            //    Position, aimedTarget, distanceToAimedTarget, distanceToAimedTarget - Vector2.Distance(Position, aimedTarget), Vector2.Distance(Position, aimedTarget));

        }
    }
}
