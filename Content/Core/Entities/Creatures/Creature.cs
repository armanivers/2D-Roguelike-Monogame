using _2DRoguelike.Content.Core.Entities.ControllingPlayer;
using _2DRoguelike.Content.Core.GameDebugger;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace _2DRoguelike.Content.Core.Entities
{
    public abstract class Creature : EntityBasis
    {
        private bool invincible = false;
        public bool Invincible { get => invincible; set => invincible = value; }
        private bool hurtTimerStart;
        private float hurtTimer;
        private const int hurtTimerLimit = 2;

        public const float MAX_MANA = 100;
        public float Mana { get; set; }
        public const float MANA_REGENERATION_SPEED = 0.56f; // 3 sekunden (100/(60*3))

        public int maxHealthPoints;
        public int HealthPoints { get; set; }

        public readonly float attackTimespan;
        public float AttackTimeSpanTimer { get; set; }


        public readonly float movingSpeed;
        public float SpeedModifier { get; set; }
        public bool dead;

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
            Mana = 0;
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
            return AttackTimeSpanTimer <= attackTimespan;
        }

        public override void Update(GameTime gameTime)
        {
            DrawHitboxes();
            RegenerateMana();
            /** TODO: Vorerst wurde alles in Humanoid eingefügt
            Die Änderungen in Humanoid müssen bald te-*ilweise hier eingefügt werden */
        }

        public void RegenerateMana()
        {
            // speed 0.1f means full regeneration is ~16 seconds
            if (Mana < MAX_MANA)
            {
                Mana += MANA_REGENERATION_SPEED;
                if (Mana > MAX_MANA)
                    Mana = MAX_MANA;

                // Debug.WriteLine("Mana is at: " + Mana);
            }
        }

        public void DeductMana(float manaAmount)
        {
            Mana -= manaAmount;

            // sanity check
            if (Mana < 0)
                Mana = 0;
            // Debug.WriteLine("Mana is at: " + Mana);
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