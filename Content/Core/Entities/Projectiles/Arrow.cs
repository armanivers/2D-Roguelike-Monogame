using _2DRoguelike.Content.Core.Entities.Weapons;
using _2DRoguelike.Content.Core.World;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Creatures.Projectiles
{
    class Arrow : Projectile
    {
        private Humanoid shootingEntity;
        private float timer;
        private int DAMAGE;
        private const float EXPIRATION_TIMER = 3;
        private const float SPEED = 10f;

        public Arrow(Humanoid creat) : base(new Vector2(creat.Hitbox.X + 16, creat.Hitbox.Y + 25), -7, +5, SPEED)
        {
            this.texture = TextureManager.projectiles.Arrow;
            DrawOrigin = TextureSize / 2;
            shootingEntity = creat;
            DAMAGE = ((Bow)shootingEntity.CurrentWeapon).weaponDamage;
            this.Hitbox = new Rectangle((int)Position.X, (int)Position.Y, (int)(13 * ScaleFactor), (int)(13 * ScaleFactor));
            this.Acceleration = Vector2.Normalize(GetDirection());
            this.rotation = (float)Math.Atan2(Acceleration.Y, Acceleration.X);
            this.timer = 0;
        }

        private Vector2 GetDirection()
        {
            return shootingEntity.GetAttackDirection() - Position;
        }

        public void checkCollision()
        {
            if (!WithinOwnHitbox())
            {
                if (CollidesWithSolidTile())
                {
                    SpeedModifier = 0f;
                    //Velocity = Vector2.Zero;
                    // isExpired = true; // Mittels expireTimer gelöst
                }
                else
                {
                    HittingLogic();
                }

            }
        }

        private bool WithinOwnHitbox()
        {
            return shootingEntity.Hitbox.Intersects(Hitbox);
        }

        private void HittingLogic()
        {
            if (shootingEntity is ControllingPlayer.Player)
            {
                // TODO: Problem lösen: LevelManager.maps.currentroom.enemylist gilt nur für Player, NICHT für Arrow
                // → wenn Arrow von Flur aus geschossen, ist dieser wirkungslos
                foreach (var enemy in EntityManager.creatures)
                {
                    if (enemy is Enemies.Enemy)
                    {
                        if (Hitbox.Intersects(enemy.Hitbox) && !((Humanoid)enemy).IsDead())
                        {
                            ((Enemies.Enemy)enemy).DeductHealthPoints(DAMAGE);
                            isExpired = true;
                        }
                    }
                }
            }
            else if (shootingEntity is Enemies.Enemy)
            {
                if (Hitbox.Intersects(ControllingPlayer.Player.Instance.Hitbox))
                {
                    ControllingPlayer.Player.Instance.DeductHealthPoints(DAMAGE);
                    isExpired = true;
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
                this.isExpired = true;
            }
        }
    }
}
