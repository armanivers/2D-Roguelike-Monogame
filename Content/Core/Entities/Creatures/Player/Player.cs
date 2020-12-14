using _2DRoguelike.Content.Core.Entities.Actions;
using _2DRoguelike.Content.Core.Entities.Creatures.Projectiles;
using _2DRoguelike.Content.Core.Entities.Loot.Potions;
using _2DRoguelike.Content.Core.Entities.Weapons;
using _2DRoguelike.Content.Core.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace _2DRoguelike.Content.Core.Entities.ControllingPlayer
{
    class Player : Humanoid
    {
        private static Player instance;

        const int WEAPON_SLOT_CNT = 5;
        public int WeaponsInPosession;
        private int currentWeaponPos = 0;

        public bool canInteract;
        private List<LootContainer> interactableContainers;

        private readonly int MAX_LEVEL = 5;
        public int currentXPLevel;
        private int currentXP;
        // ExperiencePoints needed to levelup at each level -> IMPORANT: xp cap amount needs to be atleast the amount of MAX_LEVEL 
        private List<int> xpCap;
        public int CurrentXP
        {
            get { return currentXP; }
            private set
            {
                currentXP = value;
            }
        }
       
        public int CurrentWeaponPos
        {
            get { return currentWeaponPos; }
            set
            {
                currentWeaponPos = value;
            }
        }

        private bool HasWeaponInSlot(int pos)
        {    
            return WeaponInventory[pos] != null;
        }

        private void SetNextWeapon(bool backwards = false) {
            // Nächste gültige Position im Array ermitteln

            int currentPos = CurrentWeaponPos;
            do
            {
                currentPos = currentPos + (!backwards ? 1 : -1);
                if (backwards && currentPos < 0) currentPos = WEAPON_SLOT_CNT - 1;
                else if (currentPos >= WEAPON_SLOT_CNT) currentPos = 0;
                // Debug.WriteLine("---Position: " + currentPos);
            } while (!HasWeaponInSlot(currentPos));
            ChangeCurrentWeaponSlot(currentPos);
        }

        public bool ChangeCurrentWeaponSlot(int value)
        {
            if (HasWeaponInSlot(value))
            {
                CurrentWeaponPos = value;
                CurrentWeapon = WeaponInventory[CurrentWeaponPos];
                SoundManager.EquipWeapon.Play(GameSettings.soundeffectsLevel, 0.2f, 0);
                return true;
            }
            return false;
        }

        public static Player Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Player(LevelManager.maps.getSpawnpoint(), 100, 0.4f, 5);
                }
                return instance;
            }
        }

       

        public Player(Vector2 position, int maxHealthPoints, float movingSpeed, float attackCooldown = 0.2f ) : base(position, maxHealthPoints, attackCooldown, movingSpeed)
        {

            //this.position = new Vector2(2*32, 5*32); bei statischer Map

            canInteract = false;
            interactableContainers = new List<LootContainer>();

            currentXP = currentXPLevel = 0;

            xpCap = new List<int>();
            // Number of xp caps needs to be same or bigger than MAX_LEVEL
            xpCap.Add(10);
            xpCap.Add(20);
            xpCap.Add(20);
            xpCap.Add(30);
            xpCap.Add(35);

            instance = this;
            WeaponInventory = new Weapon[WEAPON_SLOT_CNT];
            AddToWeaponInventory(new Fist(this));
            // add weapons manually
            /* 
            AddToWeaponInventory(new Dagger(this));
            AddToWeaponInventory(new Axe(this));
            AddToWeaponInventory(new Bow(this));
            */
            ChangeCurrentWeaponSlot(0); 

            texture = TextureManager.Player_Idle;

            const bool NO_LOOP = false;
            const bool PRIORITIZED = true;
            const bool REVERSE = true;
            const float frameSpeed = 0.09f;
            float tmpFrameSpeed;
            animations = new Dictionary<string, Animation>()
            {
                // Idle
                {"IdleUp", new Animation(TextureManager.Player_Idle,0,6,tmpFrameSpeed=frameSpeed*2.5f)},
                {"IdleLeft", new Animation(TextureManager.Player_Idle,1,6,tmpFrameSpeed)},
                {"IdleDown", new Animation(TextureManager.Player_Idle,2,6,tmpFrameSpeed)},
                {"IdleRight", new Animation(TextureManager.Player_Idle,3,6,tmpFrameSpeed)},
                
                // Laufbewegung
                {"WalkUp_Axe", new Animation(TextureManager.Player_Walk_Axe,0,9,tmpFrameSpeed=frameSpeed)},
                {"WalkLeft_Axe",new Animation(TextureManager.Player_Walk_Axe,1,9,tmpFrameSpeed)},
                {"WalkDown_Axe", new Animation(TextureManager.Player_Walk_Axe,2,9,tmpFrameSpeed)},
                {"WalkRight_Axe", new Animation(TextureManager.Player_Walk_Axe,3,9,tmpFrameSpeed)},

                {"WalkUp_Fist", new Animation(TextureManager.Player_Walk_Fist,0,9,tmpFrameSpeed=frameSpeed)},
                {"WalkLeft_Fist",new Animation(TextureManager.Player_Walk_Fist,1,9,tmpFrameSpeed)},
                {"WalkDown_Fist", new Animation(TextureManager.Player_Walk_Fist,2,9,tmpFrameSpeed)},
                {"WalkRight_Fist", new Animation(TextureManager.Player_Walk_Fist,3,9,tmpFrameSpeed)},

                {"WalkUp_Dagger", new Animation(TextureManager.Player_Walk_Dagger,0,9,tmpFrameSpeed=frameSpeed)},
                {"WalkLeft_Dagger",new Animation(TextureManager.Player_Walk_Dagger,1,9,tmpFrameSpeed)},
                {"WalkDown_Dagger", new Animation(TextureManager.Player_Walk_Dagger,2,9,tmpFrameSpeed)},
                {"WalkRight_Dagger", new Animation(TextureManager.Player_Walk_Dagger,3,9,tmpFrameSpeed)},
                
                 // Melee-            
                {"SlashUp_Axe", new Animation(TextureManager.Player_Slash_Axe,0,6,(tmpFrameSpeed=frameSpeed*0.5f), NO_LOOP, PRIORITIZED, REVERSE)},
                {"SlashLeft_Axe",new Animation(TextureManager.Player_Slash_Axe,1,6,tmpFrameSpeed, NO_LOOP, PRIORITIZED, REVERSE)},
                {"SlashDown_Axe", new Animation(TextureManager.Player_Slash_Axe,2,6,tmpFrameSpeed, NO_LOOP, PRIORITIZED, REVERSE)},
                {"SlashRight_Axe", new Animation(TextureManager.Player_Slash_Axe,3,6,tmpFrameSpeed, NO_LOOP, PRIORITIZED, REVERSE)},

                {"SlashUp_Dagger", new Animation(TextureManager.Player_Slash_Dagger,0,6,(tmpFrameSpeed=frameSpeed*0.5f), NO_LOOP, PRIORITIZED, REVERSE)},
                {"SlashLeft_Dagger",new Animation(TextureManager.Player_Slash_Dagger,1,6,tmpFrameSpeed, NO_LOOP, PRIORITIZED, REVERSE)},
                {"SlashDown_Dagger", new Animation(TextureManager.Player_Slash_Dagger,2,6,tmpFrameSpeed, NO_LOOP, PRIORITIZED, REVERSE)},
                {"SlashRight_Dagger", new Animation(TextureManager.Player_Slash_Dagger,3,6,tmpFrameSpeed, NO_LOOP, PRIORITIZED, REVERSE)},

                {"SlashUp_Fist", new Animation(TextureManager.Player_Slash_Fist,0,6,(tmpFrameSpeed=frameSpeed*0.5f), NO_LOOP, PRIORITIZED, REVERSE)},
                {"SlashLeft_Fist",new Animation(TextureManager.Player_Slash_Fist,1,6,tmpFrameSpeed, NO_LOOP, PRIORITIZED, REVERSE)},
                {"SlashDown_Fist", new Animation(TextureManager.Player_Slash_Fist,2,6,tmpFrameSpeed, NO_LOOP, PRIORITIZED, REVERSE)},
                {"SlashRight_Fist", new Animation(TextureManager.Player_Slash_Fist,3,6,tmpFrameSpeed, NO_LOOP, PRIORITIZED, REVERSE)},
                
                // Pfeil Schießen
                {"ShootUp", new Animation(TextureManager.Player_Shoot,0,13,(tmpFrameSpeed=frameSpeed*0.3f),NO_LOOP, PRIORITIZED)},
                {"ShootLeft",new Animation(TextureManager.Player_Shoot,1,13,tmpFrameSpeed, NO_LOOP, PRIORITIZED)},
                {"ShootDown", new Animation(TextureManager.Player_Shoot,2,13,tmpFrameSpeed, NO_LOOP, PRIORITIZED)},
                {"ShootRight", new Animation(TextureManager.Player_Shoot,3,13,tmpFrameSpeed, NO_LOOP, PRIORITIZED)},

                // Todesanimation
                {"Die", new Animation(TextureManager.Player_Hurt,0,6,frameSpeed*2f, NO_LOOP, PRIORITIZED)}
            };
            animationManager = new AnimationManager(this,animations["IdleDown"]);
        }
        public void DeleteInstance()
        {
            instance = null;
        }

        public override Vector2 GetDirection()
        {
            return InputController.GetDirection();
        }

        public override Vector2 GetAttackDirection()
        {
            return InputController.MousePosition;
        }

        public override Vector2 GetAttackLineOfSight()
        {
            var differenz = InputController.MousePosition - new Vector2(HitboxCenter.X,HitboxCenter.Y);
            var angle = System.Math.Atan2(differenz.Y, differenz.X);

            return CalculateDirection(angle);


        }

        public override Action DetermineAction()
        {
            if (InputController.IsMouseButtonPressed() && !IsAttacking() && CanAttack())
            // TODO: if(weapon.rangeAttack) return RangeAttack else return Melee ...
            {
                if (CurrentWeapon is LongRange) {
                    CurrentWeapon.CooldownTimer = 0;
                                return new RangeAttack(this);

                }
                if (CurrentWeapon is ShortRange) {
                    CurrentWeapon.CooldownTimer = 0;

                    return new Melee(this);

                }
            }
            return new Move(this);

        }

        public override void Update(GameTime gameTime)
        {
            UpdateCurrentWeaponPos();
            CheckLootCollision();
            InteractWithObject();
            base.Update(gameTime);
        }

        public void AddExperiencePoints(int xp)
        {
            CurrentXP += xp;
            
            if (currentXPLevel >= MAX_LEVEL)
                return;

            if(currentXP >= xpCap[currentXPLevel]){
                StatiscticsManager.LevelUp();
                while (currentXPLevel < MAX_LEVEL && currentXP >= xpCap[currentXPLevel])
                {
                    currentXPLevel++;
                    DetermineLevelupAward(currentXPLevel);
                    // add remaining xp after levelup to next level
                    currentXP = currentXP - xpCap[currentXPLevel - 1];
                    // play levelup sound + particle effect + (maybe reward: weapon/hp heal)
                }
            }
        }

        public void DetermineLevelupAward(int level)
        {
            switch(level)
            {
                case 1:
                    // Level 1 award = increase max hp by 20
                    maxHealthPoints += 20;
                    break;
                case 2:
                    maxHealthPoints += 50;
                    break;
                case 3:
                    break;
                default:
                    break;
            }
        }

        public void CheckLootCollision()
        {
            /* schoener wenn interaktion mit Container in seperate Methode geprueft wird, jedoch wenn wir interaktion und collision
             gleichzeitig pruefen, muessen wir die Loot liste nicht doppelt durchgehen, IDEE: liste mit container/objekte die interactable sind
            */

            canInteract = false;
            interactableContainers.Clear();

            // TODO: mit LevelManager.currentroom.entities ersetzen 
            foreach (var loot in EntityManager.loots)
            {
                if(hitbox.Intersects(loot.hitbox))    
                {     
                    if(loot is LootContainer && ((LootContainer)loot).Closed) 
                    { 
                        interactableContainers.Add((LootContainer)loot);    
                        canInteract = true;
                    }
                    else
                    {
                        loot.OnContact();
                    }

                }
            }
        }

        public void InteractWithObject()
        {
            if(InputController.IsKeyPressed(Keys.F))
            {
                foreach (var lootContainer in interactableContainers)
                {
                    lootContainer.OnContact();
                }
            }
            interactableContainers.Clear();
        }

        public bool GameOver()
        {
            return isExpired;
        }

        public override void AddToWeaponInventory(Weapon weapon)
        {
            // sollte nicht vorkommen
            if (WeaponsInPosession >= WEAPON_SLOT_CNT)
            {
                return;
            }

            if (WeaponInventory[weapon.INVENTORY_SLOT] == null)
            {
                WeaponInventory[weapon.INVENTORY_SLOT] = weapon;
                WeaponsInPosession++;
                StatiscticsManager.NewWeaponRecieved();
            }
            else
            {
                StatiscticsManager.WeaponRecieved();
            }

        }

        public bool CanAttack()
        {
            return !IsAttacking() && !CurrentWeapon.InUsage();
        }

        public void UpdateCurrentWeaponPos() {
            // TODO: Hier anhand von Eingaben prüfen, ob Waffenwechsel stattgefunden hat
            // Beispiele: Num 1 - WEAPON_SLOT_CNT und Scrollen
            if (InputController.IsKeyPressed(Keys.PageUp) || InputController.IsMouseScrolledDown())
                SetNextWeapon();
            else if (InputController.IsKeyPressed(Keys.PageDown) || InputController.IsMouseScrolledUp())
                SetNextWeapon(true);
            else if (InputController.IsKeyPressed(Keys.NumPad0))
                ChangeCurrentWeaponSlot(0);
            else if (InputController.IsKeyPressed(Keys.NumPad1))
                ChangeCurrentWeaponSlot(1);
            else if (InputController.IsKeyPressed(Keys.NumPad2))
                ChangeCurrentWeaponSlot(2);
            else if (InputController.IsKeyPressed(Keys.NumPad3))
                ChangeCurrentWeaponSlot(3);
        }
    }
}