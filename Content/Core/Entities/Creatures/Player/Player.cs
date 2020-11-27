using _2DRoguelike.Content.Core.Entities.Actions;
using _2DRoguelike.Content.Core.Entities.Creatures.Projectiles;
using _2DRoguelike.Content.Core.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace _2DRoguelike.Content.Core.Entities.Player
{
    class Player : Humanoid
    {
        private static Player instance;
        //private Attack meleeAttack;
        //private Attack rangeAttack; = new RangeAttack();

        // cooldown in seconds!

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

        public Player(Vector2 position, int maxHealthPoints, float attackCooldown, float movingSpeed) : base(position, maxHealthPoints, attackCooldown, movingSpeed)
        {
            // TODO:
            // rangeAttack = new RangeAttack(this);
            // meleeAttack = new MeleeAttack(this):

            //this.position = new Vector2(2*32, 5*32); bei statischer Map
            instance = this;
            texture = TextureManager.PlayerIdle;

            const bool NO_LOOP = false;
            const bool PRIORITIZED = true;
            const bool REVERSE = true;
            const float frameSpeed = 0.09f;
            float tmpFrameSpeed;
            animations = new Dictionary<string, Animation>()
            {
                // Idle
                {"IdleUp", new Animation(TextureManager.PlayerIdle,0,6,tmpFrameSpeed=frameSpeed*2.5f)},
                {"IdleLeft", new Animation(TextureManager.PlayerIdle,1,6,tmpFrameSpeed)},
                {"IdleDown", new Animation(TextureManager.PlayerIdle,2,6,tmpFrameSpeed)},
                {"IdleRight", new Animation(TextureManager.PlayerIdle,3,6,tmpFrameSpeed)},
                
                // Laufbewegung
                {"WalkUp", new Animation(TextureManager.PlayerWalk,0,9,tmpFrameSpeed=frameSpeed)},
                {"WalkLeft",new Animation(TextureManager.PlayerWalk,1,9,tmpFrameSpeed)},
                {"WalkDown", new Animation(TextureManager.PlayerWalk,2,9,tmpFrameSpeed)},
                {"WalkRight", new Animation(TextureManager.PlayerWalk,3,9,tmpFrameSpeed)},
                
                 // Melee-Angriff
                {"SlashUp", new Animation(TextureManager.PlayerSlash,0,6,(tmpFrameSpeed=frameSpeed*0.5f), NO_LOOP, PRIORITIZED, REVERSE)},
                {"SlashLeft",new Animation(TextureManager.PlayerSlash,1,6,tmpFrameSpeed, NO_LOOP, PRIORITIZED, REVERSE)},
                {"SlashDown", new Animation(TextureManager.PlayerSlash,2,6,tmpFrameSpeed, NO_LOOP, PRIORITIZED, REVERSE)},
                {"SlashRight", new Animation(TextureManager.PlayerSlash,3,6,tmpFrameSpeed, NO_LOOP, PRIORITIZED, REVERSE)},

                // Pfeil Schießen
                 {"ShootUp", new Animation(TextureManager.PlayerShoot,0,13,(tmpFrameSpeed=frameSpeed*0.3f),NO_LOOP, PRIORITIZED)},
                {"ShootLeft",new Animation(TextureManager.PlayerShoot,1,13,tmpFrameSpeed, NO_LOOP, PRIORITIZED)},
                {"ShootDown", new Animation(TextureManager.PlayerShoot,2,13,tmpFrameSpeed, NO_LOOP, PRIORITIZED)},
                {"ShootRight", new Animation(TextureManager.PlayerShoot,3,13,tmpFrameSpeed, NO_LOOP, PRIORITIZED)},

                // Todesanimation
                {"Die", new Animation(TextureManager.PlayerHurt,0,6,frameSpeed*2f, NO_LOOP, PRIORITIZED)}
            };
            animationManager = new AnimationManager(animations["IdleDown"]);

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
            var differenz = InputController.MousePosition - new Vector2(Hitbox.X+Hitbox.Width/2,Hitbox.Y+Hitbox.Height/2);
            var angle = System.Math.Atan2(differenz.X, differenz.Y);
            if (angle > 1 && angle < 2)
            {
                return new Vector2(1, 0);
            }
            else if (angle > 2 && angle < 3)
            {
                return new Vector2(0, -1);
            }
            else if (angle > -3 && angle < -2)
            {

                return new Vector2(0, -1);
            }
            else if (angle > -1 && angle < 1)
            {
                return new Vector2(0, 1);
            }
            else if (angle < -1 && angle > -2)
            {
                return new Vector2(-1, 0);
            }
            return Vector2.Zero;
        }

        public override Action DetermineAction()
        {
            if (InputController.IsLeftMouseButtonPressed() && !IsAttacking())
                return new RangeAttack(this);
            if(InputController.IsRightMouseButtonPressed() && !IsAttacking())
                return new Melee(this);
            return new Move(this);

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public bool GameOver() {
            return isExpired;
        }
    }
}