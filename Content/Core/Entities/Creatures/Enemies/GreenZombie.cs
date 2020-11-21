using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _2DRoguelike.Content.Core.Entities.Actions;
using _2DRoguelike.Content.Core.Entities.Creatures.Enemies;
using Microsoft.Xna.Framework;
using Action = _2DRoguelike.Content.Core.Entities.Actions.Action;

namespace _2DRoguelike.Content.Core.Entities.Creatures.Enemies
{
    class GreenZombie : Enemy
    {
        public GreenZombie(Vector2 position, int maxHealthPoints, float attackCooldown, float movingSpeed) : base(position, maxHealthPoints, attackCooldown, movingSpeed)
        {
            texture = TextureManager.ZombieBrownIdle;
            float frameSpeed = 0.09f;
            animations = new Dictionary<string, Animation>()
            {
                
               
                // Idle
                {"IdleUp", new Animation(TextureManager.ZombieBrownIdle,0,6,frameSpeed*2.5f)},
                {"IdleLeft", new Animation(TextureManager.ZombieBrownIdle,1,6,frameSpeed*2.5f)},
                {"IdleDown", new Animation(TextureManager.ZombieBrownIdle,2,6,frameSpeed*2.5f)},
                {"IdleRight", new Animation(TextureManager.ZombieBrownIdle,3,6,frameSpeed*2.5f)},
                
                // Laufbewegung
                {"WalkUp", new Animation(TextureManager.ZombieBrownWalk,0,9,frameSpeed)},
                {"WalkLeft",new Animation(TextureManager.ZombieBrownWalk,1,9,frameSpeed)},
                {"WalkDown", new Animation(TextureManager.ZombieBrownWalk,2,9,frameSpeed)},
                {"WalkRight", new Animation(TextureManager.ZombieBrownWalk,3,9,frameSpeed)},
                

                 {"ShootUp", new Animation(TextureManager.ZombieShoot,0,13,(frameSpeed *=0.3f),false, true)},
                {"ShootLeft",new Animation(TextureManager.ZombieShoot,1,13,frameSpeed, false, true)},
                {"ShootDown", new Animation(TextureManager.ZombieShoot,2,13,frameSpeed, false, true)},
                {"ShootRight", new Animation(TextureManager.ZombieShoot,3,13,frameSpeed, false, true)}
            };
            animationManager = new AnimationManager(animations["IdleDown"]);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override Action DetermineAction()
        {
            // TODO: Anhand von Fakten, wie Status, position, playerPosition, blickfeld etc. eine Action erzeugen
            return new Move(this);
        }
    }
}
