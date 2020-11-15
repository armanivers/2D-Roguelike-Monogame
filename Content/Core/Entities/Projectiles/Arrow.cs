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
        private const float expireTimer = 3;
        private float timer;

        public Arrow() : base(new Vector2(Player.Player.Instance.Hitbox.X+16, Player.Player.Instance.Hitbox.Y+16), -7, +5)
        {
            this.Hitbox = new Rectangle((int)Position.X, (int)Position.Y, 32, 13);
            this.Velocity = Vector2.Normalize(InputController.MousePosition - Position);
            this.orientation = (float)Math.Atan2(Velocity.Y, Velocity.X);
            this.speed = 1f;
            this.texture = TextureManager.Arrow;
            this.timer = 0;
        }

        public void checkCollision()
        {
            foreach (var livingEntity in EntityManager.entities)
            {
                //&& livingEntity != Player.Player.Instance pruefen damit das projectile dem spieler keinen schaden gibt
                if (livingEntity is Creature && livingEntity != Player.Player.Instance) 
                {
                    if (hitbox.Intersects(livingEntity.Hitbox))
                    {
                        ((Creature)livingEntity).GetHit(15);
                        isExpired = true;
                    }
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            checkCollision();

            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += Velocity * speed;
            
            if (timer > expireTimer)
            {
                this.isExpired = true;
            }
        }
    }
}
