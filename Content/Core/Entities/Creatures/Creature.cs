using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities
{
    abstract class Creature: EntityBasis
    {
        protected readonly int maxHealthPoints;
        protected int HealthPoints { get; set; }
        public readonly float attackCooldown;
        public float CooldownTimer { get; set; }

        protected readonly float movingSpeed;
        public float speedModifier = 1;
        protected bool dead;

        // TODO: Setter fuer die Hitbox fixen (fuer untere Klassen)
        public override Vector2 Position
        {
            get { return position; }
            set
            {
                position = value;
                hitbox.X = (int)value.X + 17;
                hitbox.Y = (int)value.Y + 14;

                if (animationManager != null)
                {
                    animationManager.Position = position;
                }
            }
        }

        public Creature(int maxHealthPoints, float attackCooldown, float movingSpeed) : base(){
            this.maxHealthPoints = maxHealthPoints;
            this.attackCooldown = attackCooldown;
            this.movingSpeed = movingSpeed;
            this.dead = false;
        }



        public Rectangle GetTileCollisionHitbox()
        {
            return new Rectangle(hitbox.X + 5, hitbox.Y + 5 + 20, hitbox.Width - 10, hitbox.Height - 10 - 20);
        }

        public abstract void Move();

        protected abstract void DetermineAnimation();

        public override void Update(GameTime gameTime)
        {
            Move();
            DetermineAnimation();

            if(CooldownTimer <= attackCooldown)
                CooldownTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            animationManager.Update(gameTime);
        }

        public abstract void Attack(Attack.AttackMethod attackMethod); 
            


        
    }
}