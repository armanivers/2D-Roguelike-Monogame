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
            texture = TextureManager.Player;
            float frameSpeed = 0.09f;
            animations = new Dictionary<string, Animation>()
            {
                // Todo: idle animation texturesheet erstellen!
                {"Idle", new Animation(TextureManager.Player,0,1,frameSpeed*4)},
                {"WalkUp", new Animation(TextureManager.ZombieBrownWalk,0,9,frameSpeed)},
                {"WalkLeft",new Animation(TextureManager.ZombieBrownWalk,1,9,frameSpeed)},
                {"WalkDown", new Animation(TextureManager.ZombieBrownWalk,2,9,frameSpeed)},
                {"WalkRight", new Animation(TextureManager.ZombieBrownWalk,3,9,frameSpeed)},
                
                // Pfeil schießen
                {"ShootUp", new Animation(TextureManager.PlayerShoot,0,13,(frameSpeed *=0.3f))},
                {"ShootLeft",new Animation(TextureManager.PlayerShoot,1,13,frameSpeed)},
                {"ShootDown", new Animation(TextureManager.PlayerShoot,2,13,frameSpeed)},
                {"ShootRight", new Animation(TextureManager.PlayerShoot,3,13,frameSpeed)}
            };
            animationManager = new AnimationManager(animations.First().Value);
            
        }
        

        protected override Vector2 GetDirection()
        {
            return InputController.GetDirection();
        }

        

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            //if (CheckAttacking()) 
            //    Attack(Entities.Attack.AttackMethod.RANGE_ATTACK);
        }

        public void DeleteInstance()
        {
            instance = null;
        }

        public override void Attack(Attack.AttackMethod attackMethod)
        {
            CooldownTimer = 0;
            SoundManager.PlayerAttack.Play(0.2f, 0.2f, 0);

            // animations["ShootRight"].Play

            // create an explosion
            //Explosion e = new Explosion();
            //EntityManager.Add(e);
            Arrow a = new Arrow();
            //Position = (new Vector2(InputController.MousePosition.X-32, InputController.MousePosition.Y-48));
            //Kill();


            // ------ Zacks Notizen

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

        protected override ActionCommand DetermineAction()
        {
            // Wenn "battleMode" (Angriff findet gerade statt): Action ändert sich nicht


            // Je nach Keyinputs wird eine Action bestimmt


            if (InputController.IsMousePressed() /* || Melee-Knop gedrückt */) {
                // if (CheckAttacking()) 
                    return ActionCommand.ATTACK;
                
            }
            // if(InputController.GetDirection() != Vector2.Zero)
            return ActionCommand.MOVE;
        }
    }
}