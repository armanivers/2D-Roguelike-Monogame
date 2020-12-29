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

        private const float expireTimer = 1;
        private float timer;

        public Explosion(Vector2 pos, float size = 1f): this(pos, Vector2.Zero, 0f, size)
        {
        }

        public Explosion(Vector2 pos, Vector2 direction, float windSpeed, float size = 1f) : base(pos, -TextureManager.projectiles.Explosion.Height/4, -TextureManager.projectiles.Explosion.Height / 4, windSpeed)
        {
            ScaleFactor = size;
            this.Hitbox = new Rectangle((int)Position.X, (int)Position.Y, (int)(TextureManager.projectiles.Explosion.Height/2 * ScaleFactor), (int)(TextureManager.projectiles.Explosion.Height/2 * ScaleFactor));
            this.Acceleration = direction;

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

        public void checkCollision()
        {
            foreach (var livingEntity in EntityManager.creatures)
            {
                if (livingEntity is Creature) //&& livingEntity != Player.Player.Instance
                {
                    if (this.Hitbox.Intersects(livingEntity.Hitbox))
                    {
                        // TODO: je näher man am Explosionsherd steht,desto höher der Schaden
                        ((Creature)livingEntity).DeductHealthPoints(2);
                    }
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            checkCollision();

            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            SetAnimation("Explode");
            Position += Acceleration * flyingSpeed * SpeedModifier;

            if (timer > expireTimer)
            {
                this.isExpired = true;
            }

            animationManager.Update(gameTime);
        }
    }
}
