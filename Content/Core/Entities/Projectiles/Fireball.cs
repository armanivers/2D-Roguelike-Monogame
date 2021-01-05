using System;
using System.Collections.Generic;
using System.Text;
using _2DRoguelike.Content.Core.Entities.Creatures.Enemies;
using _2DRoguelike.Content.Core.Entities.Creatures.Projectiles;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.Projectiles
{
    class Fireball : Projectile
    {
        private Humanoid shootingEntity;

        private float timer;
        //private int damage;
        private const float EXPIRATION_TIMER = 3;
        private const float SPEED = 5f;

        public Fireball(Humanoid shootingCreat) : base(new Vector2(shootingCreat.Hitbox.X + 16, shootingCreat.Hitbox.Y + 25), -7, +5, SPEED)
        {
            this.texture = TextureManager.projectiles.Fireball;
            DrawOrigin = TextureSize / 2;
            shootingEntity = shootingCreat;
            this.Hitbox = new Rectangle((int)Position.X, (int)Position.Y, (int)(13 * ScaleFactor), (int)(13 * ScaleFactor));
            this.Acceleration = Vector2.Normalize(GetDirection());
            this.rotation = (float)Math.Atan2(Acceleration.Y, Acceleration.X);
            this.timer = 0;
        }

        public Vector2 GetDirection()
        {
            return shootingEntity.GetAttackDirection() - Position;
        }

        public override void Update(GameTime gameTime)
        {
            Acceleration = Vector2.Normalize(GetDirection());
            checkCollision();

            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += Acceleration * flyingSpeed * SpeedModifier;

            if (timer > EXPIRATION_TIMER)
            {
                this.isExpired = true;
            }
        }

        public void checkCollision()
        {
            if (!WithinOwnHitbox())
            {
                if (CollidesWithSolidTile())
                {
                    new Explosion(Position);
                    //Velocity = Vector2.Zero;
                    isExpired = true;
                }
                else
                {
                    if (shootingEntity is ControllingPlayer.Player)
                    {
                        foreach (var enemy in EntityManager.creatures)
                        {
                            if (enemy is Enemy)
                            {
                                if (Hitbox.Intersects(enemy.Hitbox) && !((Humanoid)enemy).IsDead())
                                {
                                    new Explosion(Position);
                                    //Velocity = Vector2.Zero;
                                    isExpired = true;
                                }
                            }
                        }
                    }
                    else if (shootingEntity is Enemy)
                    {
                        if (Hitbox.Intersects(ControllingPlayer.Player.Instance.Hitbox))
                        {
                            new Explosion(Position);
                            //Velocity = Vector2.Zero;
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


        // Idee: QUEUE für Positionsbestimmmung des Targets für fließendere Bewegung???
    }
}
