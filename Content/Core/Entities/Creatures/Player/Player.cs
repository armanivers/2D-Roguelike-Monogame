using _2DRoguelike.Content.Core.World;
using _2DRoguelike.Content.Core.World.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

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
                    instance = new Player(LevelManager.maps.getSpawnpoint(), 100, 2, 5);
                }
                return instance;
            }
        }

        private Player(Vector2 position, int maxHealthPoints, float attackCooldown, float movingSpeed) : base(position, maxHealthPoints, attackCooldown, movingSpeed)
        {
            // TODO:
            // rangeAttack = new RangeAttack(this);
            // meleeAttack = new MeleeAttack(this):

            //this.position = new Vector2(2*32, 5*32); bei statischer Map
            
            texture = TextureManager.Player;
            float frameSpeed = 0.09f;
            animations = new Dictionary<string, Animation>()
            {
                // Todo: idle animation texturesheet erstellen!
                {"Idle", new Animation(TextureManager.Player,0,1,frameSpeed*4)},
                {"WalkUp", new Animation(TextureManager.ZombieBrownWalk,0,9,frameSpeed)},
                {"WalkLeft",new Animation(TextureManager.ZombieBrownWalk,1,9,frameSpeed)},
                {"WalkDown", new Animation(TextureManager.ZombieBrownWalk,2,9,frameSpeed)},
                {"WalkRight", new Animation(TextureManager.ZombieBrownWalk,3,9,frameSpeed)}
            };
            animationManager = new AnimationManager(animations.First().Value);
            
        }
        protected override void DetermineAnimation()
        {
            if (Velocity.X > 0)
            {
                animationManager.Play(animations["WalkRight"]);
            }
            else if (Velocity.X < 0)
            {
                animationManager.Play(animations["WalkLeft"]);
            }
            else if(Velocity.Y > 0)
            {
                animationManager.Play(animations["WalkDown"]);
            }
            else if(Velocity.Y < 0)
            {
                animationManager.Play(animations["WalkUp"]);
            }
            else if(Velocity.X == 0 && Velocity.Y == 0)
            {
                animationManager.Play(animations["Idle"]);
            }
            else
            {
                animationManager.Stop();
            }
        }

        protected override Vector2 GetDirection()
        {
            return InputController.GetDirection();
        }

        public void CheckAttacking() // ist NICHT die Attack() Methode
        {
            if (!InputController.GetMouseClickPosition().Equals(Vector2.Zero) && CooldownTimer > attackCooldown)
            {
                CooldownTimer = 0;
                SoundManager.PlayerAttack.Play(0.2f, 0.2f, 0);
                Camera.ShakeScreen();
                // create an explosion
                //Explosion e = new Explosion(new Vector2(InputController.MousePosition.X, InputController.MousePosition.Y));
                //EntityManager.Add(e);
                //e.Explode();
                Position = (new Vector2(InputController.MousePosition.X+position.X, InputController.MousePosition.Y+position.Y-10));
                //Kill();
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            CheckAttacking();
        }

        public void GetHit(int damage)
        {
            HealthPoints -= damage;
            if (HealthPoints <= 0)
            {
                dead = true;
            }
        }

        public void Kill()
        {
            HealthPoints = 0;
            dead = true;
        }

        public void DeleteInstance()
        {
            instance = null;
        }

        public bool IsDead()
        {
            return dead;
        }

        public override void Attack(Attack.AttackMethod attackMethod)
        {
            /* 
            switch (attackMethod)
            {
                case Entities.Attack.AttackMethod.MELEE:
                    // meleeAttack.startAttack();
                    break;
                case Entities.Attack.AttackMethod.RANGE_ATTACK:
                    // rangeAttack.startAttack();
                    break;
            }*/
        }
    }
}