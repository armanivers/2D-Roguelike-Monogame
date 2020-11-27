using System;
using System.Collections.Generic;
using System.Text;
using _2DRoguelike.Content.Core.World;
using Microsoft.Xna.Framework;

namespace _2DRoguelike.Content.Core.Entities.Creatures.Enemies
{
    abstract class Enemy : Humanoid
    {
        public override Vector2 GetDirection()
        {
            // TODO: KI nach Angaben fragen
            return new Vector2(0, 0);
        }

        public override Vector2 GetAttackDirection()
        {
            // TODO: Position zum Player bestimmen
            return Player.Player.Instance.Position;
        }

        public override Vector2 GetAttackLineOfSight()
        {
            // TODO: Blickrichtung nach Angriff bestimmen
            var differenz = new Vector2(Player.Player.Instance.Hitbox.X + Player.Player.Instance.Hitbox.Width / 2, Player.Player.Instance.Hitbox.Y + Player.Player.Instance.Hitbox.Height / 2)
                - new Vector2(Hitbox.X + Hitbox.Width / 2, Hitbox.Y + Hitbox.Height / 2);
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

        public Enemy(Vector2 position, int maxHealthPoints, float attackCooldown, float movingSpeed) : base(position, maxHealthPoints, attackCooldown, movingSpeed)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

    }


}
