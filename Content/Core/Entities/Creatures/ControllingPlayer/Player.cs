using _2DRoguelike.Content.Core.Entities.Actions;
using _2DRoguelike.Content.Core.Entities.AI.Actions;
using _2DRoguelike.Content.Core.Entities.Creatures.ControllingPlayer;
using _2DRoguelike.Content.Core.Entities.Creatures.Projectiles;
using _2DRoguelike.Content.Core.Entities.Interactables.NPCs;
using _2DRoguelike.Content.Core.Entities.Interactables.WorldObjects;
using _2DRoguelike.Content.Core.Entities.Loot;
using _2DRoguelike.Content.Core.Entities.Loot.Potions;
using _2DRoguelike.Content.Core.Entities.Weapons;
using _2DRoguelike.Content.Core.Items.InventoryItems.Weapons;
using _2DRoguelike.Content.Core.Items.ObtainableItems;
using _2DRoguelike.Content.Core.UI;
using _2DRoguelike.Content.Core.World;
using _2DRoguelike.Content.Core.World.Rooms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static _2DRoguelike.Content.Core.UI.MessageFactory.Message;

namespace _2DRoguelike.Content.Core.Entities.ControllingPlayer
{
    public class Player : Humanoid
    {
        private static Player instance;

        public bool canInteract;
        private List<InteractableBase> interactableObjects;

        public readonly int MAX_LEVEL = 5;
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

        public double LevelupPercentage
        {
            get
            {
                if (currentXPLevel < MAX_LEVEL)
                {

                    return ((double)currentXP) / xpCap[currentXPLevel];
                }
                else
                {
                    // max level reached, return 100% always
                    return 1.0;
                }
            }
        }

       

        public static Player Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Player(LevelManager.currentmap.getSpawnpoint(), 100, 0.4f, 5);
                }
                return instance;
            }
        }

        public Player(Vector2 position, int maxHealthPoints, float movingSpeed, float attackCooldown = 0.2f) : base(position, maxHealthPoints, attackCooldown, movingSpeed)
        {

            //this.position = new Vector2(2*32, 5*32); bei statischer Map

            inventory = new PlayerInventory(this);
            inventory.AddToWeaponInventory(new Fist(this));
            inventory.ChangeCurrentWeaponSlot(0);

            canInteract = false;
            interactableObjects = new List<InteractableBase>();

            currentXP = currentXPLevel = 0;

            // Number of xp caps needs to be same or bigger than MAX_LEVEL
            xpCap = new List<int>()
            {
                10, // Level 1
                20, // Level 2
                30, // Level 3
                30, // Level 4
                40  // Level 5 MAX
            };



            instance = this;

            // add weapons manually
            /*
            AddToWeaponInventory(new Dagger(this));
            AddToWeaponInventory(new Axe(this));
            AddToWeaponInventory(new Bow(this));
            AddToWeaponInventory(new BombWeapon(this));
            AddToWeaponInventory(new Spear(this));
            */
            //AddToWeaponInventory(new FireballWeapon(this));


            texture = TextureManager.Player_Idle;

            const bool NO_LOOP = false;
            const bool PRIORITIZED = true;
            const bool REVERSE = true;
            const float FRAME_SPEED = 0.09f;
            float tmpFrameSpeed;
            animations = new Dictionary<string, Animation>()
            {
                // Idle
                {"IdleUp", new Animation(TextureManager.Player_Idle,0,6,tmpFrameSpeed=FRAME_SPEED*2.5f)},
                {"IdleLeft", new Animation(TextureManager.Player_Idle,1,6,tmpFrameSpeed)},
                {"IdleDown", new Animation(TextureManager.Player_Idle,2,6,tmpFrameSpeed)},
                {"IdleRight", new Animation(TextureManager.Player_Idle,3,6,tmpFrameSpeed)},
                
                // Laufbewegung
                {"WalkUp_Axe", new Animation(TextureManager.Player_Walk_Axe,0,9,tmpFrameSpeed=FRAME_SPEED)},
                {"WalkLeft_Axe",new Animation(TextureManager.Player_Walk_Axe,1,9,tmpFrameSpeed)},
                {"WalkDown_Axe", new Animation(TextureManager.Player_Walk_Axe,2,9,tmpFrameSpeed)},
                {"WalkRight_Axe", new Animation(TextureManager.Player_Walk_Axe,3,9,tmpFrameSpeed)},

                {"WalkUp_Fist", new Animation(TextureManager.Player_Walk_Fist,0,9,tmpFrameSpeed=FRAME_SPEED)},
                {"WalkLeft_Fist",new Animation(TextureManager.Player_Walk_Fist,1,9,tmpFrameSpeed)},
                {"WalkDown_Fist", new Animation(TextureManager.Player_Walk_Fist,2,9,tmpFrameSpeed)},
                {"WalkRight_Fist", new Animation(TextureManager.Player_Walk_Fist,3,9,tmpFrameSpeed)},

                {"WalkUp_Dagger", new Animation(TextureManager.Player_Walk_Dagger,0,9,tmpFrameSpeed=FRAME_SPEED)},
                {"WalkLeft_Dagger",new Animation(TextureManager.Player_Walk_Dagger,1,9,tmpFrameSpeed)},
                {"WalkDown_Dagger", new Animation(TextureManager.Player_Walk_Dagger,2,9,tmpFrameSpeed)},
                {"WalkRight_Dagger", new Animation(TextureManager.Player_Walk_Dagger,3,9,tmpFrameSpeed)},

                {"WalkUp_Spear", new Animation(TextureManager.Player_Walk_Spear,0,9,tmpFrameSpeed=FRAME_SPEED)},
                {"WalkLeft_Spear",new Animation(TextureManager.Player_Walk_Spear,1,9,tmpFrameSpeed)},
                {"WalkDown_Spear", new Animation(TextureManager.Player_Walk_Spear,2,9,tmpFrameSpeed)},
                {"WalkRight_Spear", new Animation(TextureManager.Player_Walk_Spear,3,9,tmpFrameSpeed)},
                  
                 // Melee-Angriff            
                {"SlashUp_Axe", new Animation(TextureManager.Player_Slash_Axe,0,6,(tmpFrameSpeed=FRAME_SPEED*0.5f), NO_LOOP, PRIORITIZED, REVERSE)},
                {"SlashLeft_Axe",new Animation(TextureManager.Player_Slash_Axe,1,6,tmpFrameSpeed, NO_LOOP, PRIORITIZED, REVERSE)},
                {"SlashDown_Axe", new Animation(TextureManager.Player_Slash_Axe,2,6,tmpFrameSpeed, NO_LOOP, PRIORITIZED, REVERSE)},
                {"SlashRight_Axe", new Animation(TextureManager.Player_Slash_Axe,3,6,tmpFrameSpeed, NO_LOOP, PRIORITIZED, REVERSE)},

                {"SlashUp_Dagger", new Animation(TextureManager.Player_Slash_Dagger,0,6,(tmpFrameSpeed=FRAME_SPEED*0.5f), NO_LOOP, PRIORITIZED, REVERSE)},
                {"SlashLeft_Dagger",new Animation(TextureManager.Player_Slash_Dagger,1,6,tmpFrameSpeed, NO_LOOP, PRIORITIZED, REVERSE)},
                {"SlashDown_Dagger", new Animation(TextureManager.Player_Slash_Dagger,2,6,tmpFrameSpeed, NO_LOOP, PRIORITIZED, REVERSE)},
                {"SlashRight_Dagger", new Animation(TextureManager.Player_Slash_Dagger,3,6,tmpFrameSpeed, NO_LOOP, PRIORITIZED, REVERSE)},

                {"SlashUp_Fist", new Animation(TextureManager.Player_Slash_Fist,0,6,(tmpFrameSpeed=FRAME_SPEED*0.5f), NO_LOOP, PRIORITIZED, REVERSE)},
                {"SlashLeft_Fist",new Animation(TextureManager.Player_Slash_Fist,1,6,tmpFrameSpeed, NO_LOOP, PRIORITIZED, REVERSE)},
                {"SlashDown_Fist", new Animation(TextureManager.Player_Slash_Fist,2,6,tmpFrameSpeed, NO_LOOP, PRIORITIZED, REVERSE)},
                {"SlashRight_Fist", new Animation(TextureManager.Player_Slash_Fist,3,6,tmpFrameSpeed, NO_LOOP, PRIORITIZED, REVERSE)},
                
                // Thrust-Angriff
                 {"ThrustUp_Spear", new Animation(TextureManager.Player_Thrust_Spear,0,8,(tmpFrameSpeed=FRAME_SPEED*0.5f), NO_LOOP, PRIORITIZED, REVERSE)},
                {"ThrustLeft_Spear",new Animation(TextureManager.Player_Thrust_Spear,1,8,tmpFrameSpeed, NO_LOOP, PRIORITIZED, REVERSE)},
                {"ThrustDown_Spear", new Animation(TextureManager.Player_Thrust_Spear,2,8,tmpFrameSpeed, NO_LOOP, PRIORITIZED, REVERSE)},
                {"ThrustRight_Spear", new Animation(TextureManager.Player_Thrust_Spear,3,8,tmpFrameSpeed, NO_LOOP, PRIORITIZED, REVERSE)},



                // Pfeil Schießen
                {"ShootUp", new Animation(TextureManager.Player_Shoot,0,13,(tmpFrameSpeed=FRAME_SPEED*0.3f),NO_LOOP, PRIORITIZED)},
                {"ShootLeft",new Animation(TextureManager.Player_Shoot,1,13,tmpFrameSpeed, NO_LOOP, PRIORITIZED)},
                {"ShootDown", new Animation(TextureManager.Player_Shoot,2,13,tmpFrameSpeed, NO_LOOP, PRIORITIZED)},
                {"ShootRight", new Animation(TextureManager.Player_Shoot,3,13,tmpFrameSpeed, NO_LOOP, PRIORITIZED)},

                 // Zauber-Animation
                {"SpellcastUp", new Animation(TextureManager.Player_Spellcast,0,6,(tmpFrameSpeed=FRAME_SPEED*0.7f),NO_LOOP, PRIORITIZED)},
                {"SpellcastLeft",new Animation(TextureManager.Player_Spellcast,1,6,tmpFrameSpeed, NO_LOOP, PRIORITIZED)},
                {"SpellcastDown", new Animation(TextureManager.Player_Spellcast,2,6,tmpFrameSpeed, NO_LOOP, PRIORITIZED)},
                {"SpellcastRight", new Animation(TextureManager.Player_Spellcast,3,6,tmpFrameSpeed, NO_LOOP, PRIORITIZED)},

                // Todesanimation
                {"Die", new Animation(TextureManager.Player_Hurt,0,6,FRAME_SPEED*2f, NO_LOOP, PRIORITIZED)}
            };
            animationManager = new AnimationManager(this, animations["IdleDown"]);
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
            var differenz = InputController.MousePosition - new Vector2(HitboxCenter.X, HitboxCenter.Y);
            var angle = System.Math.Atan2(differenz.Y, differenz.X);

            return CalculateDirection(angle);
        }


        public override Action DetermineAction(float currentGameTime)
        {

            if (InputController.IsRightMouseButtonPressed() && Teleport.CanSwitchToState(this))
                return new Teleport(this, currentGameTime);

            //if (InputController.IsRightMouseButtonHeld() && Protect.CanSwitchToState(this) /*&& (Mana >= 100)*/) // statt 100 ggf. Ability.RequiredMana nutzen
            //    return new Protect(this, currentGameTime);


            if (InputController.IsLeftMouseButtonPressed() && !IsAttacking() && CanAttack())
            {
                if (inventory.CurrentWeapon is LongRange)
                {
                    inventory.CurrentWeapon.CooldownTimer = 0;
                    return new RangeAttack(this);

                }
                if (inventory.CurrentWeapon is ShortRange)
                {
                    inventory.CurrentWeapon.CooldownTimer = 0;

                    return new Melee(this);

                }
            }
            return new Move(this);

        }

        public override void Update(GameTime gameTime)
        {
            UpdateCurrentWeaponPos();
            CheckInteractableCollision();
            InteractWithObject();
            base.Update(gameTime);
        }

        public void AddExperiencePoints(int xp)
        {
            CurrentXP += xp;
            SoundManager.ExperiencePickup.Play(Game1.gameSettings.soundeffectsLevel, 0, 0);

            if (currentXPLevel >= MAX_LEVEL)
                return;

            if (CurrentXP >= xpCap[currentXPLevel])
            {
                StatisticsManager.LevelUp();
                MessageFactory.DisplayMessage("Level Up++", Color.Yellow,AnimationType.DownSimpleFade);
                while (currentXPLevel < MAX_LEVEL && currentXP >= xpCap[currentXPLevel])
                {
                    StatisticsManager.LevelUp();
                    MessageFactory.DisplayMessage("Level Up++", Color.Yellow, AnimationType.DownSimpleFade);
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
            switch (level)
            {
                case 1:
                    // Level 1 award = increase max hp by 20
                    maxHealthPoints += 20;
                    HealthPoints += 20;
                    break;
                case 2:
                    DamageMultiplier = 1.2f;
                    break;
                case 3:
                    maxHealthPoints += 50;
                    HealthPoints += 50;
                    break;
                case 4:
                    DamageMultiplier = 1.6f;
                    break;
                default:
                    break;
            }
        }

        public void CheckInteractableCollision()
        {
            /* schoener wenn interaktion mit Container in seperate Methode geprueft wird, jedoch wenn wir interaktion und collision
             gleichzeitig pruefen, muessen wir die Loot liste nicht doppelt durchgehen, IDEE: liste mit container/objekte die interactable sind
            */

            canInteract = false;
            interactableObjects.Clear();

            foreach (var interactableObject in EntityManager.interactables)
            {
                if (Hitbox.Intersects(interactableObject.Hitbox))
                {
                    if (interactableObject is LootContainer)
                    {
                        // if its a container, check if its still closed before counting it as "interactable"
                        if (((LootContainer)interactableObject).Closed)
                        {
                            interactableObjects.Add(interactableObject);
                            canInteract = true;
                        }
                    }
                    else if (interactableObject is WorldObject || interactableObject is NPCBase)
                    {
                        // if its a worldobject or npc add it directly
                        // later maybe check if npc has finished dialog and if worldobject has been initialized
                        interactableObjects.Add(interactableObject);
                        canInteract = true;
                    }
                    else
                    {
                        interactableObject.OnContact();
                    }

                }
            }
        }

        public void InteractWithObject()
        {
            if (InputController.IsKeyPressed(Keys.F))
            {
                foreach (var interactable in interactableObjects)
                {
                    interactable.OnContact();
                }
            }
            interactableObjects.Clear();
        }

        public bool GameOver()
        {
            return isExpired;
        }

        public override void Kill()
        {
            base.Kill();
            SoundManager.PlayerDie.Play(Game1.gameSettings.soundeffectsLevel, 0.3f, 0);
            MessageFactory.DisplayMessage("GAME OVER", Color.Red,AnimationType.LeftToRight);

        }

        public bool CanAttack()
        {
            return !IsAttacking() && !inventory.CurrentWeapon.InUsage();
        }
        public override bool IsInvincible()
        {
            return base.IsInvincible() || Game1.gameSettings.godMode;
        }
        public void UpdateCurrentWeaponPos()
        {
            // TEST

            /*if (InputController.IsKeyDown(Keys.B)) {
                new Explosion();
            }*/

            if (InputController.IsKeyPressed(Keys.PageUp) || InputController.IsMouseScrolledDown())
                inventory.SetNextWeapon();
            else if (InputController.IsKeyPressed(Keys.PageDown) || InputController.IsMouseScrolledUp())
                inventory.SetNextWeapon(true);
            else if (InputController.IsKeyPressed(Keys.NumPad0))
                inventory.ChangeCurrentWeaponSlot(0);
            else if (InputController.IsKeyPressed(Keys.NumPad1))
                inventory.ChangeCurrentWeaponSlot(1);
            else if (InputController.IsKeyPressed(Keys.NumPad2))
                inventory.ChangeCurrentWeaponSlot(2);
            else if (InputController.IsKeyPressed(Keys.NumPad3))
                inventory.ChangeCurrentWeaponSlot(3);
        }
    }
}