using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;

namespace _2DRoguelike.Content.Core.Entities.Player
{
    class Player : EntityBasis
    {
        private static Player instance;
        private const float playerSpeed = 5;
        // cooldown in seconds!
        private const float attackCooldown = 2;
        private float cooldownTimer;

        public static Player Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Player();
                }
                return instance;
            }
        }

        private Player()
        {
            this.Position = new Vector2(2, 2);
            this.texture = TextureManager.Player;
            this.animations = new Dictionary<string, Animation>()
            {
                {"WalkUp", new Animation(TextureManager.PlayerWalkUpAxe,9,0.6f)},
                {"WalkDown", new Animation(TextureManager.PlayerWalkDownAxe,9,0.15f)},
                {"WalkLeft",new Animation(TextureManager.PlayerWalkLeftAxe,9,0.15f)},
                {"WalkRight", new Animation(TextureManager.PlayerWalkRightAxe,9,0.15f)},
                // Todo: idle animation texturesheet erstellen!
                {"Idle", new Animation(TextureManager.PlayerWalkDownAxe,9,0.3f)}
            };
            this.animationManager = new AnimationManager(animations.First().Value);
            this.isExpired = false;
            this.cooldownTimer = attackCooldown;
        }
        private void DetermineAnimation()
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
            // Todo: idle animation texturesheet erstellen!
            //else if(Velocity.X == 0 && Velocity.Y == 0)
            //{
            //    animationManager.Play(animations["Idle"]);
            //}
            else
            {
                animationManager.Stop();
            }
        }

        public void CheckMovement()
        {
            Velocity = playerSpeed * InputController.GetDirection();
            Position += Velocity;

            // Vector2.Clamp makes sure the player doesn't go outside of screen, Minimum = (0,0), Maximum = Screensize minus playerSize
            Position = Vector2.Clamp(Position, Vector2.Zero, Game1.ScreenSize - Size);
        }

        public void CheckAttacking()
        {
            if (!InputController.GetMouseClickPosition().Equals(Vector2.Zero) && cooldownTimer > attackCooldown)
            {
                cooldownTimer = 0;
                SoundManager.PlayerAttack.Play(0.2f, 0.2f, 0);

                // create an explosion
                Explosion e = new Explosion(new Vector2(InputController.MousePosition.X-20, InputController.MousePosition.Y-20));
                EntityManager.Add(e);
                e.Explode();
            }
            //Debug.Print("Time= "+cooldown);
        }

        public override void Update(GameTime gameTime)
        {
            CheckMovement();
            DetermineAnimation();
            CheckAttacking();

            // orientation = (float)Math.Atan2(InputController.GetMousePosition().X, InputController.GetMousePosition().Y);

            cooldownTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            animationManager.Update(gameTime);
        }

    }
}
