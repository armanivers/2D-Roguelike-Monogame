using System;
using System.Collections.Generic;
using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities
{
    public abstract class Creature: EntityBasis
    {
        private bool hurtTimerStart;
        private float hurtTimer;
        private const int hurtTimerLimit = 2;

        public enum ActionCommand { 
            MOVE,
            ATTACK
        }

        public int maxHealthPoints;
        public int HealthPoints { get; private set; }

        // TODO: Dieser CooldownTimer gilt aktuell für ALLE Angriffsarten → Für jede Angriffsart eigenen Cooldown erstellen
        // (bessere Alternative überlegen, damit nicht immer ein neues Attribut hinzugefügt werden muss)
        public readonly float attackTimespan;
        public float AttackTimeSpanTimer { get; set; }
        
        // TODO: Einen ALLGEMEINEN attacking-Timer, damit nicht während eines Angriffes ein anderer Angriff gestartet werden kann
        // (die Angriffe haben aber dennoch eigene unabhängige Cooldowns (z.B. Melee hat kürzeren Cooldown))
        
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

        public Creature(Vector2 position, int maxHealthPoints, float attackTimespan, float movingSpeed) : base(position){
            EntityManager.AddCreatureEntity(this);
            this.maxHealthPoints = maxHealthPoints;
            HealthPoints = maxHealthPoints;
            this.attackTimespan = attackTimespan;
            this.movingSpeed = movingSpeed;
            dead = false;
            SpeedModifier = 1;
        }

        public Rectangle GetTileCollisionHitbox()
        {
            return new Rectangle(hitbox.X + 5, hitbox.Y + 5 + 20, hitbox.Width - 10, hitbox.Height - 10 - 20);
        }


        public bool IsAttacking()
        {
            // TODO: attackExecution Timer implementieren. CooldownTimer ist fuer Weapons wichtig
            return AttackTimeSpanTimer <= attackTimespan;
        }
        
        public override void Update(GameTime gameTime)
        {
            /** TODO: Vorerst wurde alles in Humanoid eingefügt
            Die Änderungen in Humanoid müssen bald teilweise hier eingefügt werden */
        }

        public void DeductHealthPoints(int damage)
        {

            if(this is Player)
            {
                if (GameSettings.godMode) return;
            }

            HealthPoints -= damage;
            System.Diagnostics.Debug.Print("Got Hit!");
            if (HealthPoints <= 0)
            {
                Kill();
            }
            if(!dead)
            {
                DisplayDamageTaken();
            }
        }

        public void DisplayDamageTaken()
        {
            colour = Color.Red;
            hurtTimerStart = true;
            hurtTimer = 0;
        }

        public void RefreshDamageTakenTimer()
        {
            if(hurtTimerStart)
            {
                if (hurtTimer >= hurtTimerLimit)
                {
                    colour = Color.White;
                    hurtTimerStart = false;
                }
                hurtTimer += 0.1f;
            }
        }

        public void AddHealthPoints(int health)
        {
            HealthPoints += health;
            if(HealthPoints > maxHealthPoints)
            {
                HealthPoints = maxHealthPoints;
            }
        }

        public void Kill()
        {
            HealthPoints = 0;
            // TODO: Hurt abspielen
            dead = true;
        }

        public bool IsDead()
        {
            return dead;
        }

    }
}