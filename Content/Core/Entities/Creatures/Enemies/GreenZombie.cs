using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _2DRoguelike.Content.Core.Entities.Creatures.Enemies;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.Creatures.Enemies
{
    class GreenZombie : Enemy
    {
        public GreenZombie(Vector2 position, int maxHealthPoints, float attackCooldown, float movingSpeed) : base(position, maxHealthPoints, attackCooldown, movingSpeed)
        {
            texture = TextureManager.Player;
            float frameSpeed = 0.09f;
            animations = new Dictionary<string, Animation>()
            {
                // Todo: idle animation texturesheet erstellen!
                {"Idle", new Animation(TextureManager.Player,1,frameSpeed*4)},
                {"WalkUp", new Animation(TextureManager.PlayerWalkUpAxe,9,frameSpeed)},
                {"WalkDown", new Animation(TextureManager.PlayerWalkDownAxe,9,frameSpeed)},
                {"WalkLeft",new Animation(TextureManager.PlayerWalkLeftAxe,9,frameSpeed)},
                {"WalkRight", new Animation(TextureManager.PlayerWalkRightAxe,9,frameSpeed)}
            };
            animationManager = new AnimationManager(animations.First().Value);
        }

public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            // TODO: Zeichnen etc.
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
            */
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
            else if (Velocity.Y > 0)
            {
                animationManager.Play(animations["WalkDown"]);
            }
            else if (Velocity.Y < 0)
            {
                animationManager.Play(animations["WalkUp"]);
            }
            else if (Velocity.X == 0 && Velocity.Y == 0)
            {
                animationManager.Play(animations["Idle"]);
            }
            else
            {
                animationManager.Stop();
            }
        }
    }
}
