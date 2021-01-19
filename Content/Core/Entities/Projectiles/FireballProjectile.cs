using System;
using System.Collections.Generic;
using System.Text;
using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.Entities.Creatures.Enemies;
using _2DRoguelike.Content.Core.Entities.Creatures.Projectiles;
using _2DRoguelike.Content.Core.Items.InventoryItems.Weapons;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.Projectiles
{
    class FireballProjectile : Projectile
    {
        // Idee: QUEUE für Positionsbestimmmung des Targets für fließendere Bewegung???

        private Humanoid shootingEntity;

        private float timer;
        //private int damage;
        private const float EXPIRATION_TIMER = 3;
        private const float SPEED = 4f;
        private int impactDamage;
        private float explosionDamageMultiplier = 20;

        public FireballProjectile(Humanoid shootingCreat, float explosionDamageMultiplier = 20f) : base(new Vector2(shootingCreat.Hitbox.X + 16, shootingCreat.Hitbox.Y + 25), -TextureManager.projectiles.Fireball.Width / 4, -TextureManager.projectiles.Fireball.Height / 4, SPEED)
        {
            this.ScaleFactor = 0.8f;

            this.texture = TextureManager.projectiles.Fireball;
            DrawOrigin = TextureSize / 2;
            shootingEntity = shootingCreat;

            impactDamage = ((FireballWeapon)shootingEntity.inventory.CurrentWeapon).weaponDamage;
            this.Hitbox = new Rectangle((int)Position.X, (int)Position.Y, (int)(20 * ScaleFactor), (int)(20 * ScaleFactor));
            this.Acceleration = Vector2.Normalize(GetDirection());
            this.rotation = (float)Math.Atan2(Acceleration.Y, Acceleration.X);
            this.timer = 0;
            this.explosionDamageMultiplier = explosionDamageMultiplier;
        }

        public Vector2 GetDirection()
        {
            return shootingEntity.GetAttackDirection() - Position;
        }

      
        
        public override void Update(GameTime gameTime)
        {
            
            checkCollision();

            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += Acceleration * flyingSpeed * SpeedModifier;

            if (timer > EXPIRATION_TIMER)
            {
                Incinerate();
            }
        }



        public void checkCollision()
        {
            if (!WithinOwnHitbox())
            {
                if (CollidesWithSolidTile())
                {
                    Incinerate();
                }
                else
                {
                    if (shootingEntity is Player)
                    {
                        foreach (var enemy in EntityManager.creatures)
                        {
                            if (enemy is Enemy)
                            {
                                if (Hitbox.Intersects(enemy.Hitbox) && !((Humanoid)enemy).IsDead())
                                {
                                    ((Enemy)enemy).DeductHealthPoints((int)(impactDamage * shootingEntity.temporaryDamageMultiplier));
                                    Incinerate();
                                }
                            }
                        }
                    }
                    else if (shootingEntity is Enemy)
                    {
                        if (Hitbox.Intersects(Player.Instance.Hitbox))
                        {
                            Player.Instance.DeductHealthPoints((int)(impactDamage*shootingEntity.temporaryDamageMultiplier));
                            Incinerate(shootingEntity);
                        }
                    }

                    foreach(var projectile in EntityManager.projectiles)
                    {
                        if (projectile is BombProjectile && Hitbox.Intersects(projectile.Hitbox))
                            ((BombProjectile)projectile).Explode();
                    }
                }


            }
        }

        private bool WithinOwnHitbox()
        {
            return shootingEntity.Hitbox.Intersects(Hitbox);
        }


        public void Incinerate()
        {
            Incinerate(null);
        }

        private void Incinerate(Humanoid protectedEntity)
        {
            new Explosion(Position, explosionDamageMultiplier, 1, protectedEntity);
            //Velocity = Vector2.Zero;
            isExpired = true;
        }
    }
}
