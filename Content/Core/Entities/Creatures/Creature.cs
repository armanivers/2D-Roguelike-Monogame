using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities
{
    public abstract class Creature: EntityBasis
    {

        public enum ActionCommand { 
            MOVE,
            ATTACK
        }

        public readonly int maxHealthPoints;
        public int HealthPoints { get; private set; }
        public readonly float attackCooldown;
        public float CooldownTimer { get; set; }
        
        public readonly float movingSpeed;
        public float SpeedModifier { get; set; }
        public bool dead;

        // TODO: Setter fuer die Hitbox fixen (fuer untere Klassen)
        public override Vector2 Position
        {
            get { return base.position; }
            set
            {
                base.position = value;
                hitbox.X = (int)value.X + 17;
                hitbox.Y = (int)value.Y + 14;

                if (animationManager != null)
                {
                    animationManager.Position = base.position;
                }
            }
        }

        public Creature(Vector2 position, int maxHealthPoints, float attackCooldown, float movingSpeed) : base(position){
            this.maxHealthPoints = maxHealthPoints;
            HealthPoints = maxHealthPoints;
            this.attackCooldown = attackCooldown;
            this.movingSpeed = movingSpeed;
            dead = false;
            SpeedModifier = 1;
        }

        public Rectangle GetTileCollisionHitbox()
        {
            return new Rectangle(hitbox.X + 5, hitbox.Y + 5 + 20, hitbox.Width - 10, hitbox.Height - 10 - 20);
        }

        public abstract void Move();

        // Erst wird Aktion bestimmt, dann wird Animation bestimmt

        public bool IsAttacking()
        {
            return CooldownTimer <= attackCooldown;
        }
        protected abstract void DetermineAnimation(Dictionary<string, Animation> animations);

        public override void Update(GameTime gameTime)
        {
            // Die Änderungen in Humanoid müssen bald teilweise hier eingefügt werden
        }

        public void DeductHealthPoints(int damage)
        {
            HealthPoints -= damage;
            System.Diagnostics.Debug.Print("Got Hit!");
            if (HealthPoints <= 0)
            {
                Kill();
            }
        }

        public void Kill()
        {
            HealthPoints = 0;
            dead = true;
        }

        public bool IsDead()
        {
            return dead;
        }
        public abstract void Attack(); 

    }
}