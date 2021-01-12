using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.GameDebugger;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities
{
    public abstract class Creature : EntityBasis
    {
        private bool invincible = false;
        public bool Invincible { get => invincible; set => invincible = value; }
        private bool hurtTimerStart;
        private float hurtTimer;
        private const int hurtTimerLimit = 2;


        public int maxHealthPoints;
        public int HealthPoints { get; set; }

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
            get { return base.Position; }
            set
            {
                base.Position = value;
                hitbox.X = (int)(value.X + 17 * ScaleFactor);
                hitbox.Y = (int)(value.Y + 14 * ScaleFactor);

                if (animationManager != null)
                {
                    animationManager.Position = base.Position;
                }
            }
        }



        public Creature(Vector2 position, int maxHealthPoints, float attackTimespan, float movingSpeed, float scaleFactor = 1f) : base(position, scaleFactor)
        {

            if (this is Player)
            {
                EntityManager.player = (Player)this;
            }
            else
            {
                EntityManager.AddCreatureEntity(this);
            }

            this.maxHealthPoints = maxHealthPoints;
            HealthPoints = maxHealthPoints;
            this.attackTimespan = attackTimespan;
            this.movingSpeed = movingSpeed;
            dead = false;
            SpeedModifier = 1;
        }

        public Rectangle GetTileCollisionHitbox()
        {
            return new Rectangle(Hitbox.X + 5, Hitbox.Y + 5 + 20, Hitbox.Width - 10, Hitbox.Height - 10 - 20);
        }


        public bool IsAttacking()
        {
            // TODO: attackExecution Timer implementieren. CooldownTimer ist fuer Weapons wichtig
            return AttackTimeSpanTimer <= attackTimespan;
        }

        public override void Update(GameTime gameTime)
        {
            DrawHitboxes();
            /** TODO: Vorerst wurde alles in Humanoid eingefügt
            Die Änderungen in Humanoid müssen bald teilweise hier eingefügt werden */
        }

        public virtual bool IsInvincible()
        {
            return Invincible;
        }

        public virtual bool DeductHealthPoints(int damage)
        {
            if (dead)
                return false;

            if (this is Player)
            {
                if (IsInvincible())
                {
                    return false;
                }
                SoundManager.PlayerHurt.Play(Game1.gameSettings.soundeffectsLevel, 0.1f, 0);
            }

            if (!IsInvincible())
            {
                HealthPoints -= damage;
                if (HealthPoints <= 0)
                {
                    Kill();
                }
                if (!dead)
                {
                    DisplayDamageTaken();
                }
                return true;
            }
            return false;

        }

        public void DisplayDamageTaken()
        {
            currentColor = Color.Red;
            hurtTimerStart = true;
            hurtTimer = 0;
        }

        public void RefreshDamageTakenTimer()
        {
            if (hurtTimerStart)
            {
                if (hurtTimer >= hurtTimerLimit)
                {
                    currentColor = initialColor;
                    hurtTimerStart = false;
                }
                hurtTimer += 0.1f;
            }
        }

        public void AddHealthPoints(int health)
        {
            HealthPoints += health;
            if (HealthPoints > maxHealthPoints)
            {
                HealthPoints = maxHealthPoints;
            }
        }

        public virtual void Kill()
        {
            HealthPoints = 0;
            dead = true;
        }

        public bool IsDead()
        {
            return dead;
        }

        protected virtual void DrawHitboxes()
        {
            GameDebug.AddToBoxDebugBuffer(GetTileCollisionHitbox(), Color.Black);
            GameDebug.AddToBoxDebugBuffer(Hitbox, Color.DarkBlue);
        }

    }
}