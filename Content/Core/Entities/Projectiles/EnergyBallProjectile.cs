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
    class EnergyballProjectile: Projectile
    {
        private Humanoid shootingEntity;

        private Queue<Vector2> aimedTargets = new Queue<Vector2>();
        private float timer;
        //private int damage;
        private const float EXPIRATION_TIMER = 3;
        private const float SPEED = 3f;
        private int damage;

        public EnergyballProjectile(Humanoid shootingCreat) : base(new Vector2(shootingCreat.Hitbox.X + 16, shootingCreat.Hitbox.Y + 25), -TextureManager.projectiles.EnergyBall.Width / 2, -TextureManager.projectiles.EnergyBall.Height / 2, SPEED,0.7f)
        {


            this.texture = TextureManager.projectiles.EnergyBall;
            DrawOrigin = TextureSize / 2;
            shootingEntity = shootingCreat;

            damage = ((EnergyballWeapon)shootingEntity.inventory.CurrentWeapon).weaponDamage;
            this.Hitbox = new Rectangle((int)Position.X, (int)Position.Y, (int)(TextureManager.projectiles.EnergyBall.Width * ScaleFactor), (int)(TextureManager.projectiles.EnergyBall.Height * ScaleFactor));
            this.Acceleration = Vector2.Normalize(GetDirection());
            this.rotation = (float)Math.Atan2(Acceleration.Y, Acceleration.X);
            this.timer = 0;
        }

        public Vector2 GetDirection()
        {
            return shootingEntity.GetAttackDirection() - Position;
        }

        private int positionDelay = 0;
        public void SelectNextAimedTargets(Vector2 target)
        {
            aimedTargets.Enqueue(target);
            if (positionDelay == 0)
            {
                positionDelay = 0;
                Acceleration = aimedTargets.Dequeue();

            }
            else
            {
                Acceleration = aimedTargets.Peek();
                positionDelay++;
            }

        }

        private int skipTargetAquisition = 0;
        private float turningAngle = 0f;
        public override void Update(GameTime gameTime)
        {
            if (skipTargetAquisition == 3)
            {
                skipTargetAquisition = 0;
                SelectNextAimedTargets(Vector2.Normalize(GetDirection()));
            }
            else skipTargetAquisition++;

            rotation = (float)Math.Atan2(Acceleration.Y, Acceleration.X) + (turningAngle+=0.1f);
            checkCollision();

            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += Acceleration * flyingSpeed * SpeedModifier;

            if (timer > EXPIRATION_TIMER)
            {
                isExpired = true;
            }
        }



        public void checkCollision()
        {
            if (!WithinOwnHitbox())
            {
                if (CollidesWithSolidTile())
                {
                    isExpired = true;
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
                                    ((Enemy)enemy).DeductHealthPoints((int)(damage * shootingEntity.temporaryDamageMultiplier));
                                    isExpired = true;
                                }
                            }
                        }
                    }
                    else if (shootingEntity is Enemy)
                    {
                        if (Hitbox.Intersects(Player.Instance.Hitbox))
                        {
                            Player.Instance.DeductHealthPoints((int)(damage * shootingEntity.temporaryDamageMultiplier));
                            isExpired = true;
                        }
                    }

                }


            }
        }

        private bool WithinOwnHitbox()
        {
            return shootingEntity.Hitbox.Intersects(Hitbox);
        }

    }
}
