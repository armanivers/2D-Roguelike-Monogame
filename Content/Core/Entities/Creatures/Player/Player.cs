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
            float frameSpeed = 0.09f;
            animations = new Dictionary<string, Animation>()
            {
                // Idle
                {"IdleUp", new Animation(TextureManager.PlayerIdle,0,6,frameSpeed*2.5f)},
                {"IdleLeft", new Animation(TextureManager.PlayerIdle,1,6,frameSpeed*2.5f)},
                {"IdleDown", new Animation(TextureManager.PlayerIdle,2,6,frameSpeed*2.5f)},
                {"IdleRight", new Animation(TextureManager.PlayerIdle,3,6,frameSpeed*2.5f)},
                
                // Laufbewegung
                {"WalkUp", new Animation(TextureManager.PlayerWalk,0,9,frameSpeed)},
                {"WalkLeft",new Animation(TextureManager.PlayerWalk,1,9,frameSpeed)},
                {"WalkDown", new Animation(TextureManager.PlayerWalk,2,9,frameSpeed)},
                {"WalkRight", new Animation(TextureManager.PlayerWalk,3,9,frameSpeed)},
                
                // Pfeil schießen
                {"ShootUp", new Animation(TextureManager.PlayerShoot,0,13,(frameSpeed *=0.3f), false, true)},
                {"ShootLeft",new Animation(TextureManager.PlayerShoot,1,13,frameSpeed, false, true)},
                {"ShootDown", new Animation(TextureManager.PlayerShoot,2,13,frameSpeed, false, true)},
                {"ShootRight", new Animation(TextureManager.PlayerShoot,3,13,frameSpeed, false, true)},

                 // Todesanimation
                {"Die", new Animation(TextureManager.PlayerHurt,0,6,frameSpeed*2f, false, true)}
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

        public Vector2 GetDirectionToMouse()
        {
            var differenz = InputController.MousePosition - Position;
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
            if (InputController.IsMousePressed() && !IsAttacking())
                return new RangeAttack(this);
            return new Move(this);

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            //if (CheckAttacking()) 
            //    Attack(Entities.Attack.AttackMethod.RANGE_ATTACK);
        }

        public bool GameOver() {
            return isExpired;
        }
    }
}